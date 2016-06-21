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

I want to be able to have locomotive.js recognise when I have changed files and automatically restart the server so I don't have to CTRL-C/up arrow/Enter after every edit.

References: <a href="http://jonathanmh.com/running-locomotive-js-with-node-dev/" target="_blank">this</a>.
  
Requirements:

  1. <a href="http://locomotivejs.org" target="_blank">Locomotive</a> 
  2. [nodemon](https://github.com/remy/nodemon)

Create a file named **server.js** in the root of the locomotive project and add this code:
  
```as3
var locomotive = require('locomotive'),
          
env = process.env.NODE_ENV || 'development',
          
port = process.env.PORT || 3000,
          
address = '0.0.0.0';

locomotive.boot(__dirname, env, function(err, server) {
      
if (err) { throw err; }
      
server.listen(port, address, function() {
        
var addr = this.address();
        
console.log('listening on %s:%d', addr.address, addr.port);
      
});
  
});
```

Now, in the terminal, I **cd** to the root dir of my project and type `$ nodemon server.js`. Now whenever I make a change to any of the project's source files the **nodemon** instance recognises this and restarts the server so all I have to do is refresh the page to see changes === cool

