---
---
<data>
	{% for page in site.pages %}
		{% if (page.project) %}
			<page title="{{ page.title }}" date="{{ page.date }}" url="{{ page.url }}" name="{{ page.url | split: '/' | last: }}" categories="{{ page.categories | join: ', '}}" tags="{{ page.tags | join: ', '}}" thumb="/images/square/{{ page.url | split: '/' | last: }}.jpg" tech="{{ page.tech | join: ', '}}"/>
			{% endif %}
	{% endfor %}
</data>