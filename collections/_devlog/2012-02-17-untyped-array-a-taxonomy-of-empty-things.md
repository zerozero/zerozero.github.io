---
id: 271
title: 'Untyped array: a taxonomy of empty things'
date: 2012-02-17T20:54:13+00:00
author: zerozero
layout: post
categories:
  - art
video: 32745652
disqus: yes
---
_Taking as it's point of origin Michel Foucault's analysis of Jorge Luis Borges' quotation from the &#8216;Celestial Emporium of Benevolent Knowledge's Taxonomy'<sup>[<a name="id394062" href="#ftn.id394062">*</a>]</sup>. Untyped Array is an apparatus for observing a taxonomy of Empty Objects - those that are discarded, useless, without function or inchoate._


<div class="img_row">
	<img class="col three" src="{{ site.baseurl }}/images/uploads/apparatus-drawing.jpg" alt="" title="untyped array"/>
</div>
<div class="col three caption">
	The apparatus (pen & ink)
</div>
<br/>

**The apparatus**

The apparatus array consists of a vitrine containing an array of tiny objects, each suspended by a transparent method mid-way into the cabinet. On a plane in front of the array of objects, and co-planar with it, runs a motor-driven mechanism with the capability of moving left-right and up-down. Attached to the mechanism is a magnifying glass and a tiny video camera. The camera is connected to a computer running a facial detection programme and outputting command signals to the motors controlling the x-axis and z-axis movement.

As a viewer approaches the vitrine the computer should detect the presence of a human face and respond by sending command signals to the motors. The computer will activate the motors and attempt to achieve a co-ordinate for the mechanism where the observer's face occupies a pre-determined point in the video frame such that the magnifying glass is always positioned between the observer and the object in the array that is being observed.

<div class="img_row">
	<img class="col two left" src="{{ site.baseurl }}/images/uploads/vitrine_skp.jpg" alt="" title="vitrine sketchup render"/>
</div>
<div class="col two left caption">
	Sketchup redering of the apparatus
</div>
<br/>

**The technology**

The computer is running an OpenCV implementation on OpenFrameworks and communicating over serial to an Arduino Uno. Two Stepper motors are driven by an pair of EasyDriver stepper units. The horizontal mechanism currently uses a rack and pinion drive while vertical motion will be provided by a timing belt driven by the other motor.

The video camera is a UniBrain firewire camera with a wide-angle (xxmm) lens.

<div class="img_row">
	<img class="col three" src="{{ site.baseurl }}/images/uploads/vitrine_mockup.jpg" alt="" title="The vitrine (mockup)"/>
</div>
<div class="col three caption">
	The vitrine (mockup)
</div>
<br/>

The glass panel is a found object - rummaged from a skip 20 years ago outside a bank in Westminster. The rest of the cabinet will be constructed from 3/4" ply and 3x3 timber. The substructure is 1" box section aluminium tied together with threaded rod and M8 nuts.

This video demonstrates the current progress of the work, as of November 2011 the apparatus has a functional horizontal access that is able to track a face successfully.


{% include vimeoplayer.html id=page.video %}
  <br/>

  
<sup>[<a name="ftn.id394062" href="#id394062">*</a>]</sup>
  
The Celestial Emporium of Benevolent Knowledge.

In its remote pages it is written that animals can be divided into

>  1. those belonging to the Emperor,
  2. those that are embalmed,
  3. those that are tame,
  4. pigs,
  5. sirens,
  6. imaginary animals,
  7. wild dogs,
  8. those included in this classification,
  9. those that are crazy-acting
 10. those that are uncountable
 11. those painted with the finest brush made of camel hair,
 12. miscellaneous,
 13. those which have just broken a vase, and
 14. those which, from a distance, look like flies
