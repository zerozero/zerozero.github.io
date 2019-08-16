---
layout: page
title: Art Projects
menu: Art
permalink: /artist/
order: 5
---

<ul class="post-list">
    {% assign art = site.artist | reverse %}
	{% for post in art %}
	{% include post_block.html %}
	{% endfor %}
</ul>