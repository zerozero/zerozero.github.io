---
layout: page
permalink: /photos/
title: photos
menuItem: true
description: Galleries of photos from my journeys around the world
---

{% for gallery in site.photos reversed %}

<div class="project ">
    <div class="thumbnail">
        <a href="{{ site.baseurl }}{{ gallery.url }}">
        {% if gallery.img %}
        <img class="thumbnail" src="{{ gallery.img }}"/>
        {% else %}
        <div class="thumbnail blankbox"></div>
        {% endif %}    
        <span>
            <h1>{{ gallery.title }}</h1>
            <br/>
            <p>{{ gallery.date | date: '%B %Y' }}</p>
        </span>
        </a>
    </div>
</div>

{% endfor %}