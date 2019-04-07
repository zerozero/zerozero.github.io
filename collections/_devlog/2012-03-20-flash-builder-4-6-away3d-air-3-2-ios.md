---
id: 128
title: Flash Builder 4.6 + Away3D + AIR 3.2 + iOS
date: 2012-03-20T22:29:02+00:00
author: zerozero
layout: post
categories:
  - code
disqus: yes
---
Here's how I got a basic barebones Away3D scene running under AIR 3.2 on iOS built with Flash Builder 4.6 on OS X. 

**Get Stuff:**
  
The github repo for this project is <a href="https://github.com/zerozero/Flash-Builder-4.6---Away3D---AIR-3.2---iOS-Barebones" target="_blank">here</a>.
  
Install the new sdk, instructions <a href="http://jeffwinder.blogspot.com/2011/09/installing-adobe-air-3-sdk-in-flash.html" target="_blank">here</a> and <a href="http://www.flashdeveloper.co/post/10985842021/installing-the-air3-sdk-on-flash-builder-4-5-1#disqus_thread" target="_blank">here</a>.

Both are written for 3.1 but I've done the same process for 3.2 and it applied correctly

Note in step 3. of Jeff Winder's instructions be sure to make a copy of the current sdk folder i.e. the one named 4.5.1 or suchlike. NOT the sdks folder itself - I made that mistake and had to start again when I couldn't figure out why the build script wasn't finding stuff. The aim of the game here is to copy your current SDK folder - the one that FB is using as the default SDK - so that all settings and paths remain the same, then overwriting only the necessary files to produce an updated SDK with the new AIR 3.2 distribution in place.

So assuming this is done and you have obtained the necessary provisioning files, certificates etc from Apple and provisioned your device (<a href="http://www.adobe.com/devnet/air/articles/packaging-air-apps-ios.html" target="_blank">this is quite a good explanation of that process</a>) - we can start to set up the project.

**Do Stuff:**

Start a new Mobile Project in FB. File -> New -> Flex Mobile Project.
  
Settings:
  
Use Default SDK (which should be the new SDK created above)
  
Mobile Settings:
  
