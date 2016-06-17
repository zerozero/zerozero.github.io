---
layout: page
title: Pottermore - Potions
date: 2011-07-01T16:44:29+00:00
project: true
categories:
  - code
  - projects
  - technology
tags:
  - projects
tech:
 - Flash
 - Box2D
 - Flint Particle System
description: 2D physics-based simulation game
video: J5bq0Q7rkZY
---

<div class="img_row">
	<img class="col three" src="{{ site.baseurl }}/images/heroes/pottermore_large.jpg" alt="" title="pottermore"/>
</div>
<div class="col three caption">
	Potions interface
</div>
<br/>

Featuring particle effects and physics 'Potions' is a tricky and engaging game of skill in which players must precisely follow a recipe to mix magical potions. In the game the player must accurately mix potions such as 'Cure for Boils' or 'Sleeping Draught' against the clock which they will later be able to use to cast spells elsewhere in the 'Pottermore' experience. 

The player collects ingredients hidden arround the pottermore site. They must add precise amounts of each ingredient to one of the containers - pestle & mortar or cauldron - then crush or heat, stir and finally wave their wand to complete the concoction. The result is either a new potion for their inventory or a potentially catastrophic explosion losing house-points and possibly destroying their cauldron. 

I lead a small team of flash developers within the larger project team which was run on agile principles, I undertook the analysis and design of the game liaising with the in-house UX architects, design team and .NET programmers. 

Using Flint allowed us to treat every particle as an individually addressable object which gave us the scope to perform per-particle collision detection. Knowing when a particle collided with any other object or particle meant we could produce a wide array of unique effects from a few simple rules. 

The nerdist said:

>Props must be given to Team Pottermore for devising a bit of Flash sorcery that genuinely takes getting the hang of to master
October 2010.

The game acquired something of a cult status and spawned numerous how-to videos on YouTube such as this one:

{% include youtubeplayer.html id=page.video %}
