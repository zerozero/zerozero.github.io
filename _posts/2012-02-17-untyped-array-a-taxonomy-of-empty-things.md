---
id: 271
title: 'Untyped array: a taxonomy of empty things'
date: 2012-02-17T20:54:13+00:00
author: zerozero
layout: post
categories:
  - art
---
Taking as it&#8217;s point of origin Michel Foucault&#8217;s analysis of Jorge Luis Borges&#8217; quotation from the &#8216;Celestial Emporium of Benevolent Knowledge&#8217;s Taxonomy&#8217;<sup>[<a name="id394062" href="#ftn.id394062">*</a>]</sup>. Untyped Array is an apparatus for observing a taxonomy of Empty Objects &#8211; those that are discarded, useless, without function or inchoate.<!--more-->

<div id="attachment_37" style="width: 310px" class="wp-caption alignnone">
  <a href="http://162.13.3.34/labs/wp-content/uploads/2012/02/apparatus-drawing.jpg"><img class="size-medium wp-image-37" title="apparatus drawing" src="http://162.13.3.34/labs/wp-content/uploads/2012/02/apparatus-drawing-300x206.jpg" alt="apparatus drawing" width="300" height="206" /></a>
  
  <p class="wp-caption-text">
    The apparatus (pen & ink)
  </p>
</div>

The apparatus array consists of a vitrine containing an array of tiny objects, each suspended by a transparent method mid-way into the cabinet. On a plane in front of the array of objects, and co-planar with it, runs a motor-driven mechanism with the capability of moving left-right and up-down. Attached to the mechanism is a magnifying glass and a tiny video camera. The camera is connected to a computer running a facial detection programme and outputting command signals to the motors controlling the x-axis and z-axis movement.

As a viewer approaches the vitrine the computer should detect the presence of a human face and respond by sending command signals to the motors. The computer will activate the motors and attempt to achieve a co-ordinate for the mechanism where the observer&#8217;s face occupies a pre-determined point in the video frame such that the magnifying glass is always positioned between the observer and the object in the array that is being observed.

<div id="attachment_36" style="width: 278px" class="wp-caption alignnone">
  <a href="http://162.13.3.34/labs/wp-content/uploads/2012/02/vitrine_skp.jpg"><img class="size-medium wp-image-36" title="vitrine_skp" src="http://164.177.158.250/blog/wp-content/uploads/2012/02/vitrine_skp-268x300.jpg" alt="vitrine sketchup render" width="268" height="300" /></a>
  
  <p class="wp-caption-text">
    Sketchup redering of the apparatus
  </p>
</div>

#### The technology

The computer is running an OpenCV implementation on OpenFrameworks and communicating over serial to an Arduino Uno. Two Stepper motors are driven by an pair of EasyDriver stepper units. The horizontal mechanism currently uses a rack and pinion drive while vertical motion will be provided by a timing belt driven by the other motor.

The video camera is a UniBrain firewire camera with a wide-angle (xxmm) lens.

<div id="attachment_73" style="width: 310px" class="wp-caption alignnone">
  <a href="http://162.13.3.34/labs/wp-content/uploads/2012/02/vitrine_mockup.jpg"><img class="size-medium wp-image-73" title="vitrine_mockup" src="http://162.13.3.34/labs/wp-content/uploads/2012/02/vitrine_mockup-300x96.jpg" alt="The vitrine (mockup)" width="300" height="96" /></a>
  
  <p class="wp-caption-text">
    The vitrine (mockup)
  </p>
</div>

The glass panel is a found object &#8211; rummaged from a skip 20 years ago outside a bank in Westminster. The rest of the cabinet will be constructed from 3/4&#8243; ply and 3&#215;3 timber. The substructure is 1&#8243; box section aluminium tied together with threaded rod and M8 nuts.

This video demonstrates the current progress of the work, as of November 2011 the apparatus has a functional horizontal access that is able to track a face successfully.



<

div class=&#8221;footnote&#8221;>
  
<sup>[<a name="ftn.id394062" href="#id394062">*</a>]</sup>
  
The Celestial Emporium of Benevolent Knowledge.

In its remote pages it is written that animals can be divided into

  1. those belonging to the Emperor,
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

<

div>

<div class="gk-social-buttons">
  <span class="gk-social-label">Share:</span> <a class="gk-social-twitter" href="http://twitter.com/share?text=Untyped+array%3A+a+taxonomy+of+empty+things&url=http%3A%2F%2F162.13.3.34%3A8079%2Flabs%2F%3Fp%3D271"
	            onclick="window.open(this.href, 'twitter-share', 'width=550,height=235');return false;"> <span class="social__icon--hidden">Twitter</span> </a> <a class="gk-social-fb" href="https://www.facebook.com/sharer/sharer.php?u=http%3A%2F%2F162.13.3.34%3A8079%2Flabs%2F%3Fp%3D271"
			     onclick="window.open(this.href, 'facebook-share','width=580,height=296');return false;"> <span class="social-icon-hidden">Facebook</span> </a> <a class="gk-social-gplus" href="https://plus.google.com/share?url=http%3A%2F%2F162.13.3.34%3A8079%2Flabs%2F%3Fp%3D271"
	           onclick="window.open(this.href, 'google-plus-share', 'width=490,height=530');return false;"> <span class="social__icon--hidden">Google+</span> </a>
</div>