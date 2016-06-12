---
layout: page
permalink: /work/
title: projects
menuItem: true
description: Archived projects
---

<ul class="post-list">
{% assign items = site.pages | sort: 'date' %}
{% for page in items reversed %}
{% if (page.project) %}
    <li>
        <h2><a class="poem-title" href="{{ page.url | prepend: site.baseurl }}">{{ page.title }}</a></h2>
        <p class="post-meta">{{ page.date | date: '%B %Y' }}</p>
		<p>{{ page.description }}</p>
      </li>
{% endif %}
{% endfor %}
</ul>