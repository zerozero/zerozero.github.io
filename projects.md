---
layout: page
title: Featured Projects
menu: Featured
permalink: /projects/
order: 2
---

{% include image-gallery.html folder="/images/square/" %}

{% comment %}
<ul>
  {% for project in site.projects %}
    <li>
      <a href="{{ project.url }}">{{ project.name }}</a>
    </li>
  {% endfor %}
</ul>
{% endcomment %}
