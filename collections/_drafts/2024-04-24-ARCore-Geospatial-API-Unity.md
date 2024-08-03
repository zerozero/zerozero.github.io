---
title: Follow Me!
project: true
layout: post
categories:
  - projects
  - unity
  - realtime
  - AR
  - AI
tags:
  - projects
description: A novel augmented reality interface for walking directions
video: 395757574
tech:
  - Unity
---

# Follow Me!
### _ARCore Geospatial API & Unity ml-agents_

This prototype aims to create a novel AR interface for walking directions. The premise is as follows:
- The app opens and a cute robot appears in front of the user in AR
- "Where would you like to go?" says the robot
- The user speaks their destination
- The app connects to the google maps api and retrieves a route to the destination
- "Sure", says the robot, "Follow me!"
- The robot moves off along the route, avoiding obstacles and never getting too far ahead of the user so it is always in view
- As the user follows along the robot moves further along until both the user and the robot are at the destination

see: https://developers.google.com/ar/develop/geospatial

- Install ARFoundation latest version
- Install ARKit
- Install ARCoreExtensions
    - Install package from git URL: `https://github.com/google-ar/arcore-unity-extensions.git`
- Set up Credentials for ARCoreExtensions
    - Create or retrieve API Key from Google Cloud Console `https://console.cloud.google.com/apis/credentials`
- Set up XR Scene
    - Add XROrigin
    - Add AR Session
    - Add ARAnchorManager component to XROrigin
    - Add AREarthManager component to XROrigin
    - Add ARCoreExtensions to hierarchy
<div class="img_row">
  <img class="col two" src="{{ site.baseurl }}/images/ARCoreGeospatial/ARCoreExtensions.png" alt="" title="Add ARCoreExtensions"/>
</div>
<div class="col two caption">
	Add ARCoreExtensions
</div><br><br>
  - Add `ARCORE_USE_ARF_5` to Scripting Define Symbols in Player Settings
  - Create XR -> ARCoreExtensionsConfig asset
  - Set the properties of thew ARCoreExtensions component
<div class="img_row">
  <img class="col one" src="{{ site.baseurl }}/images/ARCoreGeospatial/ARCE_properties.png" alt="" title="ARCoreExtensions properties"/>
</div>
<div class="col one caption">
	ARCoreExtensions properties
</div><br><br>
- Open Project Settings -> XR Plug-in Management -> ARCore Extensions
    - Check 'iOS Support Enabled'
    - Set 'iOS Authentication Strategy' to 'API Key' *(see below for alternative config options)*
    - Paste API Key into 'iOS API Key' field
    - Also set 'Android Authentication Strategy' to 'API Key' and paste API Key into 'Android API Key' field even if not
      used
    - Fix for EXC_BAD_ACCESS crash on iOS https://github.com/google-ar/arcore-unity-extensions/issues/154 development
      build with script debugging enabled (add -ld64 to the 'Other Linker Flags')
<div class="img_row">
  <img class="col three" src="{{ site.baseurl }}/images/ARCoreGeospatial/EXC_BAD_ACCESS.png" alt="" title="Fix for EXC_BAD_ACCESS"/>
</div>
<div class="col three caption">
	Fix for EXC_BAD_ACCESS
</div>

- Input.location will not work at runtime with out the old input system.
  - Check that Project Settings > Player > Other Settings > Active Input Handling is set to Both or Input Manager (Old)
# Geospatial Creator
see: https://developers.google.com/ar/geospatialcreator/unity/quickstart

Get the cesium package: https://github.com/CesiumGS/cesium-unity/releases/ - this should be saved to a location that will be committed to VCS


### Routing
~~Install Google.Maps.Routing.V2 from nuget
Download nuget package from nuget.org. unpack it as zip and copy dll from lib/netstandard2.0 or lib/netstandard2.1(for unity 2021.2) to you project
https://www.nuget.org/packages?q=Google.Maps.Routing.V2&tfms=netstandard2.1%2Cnetstandard2.0&includeComputedFrameworks=true&frameworkFilterMode=all&prerel=true&sortby=relevance~~
Not compatible with Unity

Requires newtonsoft json install - we can install using the package manager -> install package by name "com.unity.nuget.newtonsoft-json"

# Unity ML-Agents

Install:

