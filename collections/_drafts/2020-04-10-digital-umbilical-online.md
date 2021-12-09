---
title: Digital Umbilical Online
project: false
layout: post
categories:
  - code
  - projects
  - technology
tags:
  - projects
description: Lockdown version of our bio-sensing dance-tech collab
tech:
 - iOS
 - Unity
 - Node/Express
 - Chuck
 - Arduino
 - vvvv
 - WebRTC
soundcloud: 821008594
---

<div class="img_row">
	<img class="col three" src="{{ site.baseurl }}/images/DUO/DUO-scheme.jpg" alt="" title="Digital Umbilical Online Setup"/>
</div>
<div class="col three caption">
	Digital Umbilical Online Setup
</div>

**Technical Requirements**

1. Send heartbeat data from participants in remote locations
2. Convert heartbeat data into sound at dancer location
3. Stream live video and audio from dancer to remote viewers
4. Acquire heartbeat and breath data from dancer 
5. Control presentation remotely, mix between live and pre-recorded video/animation & audio

**1. Send heartbeat data from participants in remote locations**

Build an iOS (and later Android) app to acquire heartbeat data. The app utilizes the iPhone's camera to estimate the pulse. Using an average color filter and exposure filter are applied to the camera's image. By keeping track of the time difference between the green component of the filtered image, a pulse is estimated.  

