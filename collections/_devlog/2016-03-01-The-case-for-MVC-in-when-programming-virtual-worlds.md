<<<<<<< HEAD:collections/_drafts/The-case-for-MVC-in-when-programming-virtual-worlds.md
---
layout: post
title: The case for DI in VR
category: Code
date: 2016-03-01T16:44:29+00:00
description: How to organise code in Unity
---
<div class="img_row">
	<img class="col three" src="{{ site.baseurl }}/images/IMG_0856.jpg" alt="" title="Sequence Diagram"/>
</div>
<div class="col three caption">
	Sequence Diagram.
</div>

**The Requirements**

I recently had the pleasure of working on a VR project with a very talented team at [Masters of Pie](http://www.mastersofpie.com/) here in London. The application is a simulator to train nurses in administering a new cancer treatment and runs on the Vive platform. 

The requirements for the simulation centre around a virtual patient who is connected to a titration machine that dispenses the drug in dosages controlled by the participant. Also connected to the patient is an "Obs" machine that displays certain aspects of the patient's stats: blood pressure, respiratory rate, blood-oxygen levels, temperature etc. Arranged on a shelf near the patient are a selection of medications used for post- and pre-med administration and to treat infusion related reactions. These must be administered at the correct time to the correct part of the body (placed in the hand, oxygen mask over the mouth or onto the canular attached to the left arm).

When the nurse administers an amount of the drug a facilitator selects one of a number of possible adverse reactions that the nurse could expect to see in a real-life situation. The virtual patient reacts realistically - coughing, suffering a bronchial spasm, sneezing etc - and the nurse must select the correct medications to treat the symptoms, wait for the reaction to subside and then continue the treatment as appropriate.

All-in-all the requirements are complex enough to mean that some serious thought has to be put into the code architecture to support the many resulting options and pathways. 

 - As we know requirements change and morph so the system has to be flexible enough to accomodate that change. 
 - We must also consider that VR can place a heavy burden on the processor so we need to beware of every unnecessary cycle. 
 - Additionally we must consider longevity - if successful this project could run for 3 years or more and it is likely that I will not be available for any updates that will be needed.
 - Finally I insist that there is an element of TDD/BDD built in to the codebase - I know from experience that a project of this complexity benefits from comprehensive tests and that the life of a programmer is made immeasurably less stressful with the knowledge that the code is backed by tests.  

These are some of the thoughts that go through my mind as I begin designing the code..

**Unity & The Unity Games Engine**

It is important to realise that Unity's built-in programming paradigm is a form of entity system and is geared towards 'typical' game play as it has developed since the early days of computer gaming. Traditionally, game code is based on the concept of the game loop - that is the system keeps going regardless of user input, every frame velocities are calculated; backgrounds and foregrounds are repainted; bullets keep flying; aliens keep approaching etc. This is in contrast to applications such as word processors - for them the world stops when the user does nothing (unless it's made by Microsoft in which case an animated paperclip pops up and asks you "Why have you stopped typing? Are you having a stroke? Can I help?" right before showing the blue screen of death and wiping your hard drive).

In the beginning this game loop was usually written to loop over an array of objects updating each one as neccessary and might look something like this:

```csharp
function update( time:Number ):void
{
  game.update( time );
  spaceship.updateInputs( time );
  for each( var flyingSaucer:FlyingSaucer in flyingSaucers )
  {
    flyingSaucer.updateAI( time );
  }
  spaceship.update( time );
  for each( var flyingSaucer:FlyingSaucer in flyingSaucers )
  {
    flyingSaucer.update( time );
  }
  for each( var asteroid:Asteroid in asteroids )
  {
    asteroid.update( time );
  }
  for each( var bullet:Bullet in bullets )
  {
    bullet.update( time );
  }
  collisionManager.update( time );
  spaceship.render();
  for each( var flyingSaucer:FlyingSaucer in flyingSaucers )
  {
    flyingSaucer.render();
  }
  for each( var asteroid:Asteroid in asteroids )
  {
    asteroid.render();
  }
  for each( var bullet:Bullet in bullets )
  {
    bullet.render();
  }
}
```

