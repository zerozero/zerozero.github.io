---
layout: page
title: The Icarus Project
date: 1994-06-06T16:44:29+00:00
project: true
categories:
  - code
  - projects
  - technology
  - VR
tags:
  - projects
description: Early prototype Head-Mounted-Display
mosaic:
    images:
        - url: /images/icarus/IMG_1114.jpg
        - url: /images/icarus/IMG_1115.jpg
        - url: /images/icarus/IMG_1116.jpg
        - url: /images/icarus/IMG_1117.jpg
        - url: /images/icarus/IMG_1118.jpg
        - url: /images/icarus/IMG_1119.jpg
tech:
 - Mini TV monitors
 - Mini Video Camera
 - Symbolics rendered content

---

<div class="img_row">
	<img class="col three" src="{{ site.baseurl }}/images/hmd.jpg" alt="" title="VR Simulator"/>
</div>
<div class="caption_row">
    <div class="col three left caption">Prototype head-mounted-display (1994)</div>
</div>

These days when we have become used to a multitude of VR/AR/XR headsets and systems it is perhaps easy to forget that the current hype-cycle for these technologies is not the first. Indeed it could be argued that the idea of virtual worlds is as old as photography itself.

Back in the early 1990s there was another of the periodic blips of excitement surrounding virtual reality and stereoscopic viewing devices. Coalescing around the ideas of people such as Jaron Lanier futurologists and technologists were speculating that computing power had maybe reached a point where such technologies were finally viable. Excited by these ideas I set about researching and developing a head-mounted display system of my own. I was at the time enrolled on the Electronic Imaging course at Duncan of Jordanstone College in Dundee. 

Against the advice of my tutors - 'Don't try to re-invent the wheel' they cried (ignoring the fact that this particular wheel had not yet been invented) - I set about creating a mixed-reality head-mounted display for myself. I bought a cheap snorkelling mask, some high powered disposable reading glasses from the pharmacy, two minature LCD tvs and arranged to hire a mini video camera.

<div class="img_row">
    <a href="{{ site.baseurl }}/images/hmd-plan.jpg">
	    <img class="col three" src="{{ site.baseurl }}/images/hmd-plan.jpg" alt="" title="HMD Plan"/>
	</a>
</div>
<div class="caption_row">
    <div class="col three left caption">HMD Plan</div>
</div>

The headset was lashed together with masking tape to a specification based on average human interpupillary distance and a focal length determined by the spectacles. Of course the system was tethered - to two interlocked video players and a mixing desk (real time rendering was not an option) - there was no concept of head tracking so the displayed scene moved with the head - hello nausea! 

A colleague created some stereoscopic 3D content on the Symbolics S-System (obsolete now but at the time state-of-the-art). The 20 second looping animation took about 12 hours to render. The animated content was piped through a video mixer and keyed with output from the single mini video camera (the aim was to have stereo cameras but budget constraints didn't allow for that).

And so the prototype was made. Primitive? Yes. Cumbersome? Very. Scary having 24V LCD Tvs strapped inches in front of your eyes? Hell yes. But for those brave enough to try it not a bad attempt for the era. Who knows, had I been in Silicon Valley rather than Dundee I might even have got funding to develop it further and beaten Mark Z to the punch by 20 years or so. Ah well..   

{% include image-gallery.html folder="/images/icarus" %}

<!--div class="img_row">
	<img class="col two" src="{{ site.baseurl }}/images/icarus/IMG_1114.jpg" alt="" title="VR Simulator"/>
	<img class="col one" src="{{ site.baseurl }}/images/icarus/IMG_1115.jpg" alt="" title="VR Simulator"/>
</div>
<div class="img_row">
	<img class="col two" src="{{ site.baseurl }}/images/icarus/IMG_1117.jpg" alt="" title="VR Simulator"/>
	<img class="col one" src="{{ site.baseurl }}/images/icarus/IMG_1118.jpg" alt="" title="VR Simulator"/>
</div>
	<img class="col two" src="{{ site.baseurl }}/images/icarus/IMG_1119.jpg" alt="" title="VR Simulator"/>
</div-->