- $ git clone --branch release_21 https://github.com/Unity-Technologies/ml-agents.git
- $ cd ml-agents/
- $ conda create -n mlagents python=3.9.13 && conda activate mlagents
- $ python -m pip install --upgrade pip
- $ pip install mlagents
- ~~pip3 install torch torchvision torchaudio~~
- $ pip install protobuf==3.20.3
- $ pip install onnx
- ~~open ml-agents/Project in Unity~~
- ~~$ mlagents-learn config/ppo/3dball.yaml --run-id PoSquarePushCl1 --force~~
- ~~run Assets/ML-Agents/Examples/3DBall/Scenes/3DBall~~
- Copy the ml-agents folders to the project folder (so they get included in version control)
  - com.unity.ml-agents
  - com.unity.ml-agents.extensions
- Add to project via package manager 'add package from disk' and select the package.json file in the ml-agents folder

*Note* When trying to use ml-agents in a project that includes Google ARCore Extensions I get errors about duplicate classes in the protobuf lib. In order to mitigate this I have installed branch release_20 of ml-agents which uses barracuda rather than sentis.
__OR__
Remove the Protobuf.dll Library/PackageCache/com.google.ar.core.arfoundation.extensions@a7adcde95f/Editor/Scripts/Internal/Analytics

tensorboard --logdir results

# Agent Behaviour

I want the agent to:
- Follow a path from the current location to a target location
- Avoid obstacles either by going around them or flying over them
- Maintain 'eye contact' with the user - avoid being occluded from the camera
- Gesture to the user when about to turn a corner
- Come closer to the user if the user has stopped moving

Here is a suggested reward mechanism for the reinforcement learning agent to optimize the desired behaviors:

| Behavior | Reward |
|----------|--------|
| Following the path from current location to target | +1 for each step closer to target |
|        | -1 for each step further from target |
| Avoiding obstacles | +1 for successfully navigating around obstacle |
|        | -5 for colliding with obstacle |
| Maintaining eye contact with user | +1 for each frame user is visible |
|        | -1 for each frame user is occluded |
| Gesturing to user before turning corner | +5 for gesturing before turning |
|        | -2 for not gesturing |
| Coming closer to stopped user | +1 for each step closer to user |
|        | -1 for each step further from user |

Some additional notes:
- The path to the target should be predefined or calculated based on a map. Rewards are given for following this path.
- Obstacle positions can be detected using sensors or computer vision. Colliding with obstacles incurs a large penalty.
- User position and occlusion can be tracked using the camera. Maintaining eye contact is rewarded.
- Gesturing can be detected by recognizing specific motion patterns. Gesturing before turns is rewarded.
- User movement can be detected to determine if they have stopped. Coming closer to a stopped user is rewarded.

The agent should learn to balance these rewards to optimize the overall desired behavior. Tuning the reward magnitudes can emphasize certain priorities. This reward structure encourages the agent to efficiently navigate to the target while interacting with the user in a natural, helpful way.
 
# Training results:

### mlrf_03
I have used these rewards/penalties: 
- Every step: -1f/MaxStep
- Out of range: -1f
- Hit target: 1f

Result: After 3.5M steps the bots have learned to avoid going out of range but don't learn how to hit the target. Maybe there should be a reward for getting closer to the target?

<div class="img_row">
  <img class="col two" src="{{ site.baseurl }}/images/ARCoreGeospatial/Screenshot 2024-05-20 at 18.31.01.png" alt="" title="mlrf_03 bots have learned to avoid going out of range but don't learn how to hit the target"/>
</div>
<div class="col two caption">
	mlrf_03 bots have learned to avoid going out of range but don't learn how to hit the target
</div><br><br>

### mlrf_04
I have used these rewards/penalties:
- Every step: -1f/MaxStep
- Out of range: -1f
- Hit target: 1f
- Getting closer to target (per step): 0.1f
- Getting further from target (per step): -0.1f

Result: The bots find it more profitable to approach the target but not reach it - in this way they can maximise their rewards over the course of an episode!

<div class="img_row">
  <img class="col two" src="{{ site.baseurl }}/images/ARCoreGeospatial/Screenshot 2024-05-20 at 18.30.36.png" alt="" title="bots find it more profitable to approach the target but not reach it"/>
</div>
<div class="col two caption">
	mlrf_04 bots find it more profitable to approach the target but not reach it
</div><br><br>

### mlrf_05
I have used these rewards/penalties:
- Every step: -1f/MaxStep
- Out of range: -1f
- Hit target: 1f
- __Getting closer to target (per step): 0.001f__
- __Getting further from target (per step): -0.001f__

Result: The bots quickly learn how to hit the target. Now, I need to add subsequent targets to the scene and see how they cope with that.

<div class="img_row">
  <img class="col two" src="{{ site.baseurl }}/images/ARCoreGeospatial/Screenshot 2024-05-20 at 18.21.38.png" alt="" title="mlrf_05 successful target finding behaviour"/>
