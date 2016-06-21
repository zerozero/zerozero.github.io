---
layout: post
title: Meadowlark
category: Identity
description: Low-poly Unity game (devlog part 1)
---

<div class="img_row">
	<img class="col three" src="{{ site.baseurl }}/images/uploads/meadowlark_diorama1.jpg" alt="" title="Diorama"/>
</div>
<div class="col three caption">
	Low Poly Diorama.
</div>

Objective: Getting up to speed with **Unity 5** and **Blender 2.73 – **I’ll try to make a simple game in a low-poly style which will hopefully be manageable for me to undertake on my own and not take too long.

I’m not sure exactly what I am going to make but the basic idea is to create a game world based on a wild meadow scene viewed from a 3rd person perspective of a dragonfly. It’s going to be:

*   Low poly style
*   Both mobile and desktop/web versions (also Google Cardboard?)
*   Beautiful
*   A demo piece I can use to promote my skills
*   Documented at every stage

**Game Overview**

Meadowlark is a 3rd person survival game featuring a dragonfly as the hero. The aim of the game is to survive for 1 day in the meadow. In the game the player must navigate the game arena avoiding predators and collecting enough nectar and water to stay alive and also to have enough energy to make it to the next food source.

The game is won when night falls and the player has survived.

The meadow is populated by various other bugs and insects that are either predators or competitors for scarce food sources. The player must avoid predators using flying skills to outmanoeuvre them. The player must reach food sources before competitors in order to drink the life-giving nectar before it is depleted. If an enemy attacks the player’s health will diminish ultimately leading to death when health reaches zero.

**Game in Depth**

_The Player_

The player character is a dragonfly. The dragonfly is highly a manoeuvrable insect capable of spurts of great speed. However flying fast comes at a cost – energy is depleted very quickly and the insect must stop to drink nectar and water frequently. The dragonfly has a finite amount of health which depletes during flight and when under attack.

<div class="img_row">
	<img class="col two left" src="{{ site.baseurl }}/images/uploads/rigged-dragonfly.png" alt="" title="Rigged Dragonfly"/>
</div>
<div class="col two left caption">
	Rigged Dragonfly.
</div>
<div class="img_row">
	<img class="col two right" src="{{ site.baseurl }}/images/uploads/rigged-dragonfly2.png" alt="" title="Rigged Dragonfly x 4"/>
</div>
<div class="col two right caption">
	Rigged Dragonfly x 4.
</div>
<div class="img_row">
	<img class="col two left" src="{{ site.baseurl }}/images/uploads/dragonfly5.jpg" alt="" title="Cycles Render"/>
</div>
<div class="col two left caption">
	Cycles Render
</div>
<br/>


_Enemies_

There are a number of different enemy types:

_Spider_ – lays traps for the hero in unexpected places, when caught in a web the player can not escape and will die quickly

_Wasp_ – an aerial acrobat, the wasp will chase the hero and attack with it’s poisonous sting.

_Fish_ – if there is no other source of water available the hero can drink directly from the pond, however the fish lurking beneath the surface can strike quickly and with devastating effect.

_Power-ups_

There are 2 types of power up, nectar from flowers and water from various places.

_Nectar -_ keeps the hero alive and gives energy for flying. Competitor insects also seek out and consume nectar, once a flower has been depleted of nectar it takes a while to replenish.

_Water_ – in addition to nectar the hero needs to keep hydrated. Water droplets form in many places in the meadow – on leaves, in flower heads, on blades of grass. They are especially prevalent early in the morning but evaporate as the day goes on. In an emergency the hero can drink directly from the pond.

_Levels_

There is only one level at the moment.

<div class="img_row">
	<img class="col three" src="{{ site.baseurl }}/images/uploads/meadowlark_diorama2.jpg" alt="" title="Diorama"/>
</div>
<div class="col three caption">
	Low Poly Diorama.
</div>

_Gui_

Consider using the colour of the dragonfly tail to represent health status in place of a HUD.

Main menu. Access hi-scores, re-load paused games etc.

