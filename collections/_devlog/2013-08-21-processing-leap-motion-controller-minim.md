---
id: 245
title: Processing + Leap Motion Controller + Minim
date: 2013-08-21T21:42:23+00:00
author: zerozero
layout: post
categories:
  - code
video: 72856070
disqus: yes
---
**Objective:**
  
I want to control a synthesised sound by moving my hand in space.

**Requirements:**

[Processing](http://processing.org)
  
<a href="https://www.leapmotion.com" target="_blank">Leap Motion Controller</a>
  
<a href="https://github.com/heuermh/leap-motion-processing" target="_blank">Leap Motion library for Processing Library</a>
  
<a href="http://code.compartmental.net/tools/minim/" target="_blank">Minim Audio Library for Processing</a> 


Here's the result:

{% include vimeoplayer.html id=page.video %}
  <br/>

And here's the processing code with comments:

```csharp
  
import com.leapmotion.leap.Controller;
import com.leapmotion.leap.Frame;  
import com.leapmotion.leap.Gesture;  
import com.leapmotion.leap.Hand;  
import com.leapmotion.leap.HandList;  
import com.leapmotion.leap.Vector;  
import com.leapmotion.leap.processing.LeapMotion;
import ddf.minim.spi.*;  
import ddf.minim.signals.*;  
import ddf.minim.*;  
import ddf.minim.analysis.*;  
import ddf.minim.ugens.*;  
import ddf.minim.effects.*;

LeapMotion leapMotion;
Minim minim;
AudioOutput out;

// the Oscil we use for modulating frequency.  
Oscil fm;

void setup()  
{
     
	size(9_50, 9_50);     
	background(20);
	leapMotion = new LeapMotion(this);     
	minim = new Minim(this);

	// get a stereo line out with a sample buffer of 512 samples    
	out = minim.getLineOut(Minim.MONO, 512);

	// make the Oscil we will hear.    
	// arguments are frequency, amplitude, and waveform    
	Oscil wave = new Oscil( 200, 0.8, Waves.TRIANGLE );

	// make the Oscil we will use to modulate the frequency of wave.    
	// the frequency of this Oscil will determine how quickly the    
	// frequency of wave changes and the amplitude determines how much.    
	// since we are using the output of fm directly to set the frequency    
	// of wave, you can think of the amplitude as being expressed in Hz.    
	fm = new Oscil( 10, 2, Waves.SINE );
    
	// set the offset of fm so that it generates values centered around 200 Hz    
	fm.offset.setLastValue( 200 );
    
	// patch it to the frequency of wave so it controls it    
	fm.patch( wave.frequency );
    
	// and patch wave to the output    
	wave.patch( out );
  
}

void draw()  
{
    
	fill(20);    
	rect(0, 0, width, height);

	// draw using a white stroke    
	stroke( 255 );
    
	// draw the waveforms    
	for( int i = 0; i < out.bufferSize() – 1; i++ )
    
	{      
		// find the x position of each buffer value      
		float x1 = map( i, 0, out.bufferSize(), 0, width );      
		float x2 = map( i+1, 0, out.bufferSize(), 0, width );

		// draw a line from one buffer position to the next for both channels
	    line( x1, 50 + out.left.get(i)*50, x2, 50 + out.left.get(i+1)*50);
	    line( x1, 150 + out.right.get(i)*50, x2, 150 + out.right.get(i+1)*50);  

	}
  
}

// here we provide a way to mute out
  
void keyPressed()
  
{
    
	if ( key == 'm' )    
	{      
		if ( out.isMuted() )      
		{
        	out.unmute();
      
		}      
		else      
		{
        	out.mute();      
		}    
	}
  
}

//called reflexively by the leap controller
  
void onInit(final Controller controller)
  
{
    
	controller.enableGesture(Gesture.Type.TYPE_CIRCLE);    
	controller.enableGesture(Gesture.Type.TYPE\_KEY\_TAP);    
	controller.enableGesture(Gesture.Type.TYPE\_SCREEN\_TAP);    
	controller.enableGesture(Gesture.Type.TYPE_SWIPE);    
	// enable background policy    
	controller.setPolicyFlags(Controller.PolicyFlag.POLICY\_BACKGROUND\_FRAMES);
  
}

// we can change the parameters of the frequency modulation Oscil  
// in real-time using the hand.  
void onFrame(final Controller controller)
  
{
    
	Frame frame = controller.frame();

	HandList hands = frame.hands();

	if (hands.count() > 0){
     
		Hand hand = hands.get(0);     
		Vector pos = hand.palmPosition();     
		println(&#8220;x &#8220;+pos.getX()+&#8221; y &#8220;+pos.getY());     
		float x = pos.getX();     
		float y = pos.getY();     
		float modulateAmount = map( y, 0, height, 220, 1 );     
		float modulateFrequency = map( x, 0, width, 0.1, 100 );
		fm.frequency.setLastValue( modulateFrequency );     
		fm.amplitude.setLastValue( modulateAmount );

	}

}

//remember to stop the sound before closing  
void stop()  
{
    out.close();    
	minim.stop();    
	super.stop();  
}
  
```

