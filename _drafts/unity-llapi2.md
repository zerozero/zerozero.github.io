---
layout: post
title: Unity LLAPI Part 2 - Server
category: Code
description: Setting up a client - server network architecture with Unity's LLAPI
---

Before I begin a note about my dev environment: I use a Mac as my primary dev machine and not having a spare machine to hand the question of how to run two instances of Unity concurrently arises when trying to test client-server code. My first attempt was to launch two instances of Unity on the same machine as documented [here](http://answers.unity3d.com/questions/8408/open-multiple-projects.html). However this won't work because we need the two instances to be on different ip addresses, so my solution was to create a virtual machine (Windows) using [VBox](https://www.virtualbox.org/). I also found it useful to plug in an external drive that I could mount as a shared drive on the virtual machine. This enabled me to maintain a single copy of each of the client and server projects on the external drive and launch either project on either machine. The alternative would be to somehow maintain duplicate copies of the 2 projects since my Mac hard drive is not visible to the Windows VM.


**Server code**

OK, on to the code which to start with is much simpler than the client. As before I import the required classes:
```csharp
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.Networking;
```

In the MonoBehaviour `Start` method I initialise my NetworkConnection:

```csharp
void Start () {
    NetworkTransport.Init();
    connectionConfig = new ConnectionConfig();
    connectionConfig.AddChannel(QosType.Unreliable);
    topology = new HostTopology(connectionConfig, 10);
    hostId = NetworkTransport.AddHost(topology, port);
}
```

Notice that the HostTopology specifies 10 as the maximum number of connections the socket will accept. In my case I know that this is exactly how many connections I will need, other projects will have differing requirements.

Once again I will listen for events on the socket in the MonoBehaviour's `Update` method. The server is essentially passive and will sit and listen for connections - it won't reach out and make connections to clients for obvious reasons so I won't need to writeany code to establish connections as I did in the client script.

```csharp
void Update() {
    int recHostId;


    byte[] recBuffer = new byte[1024];
    int bufferSize = 1024;
    int dataSize;
    byte error;
    //We poll for network events every update using the NetworkTransport.Receive method
    NetworkEventType networkEvent = NetworkTransport.Receive(out recHostId, out recConnectionId, out recChannelId, recBuffer, bufferSize, out dataSize, out error);

    //If the NetworkTransport.Receive call has produced an error we handle it here
    NetworkError networkError = (NetworkError) error;
    if (networkError != NetworkError.Ok) {
        Debug.LogError(string.Format("Error recieving event: {0} with recHostId: {1}, recConnectionId: {2}, recChannelId: {3}", networkError, recHostId, recConnectionId, recChannelId));
    }

    //If the NetworkTransport.Receive method brought back some data we handle it here
    switch (networkEvent) {
    case NetworkEventType.Nothing:
        //The NetworkTransport.Receive method returned no data
        break;
    case NetworkEventType.ConnectEvent:
        //The NetworkTransport.Receive returned a connect event
        Debug.Log(string.Format("incoming connection event received with connectionId: {0}, recHostId: {1}, recChannelId: {2}", recConnectionId, recHostId, recChannelId));
        break;
    case NetworkEventType.DataEvent:
        //The NetworkTransport.Receive returned a data event
        Stream stream = new MemoryStream (recBuffer);
        BinaryFormatter formatter = new BinaryFormatter ();
        string message = formatter.Deserialize (stream) as string;
        ProcessMessage (message, recHostId, recConnectionId, recChannelId);
        break;
    case NetworkEventType.DisconnectEvent:
        //The NetworkTransport.Receive returned a disconnect event
        Debug.Log ("remote client " + recConnectionId + " disconnected");
        DestroyConnection (recConnectionId);
        break;
    }

}
```

As before there are 4 possible types of NetworkEvent that can be returned from a call to the NetworkTransport.Receive method, I use a switch statement to respond accordingly. 

With this code in place and attached to a GameObject I can fire up a second machine/spin up a virtual machine and run the server on one and the client on the other. I set both ports to the same number and set the host on the client to the ip address of the server (i.e. the local network address). If all goes well I should see logging in my console on the server machine telling me that a connection has been made.


**Serialisation**

*IP addresses and Ports

*Download code



