---
layout: page
title: Project Archive
menu: Work
permalink: /archive/
order: 3
---

<ul class="post-list">
    {% assign archive = site.posts %}
	{% for post in archive %}
	{% include post_block.html %}
	{% endfor %}
</ul>
