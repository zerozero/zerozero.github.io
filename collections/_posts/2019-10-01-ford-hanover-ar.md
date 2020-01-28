---
layout: post
title: Ford Hanover AR Billboard
project: true
categories:
  - code
  - projects
  - technology
  - AR
tags:
  - projects
description: Augmented Reality activation using rotational sensors on a large-screen display
video: 387510310
---

<div class="img_row">
	<img class="col three" src="{{ site.baseurl }}/images/ford-hanover/IMG_0181.jpeg" alt="" title="Panorama"/>
</div>
<div class="col three caption">
	Panorama
</div>

Leading London experience agency **Imagination** contracted me to create this large format interactive AR billboard as part of their activation for Ford's Hanover trade show in 2018. The digital billboard is housed in a rotating floor-mounted cabinet equipped with a rotational sensor. Data from the sensor is ingested by the Unity app and used to determine the AR content on-screen. A high definition Black Magic camera mounted on the back of the screen captures live data from the exhibition floor space while 3D content is overlaid on top.  

<div class="img_row">
	<img class="col two" src="{{ site.baseurl }}/images/ford-hanover/IMG_0147.jpeg" alt="" title="AR"/>
</div>
<div class="col two caption">
	AR Billboard
</div>
<br/>
<br/>
<br/>

On the front of the billboard the user has an ergonomic bar allowing easy rotation of the device thanks to a damped axle. While not in use a looped series of animations play as an attractor. When a user starts to interact with the device the content becomes interactive and simple mini-games are triggered.

**Technical Considerations**

In order to allow for accurate placement of the AR content on-site an admin interface was created allowing all 3D content to be calibrated left-right, up-down, in-out during setup. Videos were encoded using the .webm format to allow for transparency in Unity. Analytical data was produced through the course of the show and written to a timestamped .csv file to enable Ford to analyse usage statistics and engagement metrics.

{% include vimeoplayer.html id=page.video %}
