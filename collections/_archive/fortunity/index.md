---
layout: page
title: Fortunity
date: 2009-06-01T16:44:29+00:00
project: true
categories:
  - code
  - projects
  - technology
tags:
  - projects
tech:
 - Flash
 - PureMVC
 - as3isoLib
 - Blender 3D
description: Financial management game for school-kids

---

<div class="img_row">
	<img class="col two left" src="{{ site.baseurl }}/images/heroes/fortunity_large3.jpg" alt="" title="fortunity"/>
	<img class="col one right" src="{{ site.baseurl }}/images/heroes/fortunity-characters.jpg" alt="" title="characters"/>
</div>
<div class="col two left caption">
	Fortunity interface
</div>
<div class="col one right caption">
	Fortunity characters
</div>
<br/>

Fortunity was a game that aimed to help school-kids learn how to deal with financial challenges in a fun and informative way. After selecting one of eight avatars, and working in a group of up to four players on each machine, players take turns to roll virtual dice and move their character around the board. Just as in traditional board games each square may have an activity, item to purchase, bonus or penalty associated with it. 

Each player is given a small amount of money at the start of the game and their spending decisions as they progress are reflected in their bank balance. On advanced levels the player acquires more accounts - savings and credit cards - and must juggle their money in the most appropriate way to stay financially solvent. 

I built an isometric level editor that allowed the designers to work iteratively through the design of tiles and levels without having to interrupt the development team. 

The game is architected on a PureMVC multicore framework which allowed us to load mini-games as modules within the same application domain and utilise a single event bus for the entire application. 

The game was recognised at the 2010 IVCA awards where it was Highly Commended.

In addition to leading the flash development effort I also created a range of avatar characters in Blender 3D.