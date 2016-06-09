---
---
<data>
	{% for page in site.pages %}
		{% if (page.project) %}
			<page title="{{ page.title }}" date="{{ page.date }}" url="{{ page.url }}" categories="{{ page.categories | join: ', '}}" tags="{{ page.tags | join: ', '}}"/>
			{% endif %}
	{% endfor %}
</data>