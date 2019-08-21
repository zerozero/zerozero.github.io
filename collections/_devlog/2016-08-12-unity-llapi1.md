---
layout: post
title: Unity LLAPI Part 1 - Client
category: Code
description: Setting up a client - server network architecture with Unity's LLAPI
---

<div class="img_row">
	<img class="col three" src="{{ site.baseurl }}/images/heroes/LLAPI.png" alt="" title="Diorama"/>
</div>
<div class="col three caption">
	Transport Mechanism.
</div>

**Objective**

Unity's High-Level API (HLAPI) is a great plug-and-play solution for setting up a typical networking environment for the most common multiplayer gaming scenarios. It contains all kinds of goodness such as object spawning across the network, state synchronisation etc. However if finer-grained control is needed, if you don't need the overhead of all the functionality provided by the HLAPI or if you just enjoy getting your hands dirty the LLAPI (on which the HLAPI is built) is available for use.

**Intro**

Documentation for Unity's Low-Level networking API is a little sketchy and seems to be a cause of some [frustration](https://www.reddit.com/r/Unity3D/comments/3tvow6/trouble_with_network_llapi/). Here I will try to demystify the process and present a robust methodology for sending messages across a network from multiple client machines to a single server and back.

This example uses C#.

**Basic concepts**

Read the official blog post [here](http://blogs.unity3d.com/2014/06/11/all-about-the-unity-networking-transport-layer/). The important thing to realise is that the architecture consists of two layers: a network layer and a transport layer.

**Network and transport layers**

>Logically, the UNET low level library is a network protocol stack built on top of the UDP, containing a “network” layer and a “transport” layer. The network layer creates connections between peers, delivers packets and controls possible flow and congestion - we configure it then forget about it. The transport layer works with “messages” belonging to different communication channels.

**Quality of Service**

There are many QoS settings that allow for messages to be sent with varying degrees of reliability, packet size and grants. For now I just use `QosType.Unreliable`, the simplest of message types, it can be dropped due to network conditions, or internal buffer overflow.

**Client Code**

I start by setting up the client. For now I will simply create a socket that will connect to itself so I can test that my code works before adding a second socket to the mix.

The first thing to do is to initialise network transport. This is done by calling the static method `NetworkTransport.Init();`. If I do this in the MonoBehaviour's `Start` method I can be sure it will be called only once when the script starts up. I also make sure to shut down the NetworkTransport by calling `NetworkTransport.ShutDown();` in my MonoBehaviour's ShutDown method.

So now I'm ready to configure my service. After importing the various classes I'll be using 
```charp
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;
using System;
using UnityEngine.Networking;
```

I create a publicly accessible `Connect` method that will be called when my app is ready to start sending messages. I expose two fields (port and host) to the editor using the `[SerializeField]` metadata tag:

```csharp

[SerializeField]
private int port; // I used 5555

[SerializeField]
private string host; // this should be set to the local ip address of your computer on your LAN (e.g. mine is 192.168.1.64)

private int hostId;
private int connectionId;
private ConnectionConfig config;
private HostTopology hostTopology;
private byte channelId;
	
public void Connect() {


	byte error;
	//instantiate a new ConnectionConfig instance with the default settings
	config = new ConnectionConfig();

	//instantiate a new channel with the specified QoS type
	channelId = config.AddChannel(QosType.Unreliable);
	
	//HostTopology defines network topology for host which defines: 
	//(1) how many connection with default config will be supported and 
	//(2) what will be special connections (connections with config different from default).
	//In this case we are using the config defined above and specifying 2 maxDefaultConnections
	hostTopology = new HostTopology(config, 2);

	//Now create a host (aka open a socket) with given topology and optionally port and IP.
	//(The port is exposed as a serialized field in the editor in this case)
	hostId = NetworkTransport.AddHost(hostTopology, port);

	//Now create the connection. Params:
	//hostId	Host socket id for this connection.
	//address	Ip4 address.
	//port	Port.
	//exceptionConnectionId	0 in the case of a default connection.
	//error	Possible error, NetworkError.Ok if it is good.
	connectionId = NetworkTransport.Connect(hostId, host, port, 0, out error);
	
	//cast any error to NetworkError
	NetworkError networkError = (NetworkError) error;
	if (networkError != NetworkError.Ok) {
		//handle the error
	} else {
		//The connection did not error.. 
		//BUT **IMPORTANT** the connection HAS NOT BEEN ESTABLISHED YET!!
		//DO NOT ATTEMPT TO USE THE CONNECTION YET!!
	}
}
``` 

For now I will just be connecting back to the same machine so I use the local ip address of my computer in the host variable. Later I will want to set it to the ip address of the remote computer I am connecting to.

As you can see from the code even though a connection attempt does not error I cannot use the connection at this point. I need to wait until I get a `NetworkEventType.ConnectEvent` before I can start sending and receiving messages. Essentially the `Connect` code above merely sets up the connection and notifies me that a connection _will be established_. The actual connection happens asynchronously and I have to wait for that to happen before I can do anything with it.

So, in order to monitor the connection event I write some code in my MonoBehaviour's `Update()` method. In fact this is where I will handle all network events.

```csharp
void Update() {

	int recHostId;
	int recConnectionId;
	int recChannelId;
	byte[] recBuffer = new byte[1024];
	int bufferSize = 1024;
	int dataSize;
	byte error;
	
	//Here we grab the latest network event from NetworkTransport
	NetworkEventType networkEvent = NetworkTransport.Receive(
					out recHostId, 
					out recConnectionId, 
					out recChannelId, 
					recBuffer, 
					bufferSize, 
					out dataSize, 
					out error);

	NetworkError networkError = (NetworkError) error;
	if (networkError != NetworkError.Ok) {
		//handle any error
	}

	//There are four types of event that we can receive from NetworkTransport
	switch (networkEvent) {
	case NetworkEventType.Nothing:
		//there was no data sent
		break;
	case NetworkEventType.ConnectEvent:
		//**Now** we are connected and can start using the channel
		Connected ();
		break;
	case NetworkEventType.DataEvent:
		//we received some data
		Stream stream = new MemoryStream (recBuffer);
		BinaryFormatter formatter = new BinaryFormatter ();
		string message = formatter.Deserialize (stream) as string;
		//send the incoming message off for processing
		ProcessMessage (message);
		//UnityEngine.Debug.Log("incoming message event received: " + message);
		break;
	case NetworkEventType.DisconnectEvent:
		//handle disconnection
		break;
	}
}
```

As you can see from the code above at some point after calling `Connect` I get a networkEvent with `NetworkEventType.ConnectEvent`, at this point the connection is established and I can start sending and receiving messages on this connection. Also in the above code I handle the three other networkEvent types. The code in the `NetworkEventType.DataEvent` case shows how I grab the message from the incoming data stream. 

Finally, for this first test, I'll add a call to the `Connect` method from the MonoBehaviour's `Start` method and add simple logging to my `Connect` and `ProcessMessage` methods

```csharp
void Start () {
    NetworkTransport.Init();				//Init the newtwork transport once only
    Connect ();
}

public void ShutDown() {
    NetworkTransport.Shutdown ();
}

void Connected(){
    Debug.Log ("Connected...");
}

void ProcessMessage( string message){
    Debug.Log (message);
}
```

I create a new Empty Game Object in my scene and add the completed script to it as a component. I enter 127.0.0.1 in the host field and 5555 in the port field of the editor. I hit run and see these message in my console confirming that the socket has indeed made a connection:

>connecting...
 UnityEngine.Debug:Log(Object)
>Connected...
 UnityEngine.Debug:Log(Object)
 
 Great! That's the client set up - in the next post I'll write the corresponding 'server' code and run through a simple api that allows me to handle multiple connections and keep track of who's sending messages.
