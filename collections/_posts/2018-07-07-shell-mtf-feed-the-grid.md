---
title: Shell - Make The Future (Feed The Grid)
date: 2018-07-07T13:38:21+00:00
author: zerozero
layout: post
description: Interactive video using Leap Motion other sensors
categories:
  - code
disqus: no
video: 387755396
---

<div class="img_row">
	<img class="col three" src="{{ site.baseurl }}/images/ftg/Feed The Grid .jpg" alt="" title="Feed The Grid"/>
</div>
<div class="col three caption">
	City Simulation
</div>
**[Make the Future Live 2018](https://uk.makethefuture.shell/about.html){:target="_blank"}** is a 'festival of bright energy ideas' held at the Queen Elizabeth Olympic Park in London. The free four-day festival aims to explore solutions to the energy challenge, celebrate ingenuity and bold new thinking.

The idea is to trigger a series of animations using hand gestures and blowing actions. I have a leap motion controller to pick up gestures and a small plastic windmill which has a microphone hacked from an iphone headset. I can measure the noise produced by blowing into the microphone and set a threshold beyond which an animation will trigger. The leap motion needs to pick up a few different gestures: waving, a circular motion  and wiggling fingers. I decide to use a 2D gesture recogniser _$P_ (p-dollar) to interpret gestures made by the hands. This simple library represents gestures as an unordered point-cloud. By creating training sets of gestures I can simply compare a user's gestures to the training set and determine the gesture. It's not totally accurate but it does have the advantage of being small and efficient and works well enough for our purposes.
   
{% include vimeoplayer.html id=page.video %}
