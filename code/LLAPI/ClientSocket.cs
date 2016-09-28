using UnityEngine;
using System.Collections;

using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;
using System;
using UnityEngine.Networking;

public class ClientSocket : MonoBehaviour {

	[SerializeField]
	private int port; // I used 5555

	[SerializeField]
	private string host; // this should be set to the local ip address of your computer on your LAN (e.g. mine is 192.168.1.64)

	private int hostId;
	private int connectionId;
	private ConnectionConfig config;
	private HostTopology hostTopology;
	private byte channelId;


	void Init(){
		NetworkTransport.Init();				//Init the newtwork transport once only
	}

	// Use this for initialization
	void Start () {
		Init ();
		Connect ();
	}

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
			Debug.Log ("Error connecting...");
		} else {
			Debug.Log ("connecting...");
			//The connection did not error.. 
			//BUT **IMPORTANT** the connection HAS NOT BEEN ESTABLISHED YET!!
			//DO NOT ATTEMPT TO USE THE CONNECTION YET!!
		}
	}

	
	// Update is called once per frame
	void Update () {
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

	void Connected(){
		Debug.Log ("Connected...");
	}

	void ProcessMessage( string message){
		
	}
}
