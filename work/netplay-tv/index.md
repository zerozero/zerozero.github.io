---
layout: page
title: Netplay.tv
date: 2016-01-08T16:44:29+00:00
project: true
categories:
  - code
  - projects
  - technology
tags:
  - projects
tech:
 - angular.js
 - typescript
 - gulp
 - jasmine
 - TDD/BDD
 - Bamboo CI Server
 - RTMP streaming
 - node.js
description: Full featured Angular.js roulette gaming site
---

<div class="img_row">
	<img class="col three" src="{{ site.baseurl }}/images/heroes/netplaygrey.jpg" alt="" title="netplay"/>
</div>
<br/>

Netplay.tv is a big player in the online gaming world. Their primary USP is in delivering real-time streaming video of physical roulette machines. Initially I was contracted to work on their Flash Media Server streaming code which was a bit wonky. It soon emerged that their entire client-side offering needed updating since it still relied on Flash content delivered to the browser.

I began a period of analysis and design by researching HTML5 and javascript frameworks in order to recommend a technology stack that would be robust, tested, extensible and future-proof. It was also important that the chosen technologies would be reasonably easy for a team used to working with jquery to pick up. After considering older frameworks such as Backbone and Ember along with newer ones such as React and Angular we decided that Angular would be the best fit.

I scaffolded the project in Angular 1.x with gulp, typescript, jasmine for testing, an Express test server, and a Bamboo continuous integration server. In designing the app I was concerned with following best practices recognising that Angular was in a state of change with the impending release of version 2 and it's emphasis on a component architecture.

Over 4 months of development in a team of 6 we successfully created a working implementation of the front-end with hundreds of tests and greater-than 80% test-coverage. The fully responsive client could be themed to align with the various products delivered by Netplay and was awaiting the completion of the back-end when my contract ended.  This project has yet to go live.