iOS only (I haven't tried the others yet)
  
View Based Application (I have tried blank but it is slightly different, Tabbed should be the same as it uses a UIViewNavigator as it's top level View)
  
No Server
  
Build Path -> Source Path - enter the path to <a href="http://away3d.com/download/" target="_blank">Away3D fp11</a> core directory or Library Path if using the swc

Now open Project properties and paste [cc inline=&#8221;true&#8221;]-swf-version=15[/cc] in compiler arguments

Open My-app.xml in the src folder and make the following changes -
  
Make sure the xml namespace correctly refers to AIR 3.2
  
`<application xmlns="http://ns.adobe.com/air/application/3.2">`
  
Note - I tried using **3.2.0** after I had an error alerting me that the version I should use was 3.2.0 not 3.1.0 but the trailing zero will throw an error.

uncomment these lines and add these values:
  
```csharp
<renderMode>direct</renderMode>
<depthAndStencil>true</depthAndStencil>
```

On to the code.

There are 4 main classes needed in my setup.
  
**Barebones.mxml** - the main App
  
**views.BarebonesHomeView.mxml** - the top level view component
  
**views.UIComponent3D** - a UIComponent to wrap the Away3D sprite since sprites can't be directly added to a View(they're not an mx.core.IVisualComponent)
  
**views.Canvas3D** - the sprite that is composed of the Away3D scene

Lets look at each one in turn:

**Barebones.mxml**

```csharp
<?xml version="1.0" encoding="utf-8"?>
  
<s:ViewNavigatorApplication xmlns:fx="http://ns.adobe.com/mxml/2009"
                              
xmlns:s="library://ns.adobe.com/flex/spark" firstView="views.BarebonesHomeView" applicationDPI="160" frameRate="60">
      
<fx:Declarations>
          
<!-- Place non-visual elements (e.g., services, value objects) here -->


      
</fx:Declarations>
  
</s:ViewNavigatorApplication>
```

Not much going on here.. applicationDPI is set to 160, framerate is 60. The [cc inline=&#8221;true&#8221;]firstView[/cc] property is the reference to the View that will be first visible on load. We won't use any other views here but the **ViewNavigatorApplication** could handle loading and unloading views if we were.

**views.BarebonesHomeView.mxml**

```csharp
<?xml version="1.0" encoding="utf-8"?>


  
<s:View xmlns:fx="http://ns.adobe.com/mxml/2009"
          
xmlns:s="library://ns.adobe.com/flex/spark"
          
title="HomeView"
          
xmlns:views="views.*"
          
backgroundAlpha="0"
          
addedToStage="onAdded(event)">
      
<fx:Script>
          
<![CDATA[

            protected function onAdded(event:Event):void
            {
                fitToStage();
                stage.addEventListener(StageOrientationEvent.ORIENTATION_CHANGE,stageOrientationChangeHandler);
            }
    
            private function stageOrientationChangeHandler( e:StageOrientationEvent ):void{
                fitToStage();
            }
    
            /**
             * Fits the container3D component to stage size
             * taking into account 44 pixel high navigation bar
             * */
            private function fitToStage():void{
                container3D.width = stage.stageWidth;
                container3D.height = stage.stageHeight - 44;
            }
    
        ]]>
    </fx:Script>
    <fx:Declarations>
        <!-- Place non-visual elements (e.g., services, value objects) here -->
    </fx:Declarations>
    <views:UIComponent3D id="container3D"/>
    

</s:View>

```
  
Notice the addedToStage event is set to call the onAdded method; at this point we know we have a reference to the stage as the component is now on the display list.

In the onAdded method we call [cc inline=&#8221;true&#8221;]fitToStage()[/cc] which sizes the component to the width and height minus 44 pixels to take into account the height of the navigator bar.
  
Finally we add the UIComponent3D and give it a name of container3D.

**Very important:** Views have a white background set to alpha=1 by default. Since Stage3D always renders behind other flash content we **MUST** set alpha=0 otherwise you can't see the 3D scene.

**views.UIComponent3D**

```csharp
package views
  
{
      
import flash.events.Event;

    import mx.core.UIComponent;
    
    
    /**
     * This class is simply a UIComponent wrapper for the Canvas3D class which
     * is a Sprite so can't be directly added to the flex component
     * @author jonrowe
     *
     */
    public class UIComponent3D extends UIComponent
    {
    
    
        private var canvas3D :Canvas3D;
    
        public function UIComponent3D()
        {
            super();
        }
    
        /**
         * overide the createChildren method of UIComponent and add a Canvas3D instance as a child
         *
         */
        override protected function createChildren():void
        {
            super.createChildren();
            if (!canvas3D)
                create3DView();
        }
    
        /**
         * override the updateDisplayList method so we can set the size of the away3D view
         * @param unscaledWidth
         * @param unscaledHeight
         *
         */
        override protected function updateDisplayList(
            unscaledWidth:Number, unscaledHeight:Number):void
        {
    
            super.updateDisplayList(unscaledWidth, unscaledHeight);
            //canvas3D is a sprite not a UIComponent so we must manually set it's size
            canvas3D.setSize(unscaledWidth,unscaledHeight);
        }
    
    
        /*          PRIVATE         */
    
        /**
         * create the canvas3D, listen for sceneReady event
         *
         */
        private function create3DView():void{
    
            canvas3D = new Canvas3D();
            addChild (canvas3D);
            canvas3D.addEventListener(Canvas3D.SCENE_READY, onSceneReady);
    
        }
    
        /**
         * called when the 3D scene has been initialized and added to the stage.
         * @param e
         *
         */
        private function onSceneReady( e:Event ):void{
            //
        }
    
    
    
    
    }
    

}
  
```

This is a very simple UIComponent whose job is simply to create and display the Canvas3D sprite. Sprites can't be added directly to Views since they are not IVisualComponents so this UIComponent will act as a wrapper.

We override the createChildren method which gets called automatically when the component is added to the display list. We also override the updateDisplayList method which is called whenever the component is invalidated and needs to redraw such as on orientation changes.

The createView method creates and adds the canvas3D instance, it will only be called once. It also sets up a listener for the SCENE_READY event that calls the onSceneReady event handler. This is unused in this instance but could be a good place to do things that are reliant on the scene being ready..

**views.Canvas3D**

This is the main Away3D class and should be familiar to anyone who's used Away3D. There's just a couple of things to note. It's quite long so I won't post the whole lot but you can download from the github repo.

In the constructor we set up a listener for the ADDED\_TO\_STAGE event so we can defer building the scene until we know that the sprite has been added to the display list.

```csharp
private function onAddedToStage( e:Event ):void{
    initView();
    initLights();
    initCamController();
    initListeners();
    initScene();
    start();
    dispatchEvent( new Event(SCENE_READY));
    this.removeEventListener(Event.ADDED_TO_STAGE, onAddedToStage );
}
```

Here we:

  * initView - create the View3D instance and add it as a child
  * initLights() - create a couple of lights to light the scene
  * initCamController() - create a simple HoverCam controller
  * initListeners() - listen for objects being added to or removed from the scene - they're not used here but could be useful later.
  * initScene() - create a wireframe grid, a cube and the awayStats instance and add them as appropriate

next we call the start() method:
  
```csharp
public function start():void{
              
addEventListener(Event.ENTER_FRAME, onEnterFrame);
              
//need to call render here otherwise enterFrame events don't happen?!
              
_view3D.render();
          
}
  
```
  
An odd thing I experienced was that although it worked correctly in the simulator, when I packaged it and put it on my iPad the Event.ENTER_FRAME event _would not fire at all_ unless I called `_view3D.render();` manually here - weird.

And that's about it. Package it up and throw it onto your device and you should see DRIV: OpenGL in the stats window and a nice orange cube in the centre of the scene
  
I get a good steady 60-ish fps for this simple scene.. next up testing with many more polys.

Here are some notes on gotchas that I experienced when I was trying to get this to run:

* When configuring the air project the application descriptor namespace should read like this:

`<application xmlns="http://ns.adobe.com/air/application/3.2">`
I.E. NOT 3.2.0 but 3.2

* For stage3d we need to set

`<renderMode>direct</renderMode>`

and

`<depthAndStencil>true</depthAndStencil>`

in the application descriptor
* Make sure -swf-version=15 is set in compiler arguments
* When loading the away3D sprite into a View component remember to set the View's backgroundAlpha = 0</ul> 