And for simple (really simple) games this works ok. Attention has to paid to the order of execution so that dependencies are resolved correctly and everything is updated before rendering. But the earliest (and some of the best) games were written like this - a single function, thousands of lines long that updates every 1/30th or 1/60th of a second. In some ways the games community is still stuck in this indie mindset: it doesn't matter how ugly the code is; we don't care about things like being paid, code quality or longevity - let's just get it made and get it out there. And there is a romantic integrity in that approach - it speaks to us of hackers and anarchy and all the things that were lost when big commerce got involved in games.

But it soon became clear that, as games became more complex, a more sophisticated approach was necessary - and so Entity Component Systems (ECS) were developed. While applications software became increasingly about Object-Orientation games diverged into ECSs which break the OOP paradigm in many ways - and for good reason. It turns out that OOP is really bad at modelling the kinds of relationship that are important in making games - for a great overview of how ECSs work check out this article: [What is an Entity System?](http://www.richardlord.net/blog/what-is-an-entity-framework).

The ECS is a great architecture for making games and is used in one form or another in most game engines, Unity included. At the core of an ECS is the game engine that knows about all the components and systems in the game and keeps track of them when they are added or destroyed. The entity system originates from a desire to simplify the game loop and optimise the architecture, it's focus is on getting the entire system updated once every 1/60th of a second (or 1/90th+ for VR).

**Manager Manager!**

One of the most powerful things about entity systems - especially the Unity implementation - is that you can easily add complex behaviours to objects and have them update every frame without any further work. But with great power - as the saying goes - comes great responsibility. Pretty soon, when creating your game you realise that actually you don't want your NPC to be attacking all the time, you need to have some control over when attacks are launched in response to player actions.. you need to _manage_ your gameObjects. More often than not the neophyte Unity developer will reach for the Singleton (anti-)pattern in order to create a globally accessible object that will manage all instances of a particular type of entity. And in the short-term the singleton pattern is relatively benign but our developer will realise that there is a technical debt accruing that will have to be paid down one day - and this particular loan shark ain't going to take kindly to pleas of poverty.

So what's wrong with the singleton? First and foremost it's a _global variable_. So? _Globals are bad mkay?_ Why? _let me list the ways.._
1. They make it harder to reason about code
2. They encourage coupling
3. They aren't concurrency friendly
(plus)
4. Lazy instantiation takes control away from the developer

Often these singleton managers get completely out of hand with every class of entity in the game acquiring it's own manager: PlayerManager, EnemyManager, RewardManager, SoundManager etc etc.. sometimes even managers acquire managers _ManagerManager_!! All globally accessible, all untestable, all kinds of pain.

**My Preferred Approach**

While there are a number of alternatives to the singleton pattern for this project I decided to use MVC(S) as implemented by Strange IoC.

- MVC(S)
- Strange IoC and Dependency Injection
- Testability
- MVC + Entities = Meta Pattern
- FSM
- Scalable experience - not possible on the game loop
- multiple actors - all interactive
- still composable
- Real vs Virtual worlds

**__Update__**

While I enjoyed StrangeIOC as an easy path into MVC in Unity having worked extensively with Robotlegs for Flash in the end I found it to be a rather heavyweight solution and a bit proscriptive. Recently I have favoured [Zenject](https://github.com/modesttree/Zenject) which is a much lighter-weight framework and doesn't insist on the MVC meta-pattern. It also has great documentation and is being actively developed. I have used it on a number of projects now and I really think for mid- to large- scale projects it is a great tool.

I always wonder whether this type of framework has a place in games development - I hardly ever get involved in making games per se - it is never mentioned in any of the Unity docs or blog posts which makes me wonder what people are using as an alternative. I have seen some examples that use Scriptable Objects to achieve similar results enabling developers to access 'global' variables across a project without resorting to the singleton pattern. Maybe one day we'll have a best-practice guide to solving this problem.

**References**
- [Why not to use singleton pattern](http://gameprogrammingpatterns.com/singleton.html) 
- [What is an Entity System?](http://www.richardlord.net/blog/what-is-an-entity-framework) 
- [Why use an entity system?](http://www.richardlord.net/blog/why-use-an-entity-framework)
- [Robotlegs](https://github.com/robotlegs/robotlegs-framework/blob/master/readme.md) 
- [StrangeIoC](http://strangeioc.github.io/strangeioc/TheBigStrangeHowTo.html) 
- [Game Programming Patterns](http://gameprogrammingpatterns.com/) 


=======
---
layout: post
title: The case for DI in VR
category: Code
date: 2016-03-01T16:44:29+00:00
description: How to organise code in Unity
---
<div class="img_row">
	<img class="col three" src="{{ site.baseurl }}/images/IMG_0856.jpg" alt="" title="Sequence Diagram"/>
</div>
<div class="col three caption">
	Sequence Diagram.
</div>

**The Requirements**

I recently got involved in a great VR project with the team at [Masters of Pie](http://www.mastersofpie.com/) here in London. The application is a simulator to train nurses in administering a new cancer treatment and runs on the Vive platform. 

The requirements for the simulation centre around a virtual patient who is connected to a titration machine that dispenses the drug in dosages controlled by the participant. Also connected to the patient is an "Obs" machine that displays certain aspects of the patient's stats: blood pressure, respiratory rate, blood-oxygen levels, temperature etc. Arranged on a shelf near the patient are a selection of medications used for post- and pre-med administration and to treat infusion related reactions. These must be administered at the correct time to the correct part of the body (placed in the hand, oxygen mask over the mouth or onto the canular attached to the left arm).

When the nurse administers an amount of the drug a facilitator selects one of a number of possible adverse reactions that the nurse could expect to see in a real-life situation. The virtual patient reacts realistically - coughing, suffering a bronchial spasm, sneezing etc - and the nurse must select the correct medications to treat the symptoms, wait for the reaction to subside and then continue the treatment as appropriate.

All-in-all the requirements are complex enough to mean that some serious thought has to be put into the code architecture to support the many resulting options and pathways. 

 - As we know requirements change and morph so the system has to be flexible enough to accomodate that change. 
 - We must also consider that VR can place a heavy burden on the processor so we need to beware of every unnecessary cycle. 
 - Additionally we must consider longevity - if successful this project could run for 3 years or more and it is likely that I will not be available for any updates that will be needed.
 - Finally I insist that there is an element of TDD/BDD built in to the codebase - I know from experience that a project of this complexity benefits from comprehensive tests and that the life of a programmer is made immeasurably less stressful with the knowledge that the code is backed by tests.  

These are some of the thoughts that go through my mind as I begin designing the code..

**Unity & The Unity Games Engine**

It is important to realise that Unity's built-in programming paradigm is a form of entity system and is geared towards 'typical' game play as it has developed since the early days of computer gaming. Traditionally, game code is based on the concept of the game loop - that is the system keeps going regardless of user input, every frame velocities are calculated; backgrounds and foregrounds are repainted; bullets keep flying; aliens keep approaching etc. This is in contrast to applications such as word processors - for them the world stops when the user does nothing (unless it's made by Microsoft in which case an animated paperclip pops up and asks you "Why have you stopped typing? Are you having a stroke? Can I help?" right before showing the blue screen of death and wiping your hard drive).

In the beginning this game loop was usually written to loop over an array of objects updating each one as neccessary and might look something like this:

```csharp
function update( time:Number ):void
{
  game.update( time );
  spaceship.updateInputs( time );
  for each( var flyingSaucer:FlyingSaucer in flyingSaucers )
  {
    flyingSaucer.updateAI( time );
  }
  spaceship.update( time );
  for each( var flyingSaucer:FlyingSaucer in flyingSaucers )
  {
    flyingSaucer.update( time );
  }
  for each( var asteroid:Asteroid in asteroids )
  {
    asteroid.update( time );
  }
  for each( var bullet:Bullet in bullets )
  {
    bullet.update( time );
  }
  collisionManager.update( time );
  spaceship.render();
  for each( var flyingSaucer:FlyingSaucer in flyingSaucers )
  {
    flyingSaucer.render();
  }
  for each( var asteroid:Asteroid in asteroids )
  {
    asteroid.render();
  }
  for each( var bullet:Bullet in bullets )
  {
    bullet.render();
  }
}
```

And for simple (really simple) games this works ok. Attention has to paid to the order of execution so that dependencies are resolved correctly and everything is updated before rendering. But the earliest (and some of the best) games were written like this - a single function, thousands of lines long that updates every 1/30th or 1/60th of a second. In some ways the games community is still stuck in this indie mindset: it doesn't matter how ugly the code is; we don't care about things like being paid, code quality or longevity - let's just get it made and get it out there. And there is a romantic integrity in that approach - it speaks to us of hackers and anarchy and all the things that were lost when big commerce got involved in games.

But it soon became clear that, as games became more complex, a more sophisticated approach was necessary - and so Entity Component Systems (ECS) were developed. While applications software became increasingly about Object-Orientation games diverged into ECSs which break the OOP paradigm in many ways - and for good reason. It turns out that OOP is really bad at modelling the kinds of relationship that are important in making games - for a great overview of how ECSs work check out this article: [What is an Entity System?](http://www.richardlord.net/blog/what-is-an-entity-framework).

The ECS is a great architecture for making games and is used in one form or another in most game engines, Unity included. At the core of an ECS is the game engine that knows about all the components and systems in the game and keeps track of them when they are added or destroyed. The entity system originates from a desire to simplify the game loop and optimise the architecture, it's focus is on getting the entire system updated once every 1/60th of a second (or 1/90th+ for VR).

**Manager Manager!**

One of the most powerful things about entity systems - especially the Unity implementation - is that you can easily add complex behaviours to objects and have them update every frame without any further work. But with great power - as the saying goes - comes great responsibility. Pretty soon, when creating your game you realise that actually you don't want your NPC to be attacking all the time, you need to have some control over when attacks are launched in response to player actions.. you need to _manage_ your gameObjects. More often than not the neophyte Unity developer will reach for the Singleton (anti-)pattern in order to create a globally accessible object that will manage all instances of a particular type of entity. And in the short-term the singleton pattern is relatively benign but our developer will realise that there is a technical debt accruing that will have to be paid down one day - and this particular loan shark ain't going to take kindly to pleas of poverty.

So what's wrong with the singleton? First and foremost it's a _global variable_. So? _Globals are bad mkay?_ Why? _let me list the ways.._
1. They make it harder to reason about code
2. They encourage coupling
3. They aren't concurrency friendly
(plus)
4. Lazy instantiation takes control away from the developer

Often these singleton managers get completely out of hand with every class of entity in the game acquiring it's own manager: PlayerManager, EnemyManager, RewardManager, SoundManager etc etc.. sometimes even managers acquire managers _ManagerManager_!! All globally accessible, all untestable, all kinds of pain.

**My Preferred Approach**

While there are a number of alternatives to the singleton pattern for this project I decided to use MVC(S) as implemented by Strange IoC.

- MVC(S)
- Strange IoC and Dependency Injection
- Testability
- MVC + Entities = Meta Pattern
- FSM
- Scalable experience - not possible on the game loop
- multiple actors - all interactive
- still composable
- Real vs Virtual worlds

**__Update__**

While I enjoyed StrangeIOC as an easy path into MVC in Unity having worked extensively with Robotlegs for Flash in the end I found it to be a rather heavyweight solution and a bit proscriptive. Recently I have favoured [Zenject](https://github.com/modesttree/Zenject) which is a much lighter-weight framework and doesn't insist on the MVC meta-pattern. It also has great documentation and is being actively developed. I have used it on a number of projects now and I really think for mid- to large- scale projects it is a great tool.

I always wonder whether this type of framework has a place in games development - I hardly ever get involved in making games per se - it is never mentioned in any of the Unity docs or blog posts which makes me wonder what people are using as an alternative. I have seen some examples that use Scriptable Objects to achieve similar results enabling developers to access 'global' variables across a project without resorting to the singleton pattern. Maybe one day we'll have a best-practice guide to solving this problem.

**References**
- [Why not to use singleton pattern](http://gameprogrammingpatterns.com/singleton.html) 
- [What is an Entity System?](http://www.richardlord.net/blog/what-is-an-entity-framework) 
- [Why use an entity system?](http://www.richardlord.net/blog/why-use-an-entity-framework)
- [Robotlegs](https://github.com/robotlegs/robotlegs-framework/blob/master/readme.md) 
- [StrangeIoC](http://strangeioc.github.io/strangeioc/TheBigStrangeHowTo.html) 
- [Game Programming Patterns](http://gameprogrammingpatterns.com/) 


>>>>>>> 36defa7f54419f1d0e63d73f19bd207692f0123a:collections/_devlog/2016-03-01-The-case-for-MVC-in-when-programming-virtual-worlds.md