Create a Unity plugin for iOS based on [this SDK](https://github.com/ItamarM/HeartRate-iOS-SDK/). Every detected beat an event will be fired from the iOS plugin to the Unity app where a VFX visualisation will provide user feedback. Concurrently, a message will be sent via a persistent websocket to the dancer's computer. 

<div class="img_row">
	<img class="col one" src="{{ site.baseurl }}/images/DUO/iPhone-XS-Wireframe-DUO.jpg" alt="" title="Digital Umbilical Online Setup"/>
</div>
<div class="col one caption">
	Heartbeat visualisation
</div><br>

**2. Convert heartbeat data into sound at dancer location**

When heartbeats acquired by the remote iOS apps are received at the dancer's computer (Express server) I want to convert each beat into a musical note. I choose four notes from a measure in our original performance (created by Arthur Astier) and a harmonic of each. I install [ChucK](https://chuck.cs.princeton.edu/) - a programming language for real-time sound synthesis and music creation - which can be run from the commandline. The Express server can execute terminal commands using the `exec` or `spawn` methods of _child_process_. With the chuck program running I send OSC commands to generate notes every time a heartbeat is received.

In order to enable polyphony I _spork_ a new Shred for each note (ChucK-speak for creating instances). It is also important to yield in the main thread to allow all voices to be played. I pass in a commandline argument to enable me to set the level of polyphony externally:  

`$ chuck osc_dump_hb.ck:8`

```javascript
// extended event
class TheEvent extends Event
{
    int index;
    float freq;
}

// the event
TheEvent e;

//patch (to spork)
fun void note( TheEvent event, int i )
{
    i => int index;
    // patch
    ModalBar bar => dac;
    4 => bar.preset;
    // infinite time loop
    while( true )
    {
        event => now;
        if (event.index == index){
            event.freq => bar.freq;
            1 => bar.noteOn;
            0.2 :: second => now;
        }
        
    }
}


Std.atoi(me.arg( 0 )) => int num; //get commandline param (number of participants)

Shred notes[num]; //create an array of length num to hold sporked Shreds

for (0 => int i; i < num ; i++){
    // create shreds
    spork ~ note ( e, i ) @=> notes[i]; //chuck a _reference- to the shred into the array
}

OscIn oin;
6449 => oin.port;
oin.listenAll();

OscMsg msg;



while(true)
{

    oin => now;
    
    while(oin.recv(msg))
    {

        if (msg.address == "/heartbeat"){
            
            Std.mtof(msg.getInt(0)) => e.freq; //convert midi value into frequency, put into event freq
            msg.getInt(1) => e.index;//put index into event index;
            e.broadcast();//broadcast to all shreds
            me.yield();//yield without advancing time to allow shred to run
            
        }
        
    }
}

```

{% include soundcloudplayer.html id=page.soundcloud %}

**3. Stream live video and audio from dancer to remote viewers**

My initial thought is to create a small WebRTC app running on a server on my local machine and using ngrok again to tunnel through to provide access to the stream. However, while this works ok for a small number of users it's not going to hold up for a wider audience and while our predicted viewership may not number millions, it would be nice to think in the hundreds. So my next thought is to use OBS (Open Broadcaster Software). This free application will run on the local machine and stream from a variety of sources into the cloud. Initially I try Twitch but the latency (built-in?) is a killer, we need something with a latencey of sub 1-second. Searchinbg around I find Janus ands Millicast. Janus wouyld require configuring and administeriung my own linux server. Well, I could do that but who has time for that these days? Millicast, while a bit expensive, has a ready-built OBS plugin and minimal configuration, and boasts sub-second latency. I sign up for a trial account - 30 days only.. note to self: don't forget to de-register before the paid tier kicks in (30p days).

The game here is that they provide their own patched version of OBS. I download and install but woes it crashes whenever I tyry to capture my webcam input (Mac FaceTime Camera). Google-fu tells me that I have to sdtart OBS from the command line in order for it to prompt for webcam access. I do so and all is fiune. Looking deeper into OBS I find a handy script feature that allows pythin scripts to be run to autromate any part of thew app. Awesome. Now if I can send OSC commands across from my Express app I can remotely control the broadcast from London.  


**Media Server**

https://bloggeek.me/media-server-for-webrtc-broadcast/

Before I go into an explanation, you need to understand that there are 4 types of WebRTC servers:

Application server
Signaling server
NAT traversal server (STUN & TURN)
Media server
This question is specifically focused on (4) Media server. The other 3 servers are needed here no matter if this is 1:1 or 1:many session.

The Hard Upper Limit
Whenever we want to connect one browser to another with a direct stream, we need to create and use a peer connection.

Chrome 65 includes an upper limit to that which is used for garbage collection purposes. Chrome is not going to allow more than 500 concurrent peer connections to exist.

Bitrates, Speeds and Feeds
This is probably the main reason why you can’t broadcast with WebRTC, or with any other technology.

We are looking at a challenging domain with WebRTC. Media processing is hard. Real time media processing is harder.

Assume we want to broadcast a video at a low VGA resolution. We checked and decided that 500kbps of bitrate offers good results for our needs.

What happens if we want to broadcast our stream to 10 people?

Broadcasting our stream to 10 people requires bitrate of 5mbps uplink.

If we’re on an ADSL connection, then we can find ourselves with 1-3mbps uplink only, so we won’t be able to broadcast the stream to our 10 viewers.

For the most part, we don’t control where our broadcasters are going to be. Over ADSL? WiFi? 3G network with poor connectivity? The moment we start dealing with broadcast we will need to make such assumptions.

That’s for 10 viewers. What if we’re looking for 100 viewers? A 1,000? A million?

With a media server, we decide the network connectivity, the machine type of the server, etc. We can decide to cascade media servers to grow our scale of the broadcast. We have more control over the situation.

Broadcasting a WebRTC stream requires a media server.

In most scenarios, you will need a media server in your implementation at some point.

If you are broadcasting, then a media server is mandatory. And no. Google doesn’t offer such a free service or even open source code that is geared towards that use case.

It doesn’t mean it is impossible – just that you’ll need to work harder to get there.

**Of OBS, python and Lua - Oh My!**

So I want to be able to send OSC commands from a remote machine to control OBS in real-time as the performance is going on - change scenes, add dynamically generated graphics, play animations, that kind of thing. Turns out OBS has scripting functionality - either python or Lua. Greta. But... python on a Mac isn't (officially) supported and Lua... I don't know Lua but, hey, why not. I try to install Lua.

```textmate
$ curl -R -O http://www.lua.org/ftp/lua-5.3.5.tar.gz
$ tar zxf lua-5.3.5.tar.gz
$  cd lua-5.3.5
$  make macosx test
``` 

Which spits hate at me:

```textmate
cd src && /Applications/Xcode.app/Contents/Developer/usr/bin/make macosx
/Applications/Xcode.app/Contents/Developer/usr/bin/make all SYSCFLAGS="-DLUA_USE_MACOSX" SYSLIBS="-lreadline"
gcc -std=gnu99 -o lua   lua.o liblua.a -lm -lreadline 
ld: warning: ignoring file /usr/local/lib/libreadline.dylib, building for macOS-x86_64 but attempting to link with file built for unknown-i386
Undefined symbols for architecture x86_64:
  "_add_history", referenced from:
      _doREPL in lua.o
  "_readline", referenced from:
      _pushline in lua.o
ld: symbol(s) not found for architecture x86_64
clang: error: linker command failed with exit code 1 (use -v to see invocation)
make[2]: *** [lua] Error 1
make[1]: *** [macosx] Error 2
make: *** [macosx] Error 2
```

So it seems libreadline.dylib is not built for x86_64 architectures. Hmmm. Do I even need to install Lua? OBS comes with a Lua runtime(?) embedded - maybe I can just hack away in TextMate and hope I don't break anything! 

Ha! I found an alternative way in... OBS plugins and someone has thoughtfully created a websocket plugin that allows remote control of OBS - I can use that! https://github.com/Palakis/obs-websocket/releases 

But aaarggghhh... the millicast version of OBS doesn't respond to my efforts to install the plugin. Damn. I send a message to Dr P at Millicast.com - can you help me? I await his reply...


**Setup the server machine**

*   Soundflower

    In ordewr to pipe audio into the OBS broadcaster we need to install SOundFlower - this will intercept any audio happening on the sound card and create a new virtual output. Install Soundflower then:

    Open Audio MIDI Setup: (found in /Applications/Utilities)

    hit the '+' button in the bottom left corner and select "Create Multi Output Device"
in the the panel that appears on the right, select "Built-in Output" AND "Soundflower (2ch)"
then hit the button with the gear icon bottom left and select "use this device for sound output" (you should still be hearing your iTunes output    -- it is now going to both Soundflower and the built-in speakers)

*   Audio is too low when recording and/or during playback of a recording

    Sometimes when Replay Video Capture switches the audio input/output to Soundflower the volume will decrease some. Here's how to make sure that you can hear the audio:

    Go to System Preferences > Audio
    Make note of your current default audio input and output devices so that you can switch them back when we're done changing the Soundflower volume settings.
    In the Output tab, select Soundflower (2ch) then increase the volume slider at the bottom.

    Now re-select your default device for output (that you noted in step 2 above).
    In the Input tab, select Soundflower (2ch) then increase the volume sliders at the bottom.
    Now re-select your default device for input (that you noted in step 2 above).

    You'll now hear the audio louder while recording, and have louder audio in your recordings. You can adjust these settings to your liking for the best audio experience.
    
    Tip: Monitor through internal speakers first to make sure we have audio, then switch to multi-output device...

*   Launch OBS

`$ /Applications/OBS-WebRTC.app/Contents/MacOS/OBS-WebRTC`

**5. Control presentation remotely, mix between live and pre-recorded video/animation & audio**

Need to comm with both Adam  Servewr and OBS
OBS via websockets pplugin (not working)
Server via websockets... but need to learn RxJS
Possible using vvvv? But first try with Angular because...


Covid-19 adaptation
    
No sound from chuck? maybe another VM already running - go to activity monitor and kill the process!

Polyphony => sporking
    