</div>
<div class="col two caption">
	mlrf_05 successful target finding behaviour
</div><br><br>

>How hard would you say that your environment is? If the target is rarely reached, the agent will not be able to learn. In that case, you need to add some intrinsic reward when the agent acts in the right direction. That allows the agent to learn even if the rewards are sparse.

>There might also be a problem with reward hacking by the way you have designed the rewards. If the agent is not able to find the target to get the larger reward, the most efficient way is to fall off the platform as quickly as possible to not suffer from the small penalty in each timestep. 

This is a common problem in reinforcement learning and is called the reward shaping problem.  

> RuntimeError: The size of tensor a (8) must match the size of tensor b (11) at non-singleton dimension 1

This error occurs when I try running the learning process using the `intitialize_from` parameter after changing the Vector Observation Space Size - need to retrain from scratch in this case.

The agent's performance become progressively worse over the length of the path - maybe because it is trained on only 5 legs or maybe because the training ended before it had reached a stable state... it was still improbrving after 5M steps.

**31.5.24**

So, I made a few decisions today to try to improve the model.
- In order to better mimic real user behaviour I changed the user script to allow moving in reverse. This involved arbitrarily setting the waypoint to a previous waypoint. This causes the user to move backwards as well as pausing occasionally.
- The effect of this is that it becomes quite easy for the roibot to get very far ahead and potentially out of sight of the user (in the real world). To mitigat ethis I also want the robot to be able to move backwards. To achieve this I look at the position and orientation of the user transofrm and make a calculation to determine the nearest waypoint. Now, the next time the robot acquires a target (reaches a waypoint) it requests the user's nearest waypoint and sets it's next waypoint accordingly to userWaypoint + 1.
- I notice that my reward/penalty scheme makes the agent behave a little strangely as it approaches a waypoint. For some reason, rather than continuing to approach the waypoint until the distance is within the specified tolerance, it hovers close to the waypoint never quite closing in. I'm not exactly sure why this strategy should be effective for the agent but in order to prompt it to move directly to the waypoint I have implemented a scheme whereby the reward scales proportional to the inverse of the distance to the target. Unfortunately, this seems to result in the agent simply deciding it is more effective to commit suicide as early as possible in order to minimise losses.
- My next strategy is to reward the agent based on distance, speed and direction

**01.06.24**

Just before I finished yesterday I came across an article discussing "Solving sparse-reward tasks with Curiosity" - hmmm. Interesting. So, today I tried implementing the same strategy but with thew Curiosity intrinsic rewarded activated. You can see the results here:

I quite like the improvement and it feels right intuitively since I am in a sparse-reward scenario. However the movement I'm getting from the robot agent isn't at all what I'm looking for. While it may be doing a decent job of finding it's way (actually not that great of a job) It behaves in a very unnatural way, circling around every waypoint before closing in. And, you know what? I think I'm going to re-think my whole approach. I remember reading somewhere an AI guru responding when asked "how should I use AI in my project?" answering "Don't". His meaning being that we should only use AI when absolutely necessary. Well, I have a perfectly good path defined that leads my robot to the exact centre of each waypoint - why am I trying to teach the robot what is already known? I can simply tellthe robot to follow the path. Now, when it comes to avoiding obstacles on the path, obstacles who's location and size are not known... there is where AI can be useful.

So it's a bit of a rewrite. Ah well, that's development for ya! I've learned some really valuable lessons in the process of getting to this point and can attack the problem afresh with a greater understanding.

**07.06.24**

I need to turn my attention to the actual behaviour of the robot/agent now that I have a handle on training and its quirks. I have previously tried to train the agent so that it attempts to 
- follow the path defined by n waypoints
- never get too far ahead of the user
- return to find the user if the user stops or goes backwards

This has worked ok but I feel that attempting to train weights that satisfy these (and potentially more) requirements results in behaviour that feels 'soft'. By that I mean that the agent doesn't completely fulfill any of the requirements, but finds an approximation somewhere between them all.

So, my next strategy is to create a kind of super-state that will switch between different goals as the episode progresses. This way the agent will only attempt to achieve on goal at a time. I want the agent to
- attempt to stay a short distance ahead of the user
- move to a waypoint when one is in range
- return to the user if they turn around or leave the path

Imagine the behaviour of a young, excitable puppy bouncing around and dashing from its owner to some interesting new location and back again. That's how I want it to feel.

Taking inspiration from one of the Unity ml-agents demos I have created a state class which will become part of  the agent's observations input. The question I ask is how to assign rewards? Maybe I could change the rewards dependent on state too? Or - since there is only ever one target at a time (user or a waypoint) - I can simply switch the target with the state?  


