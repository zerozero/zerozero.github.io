---
title: Full body VR with Final IK + Oculus Touch (Part 1)
date: 2018-02-03T13:38:21+00:00
author: zerozero
layout: post
categories:
  - code
disqus: no
---


<div class="img_row">
	<img class="col three" src="{{ site.baseurl }}/images/heroes/FinalIKOculusTouch.jpg" alt="" title="Avatar"/>
</div>
<div class="col three caption">
	Rigify Rigged Avatar.
</div>

**Objective**

Create a full-body avatar driven with inverse kinematics inside VR while maintaining fully articulated hands linked to Oculus touch controllers.

**Intro**

A client recently asked me to create such a thing for a VR experience on a trade-show stand. It occurs to me that I haven't seen many implementations of such a system in the VR experiences I have tried. Lone Echo being one of the few where the user has a full body (albeit a hard robotic body). After some research I find the excellent Final IK on the Unity Asset Store which includes a VR-ready implementation. This seems a good starting point and setup in Unity has been [well documented](http://andytsen.com/2016/12/24/virtual-reality-ik-for-humanoid-avatars-using-final-ik-in-15-minutes/).

Less info is available on hooking up the hands of a rigged IK-driven avatar to the Oculus Touch controllers, and indeed creating custom hands for the Rift is a little-known art. So I'll be taking a look at that.

So here are the problems that need addressing:
- Create a rigged humanoid character model with the correct bones and animations to be mapped to Oculus Touch controllers
- Set up the character in Unity to use the Final IK VRIK plugin
- Set up the mecanim animator in Unity to map Oculus Touch poses onto the avatar hands  
- Adapt the Oculus custom hand code to operate on a single mesh with animations defined for each hand.

**Tools**

* Blender
* Unity (2017.x)
* [Final IK]()

Rigging The Character
===

