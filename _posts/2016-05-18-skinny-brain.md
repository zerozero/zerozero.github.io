---
layout: post
title: Can you lose weight by thinking harder?
category: Philosophy
---

He thought these thoughts: *The human brain uses more energy than any other organ - 20% of the total energy consumption of the body. Does this mean that if I think harder I use more energy? Can I lose weight by thinking alone? Is this why scientists are skinny? - just look at Stephen Hawking; brilliant man; skinny as a rake!*

![Stephen Hawking](/images/Stephen-Hawking-AI-248011 copy.jpg)
*Stephen Hawking thinking hard.*

Some javascript

{% highlight JavaScript %}
'use strict';

var express = require('express');

/**
 * Main application file
 */

// Set default node environment to development
process.env.NODE_ENV = process.env.NODE_ENV || 'development';

// Application Config
var config = require('./lib/config/config');

var app = express();

// Express settings
require('./lib/config/express')(app);

// Routing
require('./lib/routes')(app);

// Start server
app.listen(config.port, function () {
  console.log('Express server listening on port %d in %s mode', config.port, app.get('env'));
});

// Expose app
exports = module.exports = app;

{% endhighlight %}