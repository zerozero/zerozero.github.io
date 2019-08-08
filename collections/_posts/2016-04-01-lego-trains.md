---
layout: page
title: Lego Trains
date: 2016-04-01T16:44:29+00:00
project: true
video: 170275262
categories:
  - code
  - projects
  - technology
tags:
  - projects
tech:
- Unity
- LLAPI
- 2D
- 3D
- C#
description: Unity 3D Multiplayer train simulation
---

<div class="img_row">
	<img class="col three" src="{{ site.baseurl }}/images/heroes/lego.jpg" alt="" title="Lego Discovery Centre"/>
</div>
<br/>

This project used the Unity Low-Level API for UDP socket communication between 5 touch-screen controlled client machines and a single 'server' hub machine. The server ran a 3D game containing 5 lego tracks each with train controlled by one of the client machines. State machine implementations on both server and client together with a simple API allowed efficient bi-directional communication between server and client.

High quality 3D assets were supplied by LEGO and optimised for display in Unity. Control of the trains was achieved using the [Tracks and Rails plugin](http://zenfulcrum.com/) to provide realistic physics properties for the rolling stock. Carriages were connected by a system of joints and springs to the engine. In April 2016 I travelled to Tokyo to complete the installation working alongside staff at the discovery centre. The new exhibit was launched with a fanfare and local TV coverage. 


{% include vimeoplayer.html id=page.video %}