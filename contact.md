---
layout: page
title: Contact
menu: Contact
permalink: /contact/
order: 5

forms:
  - to: info@jonrowe.com
    subject: Contact Request
    redirect: /
    form_engine: formspree
    placeholders: false
    fields: 
      - name: name
        input_type: text
        placeholder: Name
        required: true
      - name: email
        input_type: email
        placeholder: Email address
        required: true
      - name: message
        input_type: textarea
        placeholder: Message
        required: false
      - name: submit
        input_type: submit
        placeholder: Submit form
        required: true

---

**Say 'Hello' or something..**

{% if page.forms[0] %}{% include form.html form="1" %}{% endif %}

{% include follow-buttons.html %}
