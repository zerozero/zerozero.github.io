---
layout: page
title: Lego Trains
date: 2016-04-01T16:44:29+00:00
project: true
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
---

Unity Multiplayer 3D train simulation game for Lego Discovery Centre, Tokyo.

This project used the Unity Low-Level API for UDP socket communication between 5 touch screen controlled client machines and a single 'server' hub machine. The server ran a 3D game containing 5 lego tracks each with a different engine controlled by each of the client machines. State machine implementations on both server and client together with a simple API allowed efficient communication between each.

>Technology:
{% for tech in page.tech %} 
>{{tech}} {% endfor %}