I decide to use an Adobe Fuse -> Mixamo model workflow as I know this will create a compliant humanoid rig in a few simple steps. Downloaod the mixamo fbx in T-Pose and import into Blender. Install [Simple Renaming Panel](https://github.com/Weisl/simple_renaming_panel) then use it to search and replace "mixamorig:" with "" - this will allow Blender to recognise symmetry in the bones.

Set up the bones, either by hand or with IK, creating an animation for each pose or a single animation that can be split in Unity later.

<div class="img_row">
	<img class="col one" src="{{ site.baseurl }}/images/vrik/mixamo-rigged-character.png" alt="" title="mixamo-rigged-character"/>
</div>
<div class="col one caption">
	Mixamo rigged character
</div>

With my Character rigged I want to create an animation for each of the hand poses I'm going to map to the Touch Controller. I find the [Oculus Sample Framework](https://developer.oculus.com/documentation/unity/latest/concepts/unity-sample-framework/) and the SampleScenes/Hands/Animations folder within.

I can see the list of animations that need to be created to fulfill all the requirements of Mecanim state machine. Opening each one I can see the hand pose for each one. Mostly they are single frames that will be blended together in a blend tree in response to  input from the Touch Controllers. 

<div class="img_row">
	<img class="col one" src="{{ site.baseurl }}/images/vrik/hand_pose_list.jpg" alt="" title="l_hand_cap_touch"/>
</div>
<div class="col one caption">
	Animation List.
</div>

I can see there are 13 poses I need to create for each hand. Using Rigify in Blender I should be able to create the poses for one side then mirror to the other side. When the rig is brought in to Unity I will be able to use [Avatar masking](https://docs.unity3d.com/Manual/class-AvatarMask.html) to restrict movement to the left hand for the left controller and vice versa for the right.

<div class="img_row">
	<img class="col one" src="{{ site.baseurl }}/images/vrik/cropped/pose_0000_l_hand_cap_touch_anim.jpg" alt="" title="l_hand_cap_touch_anim"/>
	<img class="col one" src="{{ site.baseurl }}/images/vrik/cropped/pose_0001_l_hand_cap_touch_index_mask_anim.jpg" alt="" title="l_hand_cap_touch_index_mask_anim"/>
	<img class="col one" src="{{ site.baseurl }}/images/vrik/cropped/pose_0002_l_hand_cap_touch_thumb_mask_anim.jpg" alt="" title="l_hand_cap_touch_thumb_mask_anim"/>
</div>
<div class="img_row">
	<img class="col one" src="{{ site.baseurl }}/images/vrik/cropped/pose_0003_l_hand_default_anim.jpg" alt="" title="l_hand_default_anim"/>
	<img class="col one" src="{{ site.baseurl }}/images/vrik/cropped/pose_0004_l_hand_fist_anim.jpg" alt="" title="l_hand_fist_anim"/>
	<img class="col one" src="{{ site.baseurl }}/images/vrik/cropped/pose_0005_l_hand_hold_generic_anim.jpg" alt="" title="l_hand_hold_generic_anim"/>
</div>
<div class="img_row">
	<img class="col one" src="{{ site.baseurl }}/images/vrik/cropped/pose_0006_l_hand_hold_l_controller_anim.jpg" alt="" title="l_hand_hold_l_controller_anim"/>
	<img class="col one" src="{{ site.baseurl }}/images/vrik/cropped/pose_0007_l_hand_hold_ping_pong_ball_anim.jpg" alt="" title="l_hand_hold_ping_pong_ball_anim"/>
	<img class="col one" src="{{ site.baseurl }}/images/vrik/cropped/pose_0008_l_hand_pinch_anim.jpg" alt="" title="l_hand_pinch_anim"/>
</div>
<div class="img_row">
	<img class="col one" src="{{ site.baseurl }}/images/vrik/cropped/pose_0009_l_hand_point_anim.jpg" alt="" title="l_hand_point_anim"/>
	<img class="col one" src="{{ site.baseurl }}/images/vrik/cropped/pose_0010_l_hand_relax_3qtr_fist_anim.jpg" alt="" title="l_hand_relax_3qtr_fist_anim"/>
	<img class="col one" src="{{ site.baseurl }}/images/vrik/cropped/pose_0011_l_hand_relax_mid_fist_anim.jpg" alt="" title="l_hand_relax_mid_fist_anim"/>
</div>
<div class="img_row">
	<img class="col one" src="{{ site.baseurl }}/images/vrik/cropped/pose_0012_l_hand_thumbs_up_anim.jpg" alt="" title="l_hand_thumbs_up_anim"/>
</div>
<div class="col three caption">
	Hand Poses.
</div>

<br/>

<div class="img_row">
	<img class="col one" src="{{ site.baseurl }}/images/vrik/flex_layer.jpg" alt="" title="flex_layer"/>
	<img class="col one" src="{{ site.baseurl }}/images/vrik/flex_layer_blend_tree.jpg" alt="" title="flex_layer_blend_tree"/>
</div>

<div class="col three caption">
	Blend Tree.
</div>




So I spend a few hours Posing the hands. 

First I want to create some IK constraints to help animating.
Clear the parent of the last bone (Alt-P in edit mode)
In pose mode select the 3rd bone in the index finger - in the constraints tab select add constraint - IK
set the chain length to 3
Set the target to Armature -> Index4
Now in the bone tab for each joint constrain rotation to X by locking the other rotations
Set rotation limits to sensible values
Set stiffness where necessary.
Repeat for all fingers of the hand.
Use shape keys to tweak the mesh where it deforms oddly.
- Select the mesh and Add a shape key, first one is the basis, second one will be the first modified shape key
- Preserve volume off? try toggling to see which gives best result.
- Right click on value field below shape keys widget and "Add Driver"
- Open a graph editor
- Select the key, hit N to show properties
- In drivers tab set Object = Armature, Bone = the appropriate bone, Type = X,Y or Z rotation, Space = Local. Finally set type to Averaged Value (important to do this last to avoid error)
Now moving the IK target should deform the mesh correctly - edit as required
It is important for the sense of presence and immersion that the hands are not crap.

[Continues in Part 2]()

Setup like 

VRIK
====

Import the model
Setup Oculus OVRCamera Rig
Set tracking origin type to Floor Level
Import Final IK
Drag model into world
Duplicate head and 2 hands bones
set the local position of each to 0,0,0
parent hands to LeftHandAnchor/RightHandAnchor
parent head to centerEyeAnchor
**Now** add the VRIK component to the character - this MUST be done after parenting the duplicates!
Drag the *parented* head and hands transforms to the correct slots on the VRIK component
Adjust the anchors at runtime to position correctly.

Touch Controllers
=================

Set the rig animation type to Humanoid, hit apply (if success Configure will be available)
Go to Animations Tab
Create clip for each animation (Don't forget to hit Apply)
Create a new Avatar mask for each hand (right click in project window -> create -> avatar mask)
Mask all but the respective hand in the inspector
Create a new Animation Controller
For thumbs up gesture create a new Layer in the animator named Thumb Layer
Add the Avatar Mask to the Thumb Layer
Right Click and Create New State -> From Blend Tree
Double Click State
Create 2 new Motion Slots in the Blend Tree Inspector for a 1D Blend
Drag in an idle pose and the thumbs up motions into these slots
Drag Hand Script onto character


Alpha Video
===========

webm VP8 codec, vorbis audio codec, include alpha channel
Import to Unity
In VideoClip importer check Keep Alpha *and* transcode

Unity's layout-based workaround to support transparency in VP8 and H.264

	In order to work around the difficulties of using standard clips with alpha in your Unity built game, Unity offers the "Keep Alpha" option in its VideoClip importer. When Unity detects the video has native alpha in it, the Keep Alpha option shows up in the VideoClip importer and if you enable it along with the transcode option, the resulting VideoClip asset will use an internal layout where the alpha is encoded in hidden color channels. During decoding at runtime, we recombine these hidden color channels with the actual color content on the GPU to yield a RGBA movie without needing any special decoding capabilities from the platform. 
	
Adding a laser pointer to Oculus hand
=====================================

Add OVRPhysicsRaycaster to OVRCameraRig
create an empty object as a child of RightHandAnchor
assign the empty object as the value for Ray Transform on:
	GazePointerRing:OVRGazePointer
	LaserLine:LaserLine
	EventSystem:GazeInteractionInputModule
Add GazeInteractionInputModule to EventSystem
assign GazePointerRing:OVRGazePointer to Pointer of Laser Line