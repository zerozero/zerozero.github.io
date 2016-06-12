---
layout: page
title: Wow Face!
date: 2016-05-16T16:44:29+00:00
project: true
video: 167115527
description: Control audio output with facial expressions.
tech:
 - OpenFrameworks
 - OpenCV
 - Wekinator
 - Max/MSP
---

{% include vimeoplayer.html id=page.video %}

<br/>
Using machine learning algorithms this project enables the user to control audio output through facial expressions.

A webcam feed inputs picture information to an implementation of [OpenCV](http://opencv.org/) in [OpenFrameworks](http://openframeworks.cc/). Custom C++ code extracts features from the OpenCV output which are then passed via a socket connection to [Wekinator](http://www.wekinator.org/). Wekinator employs Neural Networks trained to detect attributes of facial expressions - mouth width, eye open-ness etc -. The Wekinator code then outputs control parameters to a customised implementation of the [Blotar](http://www.maxobjects.com/?v=objects&id_objet=136) instrument max patch to produce audio. 

By facing the webcam and pulling faces the user is able to play the instrument in a predictable and fun way.

This project formed part of the final submission for the [Machine Learning for Musicians and Artists course run on Kadenze](https://www.kadenze.com/courses/machine-learning-for-musicians-and-artists/info) by Rebecca Fiebrink of Goldsmith's college, London. 


