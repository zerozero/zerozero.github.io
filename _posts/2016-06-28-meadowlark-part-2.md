---
layout: post
title: Unity3D + LEAP + Google Cardboard. Meadowlark Part 2.
category: Code
description: Low-poly Unity game (devlog part 2)
video: 172549546
disqus: yes
---

**Objective:**
Picking up again on the development of this game. I have upgraded my skills in several areas over the last year so I now want to push this idea through but - as is the way of these things - my scope has become a bit more ambitious. I really want to get some VR going and I have a Google Cardboard rig to hand so I'm going to target that primarily. My Unity skills are up to that task now. I have also been working with machine learning techniques and have started developing some interesting ways of working with the LEAP motion controller along with Wekinator (a tool for real-time ML processing). So here's the aim

	LEAP Motion --> OpenFrameworks --> Wekinator --> Google Cardboard
	
All messaging will be done with OSC and Wekinator will learn to recognise certain hand gestures which it will pass into the Cardboard VR app to control the hero dragonfly in Meadowlark.

In part 2 I'll be attempting to get a simple communication going between the LEAP and a Unity scene with OSC.

**Get Things:**

- [LeapOSC](https://github.com/genekogan/LeapOSC) - open source openFrameworks LEAP motion data streaming
- [UnityOSC](https://github.com/jorgegarcia/UnityOSC) - open source library for sending and receiving OSC

**Set up:**

_openFrameworks_
Opening LeapOSC in XCode the first thing to check is that the active scheme is set correctly, this is set in the upper left of the toolbar - for some reason it often defaults to openFrameworks but it needs to be changed to "LeapOSC Debug" (or Release).

![Active Scheme]({{ site.baseurl }}/images/uploads/meadowlark-part-2/m1.png)

With the LEAP controller attached via USB we can hit Run and view the input from our hands.

![Viewport]({{ site.baseurl }}/images/uploads/meadowlark-part-2/m3.png)

Looking in `src/ofApp.h` we can see the default values for OSC communication are:

```csharp
#define DEFAULT_LEAP_OSC_ADDRESS "/wek/inputs"
#define DEFAULT_LEAP_OSC_IP "localhost"
#define DEFAULT_LEAP_OSC_PORT 6448
```

Let's leave these as they are for now since to start with we'll run Unity on the same machine. 

_Unity_
I've created a new empty Unity project and I'm going to use [this library](https://github.com/jorgegarcia/UnityOSC) to receive OSC messages. I'm not going to use the OSC helper editor panel that's included so all I need to copy over is the OSC folder which I drag into my Assets folder.

Make sure to enable running in the background for the Unity app by going to Edit -> Project Settings -> Player and toggling 'Run in Background' to true. Then when running both apps make sure the ofApp has focus otherwise it pauses execution and no values are output.

For this first test I'll simply attach a new script to the main camera and call it LeapOSCHandler - I don't need to send any data back to the LEAP so this script will just handle incoming messages. The LeapOSC openFrameworks app can send left and/or right hand data out, to keep things simple for now I'm just using the right hand.  We know that the port the ofApp is sending on is 6448 so we'll configure a server for that. Here's my LeapOSCHandler script with inline commentary:

```csharp
using System;
using System.Net;
using System.Collections.Generic;

using UnityEngine;
using UnityOSC;




public class LeapOSCHandler : MonoBehaviour {


	#region Member Variables

	private OSCMessage _message = null; //this will hold the latest incoming message
	private OSCServer _server = null;	//this is a reference ot the server (receiver)
	private GameObject cube = null;		//the cube will update it's pitch, roll and yaw according to the values received from LEAP

	#endregion


	#region Properties

// 	The order of arguments coming from the LeapOSC openFrameworks app
//	[0] ==> right hand position x
//	[1] ==> right hand position y
//	[2] ==> right hand position z
//	[3] ==> right hand velocity x
//	[4] ==> right hand velocity y
//	[5] ==> right hand velocity z
//	[6..20] ==> right fingertips position x,y,z
//	[21] ==> right open hand size
//	[22] ==> right hand normal x
//	[23] ==> right hand normal y
//	[24] ==> right hand normal z
//	[25] ==> right hand direction x
//	[26] ==> right hand direction y
//	[27] ==> right hand direction z
//	[28] ==> right hand roll (radians)
//	[29] ==> right hand pitch (radians)
//	[30] ==> right hand yaw (radians)


	//--------------------------------------------------------------------------------------
	/// <summary>
	/// public accessor for the latest message
	/// </summary>
	//--------------------------------------------------------------------------------------
	public OSCMessage message
	{
		get
		{
			return _message;
		}
	}


	//--------------------------------------------------------------------------------------
	/// <summary>
	/// public accessor for the data list of the latest message
	/// </summary>
	//--------------------------------------------------------------------------------------

	public List<object> data
	{
		get{
			if (message == null)
				return null;
			return message.Data;
		}
	}

	//--------------------------------------------------------------------------------------
	/// <summary>
	/// public accessor for the latest roll value
	/// </summary>
	//--------------------------------------------------------------------------------------
	public float roll
	{
		get{
			if (data == null)
				return 0;
			return RadianToDegree((float)data [28]);
		}
	}

	//--------------------------------------------------------------------------------------
	/// <summary>
	/// public accessor for the latest pitch value
	/// </summary>
	//--------------------------------------------------------------------------------------
	public float pitch
	{
		get{
			if (data == null)
				return 0;
			return RadianToDegree((float)data [29]);
			}
	}

	//--------------------------------------------------------------------------------------
	/// <summary>
	/// public accessor for the latest yaw value
	/// </summary>
	//--------------------------------------------------------------------------------------
	public float yaw
	{
		get{
			if (data == null)
				return 0;
			return RadianToDegree((float)data [30]);
			}
	}
	#endregion

	#region Methods


	//--------------------------------------------------------------------------------------
	/// <summary>
	/// Clean up on quit
	/// </summary>
	//--------------------------------------------------------------------------------------
	void OnApplicationQuit() 
	{
		if (_server != null){
			_server.PacketReceivedEvent -= OnPacketReceived; 	//stop listeneing for events
			_server.Close();									//close the server
		}
	}


	//--------------------------------------------------------------------------------------
	/// <summary>
	/// Create a new server instance
	/// </summary>
	/// <param name="serverId">a name for the server </param>
	/// <param name="port">the port to listen on </param>
	//--------------------------------------------------------------------------------------
	public void CreateServer(string serverId, int port)
	{
		_server = new OSCServer(port);							//create the server listening on the specified port
		_server.PacketReceivedEvent += OnPacketReceived;		//start listening for update events

	}

	//--------------------------------------------------------------------------------------
	/// <summary>
	/// Event handler for incoming OSCPackets   	
	/// </summary>
	/// <param name="server">OSCServer </param>
	/// <param name="packet">OSCPacket </param>
	//--------------------------------------------------------------------------------------
	void OnPacketReceived(OSCServer server, OSCPacket packet)
	{
		//top level is the #bundle - in case of 2 hands there will be 2 messages in the bundle
		OSCBundle bundle = (OSCBundle)packet;
		List<object> messageList = bundle.Data;
		_message = (OSCMessage)messageList [0];

	}

	//--------------------------------------------------------------------------------------
	/// <summary>
	/// Utility method to convert degrees to radians   	
	/// </summary>
	/// <param name="angle">double, the angle in degrees to convert </param>
	//--------------------------------------------------------------------------------------
	private double DegreeToRadian(double angle)
	{
		return Math.PI * angle / 180.0;
	}

	//--------------------------------------------------------------------------------------
	/// <summary>
	/// Utility method to convert radians to degrees   	
	/// </summary>
	/// <param name="angle">double, the angle in radians to convert </param>
	//--------------------------------------------------------------------------------------
	private float RadianToDegree(float angle)
	{
		return (float) (angle * (180.0 / Math.PI));
	}


	//--------------------------------------------------------------------------------------
	/// <summary>
	/// Initialise the server and set up references to game objects
	/// </summary>
	//--------------------------------------------------------------------------------------
	public void Init()
	{
		cube = GameObject.Find ("Cube");
		CreateServer ("LeapOSC", 6448);
	}

	// Use this for initialization
	void Start () {
		Init();
	}


	//--------------------------------------------------------------------------------------
	/// <summary>
	/// Update is called once per frame by the game engine
	/// We update  any game objects here
	/// </summary>
	//--------------------------------------------------------------------------------------
	void Update () {
		cube.transform.eulerAngles = new Vector3 (pitch, yaw, roll);
	}

	#endregion

}

```

Here's how it works.

Lines [16..18] set up 3 variables; 
 - `OSCMessage _message` is going to be updated every time a new message comes in however I'll only access it to update my view once every frame in the `Update` method (line 210).
 - `OSCServer _server` will reference the instance of my server - I only need one for this project - but we need a way to destroy it when the program quits.
 - `GameObject cube` is a simple primitive to allow me to see the effects of my output from the LEAP.

Jumping to the bottom of the script the `Start` method [line 199] is automatically called by the Unity Engine and simply calls the `Init` method [line 192]. Inside `Init` I grab a reference to the Cube GameObject on stage and call `CreateServer` with a name of 'LeapOSC and the port number 6448 (which is the port the ofApp is outputting on).

The `CreateSever` method [line 141] instantiates a new `OSCServer` and assigns it to the `_server` variable and then adds an event listener for the server's `PacketReceivedEvent` which provides a callback method of `OnPacketReceived`.

[Line 155] `OnPacketReceived` receives a packet from the LeapOSC ofApp and unpacks the incoming `OSCPacket`. Each OSCPacket can contain any number of messages wrapped up in a packet with a #bundle type. In this case there is only one message at `messageList[0]`. Each new message simply updates the `_message` variable.

Now in the `Update` method [line 210] I can update the cube with the latest values from the leap. Because the `pitch`,`roll` and `yaw` values coming form the LEAP are in radians each one is converted to degrees in it's accessor method [lines 79..114]

And that's it really, the cube should rotate to match the orientation of your hand. There are plenty of other values coming from the LEAP too - Lines [25..42] are a lookup guide to these values (for the right hand only).

Next job is to get the app running on the iPhone.

**Running on the iPhone:**

I export the Unity app by hitting File -> Build Settings and selecting the iOS platform. I select 
 - Run in Xcode as: Debug. 
In _Player Settings_ I set 
 - Default Orientation: Landscape Left
 - Requires FullScreen: true
 - Everything Else: Leave at default
Then hit the Build button.
When the project has completed building I go over to Xcode and open the .xcodeproj file, my iPhone5 is connected to my Mac by USB. I find I have to look in the _Build Settings_ tab and change:
 - BaseSDK: Latest iOS
 - Architectures: Standard Architectures (armv7s, arm64)
Then I _Build & Run_.. which results in a **Failed Build**

>ld: -undefined and -bitcode_bundle (Xcode setting ENABLE_BITCODE=YES) cannot be used together
clang: error: linker command failed with exit code 1 (use -v to see invocation)

So I go back to _Build Settings_ and toggle off Enable Bitcode. Now the product builds correctly but the app dies with a fatal error:

>dyld: Symbol not found: __ZN6il2cpp6icalls6System6System14ComponentModel14Win32Exception15W32ErrorMessageEi
  Referenced from: /var/containers/Bundle/Application/CE52036C-B9DC-499A-AB18-A2B8A8D65E84/meadowlark.app/meadowlark
  Expected in: flat namespace
 in /var/containers/Bundle/Application/CE52036C-B9DC-499A-AB18-A2B8A8D65E84/meadowlark.app/meadowlark

So why would it run in the simulator but not on the device? After some Googling I go back to Unity's _Player Settings_ and under _Other Settings_ set
 - Target SDK: Device SDK (was Simulator SDK)

That runs now.

So one change to make in the OpenFrameworks app. In 'ofApp.h' I need to set the `#define DEFAULT_LEAP_OSC_IP` to the local network address of my iPhone. Run both apps and this is the result - the cube on the iPhone is controlled using data sent as OSC messages from the LEAP controller app running on the Mac. **This will enable me now to interact with objects in a VR world using hand gestures.**

{% include vimeoplayer.html id=page.video %}

**Adding Stereoscopic View:**

First I head on over to [Google VR for Unity](https://developers.google.com/vr/unity/download) page and download their sdk. Enabling a stereo camera is as simple as adding a `StereoController` script component to the camera. I build and run the project now, slip my iPhone into the cardboard headset and I am now controlling the cube in the VR world!