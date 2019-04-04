---
layout: post
title: Unity LLAPI Part 2 - Server
category: Code
description: Setting up a client - server network architecture with Unity's LLAPI
---

Before I begin, a note about my dev environment: I use a Mac as my primary dev machine and not having a spare machine to hand the question of how to run two instances of Unity concurrently arises when trying to test client-server code. My first attempt was to launch two instances of Unity on the same machine as documented [here](http://answers.unity3d.com/questions/8408/open-multiple-projects.html). However this won't work because we need the two instances to be on different ip addresses, so my solution was to create a virtual machine (Windows) using [VBox](https://www.virtualbox.org/). I also found it useful to plug in an external drive that I could mount as a shared drive on the virtual machine. This enabled me to maintain a single copy of each of the client and server projects on the external drive and launch either project on either machine. The alternative would be to somehow maintain duplicate copies of the 2 projects since my Mac hard drive is not visible to the Windows VM.


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

Once again I will listen for events on the socket in the MonoBehaviour's `Update` method. The server is essentially passive and will sit and listen for connections - it won't reach out and make connections to clients for obvious reasons so I won't need to write any code to establish connections as I did in the client script.

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

With this code in place and attached to a GameObject I can fire up a second machine/spin up a virtual machine and run the server on one and the client on the other. I set both ports to the same number and set the 'host' value in the editor on the client to the ip address of the server (i.e. the local network address). If all goes well I should see logging in my console on the server machine telling me that a connection has been made.


**Serialisation/Deserialization**

Every project will have differing needs but the basic idea behind sending and receiving small messages will be the same. In a simple setup like mine I have 5 clients each with a unique ID assigned on the client machine via a static config file which is read in at runtime. Whenever the client wants to send a message to the server it creates an object that contains:
- The Unique ID
- The type of message
- The value of the message
The object is serialized to json, sent across the socket and deseriazed at the other end. In Unity we [define a class to describe the data to be serialized](https://docs.unity3d.com/Manual/JSONSerialization.html) so this will simply look like this:

 
```csharp
[Serializable]
public class Message
{
    public string type;
    public string value;
    public string uid;
}
```
 
 I have defined all the properties of the class as strings even though sometimes I will want to send floats. That's ok, I'll cast to float at the receiving end if I need to.
 
 The first message I want to send after establishing a connection will be a 'register' message containing the unique ID of the client. The server will store this unique ID in a dictionary object with the socket connection as the value and the ID as the key.
 
 First the client code:
 
```csharp
 //ClientSocket.cs
 
 public void Dispatch( string type, float value, string uid){
    Message message = new Message ();
    message.type = type;
    message.value = value.ToString();
    message.uid = uid;
    string json = JsonUtility.ToJson (message);

    SendSocketMessage (json);
}

 public void SendSocketMessage(Message msg) {
  
    byte error;
    byte[] buffer = new byte[1024];
    Stream stream = new MemoryStream(buffer);
    BinaryFormatter formatter = new BinaryFormatter();
    formatter.Serialize(stream, msg);
    int bufferSize = 1024;

    NetworkTransport.Send(hostId, connectionId, channelId, buffer, bufferSize, out error);
    NetworkError networkError = (NetworkError) error;
    if (networkError != NetworkError.Ok) {
        string err_msg = string.Format("Error: {0}, hostId: {1}, connectionId: {2}, channelId: {3}", networkError, hostId, 0, channelId);
        Debug.LogError(err_msg);
    } else {
        Debug.Log("Message sent!");
    }
}
```
 
 Which I can call with `Dispatch('register', null, uid)`.
  
 Now, on the server I will deserialize the message and store the uid in a dictionary of `Connections` - both client and server will have identical `Message` definitions, the incoming message will be caught in the `NetworkEventType.DataEvent` clause of the `Update` method's switch statement which then calls out to `ProcessMessage()`:
  
```csharp
 //ServerSocket.cs
 
[Serializable]
public class Connection
{
    public int connectionId;
    public int hostId;
    public int channelId;
}

Dictionary<string,Connection> connections = new Dictionary<string,Connection>(){};
 
void ProcessMessage( string msg, int recHostId, int recConnectionId, int recChannelId ){
 		
    Message data = JsonUtility.FromJson<Message> (msg);

    if (data.type == "register"){
        
        var conn = new Connection ();
        conn.connectionId = recConnectionId;
        conn.hostId = recHostId;
        conn.channelId = recChannelId;
        connections [data.uid] = conn;

    }else{
        ExecuteMessageString (data.type, data.value, data.trackId);	
    }


}
```
 
Going back the other way from the server to the client the code is almost exactly the same except now I need to grab the relevant connection from the Connections dictionary so I can send the message to the correct client. I perform a couple of simple checks up front to make sure that the connection exists and is connected:

```csharp
public void SendSocketMessage(string msg, string uid) {

    if (!connections.ContainsKey (uid))
        return;

    Connection conn = connections [uid];

    if (conn == null)
        return;
    
    byte error;
    byte[] buffer = new byte[1024];
    Stream stream = new MemoryStream(buffer);
    BinaryFormatter formatter = new BinaryFormatter();
    formatter.Serialize(stream, msg);
    int bufferSize = 1024;


    NetworkTransport.Send(conn.hostId, conn.connectionId, conn.channelId, buffer, bufferSize, out error);
    NetworkError networkError = (NetworkError) error;
    if (networkError != NetworkError.Ok) {
        LogError(string.Format("Error: {0}, hostId: {1}, connectionId: {2}, channelId: {3}", networkError, hostId, connectionId, recChannelId));
    } else {
        Debug.Log("Message sent! "+msg);
    }
}

```

So now I can send messages back and forth from the server to any connected client. How the server and clients respond to messages will be dependent on the nature of the messages sent. For my purposes there are only a small number of message types being sent so I simply have a switch statement containing a clause for each of the message types I expect to receive:

```csharp
//ServerSocket.cs

public void ExecuteMessageString( string type, float value, string trackId )
{
    Engine engine = engines [uid]; //this is a class particular to my project
    
    switch(type){

    case "home":
        engine.GoHome ();
        break;
    case "impulse":
        engine.speed = value;
        break;
    case "stopTrain":
        engine.Stop();
        break;
    case "deliver":
        engine.Deliver ();
        break;
    case "resupply":
        engine.Resupply();
        break;
    case "seek":
        engine.Seek ();
        break;
    case "reset":
        engine.Reset ();
        break;
    case "selectTrain":
        engine.SwitchTrain (value);
        break;
    }

}
```

And that's it! The clients each have similar methods that execute the incoming message from the server. For my purposes this was all I needed along with a state machine implementation that I used to give some structure and prevent the clients from getting into impossible states.
 
 You can grab the code frome [here](https://github.com/zerozero/zerozero.github.io/tree/master/code/LLAPI) and try it out for yourself. It should work with any reasonably current version of Unity and has no external dependencies. Good Luck..



