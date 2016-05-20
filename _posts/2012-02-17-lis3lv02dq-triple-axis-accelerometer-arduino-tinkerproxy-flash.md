---
id: 272
title: LIS3LV02DQ TRIPLE AXIS ACCELEROMETER + ARDUINO + TINKERPROXY + FLASH
date: 2012-02-17T20:42:36+00:00
author: zerozero
layout: post
categories:
  - code
---
This week I have been playing with the [LIS3LV02DQ TRIPLE AXIS ACCELEROMETER](http://www.sparkfun.com/products/758) (now retired) and attempting to get it spitting data into flash.<!--more-->

<div id="attachment_51" style="width: 234px" class="wp-caption alignnone">
  <a href="http://162.13.3.34/labs/wp-content/uploads/2012/02/IMG_1712_c.jpg"><img class="size-medium wp-image-51" title="LIS3LV02DQ Breakout" src="http://162.13.3.34/labs/wp-content/uploads/2012/02/IMG_1712_c-224x300.jpg" alt="LIS3LV02DQ" width="224" height="300" /></a>
  
  <p class="wp-caption-text">
    Accelerometer and Protoshield
  </p>
</div>

Here&#8217;s a quick run down of how to set up the system:

**Get this stuff:**

    Arduino
    LIS3LV02DQ TRIPLE AXIS ACCELEROMETER
    Breadboard or Protoshield
    TinkerProxy
    Some way of compiling flash movies
    

**Do it:**

**Plug stuff in**. Plug the accelerometer into the breadboard (I added an LED too so I can run simple tests in my code) and hook up power and connections as in the diagrams below. Lots of interesting info and the original arduino code by [**Julian Bleecker** is here](http://nearfuturelaboratory.com/2006/09/22/arduino-and-the-lis3lv02dq-triple-axis-accelerometer/). Note: these diagrams use a different accelerometer but the pins are equivalent.

<div id="attachment_53" style="width: 310px" class="wp-caption alignnone">
  <a href="http://162.13.3.34/labs/wp-content/uploads/2012/02/acc_ard_fla_bb.jpg"><img src="http://162.13.3.34/labs/wp-content/uploads/2012/02/acc_ard_fla_bb-300x251.jpg" alt="Triple Axis Accelerometer Fritzing Layout" title="acc_ard_fla_bb" width="300" height="251" class="size-medium wp-image-53" /></a>
  
  <p class="wp-caption-text">
    Triple Axis Accelerometer Fritzing Layout
  </p>
</div>

  


<div id="attachment_52" style="width: 259px" class="wp-caption alignnone">
  <a href="http://162.13.3.34/labs/wp-content/uploads/2012/02/acc_ard_fla_schem.jpg"><img src="http://162.13.3.34/labs/wp-content/uploads/2012/02/acc_ard_fla_schem-249x300.jpg" alt="Accelerometer Fritzing Schematic" title="acc_ard_fla_schem" width="249" height="300" class="size-medium wp-image-52" /></a>
  
  <p class="wp-caption-text">
    Accelerometer Fritzing Schematic
  </p>
</div>

**Install Arduino and TinkerProxy**. There are very clear instructions by [Mike Chambers here](http://www.mikechambers.com/blog/2010/08/04/getting-started-with-flash-and-arduino/). One thing that caught me out, however, was the config for TinkerProxy (or SerProxy): it is important to make sure that the value for [cc inline=&#8221;true&#8221;]serial_device1[/cc] in the TinkerProxy config on the mac is set to the **/dev/cu_</strong> serial port and NOT the **/dev/tty</em>** serial port. This had me stumped for a while as I had both set to /dev/tty* and couldn&#8217;t work out why I was getting EOF from SIO errors (see footnote).<sup>[<a name="id394062" href="#ftn.id394062">*</a>]</sup>
  
By the way you can get a list of ports on the Mac by opening a terminal and entering [cc inline=&#8221;true&#8221;]$ ls -l /dev/[/cc] at the prompt.</p> 

**Program the Arduino.** I downloaded the source from <a href="http://www.nearfuturelaboratory.com/2006/09/22/arduino-and-the-lis3lv02dq-triple-axis-accelerometer/" target="_blank">nearfuturelaboratory.com</a>, merged it with the <a href="http://www.mikechambers.com/blog/2010/08/04/getting-started-with-flash-and-arduino/" target="_blank">blink script from Mike Chambers</a> and made a few modifications: I stripped out all the [cc inline=&#8221;true&#8221;]serial.print[/cc]s so that the only thing that would be sent to flash would be the values for the 3 coordinates &#8220;x,y,z&#8221; as a comma delimited string (is there a better way to do this?). This is going to make it easy to parse into an array of integers to supply values to my flash code.

**Connect it all together**. Arduino hooks up to computer via USB as usual, the accelerometer gets it&#8217;s power from the 3.3V pin (don&#8217;t know why [**Julian Bleecker**](http://nearfuturelaboratory.com/2006/09/22/arduino-and-the-lis3lv02dq-triple-axis-accelerometer/) uses the 5V pin). Open the Arduino IDE and select the correct flavour of board from **Tools -> Board** and select the correct port from **Tools -> Serial Port**. Start a new project and open the **Accelerometer_Arduino.pde** script. Upload the script to the Arduino board and open the serial monitor. You can toggle the LED&#8217;s blinking by entering &#8220;t&#8221; and SEND. Should now see a stream of data in the serial monitor. Wave the accelerometer around and watch the values changing. If you place any of the chip’s axes normal to the ground, you should see the value 1024 (or -1024). If all that works ok the next thing to do is set up and run TinkerProxy.

**Tinker Proxy**. This is my config file, I&#8217;m running OSX 10.6 my flash movie is going to be written in AS3[cc lang=&#8221;bash&#8221; highlight=&#8221;5,7&#8243;]

# Config file for serproxy

# See serproxy&#8217;s README file for documentation

# Transform newlines coming from the serial port into nils

# true (e.g. if using Flash) or false

newlines\_to\_nils=false

# on a mac you will need to add this

serial_device1=/dev/cu.usbmodem3d11

# Comm ports used

comm_ports=1
  
[/cc]Make sure to use the **[cc inline=&#8221;true&#8221;]/dev/cu*[/cc]** serial port. Earlier versions of actionscript required [cc inline=&#8221;true&#8221;]newlines\_to\_nils=true[/cc]
  
[cc lang=&#8221;bash&#8221;]

# Default settings

comm_baud=9600
  
comm_databits=8
  
comm_stopbits=1
  
comm_parity=none
  
[/cc]
  
Baud rate can be set to any legal value.
  
[cc lang=&#8221;c++&#8221;]

# Idle time out in seconds

timeout=300

# Port 1 settings (ttyS0)

net_port1=5331[/cc]
  
The port number must match the port number defined in the Arduino script.

**Rename serproxy.cfg to serproxy.osx.cfg** if you&#8217;re on a mac. Run TinkerProxy from a terminal session by cd&#8217;ing to the TinkerProxy directory and entering [cc inline=&#8221;true&#8221;]$ ./serproxy.osx[/cc] you should get a &#8220;**Waiting for clients**&#8221; message.

[cc lang=&#8221;cpp&#8221;]
  
//The digital pin that the LED is connected to
  
#define LED_PIN 8
  
[/cc]
  
I changed the pin for the LED to 8 from 13 as the accelerometer is using Pin 13
  
[cc lang=&#8221;cpp&#8221;]

        Serial.print(x_val, DEC);
      //..
        Serial.print(","); Serial.print(y_val, DEC);
      //..
        Serial.print(","); Serial.println(z_val, DEC);
    

[/cc]

I removed all [cc inline=&#8221;true&#8221;]Serial.print[/cc]s and modified the code so that arduino will send just the 3 integer values from the accelerometer as a comma delimited string.

**Create the flash movie.** I&#8217;m using Flash Builder 4.5 to create a pure actionscript project. I have the greensock tweenlite library in my library path and minimalcomps src in my build path so I can use some simple components for the interface and apply some tweening to the resulting animation to smooth out the motion of the sliders and knobs Ill be using. The finished interface is below. I should be able to see the values updating in near real-time as I wave the accelerometer around.

<div id="attachment_103" style="width: 413px" class="wp-caption alignnone">
  <a href="http://162.13.3.34/labs/wp-content/uploads/2012/02/acc_ard_fla_screenshot1.jpg"><img src="http://162.13.3.34/labs/wp-content/uploads/2012/02/acc_ard_fla_screenshot1.jpg" alt="Flash interface screenshot" title="acc_ard_fla_screenshot" width="403" height="403" class="size-full wp-image-103" /></a>
  
  <p class="wp-caption-text">
    Flash interface screenshot
  </p>
</div>

For the main part the actionscript code is the same as Cliff&#8217;s but I created a new **Widget** class that is going to create a strip of controls for one axis comprising a Knob component to display angle, and three sliders for acceleration, velocity and distance. Here&#8217;s the createChildren method, I have given each of the controls appropriate min and max values:
  
[cc lang=&#8221;actionscript3&#8243;]/**
           
* create the child components
           
*
           
*/
          
private function createChildren():void{

            var label :Label = new Label(this, 0,0, axis+" AXIS");
    
            knob = new Knob(this, 0, 20, "rotation");
            knob.minimum = 0;
            knob.maximum = 180;
            knob.value = 0;
    
            slider1 = new HUISlider(this, 120,30, "acceleration");
            slider1.minimum = -1024;
            slider1.maximum = 1024;
            slider1.value = 0;
    
            slider2 = new HUISlider(this, 120,50, "velocity");
            slider2.minimum = -1000;
            slider2.maximum = 1000;
            slider2.value = 0;
    
            slider3 = new HUISlider(this, 120,70, "distance");
            slider3.minimum = 0;
            slider3.maximum = 2;
            slider3.value = 0;
        }[/cc]
    

And here is the update method that will get called every time we get data from the arduino:
  
[cc lang=&#8221;actionscript3&#8243;]/**
           
* update the gui
           
* @param val &#8211; the reading from one axis of the accelerometer
           
* @param ms &#8211; the elapsed time in milliseconds
           
*
           
*/
          
public function update( val :int, ms :int ):void{

            var dt :Number = ms/1000.;//convert milliseconds to seconds
            var a :int = val - 1024;//compensate for gravity
            var v : Number = a*dt;//calculate velocity
            var d : Number = v*dt;//calculate distance
            var r : Number = a*g_to_dir;//convert g force to direction
    
    
            //add a little smoothing with tweenlite
            TweenLite.to(knob,0.1,{"value": r});
            TweenLite.to(slider1,0.1,{"value": a});
            TweenLite.to(slider2,0.1,{"value": v});
            TweenLite.to(slider3,0.1,{"value": d});
        }[/cc]
    

Each call to update will be received by the Widget responsible for displaying the data for one of the axes. The parameters are **val** (an int) which is the value for the axis and **ms** which is the number of milliseconds elapse since the last update.

  1. I convert milliseconds to seconds by dividing by 1000. 
  2. I set the variable **a** to val and I subtract 1024 to compensate for gravity. 
  3. Next I calculate velocity (**v**) which is the acceleration (**a**) multiplied by elapsed time in seconds (**dt**). 
  4. To calculate distance (**d**) I multiply velocity by **dt** again. 
  5. The final value rotation (**r**) uses [cc inline=&#8221;true&#8221;]private var g\_to\_dir :Number = 360/4096[/cc] to calculate a rotation in degrees of the axis from straight down.

Since the accelerometer can only measure angle and not direction we don&#8217;t know if the accelerometer is tipped +20 or -20 degrees.. we just know it&#8217;s 20.. is there a way to fix this?
  
Finally I run the values through a [cc inline=&#8221;true&#8221;]TweenLite.to[/cc] method to add a little smoothness to the movement.
  
If you look in the Main.as file you&#8217;ll see where I&#8217;m creating the widget instances and where I&#8217;m calling the update code for each one.
  
And that should be it. If everything is working correctly the sliders and knob should update as you wave the accelerometer around. Here&#8217;s a video of it in action:
  
\[box color=&#8221;blue&#8221;]Download the code for this project[button type=&#8221;download&#8221;]download[/button\]\[/box\]
  
Credits:

LIS3LV02DQ accelerometer setup:
  
<http://www.nearfuturelaboratory.com/2006/09/22/arduino-and-the-lis3lv02dq-triple-axis-accelerometer/>

TinkerProxy:
  
<http://code.google.com/p/tinkerit/downloads/list>

Flash Arduino setup:
  
<http://www.mikechambers.com/blog/2010/08/04/getting-started-with-flash-and-arduino/>

MinimalComps:
  
<http://www.minimalcomps.com/>

<div class="footnote">
  <p>
    <sup>[<a name="ftn.id394062" href="#id394062">*</a>]</sup>[box color=&#8221;grey&#8221;]<br /> The idea is to supplement software in sharing a line between incoming and outgoing calls. The callin device (typically /dev/tty*) is used for incoming traffic. Any process trying to open it blocks within the open() call as long as DCD is not asserted by hardware (i.e. as long as the modem doesn&#8217;t have a carrier). During this, the callout device (typically /dev/cu* &#8212; cu stands for &#8220;calling unit&#8221;) can be freely used. Opening /dev/cu* doesn&#8217;t require DCD to be asserted and succeeds immediately. Once succeeded, the blocked open() on the callin device will be suspended, and cannot even complete when DCD is raised, until the cu device is closed again.[/box]
  </p>
</div>

<div class="gk-social-buttons">
  <span class="gk-social-label">Share:</span> <a class="gk-social-twitter" href="http://twitter.com/share?text=LIS3LV02DQ+TRIPLE+AXIS+ACCELEROMETER+%2B+ARDUINO+%2B+TINKERPROXY+%2B+FLASH&url=http%3A%2F%2F162.13.3.34%3A8079%2Flabs%2F%3Fp%3D272"
	            onclick="window.open(this.href, 'twitter-share', 'width=550,height=235');return false;"> <span class="social__icon--hidden">Twitter</span> </a> <a class="gk-social-fb" href="https://www.facebook.com/sharer/sharer.php?u=http%3A%2F%2F162.13.3.34%3A8079%2Flabs%2F%3Fp%3D272"
			     onclick="window.open(this.href, 'facebook-share','width=580,height=296');return false;"> <span class="social-icon-hidden">Facebook</span> </a> <a class="gk-social-gplus" href="https://plus.google.com/share?url=http%3A%2F%2F162.13.3.34%3A8079%2Flabs%2F%3Fp%3D272"
	           onclick="window.open(this.href, 'google-plus-share', 'width=490,height=530');return false;"> <span class="social__icon--hidden">Google+</span> </a>
</div>