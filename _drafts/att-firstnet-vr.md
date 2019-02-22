---
layout: post
title: AT&T FirstNet VR
date: 2018-03-01T16:44:29+00:00
project: true
categories:
  - code
  - projects
  - technology
  - VR
tags:
  - projects
description: Experiential introduction to AT&T First Responder Network 
tech:
 - Oculus Rift
 - C#
 - Unity
 - Zenject
 - CityEngine
 - Ambisonics
video: 201068644
---
<div class="img_row">
	<img class="col three" src="{{ site.baseurl }}/images/heroes/firstnet.jpg" alt="" title="FirstNet City"/>
</div>
<div class="col three caption">
	FirstNet City.
</div>

Experience marketing agency George P Johnson hired me to develop a VR app for AT&T/FirstNet - "the first ever nationwide public safety broadcast network for American First Responders".

The requirements were to demonstrate how various technologies using the FirstNet network could be deployed in the field to assist in public safety situations.

Working with a team comprised of graphic and UX designers and account managers in planning we determined that the best way to convey the power of the system would be to have the user participate in scenarios that mimicked real world situations. We realised that the key to understanding how the various systems could assist first responders in the real world was to be active in using the system in the virtual world. This approach threw up a problem, however, in that the real world is complex - too complex to be modelled in VR with a short timescale. So we decided to create a world rendered in a low-poly 'papercraft' style ánd where the users attention was drawn to the important stuff by rendering that stuff in a higher poly, full colour style.

In prototyping we discovered that teleporting the user around the world at different scales was perfectly acceptable and not as jarring an experience as we had previously imagined so we conceived of a user flow that looked like this:

<div class="col two img_row_tall">
	<img class="col" src="{{ site.baseurl }}/images/attfirstnet/scenario-flow.png" alt="" title="Scenario Flow"/>
</div>
<div class="col two caption">
	Scenario Flow
</div>

For each scenario the user is teleported to a number of establishing viewpoints that allow us to run timeline animations combined with voiceover that describes the situation and the task to be completed.  The participant is then teleported to ground level where they will be presented with a simple interaction to activate the FirstNet technology. Recognising that not everyone is familiar with interaction in VR (and to facilitate accessibility requirements) a timeout period is activated after which the task will auto-complete.

Finally a 'resolution' scene (timeline animation again) is played out and the user is returned to the menu to choose another scenario. 

Initially the requirement was for just three interactive scenarios but, inevitably, this grew to six, then nine as the planning process progressed. Of course we plan for change when developing any application so this increase in scenarios was not met with undue concern. 

My go-to organisational tool at this point was [Playmaker by Huton Games](http://www.hutonggames.com/) which enables drag and drop functionality to create simple Finite State Machines. It's not perfect and it is *very* easy to break encapsulation using this tool which can lead to painful experiences when trying to reason about the project after the fact. However, if treated with care it is great for quickly getting projects of medium complexity up and running in short order. Here is the state machine that emerged from the planning stage and carried us throughout the project with very few changes:

<div class="col three img_row">
	<img class="col" src="{{ site.baseurl }}/images/attfirstnet/gamestatefsm.PNG" alt="" title="Game State FSM"/>
</div>
<div class="col three caption">
	Game State Machine
</div>
 
While Playmaker does not support nested state machines as such it was quite simple to create state in sub-components that communicated with the main FSM via messaging coordinated by another favourite tool - [Zenject Dependency Injection framework](https://github.com/svermeulen/Zenject/tree/master/Documentation). In fact this framework alongside a set of rules I specified governing Playmaker provided the tools necessary to avoid the issues with breaking encapsulation I mentioned earlier. House rules stated that:
- No component shall ever call a method in it's parent game object's components.
- Unity events shall not be used to send messages to components elsewhere in the hierarchy (use Zenject Signals instead).
- A Playmaker State Machine may only call a method on components attached to the game object on which it resides.
- Messaging 'up' the hierarchy shall be achieved using Zenject Signals.  
- A component *may* call methods on components attached to it's children (via GetComponentsInChildren for example).
- Do not store game objects as variables, prefer dynamic calls to GetComponent(s)InChildren, caching these at 'awake' time if desired.
- Use auto-naming of actions in the state machine.



In FirstNet VR the user becomes a first responder in FirstNet City, a low-poly rendition of a cityscape where various disasters and emergencies unfold. Using the FirstNet platform the user must resolve each of the incidents and make FirstNet City safe.

FirstNet VR was created for the Oculus Rift using Unity. Architecturally I use Zenject for dependency injection and Playmaker for a simple state machine implementation (making sure to limit the scope of any FSM to it's own children to prevent breaking encapsulation).

In FirstNet VR the user becomes a first responder in FirstNet City, a low-poly rendition of a cityscape where various disasters and emergencies unfold. Using the FirstNet platform the user must resolve each of the incidents and make FirstNet City safe.

FirstNet VR was created for the Oculus Rift using Unity. Architecturally I use Zenject for dependency injection and Playmaker for a simple state machine implementation (making sure to limit the scope of any FSM to it's own children to prevent breaking encapsulation).

In FirstNet VR the user becomes a first responder in FirstNet City, a low-poly rendition of a cityscape where various disasters and emergencies unfold. Using the FirstNet platform the user must resolve each of the incidents and make FirstNet City safe.

FirstNet VR was created for the Oculus Rift using Unity. Architecturally I use Zenject for dependency injection and Playmaker for a simple state machine implementation (making sure to limit the scope of any FSM to it's own children to prevent breaking encapsulation).
 
Zenject DI
CityEngine
Ambisonics
Lead Developer
5 person team
Advise on best praxctice
prototype
optimise
timeline animation
involved in full project lifecycle from concept to delivery
align with client brand
