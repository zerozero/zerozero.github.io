---
title: ARCore Geospatial API & Unity
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
description: Set up Unity for ARCore Geospatial API
video: 395757574
tech:
  - Unity
---

# ARCore Geospatial API & Unity
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


