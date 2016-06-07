---
layout: page
title: Wow Face!
date: 2016-05-16T16:44:29+00:00
project: true
---

London, May 2012

[![Wow Face!](https://zerozero.github.io/images/heroes/machine-learning.jpg)](https://vimeo.com/167115527 "Wow Face! - Click to Watch!"){:target="_blank"}

Using machine learning algorithms this project enables the user to control audio output through facial expressions.

A webcam feed inputs picture information to an implementation of OpenCV in OpenFrameworks. Custom C++ code extracts features from the OpenCV output which are then passed via a socket connection to Wekinator. Wekinator employs Neural Networks trained to detect attributes of facial expressions - mouth width, eye open-ness etc -. The Wekinator code then outputs control parameters to a customised implementation of the Blotar instrument max patch to produce audio. 

By facing the webcam and pulling faces the user is able to play the instrument in a predictable and fun way.

This project formed part of the final submission for the Machine Learning for Musicians and Artists course run on Kadenze by Rebecca Firebrick of Goldsmith's college, London.