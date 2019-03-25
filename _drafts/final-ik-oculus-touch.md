Final IK + Oculus Touch
===

Setup like http://andytsen.com/2016/12/24/virtual-reality-ik-for-humanoid-avatars-using-final-ik-in-15-minutes/

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