---
id: 270
title: Blender 3D + Carnegie Mellon .bvh motion capture remapping
date: 2012-08-19T20:11:57+00:00
author: zerozero
layout: post
categories:
  - 3D
  - art
tags:
  - blender
  - bvh
  - cmu
---
Today I&#8217;m investigating how to use bvh motion capture files with my blender models (~Blender 2.49). The bvh files are provided by <a href="http://mocap.cs.cmu.edu/" title="CMU original mocap data" target="_blank">Carnegie Melon University</a> with the Blender compatible conversions <a href="https://sites.google.com/a/cgspeed.com/cgspeed/motion-capture/cmu-bvh-conversion" title="CMU .bvh files" target="_blank">here</a>, there&#8217;s over 2000 files and they&#8217;re an awesome resource if you&#8217;re looking for naturalistic human motion, less so if you&#8217;re looking for the &#8216;stretch-and-bounce&#8217; type animation you&#8217;d find in cartoons. They won&#8217;t fit every situation and they will need cleaning up before they are really useful but for quickly having access to a large number of actions &#8211; for free &#8211; they can&#8217;t be beat. <!--more-->

When considering whether to go the bvh route also bear in mind that humans, being intrinsically asymmetric organisms, are unlikely to produce perfectly matched strides, for example, so if you&#8217;re after looping animations be aware that there will likely be a fair amount of work needed to get loops to line up.

A note about the files &#8211; the authors point out that in some of the earlier captures things can be a bit wonky as they were ironing out kinks in the system so for preference start searching from the end of the listing and work backwards towards the beginning for best results.

To start you&#8217;ll need a blender model &#8211; since all the motion capture files provided by CMU are humans the model should be bipedal unless you specifically want a lama walking on it&#8217;s hind legs and moving it&#8217;s front legs like human arms. Here is my model, it&#8217;s the ancient Greek philosopher Socrates in case you didn&#8217;t recognise him &#8211; I created him for an educational project I was working on recently for Edcoms in London.

