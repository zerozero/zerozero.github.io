---
id: 269
title: Pure Oxygen code generator for PureMVC
date: 2012-01-28T19:02:21+00:00
author: zerozero
layout: post
categories:
  - code
---
Pure oxygen is a fork of [Greg Jastrab&#8217;s puremvcgen ant tool](http://blog.smartlogicsolutions.com/2008/12/05/introducing-puremvcgen-an-ant-based-puremvc-flex-generator/). I&#8217;ve been successfully using his code generation tool on my [PureMVC](puremvc.org) projects for a few years now and it has made my life a bit easier. It&#8217;s good but it does have it&#8217;s limitations and is slightly annoying in forcing the user to manually enter names for static variable names and values and view component getters when these could (and probably should) all be derived. I like my mvc classes to be as consistent as possible so code generation is a great way to achieve this but manually entering the same thing in camel-case, upper-case and lower-case is no fun and an invitiation for errors to creep in.<!--more-->

Recently I stumbled upon a couple of posts about using javascript within ANT and that got me thinking of ways I could modify Greg&#8217;s code to auto-magically derive more of it&#8217;s values from a single manual entry.

In addition I wanted to be able to choose whether the generated view components would be Flex &#8211; mx or spark &#8211; or actionscript, create test cases for all generated proxies and also to support both standard and multicore flavors of PureMVC.

Not being a ruby programmer I also wanted to remove that from the tool-chain and have everything located in a single place. I&#8217;ve been using HTMLBoilerPlate recently to optimize and minify html/css/javascript and their build script does just that; simply drag the entire folder to the correct location, open a terminal and start ant-ing.

With PureOxyGen you can generate a directory structure and boilerplate actors for all your pureMVC classes, both single and multicore, along with components for mxml or pure actionscript projects and also test cases with minimal keystrokes.

**How To.**

- The only thing you absolutely have to install is ANT &#8211; there&#8217;s plenty of info on how to do that on the interwebs.
- Next head on over to my [github page](https://github.com/zerozero/pureoxygen) and grab the source code.
- Now you&#8217;ll need to create a new project, I use Flash Builder 4.5 currently but any IDE should be ok.
- After the project is created you need to add a couple of swc&#8217;s to your project&#8217;s build path: the correctly flavored puremvc and (if you&#8217;re going to be unit testing) puremvc-flexunit-testing libraries &#8211; [single](http://code.google.com/p/puremvc-flexunit-testing/) or [multicore](https://github.com/cameronyule/puremvc-multicore-flexunit-testing).
- Now the project is set up copy the **tools** folder from PureOxyGen into the root project directory.
- There&#8217;s a few properties we&#8217;ll need to set so ant knows who you are, where everything is located and what type of generation you want to do.
- First open author.properties in a text editor and fill in the relevant info, this info will appear in the head comment of every generated file:

```as3
author.name = Your Name
author.email = mail@youremail.com
```

- Next open project.properties and make the following changes
  
```as3
#The prefix for the generated Facade e.g. Application will become ApplicationFacade
app.prefix = Application
#The name to give the main application file e.g. PureOxygen.mxml
project.name = PureOxygen
#The namespace and derived directory structure - this is where the mvc package structure will be created
core.namespace = com.jonrowe.oxygen
#The path from the build file to your project's src directory - shouldn't need to change if you dragged tools to your root directory
src.dir = ../src
#standard or multicore
pmvc.flavor = standard
#mx or spark or air or as3
component.set = spark
```

- OK, that's it for setup. Now open a terminal window and cd into the tools directory within your project folder.
- First thing to do is check everything is correctly set up so type `$ ant chk`
  
If everything is present and correct you should get a success message.

Now, let&#8217;s get generating.. I&#8217;ve tried to keep the amount of typing required down to an absolute minimum so most targets can be started with just a 2 letter command. It is possible to pass variables in from the command line using the -D_variable_ format but in practice it&#8217;s easier to let the console pop up it&#8217;s text entry dialog and enter the required variable at that point. here&#8217;s the full list:

  * `$ ant chk` - runs a check on all properties and directory locations
  * `$ ant mk` - creates all directories and main fixtures using the properties defined in proj.properties
  * `$ ant mc [-Dname="MyCommandName"]` - creates a new macro command
  * `$ ant sc [-Dname="MyCommandName"]` - creates a new simple command
  * `$ ant px [-Dname="MyProxyName"]` - creates a new proxy
  * `$ ant ev [-Dname="MyEventName"]` - creates a new event
  * `$ ant md [-Dname="MyMediatorName"]` - creates a new mediator/view component combo

Well the first command to run is `$ ant mk` which will generate the entire pureMVC directory structure as well as the application facade, main view component, a startup command, a prepare actors command containing the registration code for the main view component and the application mediator. All with just 2 characters! If you look in your src directory now they should all be there (refresh the directory in flash builder for them to show up).

To successfully run the newly created project you will need to include the correct version of PureMVC (standard or multicore, swc or source) in your project&#8217;s build path and change your IDE&#8217;s project settings to make the newly created application file (_yourProjectName.mxml_ in the default package) to be the default application.

You can safely delete any files generated by your IDE such as the original main application file since we won&#8217;t be using these.

If you try running the application now it should build, compile and run without errors, though there won&#8217;t be anything to see. Right-click in the browser window and check that you get the Flash context menu (it might not be left-aligned depending on how your html-template file is set up).

After that you&#8217;re on your way to creating your puremvc application, carry on creating mediators, proxies, commands and events to your hearts content. Any time you create a proxy you also get a free test case which you can use to run FlexUnit unit tests on the proxy (more on that in another post). Any time you create a mediator you also get a free view component of the correct type (mx, spark or as3 sprite).

That's all there is to it. Of necessity the script only sets up a fairly basic project boilerplate but I find maybe 80% of the projects I do at least start out set up like this, and then I modify them later as circumstances require, adding pre-loaders etc as the project nears release.

