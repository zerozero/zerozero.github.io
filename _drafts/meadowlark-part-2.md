---
layout: post
title: Meadowlark (part 2)
category: Identity
description: Low-poly Unity game (devlog part 2)
---

**Objective:**
Picking up again on the development of this game. I have upgraded my skills in several areas over the last year so I now want to push this idea through but - as is the way of these things - my scope has become a bit more ambitious. I really want to get some VR going and I have a Google Cardboard rig to hand so I'm going to target that primarily. My Unity skills are up to that task now. I have also been working with machine learning techniques and have started developing some interesting ways of working with the LEAP motion controller along with Wekinator (a tool for real-time ML processing). So here's the aim

	LEAP Motion --> OpenFrameworks --> Wekinator --> Google Cardboard
	
All messaging will be done with OSC and Wekinator will learn to recognise certain hand gestures which it will pass into the Cardboard VR app to control the hero dragonfly in Meadowlark.

In part 2 I'll be attempting to get a simple communication going between the LEAP and a Unity scene with OSC.

**Get Things:**
- [LeapOSC](https://github.com/genekogan/LeapOSC) - open source openFrameworks LEAP motion data streaming

**Set up**

_openFrameworks_
Opening LeapOSC in XCode the first thing to check is that the active scheme is set correctly, this is set in the upper left of the toolbar - for some reason it often defaults to openFrameworks but it needs to be changed to "LeapOSC Debug" (or Release).

![Active Scheme]({{ site.baseurl }}/images/uploads/meadowlark-part-2/m1.png)

With the LEAP controller attached via USB we can hit Run and view the input from our hands.

Looking in `src/ofApp.h` we can see the default values for OSC communication are:

```cpp
#define DEFAULT_LEAP_OSC_ADDRESS "/wek/inputs"
#define DEFAULT_LEAP_OSC_IP "localhost"
#define DEFAULT_LEAP_OSC_PORT 6448
```

Let's leave these as they are for now since to start with we'll run Unity on the same machine. 

_Unity_
I've created a new empty Unity project and I'm going to use [this library](https://github.com/jorgegarcia/UnityOSC) to receive OSC messages. Follow the instructions in the readme to install (hint: create an Editor folder inside Assets in your Unity project if one doesn't exist already).

For this first test we'll create a new Empty Game Object and attach the OSC Handler Script to it. Open that script and we can find the Init method which is where we'll configure our OSC message handlers. We're just dealing with incoming message for this project so we want to set up a receiver/server. We know that the port the ofApp is sending on is 6448 so we'll configure a server for that:

```csharp
/// <summary>
/// Initializes the OSC Handler.
/// Here you can create the OSC servers and clientes.
/// </summary>
public void Init()
{
       //Initialize OSC clients (transmitters)
       //Example:		
       //CreateClient("SuperCollider", IPAddress.Parse("127.0.0.1"), 5555);

       //Initialize OSC servers (listeners)
       //Example:

       CreateServer("LeapOSC", 6448);
}
```

This Init method needs to be called from somewhere when we start our app so we create a new component -> new script on the Empty Game Object and in it's `Start` method call the OSCHandler method Init:

```csharp
// Use this for initialization
void Start () {
	OSCHandler.Instance.Init();
}
```

If we open the OSCHelper panel (installed in the Editor folder) and hit Play we should see the server instance displayed.

![Server Inited]({{ site.baseurl }}/images/uploads/meadowlark-part-2/m2.png)
