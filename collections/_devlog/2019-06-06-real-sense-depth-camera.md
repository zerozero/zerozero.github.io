---
title: Nuitrack SDK + Intel RealSense Depth Camera
project: false
layout: post
categories:
  - code
  - projects
  - technology
tags:
  - projects
description: Notes and learnings from a month of R&D
tech:
 - Intel Realsense Depth Camera
 - Nuitrack SDK
---

<div class="img_row">
	<img class="col three" src="{{ site.baseurl }}/images/realsense/stereo_DT_d435_front-crop1a.png" alt="" title="Intel RealSense Depth Camera"/>
</div>
<div class="col three caption">
	Intel RealSense Depth Camera
</div>

- The system captures an image of the object being held in the hand nearest to the camera. 
-	The system uses the depth sense camera to track the location of the subject’s hands while in the space,
-	The resolution of the snapshot image is estimated by finding the depth of the hand then performing a flood fill of all surrounding depth pixels whose z-position is within a certain tolerance of the original.(1)
    