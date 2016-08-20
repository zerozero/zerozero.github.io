---
layout: post
title: Unity LLAPI
category: Code
description: Setting up a client - server network architecture with Unity's LLAPI
---

*Objective*
Communicating over a LAN network using full duplex communication.

Step-by-step guide to using Unity's UNET Low-Level API (LLAPI)

*Intro

Documentation for Unity's Low-Level networking API is a little sketchy and seems to be a cause of some frustration (https://www.reddit.com/r/Unity3D/comments/3tvow6/trouble_with_network_llapi/). Here I will try to demystify the process and present a robust methodology for sending messages across a network from multiple client machines to a single server and back.

This example uses C#

*Basic concepts

Read here (http://blogs.unity3d.com/2014/06/11/all-about-the-unity-networking-transport-layer/)

Network and transport layers

Logically, the UNET low level library is a network protocol stack built on top of the UDP, containing a “network” layer and a “transport” layer. The network layer is used for creating connections between peers, delivering packets and controlling possible flow and congestion. The transport layer works with “messages” belonging to different communication channels.

*Quality of Service

*Server code

*Client Code

*Connection

*Serialisation

*IP addresses and Ports

*Download code