[<img src="http://162.13.3.34/labs/wp-content/uploads/2012/03/throw_00143.png" alt="" title="throw_00143" width="200" height="320" class="alignnone size-full wp-image-118" />](http://162.13.3.34/labs/wp-content/uploads/2012/03/throw_00143.png)

Normally, once the modelling is finished you&#8217;d want to start constructing an armature to rig the character with. While in theory it wold be possible to use your own armature you&#8217;d have to make sure the bone hierarchy exactly matched that of the bvh files we are going to import later. Easier, then, to use the armatures that are created by Blender&#8217;s import .bvh script and tweak that to fit the model.

With the model in place it&#8217;s time to grab some bvh files, the full repository of Blender compatible files I used are <a href="https://sites.google.com/a/cgspeed.com/cgspeed/motion-capture/cmu-bvh-conversion" title="bvh downloads" target="_blank">here</a>, and the reference list of al 2548 motions is <a href="https://sites.google.com/a/cgspeed.com/cgspeed/motion-capture/cmu-bvh-conversion/bvh-conversion-release---motions-list" title="listing of motions" target="_blank">here</a>. I&#8217;ve already used a walk and a run action so I&#8217;m going to choose something a bit more exciting for this demo.. how about **143_34 Chicken Dance** &#8211; just the sort of thing a good philosopher needs to be able to do!

I&#8217;ll animate him naked for starters then maybe have a go at some cloth simulation on his toga when I&#8217;m happy with the movement. If you can&#8217;t wait to see Socrates doing the chicken dance the completed animation is here.

4.Get BVhacker (win only) but I have Virtual Box set up on mac.
  
5. In BVHacker import the bvh file (there is a list of which action is in which file here or in every download package.
  
6.Notice that the model is not centred (try importing directly into Blender first as a previous step to illustrate). Also the frame rate is much too high.
  
7. IN BVHacker reduce the framerate by twirling the up button(!) like so:

[<img src="http://162.13.3.34/labs/wp-content/uploads/2012/03/BVHacker1.png" alt="" title="BVHacker1" width="274" height="191" class="alignnone size-full wp-image-123" />](http://162.13.3.34/labs/wp-content/uploads/2012/03/BVHacker1.png)
   
until it&#8217;s at around 30fps or whatever you need for your project

  1. Now we need to center the motion, again in bvhacker click &#8216;center&#8217;
  2. make sure the animation is at frame 1 and in the same group of buttons click set T

[<img src="http://162.13.3.34/labs/wp-content/uploads/2012/03/BVHacker2.png" alt="" title="BVHacker2" width="141" height="151" class="alignnone size-full wp-image-124" />](http://162.13.3.34/labs/wp-content/uploads/2012/03/BVHacker2.png)
  
10. Save the file and open your model in Blender.
  
11. Hit File -> Import -> Motion Capture (.bvh) and select the saved bvh file.
  
12. In the dialog that pops up select Armature and scale to 1.0 (this worked pretty good for me but it will depend on the size of your model &#8211; you may need to try a few times to get a reasonable scale, write it down and remember it so you can use it for any other armatures you import)
  
13. Blender operates with a z-Up so the first thing to do, in Object mode, is rotate the imported armature/actions -90
  
Toggle between edit and pose mode with the armature selected, there should be no movement &#8211; if there is we&#8217;ll need to do some work to correct
  
In IPO mode we want to select the hip bone (very small so use outliner to select) as move it&#8217;s z position down across the whoile range of frames.. this will retain any movement relative to it&#8217;s base position.
  
14. Now it&#8217;s time to fit the armature to your model. Note this will only need to be done once for each model so it&#8217;s worth taking your time and doing it well now. Switch between edit mode for both the model and the armature and pull vertices around where necessary, and resize bones where necessary. This will be easier if you cut your symetrical mesh in 1/2 and apply a mirror mdifier &#8211; we will apply the modifier after everything is lined up.
  
15. Once the armature fits nicely inside the mesh apply the mirror modifier and then it&#8217;s time to assign vertex weights so that movements of the armature will pull the correct vertices around. In order to do this you can either paint vertices directly in weight-paint mde (really difficult to do well) or have blender do it automatically for you (really easy).
  
16. Lets choose &#8220;really easy&#8221; as a way forward.. In Object mode a(-a) to deselect everything, select the model, shift select the armature then CTRL-P Make Parent To -> Armature, Create Vertex Groups? -> Create From Bone Heat. (If you need to unparent use ALT-P)
  
17. In theory that&#8217;s it, however theory is one thing and the real world is something else. If bits of your model get left behind when your character starts moving or if the mesh deforms in strange ways the create from bone heat process has likely not mapped all vertices correctly. Not to worry, we can simply go into weight paint mode (CTRL-Tab) and manually paint any unmapped vertices. I find it beneficial to make use of Blender&#8217;s &#8216;Set Clipping Border&#8217; function Alt-B to isolate portions of the mesh with hard-to-access vertices (such as the interior of the mouth &#8211; so I can attack them from the inside (img)
  
Sometimes you might find that a particular bone is having too much effect on certain vertices in which case you can select the bone to see what influence it&#8217;s having and repaint to modify. Here is the color spectrum/weight mapping blender uses.
  
[<img src="http://162.13.3.34/labs/wp-content/uploads/2012/03/heatmap.jpg" alt="" title="heatmap" width="610" height="145" class="alignnone size-full wp-image-125" />](http://162.13.3.34/labs/wp-content/uploads/2012/03/heatmap.jpg)

  1. When you&#8217;re happy with the movement of all vertices in the mesh you&#8217;re done. Now it&#8217;s a simple job to have your character perform any of the 2000+ mocapped actions from the CMU files. Simply prepare the BVH file as above in steps 5..10 then import into blender. Delete the new armature &#8211; we don&#8217;t need that. Open the Action editor and with the character&#8217;s armature selected change the action in the dropdown.

Other things you might need to do:
  
-parenting eyes, hair, glasses etc
  
-editing ipo&#8217;s:
  
I needed to maintain vertical movement in the armature which would be lost if I centered the Y attribute in BVHacker so instead I did this in the IPO editor, bone heat went screwy otherwise since the armature was not inside the mesh in object mode. Select hip bone (master) open the IPO editor, .. the important thing is that the model and the armature match up in all states of edit, pose and object modes.

<div class="gk-social-buttons">
  <span class="gk-social-label">Share:</span> <a class="gk-social-twitter" href="http://twitter.com/share?text=Blender+3D+%2B+Carnegie+Mellon+.bvh+motion+capture+remapping&url=http%3A%2F%2F162.13.3.34%3A8079%2Flabs%2F%3Fp%3D270"
	            onclick="window.open(this.href, 'twitter-share', 'width=550,height=235');return false;"> <span class="social__icon--hidden">Twitter</span> </a> <a class="gk-social-fb" href="https://www.facebook.com/sharer/sharer.php?u=http%3A%2F%2F162.13.3.34%3A8079%2Flabs%2F%3Fp%3D270"
			     onclick="window.open(this.href, 'facebook-share','width=580,height=296');return false;"> <span class="social-icon-hidden">Facebook</span> </a> <a class="gk-social-gplus" href="https://plus.google.com/share?url=http%3A%2F%2F162.13.3.34%3A8079%2Flabs%2F%3Fp%3D270"
	           onclick="window.open(this.href, 'google-plus-share', 'width=490,height=530');return false;"> <span class="social__icon--hidden">Google+</span> </a>
</div>