---
layout: page
title: Science Museum - Collider
date: 2013-11-01T16:44:29+00:00
project: true
categories:
  - code
  - projects
  - technology
tags:
  - projects
description: Proximity activated sound installation
tech:
 - Arduino
 - Wave Shield
 - Ultrasonic Sensor
---


<div class="img_row">
	<img class="col three" src="{{ site.baseurl }}/images/heroes/collider.jpg" alt="" title="collider"/>
</div>
<br/>
As part of the Science Museum's "Collider" exhibition celebrating the LHC at Cern and the work of Professor Higgs I was asked to create this small sound installation. The exhibition itself was an unusual mix of audio-visual content arranged in a mock-up of part of the LHC building through which visitors moved. In order to minimise cross-talk between the radio installation and the various other audio pieces it was specified that the radio be activated by human presence.

I created an Arduino program that used a state machine implementation to order the playing, pausing and re-starting of the audio piece according to the various states of human absence, presence, dwell and concurrency measured by the ultrasonic sensor.