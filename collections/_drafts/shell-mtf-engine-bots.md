---
title: Shell 'Make The Future - Engine Bots'
date: 2018-05-03T13:38:21+00:00
author: zerozero
layout: post
categories:
  - code
disqus: no
video: 329891751
---

<div class="img_row">
	<img class="col three" src="{{ site.baseurl }}/images/mtf/engine-bots-setup.jpg" alt="" title="EngienBots Setup"/>
</div>
<div class="col three caption">
	Setting up Engine Bots
</div>
**[Make the Future Live 2018](https://uk.makethefuture.shell/about.html){:target="_blank"}** was a 'festival of bright energy ideas' held at the Queen Elizabeth Olympic Park in London. The free four-day festival aimed to explore solutions to the energy challenge, celebrate ingenuity and bold new thinking.

My brief is to create an educational game for kids that features five 'EngineBot' avatars displayed on a massive LED screen. Using Kinect-like motion sensing devices I should use the players body to control the rigged avatars enabling participants to catch virtual 'fuel crystals' as they drop into the play space.

Since Microsoft mothballed the Kinect I turn to the [Orbbec Persee](https://orbbec3d.com/product-persee/){:target="_blank"} to provide the depth-sensing, skeleton tracking hardware. Orbbec's advanced structured light capture technology and built-in ARM processor allow me to create a system capable of accurately tracking five skeletons simultaneously and piping the data over a web socket to a high-powered PC rendering real-time motion-controlled avatars on screen. 

#### Skeletal Tracking ####

To perform the tracking I use the [Nuitrack SDK](https://nuitrack.com/){:target="_blank"} to puppet a rigged animation in Unity. The requirement is for 5 bots to be controlled simultaneously. The user's face should be visible through the bot helmet.

<div class="img_row">
	<img class="col one left" src="{{ site.baseurl }}/images/mtf/bot.jpg" alt="" title="EngienBot"/>
</div>
<div class="caption_row">
    <div class="col one left caption">EngineBot with transparent visor</div>
</div>

This requires mapping skeleton data provided by the Nuitrack software to bone data for a standard Unity biped rig.

<div class="img_row">
    <a href="{{ site.baseurl }}/images/mtf/mappings.jpg">
	    <img class="col one" src="{{ site.baseurl }}/images/mtf/mappings.jpg" alt="" title="Bone Mapping"/>
	</a>
</div>
<div class="caption_row">
    <div class="col one left caption">Bone Mapping</div>
</div>

I find that there is a limit of 3 skeletons that can be tracked simultaneously by the Persee - theoretically it is possible to track more but there is no way of changing the device configuration. So I decide to attempt to run three units and track 2 bodies on each (or 2-1-2 actually).This approach shows promise but throws up another problem as tracing appears to fail towards the edge of each units field of view.

Knowing that the depth sensor is a structured light depth scanner that functions by projecting a pattern of infra red data into the space I theorise that overlapping areas of projection are confusing the sensor readings. There are two commonly used methods of structured light scanning: one uses a grid pattern and the other a random speckle pattern. I need to know which the Persee is using.

Hoping to be able to image the IR projection I point my iPhone at the Persee projector but I see nothing - the iPhone camera filters IR light. But I remember that even though Apple started filtering IR on the back-facing camera some time ago the front-facing camera does not. I flip the phone around and this is what I manage to capture:

<div class="img_row">
    <a href="{{ site.baseurl }}/images/mtf/IR-pattern.jpg">
	    <img class="col one" src="{{ site.baseurl }}/images/mtf/IR-pattern.jpg" alt="" title="IR pattern"/>
	</a>
</div>
<div class="caption_row">
    <div class="col one left caption">IR pattern</div>
</div>

Evidently a speckle pattern. With the three Persees in a line I use masking tape to shutter the projector and prevent the projected images from overlapping. With a little tweaking I am able to successfully track 5 skeletons.

{% include vimeoplayer.html id=page.video %}