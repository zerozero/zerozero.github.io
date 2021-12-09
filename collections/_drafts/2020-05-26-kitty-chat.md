---
title: Kitty Chat
project: false
layout: post
categories:
  - code
  - projects
  - technology
tags:
  - projects
description: Chat with other cats
tech:
 - iOS
 - Unity
 - Node/Express
 - Chuck
 - WebRTC
video: 422796624
---

<div class="img_row">
	<img class="col three" src="{{ site.baseurl }}/images/kittychat/KittyChatVisualisation.jpg" alt="" title="Kitty Chat Previz"/>
</div>
<div class="col three caption">
	Kitty Chat Previz
</div>

**Elevator Pitch**

In the depths of the Covid-19 outbreak when we are all feeling disconnected and depressed, missing our friends and families and stressed by the dull, dull, dull nature of video-conferencing, what better than a way to lighten the mood with a frivolous little app that allows us to be represented by cute, fluffy animated cats. We can choose a kitty to be our avatar and communicate with remote participants who can hear our pitch-shifted voice or switch to 'native' cat speech with subtitles.

**Technical Requirements**

1. Face capture from a standard webcam or FaceKit cam
2. Convert facial expressions to Action Units for animated characters
3. Stream live WebGL rendered content and audio from web app to remote viewers
4. Receive live video from remote animated content
5. Process spoken word to text, cat-like sounds and pitch-shifted audio

**1. Face capture from a standard webcam or FaceKit cam**

Status: On Hold.
I'm awaiting an evaluation version of Banuba Face Capture plugin for Unity before I drop most of $1K on buying it. I also want to wait for the iPhone 12 release later in the summer before upgrading my device. 

**2. Convert facial expressions to Action Units for animated characters**

Status: To evaluate.
I have a demo model from ??? to use for prototyping. I need to evaluate whether their semi-automated system will give me the result I'm looking for - animated 'stretch-and-bounce' for full 'Loony Tunes' goodness. It may be that the services of a pro 3D animator would be better.

**3. Stream live WebGL rendered content and audio from web app to remote viewers**
&
**4. Receive live video from remote animated content**

My initial efforts focussed on investigating ways to emulate snap camera's functionality to intercept the video feed from the webcam, run it through a filter and create a virtual webcam to stream from. While this is a reasonable solution the necessity for a deeper knowledge of the underlying OS architecture and low-level C programming were a blocker for me.

Next I started to look at a potential solution in Unity using their RemoteStream package. I download and install the package and get it running which inviolves running a small server locally through which the Unity app can route encoded video. All works well on the local network but without a full-fledged media server this isn't going to be a scalable solution. I reason that I can potentially use millicast.com to provide media streaming services but attempts to publish Unity to WebGL for browser playback fail due to encoding problems with the WebGL player - as far as I can see encoding within the player isn't going to be possible without some major refactoruing,.

On the point of admitting defeat I chance upon the javascript CaptureStream method which allows for capturing and encoding a media stream from a canvas element, which is of course exactly the element that Unity#s webGL player runs in. 

I quickly set up an Express server to serve a web page with my test WebGL Unity scene embedded. The first blocker i that I need some way to know when Unity has loaded and the media track is available. I look into Unity to js communication and find I can create a .jslib plugin easily that will handle interaction between the two environments. For now I just need to be able to send a message across as soon as the Start method of a gameObject is run.

```javascript
using UnityEngine;
using System.Runtime.InteropServices;

public class BrowserComms : MonoBehaviour {

    
    #if UNITY_WEBGL
   
    //Let the browser know that Unity has started (via. Plugins/browsercomms.jslib)
    [DllImport("__Internal")]
    private static extern void Ready();

    void Start() {
        
        Ready();
    }
    
    #endif
}
``` 

and the plugin, placed in the Assets/Plugins directory goes like this:

```javascript
mergeInto(LibraryManager.library, {

    Ready: function() {
        window.streamer.onReady();
    }
});
```

In javascript on my Express site I have added a global class named `streamer` with an `onReady` method to respond to this method call. From there I can begin the process of streaming the media element safe in the knowledge that Unity has started correctly.  

The actual streaming code is exactly as described [here](https://dash.millicast.com/docs.html?pg=how-to-broadcast-in-js) on the Millicast site with the exception that I moved the auth code to the server side for safety reasons and replaced the `getMediaTrack()` method call with the new `canvas.captureStream();` call.

Testing on Millicast's hosted player path (provided as part of their service) is successful. This is great I can publish a stream from a Unity WebGL player with full interactivity via a media server to an audience of potential millions with ultra-low latency. This can be useful for more than just a chat app. It opens up potential uses for online game streaming, streaming AR, VR sessions and much more. And, of course, this isn't restricted to the browser, WebRTC shouyld function in native apps on iOS, Android, Windows and Mac.

Here's a quick video showing the Unity WebGL content (on the left hand side of the Macbook Pro) streaming out to Millicast and then subscribed to in the browser on the Mac, a Windows machine and an iPad. As you can see latency is less than a second.

{% include vimeoplayer.html id=page.video %}