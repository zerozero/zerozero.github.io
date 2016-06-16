---
layout: page
title: Science Museum - Tell
date: 2014-05-01T16:44:29+00:00
project: true
categories:
  - code
  - projects
  - technology
tags:
  - projects
tech:
 - angular.js
 - express.js
 - twitter real-time API
 - instagram real-time API
description: Visitor generated content system R&D
---

It's 2014 and the watchword in the museum community is Visitor Generated Content (VGC). The walls of the museum are breaking down and the role of the curator handing down information to a passive public is being challenged. Now it is the audience who are invited to bring their own interpretation to the artefacts on display, and encouraged to share their thoughts in new and interesting ways. Working with the New Media team at the Science Museum I am tasked to produce a report on the current state of VGC in the museum community and to come up with innovative ways of allowing visitors to the museum to interact with the museum's collections and with each other.

The system I create is designed to be flexible enough to adapt to any exhibition and scale from the smallest to the largest potential audience. One of the big problems with VGC is the potential for inappropriate material to be posted and the difficulty of moderating visitor input. To mitigate this the system is designed with quarantining functionality provided by machine learning algorithms and natural language processing techniques.

The smartphone and social networks are ubiquitous on the museum floor so I create a system to utilise these technologies. Visitors are invited to take photos and post to instagram or twitter using predefined hashtags which are monitored over the real-time APIs by a server. This server performs some basic moderation tasks using machine learning to quarantine suspect content. The rest of the content is allowed through instantly and appears on monitors in the gallery. Quarantined content is sent to human moderators who perform a secondary check and have the final say on whether the material gets shown.

![VGC](/images/real-time1.png)

[Here]({{ site.baseurl }}/images/Tell.pages.pdf) is the report.
