---
id: 272
title: LIS3LV02DQ TRIPLE AXIS ACCELEROMETER + ARDUINO + TINKERPROXY + FLASH
date: 2012-02-17T20:42:36+00:00
author: zerozero
layout: page
categories:
  - code
video: 171226321
disqus: yes
---
This week I have been playing with the [LIS3LV02DQ TRIPLE AXIS ACCELEROMETER](http://www.sparkfun.com/products/758) (now retired) and attempting to get it spitting data into flash.

<div class="img_row">
    <a href="{{ site.baseurl }}/images/uploads/IMG_1712_c.jpg">
        <img class="col one left" src="{{ site.baseurl }}/images/uploads/IMG_1712_c.jpg" alt="" title="accelerometer"/>
	</a>
</div>
<div class="caption_row">
    <div class="col one left caption">Accelerometer and Protoshield</div>
</div>
<br/>


Here's a quick run down of how to set up the system:

**Get this stuff:**

- Arduino
- LIS3LV02DQ TRIPLE AXIS ACCELEROMETER
- Breadboard or Protoshield
- TinkerProxy
- Some way of compiling flash movies
    

**Do it:**

**Plug stuff in**. Plug the accelerometer into the breadboard (I added an LED too so I can run simple tests in my code) and hook up power and connections as in the diagrams below. Lots of interesting info and the original arduino code by [**Julian Bleecker** is here](http://nearfuturelaboratory.com/2006/09/22/arduino-and-the-lis3lv02dq-triple-axis-accelerometer/). Note: these diagrams use a different accelerometer but the pins are equivalent.

<div class="img_row">
	<a href="/images/uploads/acc_ard_fla_bb.jpg">
	<img class="col one left" src="{{ site.baseurl }}/images/uploads/acc_ard_fla_bb.jpg" alt="" title="Triple Axis Accelerometer Fritzing Layout"/>
	</a>
</div>
<div class="col one left caption">
	Triple Axis Accelerometer Fritzing Layout
</div>

<div class="img_row">
<a href="/images/uploads/acc_ard_fla_schem.jpg">
	<img class="col one left" src="{{ site.baseurl }}/images/uploads/acc_ard_fla_schem.jpg" alt="" title="Accelerometer Fritzing Schematic"/>
	</a>
</div>
<div class="col one left caption">
	Accelerometer Fritzing Schematic
</div>
<br/>
  

**Install Arduino and TinkerProxy**. There are very clear instructions by [Mike Chambers here](http://www.mikechambers.com/blog/2010/08/04/getting-started-with-flash-and-arduino/). One thing that caught me out, however, was the config for TinkerProxy (or SerProxy): it is important to make sure that the value for `serial_device1` in the TinkerProxy config on the mac is set to the `/dev/cu_` serial port and NOT the `/dev/tty` serial port. This had me stumped for a while as I had both set to `/dev/tty` and couldn't work out why I was getting EOF from SIO errors.
  
By the way you can get a list of ports on the Mac by opening a terminal and entering `$ ls -l /dev/` at the prompt.

**Program the Arduino.** I downloaded the source from <a href="http://www.nearfuturelaboratory.com/2006/09/22/arduino-and-the-lis3lv02dq-triple-axis-accelerometer/" target="_blank">nearfuturelaboratory.com</a>, merged it with the <a href="http://www.mikechambers.com/blog/2010/08/04/getting-started-with-flash-and-arduino/" target="_blank">blink script from Mike Chambers</a> and made a few modifications: I stripped out all the `serial.print`s so that the only thing that would be sent to flash would be the values for the 3 coordinates "x,y,z" as a comma delimited string (is there a better way to do this?). This is going to make it easy to parse into an array of integers to supply values to my flash code.

**Connect it all together**. Arduino hooks up to computer via USB as usual, the accelerometer gets it's power from the 3.3V pin (don't know why [Julian Bleecker](http://nearfuturelaboratory.com/2006/09/22/arduino-and-the-lis3lv02dq-triple-axis-accelerometer/) uses the 5V pin). Open the Arduino IDE and select the correct flavour of board from _Tools -> Board__ and select the correct port from _Tools -> Serial Port_. Start a new project and open the _Accelerometer_Arduino.pde_ script. Upload the script to the Arduino board and open the serial monitor. You can toggle the LED's blinking by entering "t" and SEND. Should now see a stream of data in the serial monitor. Wave the accelerometer around and watch the values changing. If you place any of the chip’s axes normal to the ground, you should see the value 1024 (or -1024). If all that works ok the next thing to do is set up and run TinkerProxy.

**Tinker Proxy**. This is my config file, I'm running OSX 10.6 my flash movie is going to be written in AS3

Config file for serproxy

See serproxy's README file for documentation


```csharp
#Transform newlines coming from the serial port into nils
#true (e.g. if using Flash) or false
newlines_to_nils=false
#on a mac you will need to add this
serial_device1=/dev/cu.usbmodem3d11
#Comm ports used
comm_ports=1  
#Make sure to use the /dev/cu serial port. Earlier versions of actionscript required newlines_to_nils=true
#Default settings
comm_baud=9600  
comm_databits=8  
comm_stopbits=1  
comm_parity=none 
#Baud rate can be set to any legal value.  
#Idle time out in seconds
timeout=300
#Port 1 settings (ttyS0)
net_port1=5331
```
  
The port number must match the port number defined in the Arduino script.

_Rename serproxy.cfg to serproxy.osx.cfg_ if you're on a mac. Run TinkerProxy from a terminal session by cd'ing to the TinkerProxy directory and entering `$ ./serproxy.osx` you should get a "_Waiting for clients_" message.


```csharp  
//The digital pin that the LED is connected to
  
#define LED_PIN 8
  
```
  
I changed the pin for the LED to 8 from 13 as the accelerometer is using Pin 13
  
```csharp

        Serial.print(x_val, DEC);
      //..
        Serial.print(","); Serial.print(y_val, DEC);
      //..
        Serial.print(","); Serial.println(z_val, DEC);
    

```

I removed all `Serial.print`s and modified the code so that arduino will send just the 3 integer values from the accelerometer as a comma delimited string.

**Create the flash movie.** I'm using Flash Builder 4.5 to create a pure actionscript project. I have the greensock tweenlite library in my library path and minimalcomps src in my build path so I can use some simple components for the interface and apply some tweening to the resulting animation to smooth out the motion of the sliders and knobs Ill be using. The finished interface is below. I should be able to see the values updating in near real-time as I wave the accelerometer around.

![screenshot](/images/uploads/acc_ard_fla_screenshot1.jpg)
<div class="col three caption">
	Flash interface screenshot
</div>
<br/>

For the main part the actionscript code is the same as Cliff's but I created a new **Widget** class that is going to create a strip of controls for one axis comprising a Knob component to display angle, and three sliders for acceleration, velocity and distance. Here's the createChildren method, I have given each of the controls appropriate min and max values:
  
```csharp

/**
* create the child components         
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
}
```
    

And here is the update method that will get called every time we get data from the arduino:
  
```csharp
/**
           
* update the gui
* @param val - the reading from one axis of the accelerometer           
* @param ms - the elapsed time in milliseconds           
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
}
```

Each call to update will be received by the Widget responsible for displaying the data for one of the axes. The parameters are **val** (an int) which is the value for the axis and **ms** which is the number of milliseconds elapse since the last update.

  1. I convert milliseconds to seconds by dividing by 1000. 
  2. I set the variable **a** to val and I subtract 1024 to compensate for gravity. 
  3. Next I calculate velocity (**v**) which is the acceleration (**a**) multiplied by elapsed time in seconds (**dt**). 
  4. To calculate distance (**d**) I multiply velocity by **dt** again. 
  5. The final value rotation (**r**) uses `private var g\_to\_dir :Number = 360/4096` to calculate a rotation in degrees of the axis from straight down.

Since the accelerometer can only measure angle and not direction we don't know if the accelerometer is tipped +20 or -20 degrees.. we just know it's 20.. is there a way to fix this?
  
Finally I run the values through a `TweenLite.to` method to add a little smoothness to the movement.
  
If you look in the Main.as file you'll see where I'm creating the widget instances and where I'm calling the update code for each one.
  
And that should be it. If everything is working correctly the sliders and knob should update as you wave the accelerometer around. Here's a video of it in action:
  
{% include vimeoplayer.html id=page.video %}
  <br/>
**Links:**
[LIS3LV02DQ accelerometer setup](http://www.nearfuturelaboratory.com/2006/09/22/arduino-and-the-lis3lv02dq-triple-axis-accelerometer)  
[TinkerProxy](http://code.google.com/p/tinkerit/downloads/list)  
[Flash Arduino setup](http://www.mikechambers.com/blog/2010/08/04/getting-started-with-flash-and-arduino/)  
[MinimalComps](http://www.minimalcomps.com/)
  


