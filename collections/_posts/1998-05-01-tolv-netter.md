---
layout: page
title: Transmute - Dark Sound
date: 1998-05-01T16:44:29+00:00
project: true
categories:
  - code
  - projects
  - technology
tags:
  - projects
tech:
 - Director
 - FFT
description: Sound activated animation artwork
video: 72633767
---

<div class="img_row">
	<img class="col three" src="{{ site.baseurl }}/images/heroes/tolv0213.gif" alt="" title="tolvnetter"/>
</div>
<br/>

Back in 1998 I had the opportunity, along with my some-time-collaborator Georgina Evans, to take part in a very special show curated by Jana Winderen: Tolv Netter (Twelve Nights). It would take place during the NattJazz festival in Bergen, Norway. Together with a number of other artists, we would produce sound-based work to fill the voids between concert spaces at the United Sardine Factory.

For our piece we hooked up a microphone to the computer and analysed the incoming audio using a fast fourier transform (FFT). This allowed us to compute the relative velocities of each frequency band in the incoming signal. A simple artificial intelligence implementation mapped the frequencies to one of a number of animations such that the higher the velocity the higher the frame number of the animation. This meant that by singing a very 'pure' tone with increasing loudness the singer would play the animation forwards and the inverse would play the animation backwards.  

Naturally this is a difficult technique to master but great entertainment and appropriate at a jazz festival: the sounds produced were like an avant-garde jazz composition, an induced _scat_ elicited by the playfulness of the participants in response to the software.

From the catalogue:


>"The artists converted the first floor balcony, which overlooks the crowded entrance hall downstairs, into a small stage. A microphone at the disposal of the public, who were invited to use their vocal chords to the best of their ability. In response to the sounds expressed, images appeared on a large video screen on the opposite wall. Astonished we could notice how our voice, which normally only produces sound, turned into a make of images. Cryptic symbols and image sequences appeared and metamorphosed in succession; a ball of fire, a face, an effigy, enigmatic hieroglyphs or ideograms; became imbued with almost archetypal qualities."


{% include vimeoplayer.html id=page.video %}

<div class="img_row">
	<img class="col one left" src="{{ site.baseurl }}/images/tolvnetter/tolv201.gif" alt="" title="tolvnetter"/>
	<img class="col two right" src="{{ site.baseurl }}/images/tolvnetter/deep02.gif" alt="" title="tolvnetter"/>
</div>
<div class="col three caption">
	Views of Our Work 'Transmute: Dark Sound'
</div>
<br/>