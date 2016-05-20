---
id: 162
title: Running Locomotive.js with nodemon
date: 2013-05-02T15:40:32+00:00
author: zerozero
layout: post
categories:
  - code
---
Easy auto-restart of a locomotive.js server when editing files.

I want to be able to have locomotive.js recognise when I have changed files and automatically restart the server so I don&#8217;t have to CTRL-C/up arrow/Enter after every edit.

References: <a href="http://jonathanmh.com/running-locomotive-js-with-node-dev/" target="_blank">this</a>.
  
Requirements:

  1. <a href="http://locomotivejs.org" target="_blank">Locomotive</a> 
  2. [nodemon](https://github.com/remy/nodemon)

Create a file named **server.js** in the root of the locomotive project and add this code:
  
[cc]var locomotive = require(&#8216;locomotive&#8217;),
          
env = process.env.NODE_ENV || &#8216;development&#8217;,
          
port = process.env.PORT || 3000,
          
address = &#8216;0.0.0.0&#8217;;

locomotive.boot(__dirname, env, function(err, server) {
      
if (err) { throw err; }
      
server.listen(port, address, function() {
        
var addr = this.address();
        
console.log(&#8216;listening on %s:%d&#8217;, addr.address, addr.port);
      
});
  
});[/cc]

Now, in the terminal, I **cd** to the root dir of my project and type [cc inline=&#8221;true&#8221;]$ nodemon server.js[/cc]. Now whenever I make a change to any of the project&#8217;s source files the **nodemon** instance recognises this and restarts the server so all I have to do is refresh the page to see changes === cool

<div class="gk-social-buttons">
  <span class="gk-social-label">Share:</span> <a class="gk-social-twitter" href="http://twitter.com/share?text=Running+Locomotive.js+with+nodemon&url=http%3A%2F%2F162.13.3.34%3A8079%2Flabs%2F%3Fp%3D162"
	            onclick="window.open(this.href, 'twitter-share', 'width=550,height=235');return false;"> <span class="social__icon--hidden">Twitter</span> </a> <a class="gk-social-fb" href="https://www.facebook.com/sharer/sharer.php?u=http%3A%2F%2F162.13.3.34%3A8079%2Flabs%2F%3Fp%3D162"
			     onclick="window.open(this.href, 'facebook-share','width=580,height=296');return false;"> <span class="social-icon-hidden">Facebook</span> </a> <a class="gk-social-gplus" href="https://plus.google.com/share?url=http%3A%2F%2F162.13.3.34%3A8079%2Flabs%2F%3Fp%3D162"
	           onclick="window.open(this.href, 'google-plus-share', 'width=490,height=530');return false;"> <span class="social__icon--hidden">Google+</span> </a>
</div>