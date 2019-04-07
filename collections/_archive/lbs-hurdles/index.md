---
layout: page
title: Hurdles Game
date: 2015-11-01T16:44:29+00:00
project: true
categories:
  - code
  - projects
  - technology
tags:
  - projects
tech:
 - angular.js
 - ash.js
 - php
description: Psychological tests delivered in a game using Angular.js and an entity component system
---

<div class="img_row">
	<img class="col three" src="{{ site.baseurl }}/images/heroes/hurdles.jpg" alt="" title="hurdles"/>
</div>
<br/>

A post-graduate researcher at the London Business School commissioned me to make this game as part of her final project. The game was designed to collect data on psychological responses to competitive challenges in a seemingly playful environment.

In the game participants were led to believe that they were competing against human opponents but, in fact, the other runners were AI entities programmed to respond to the player's actions in a variety of ways.

Data was collected through a RESTful interface with a management interface and downloadable data available to the researcher. The game also featured a Quantics survey that was integrated into the game interface.

I used a javascript port of the [Ash entity component system](https://github.com/brejep/ash-js) to create the game mechanics. This allowed me to add and remove components from the NPC entities easily to provide modified behaviours in response to player actions. By combining this with an angular.js wrapper I was able to leverage the data layer in the MVC implementation to read and write to the back-end and inject values into the game as required.