---
layout: page
title: Featured Projects
menu: Featured
permalink: /projects/
order: 2
---

{% assign featured = site.posts |  where: "featured", "true" %}
{% include image-gallery-projects.html projects=featured folder="/images/square/" %}



{% comment %}
{% assign featured = site.posts |  where: "featured", "true" %}
<ul>
  {% for project in featured %}
    <li>
      <a href="{{ project.url }}">{{ project.title }}</a>
    </li>
  {% endfor %}
</ul>
{% endcomment %}