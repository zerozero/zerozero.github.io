---
layout: page
title: Pharma VR training simulator
date: 2017-01-12T16:44:29+00:00
project: true
categories:
  - code
  - projects
  - technology
  - VR
tags:
  - projects
description: VR training simulator for administering novel cancer treatment
tech:
 - HTC/Vive
 - C#
 - Unity
 - NewtonVR
 - Strange IoC
video: 201068644
---

<div class="img_row">
	<img class="col three" src="{{ site.baseurl }}/images/heroes/Darzalex.jpg" alt="" title="VR Simulator"/>
</div>
<br/>

This project is not yet in the public domain - please [contact me](mailTo:mail@jonrowe.com?subject='password please') to request access to the video.

{% include vimeoplayer.html id=page.video %}

<br/>
Created for a world leading pharmaceutical company I was the developer/technical artist on this project for Masters of Pie. The application enables healthcare professionals to experience administering a ground-breaking new cancer treatment to a simulated patient in a realistic environment. The experience provides guidance on best practice for administration and on handling infusion related reactions to the treatment. It is conceived as a group activity with one person wearing the headset while the rest of the group guides and discusses the various treatment options available while watching on an external screen.

To efficiently manage the data model of the application I used StrangeIoC dependency injection framework combined with the NewtonVR physics library to realistically simulate physical interactions. The patient is data modelled with various physical properties such as blood pressure, respiratory rate and blood oxygen levels, all of which respond realistically to the administration of the drug. In addition character animations controlled by a Mecanim state machine and animated shader values to produce skin-tone and sweat responses are built into the system.

My responsibilities included:

+ Define frameworks and tooling
+ Architect code base and define source control and collaboration methodology
+ Implement 3D models, materials and shaders in line with art team vision 
+ Create flexible, dynamic animation system to accurately convey patient health
+ Produce robust, clean, tested code and documentation




