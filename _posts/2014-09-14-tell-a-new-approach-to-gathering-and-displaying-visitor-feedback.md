---
id: 346
title: 'Tell &#8211; A new approach to gathering and displaying visitor feedback'
date: 2014-09-14T15:33:59+00:00
author: zerozero
layout: post
categories:
  - code
  - projects
  - technology
---
Commissioned by the Science Museum in 2013 this R&D project&#8217;s aim was to find alternatives to the existing in-gallery feedback systems that were difficult to use, expensive to maintain and had resulted in tens of thousands of user generated comments remaining unseen and unread on the museum&#8217;s servers.

**The desire of the museum**

Although unstated I felt there was a definite desire on the part of the museum to create a system that took advantage of the ubiquity of connected devices among museum attendees. This is hardly surprising since previous research had found that large numbers of museum-goers carried mobile phones and tablets with them and that one of the primary activities visitors engaged in was taking photographs.

Museums and public institutions are under a huge amount of pressure In a political environment of restricted budgets to justify their existence and one of the primary ways a centre of learning can do so is to prove that the public actually do learn something from them. In order to illustrate this metric commenting systems producing user-generated content (UGC) are a popular solution. Unfortunately such systems have previously relied on expensive bespoke systems installed in the gallery. In this context keyboards and screens must be hardened and robust enough to withstand everything from baby dribble and ice cream to being sat on and beaten up. This comes with a dual price: both in terms of actual cost of production and in terms of the final product being difficult to use (you&#8217;ve probably experienced those metal keyboards and touchscreens that require arms like Arnie to input a character). So it makes absolute sense to try to move the costs away from the institution and over to the visitor who has a familiar personal input device in their pocket already.

**How the research was conducted**

Despite the result being something of a foregone conclusion I cast my net far and wide looking  at everything from existing UGC systems and active visualisation mechanisms to researching what kinds of prompts can be used to encourage people to participate in a meaningful way; how to filter out bad input; what can be done in terms of curation and moderation; principles of interactive design and how to scaffold the experience. I looked at technologies as diverse as Local positioning systems and handwriting recognition, social bookmarking and augmented reality. The entire report is in the public domain and is available here:

[Tell.pages](http://162.13.3.34:8079/labs/wp-content/uploads/2015/05/Tell.pages.pdf)

**The proposed solution**

The final proposal was for a system that used the audience&#8217;s device and social networking sites Twitter and Instagram. Visitors would be encouraged to take photos around the gallery and post them to a specific hashtag using the standard familiar interface, adding comments if they wished. Prompts would be displayed prominently around the gallery and publicity would be given to special &#8216;Hashtag Days&#8217; during which particular ideas and concepts would be explored and promoted.

Once posted our server would be notified via the Twitter/Instagram real-time API, it was proposed the content would be filtered automatically with a basic profanity filter and anything dubious would be quarantined for human moderation. (A nudity filter was also proposed for which I created a prototype using Patrick Weid&#8217;s nude.js). After passing through the real-time system approved posts would be displayed in-gallery on large projection screens giving the immediate feedback that is the hallmark of a good UGC system.

[<img class="aligncenter size-full wp-image-353" src="http://162.13.3.34:8079/labs/wp-content/uploads/2015/05/real-time1.png" alt="real-time" width="820" height="615" />](http://162.13.3.34:8079/labs/wp-content/uploads/2015/05/real-time1.png)

**The prototype**

I built a working prototype of the system using an Express.js backend and Angular.js front end and CMS which was used during an event at the Dana Centre in May 2014 discussing the issues surrounding drones. During the event participants were able to take photos of their sketched ideas during break-out sessions and post them via Instagram. The images were then displayed in near real-time on a pair of projection screens at the venue. Here are some images from the event:

[<img class="aligncenter size-full wp-image-355" src="http://162.13.3.34:8079/labs/wp-content/uploads/2015/05/IMG_20140521_200415.jpg" alt="IMG_20140521_200415" width="820" height="820" />](http://162.13.3.34:8079/labs/wp-content/uploads/2015/05/IMG_20140521_200415.jpg) [<img class="aligncenter size-full wp-image-356" src="http://162.13.3.34:8079/labs/wp-content/uploads/2015/05/IMG_20140521_200517.jpg" alt="IMG_20140521_200517" width="820" height="820" />](http://162.13.3.34:8079/labs/wp-content/uploads/2015/05/IMG_20140521_200517.jpg) [<img class="aligncenter size-full wp-image-357" src="http://162.13.3.34:8079/labs/wp-content/uploads/2015/05/IMG_20140521_200528.jpg" alt="IMG_20140521_200528" width="820" height="1093" />](http://162.13.3.34:8079/labs/wp-content/uploads/2015/05/IMG_20140521_200528.jpg) [<img class="aligncenter size-full wp-image-358" src="http://162.13.3.34:8079/labs/wp-content/uploads/2015/05/IMG_20140521_200551.jpg" alt="IMG_20140521_200551" width="820" height="820" />](http://162.13.3.34:8079/labs/wp-content/uploads/2015/05/IMG_20140521_200551.jpg) [<img class="aligncenter size-full wp-image-359" src="http://162.13.3.34:8079/labs/wp-content/uploads/2015/05/Screen-Shot-2015-05-11-at-23.05.46-copy.png" alt="Screen Shot 2015-05-11 at 23.05.46 copy" width="820" height="616" />](http://162.13.3.34:8079/labs/wp-content/uploads/2015/05/Screen-Shot-2015-05-11-at-23.05.46-copy.png)

[<img class="aligncenter size-full wp-image-342" src="http://162.13.3.34:8079/labs/wp-content/uploads/2015/05/drones-ui-crop.png" alt="drones-ui-crop" width="2481" height="2153" />](http://162.13.3.34:8079/labs/wp-content/uploads/2015/05/drones-ui-crop.png)

<div class="gk-social-buttons">
  <span class="gk-social-label">Share:</span> <a class="gk-social-twitter" href="http://twitter.com/share?text=Tell+%26%238211%3B+A+new+approach+to+gathering+and+displaying+visitor+feedback&url=http%3A%2F%2F162.13.3.34%3A8079%2Flabs%2F%3Fp%3D346"
	            onclick="window.open(this.href, 'twitter-share', 'width=550,height=235');return false;"> <span class="social__icon--hidden">Twitter</span> </a> <a class="gk-social-fb" href="https://www.facebook.com/sharer/sharer.php?u=http%3A%2F%2F162.13.3.34%3A8079%2Flabs%2F%3Fp%3D346"
			     onclick="window.open(this.href, 'facebook-share','width=580,height=296');return false;"> <span class="social-icon-hidden">Facebook</span> </a> <a class="gk-social-gplus" href="https://plus.google.com/share?url=http%3A%2F%2F162.13.3.34%3A8079%2Flabs%2F%3Fp%3D346"
	           onclick="window.open(this.href, 'google-plus-share', 'width=490,height=530');return false;"> <span class="social__icon--hidden">Google+</span> </a>
</div>