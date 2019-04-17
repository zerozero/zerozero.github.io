---
layout: page
title: Devlog
menu: Devlog
permalink: /devlog/
order: 4
---

<ul class="post-list">
    {% assign devlog = site.devlog | reverse %}
	{% for post in devlog %}
	{% include post_block.html %}
	{% endfor %}
</ul>