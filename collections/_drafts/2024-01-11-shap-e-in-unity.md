---
title: Speech-to-3D in Unity
project: true
layout: post
categories:
  - projects
  - unity
  - realtime
  - machine learning
tags:
  - projects
description: Create a speech-to-3D prototype in Unity using HuggingFace models
video: UQpV9D3XYCE
tech:
  - Unity
  - Python

---

{% include youtubeplayer.html id=page.video %}


# Speech to 3D in AR

## Why?
Gen AI is the hot thing right now and while there have been rapid advances in image and video generation, 3D is still lagging behind. I want to explore the possibilities of creating 3D objects from speech prompts in AR. Ultimately, I want to create something that gives the user the poser to create their environment in AR using only their voice - something like the Star Trek Holodeck. But this isn't just about creating a cool toy - what I am exploring is a new way of interacting with AI that will lead us away from the current paradigm of screens and keyboards and towards a world of more natural interaction with AI. Just like we would interact with another human or team of humans.
## How?
There are 3 ways in which we will be interfacing with the various Deep Learning models that will be used in this project
- Running on the device (Edge AI)
- Running on the Huggingface inference API
- Running as a HuggingFace Space using Gradio and accessed via a custom cloud API

So there will be 3 models that we will be using:
- Speech to text - Whisper-tiny running in Unity Sentis on the device
- Text to Image - Using the huggingface inference api to access stabilityai/stable-diffusion-xl-base-1.0
- Image to 3D - We will build a custom FastAPI interface to access stabilityai/TripoSR running in a huggingface space as a gradio app.

The reasons for this separation are multiple. Firstly, I want to create POC implementations for future reference. 2ndly, from a practicval POV certain models are too large to run on device and need resources that are best served in the cloud. And thirdly, the tripoSR model is not available via the inference API so we need to access it in another manner. 

Of course, none of this is intended to represent anything thst could be thoght id as a production environment - we arer stricltly in thge realms if prototypoinv herer and there are manbyfiold considerationfs that would have to be rewsolved in iorder to bring this to a wider audioence.

## What?
We will be creating a Unity app toi runon iOS, Andrtoid and (hopefully) visionos. I can see there being many use cases for this in the future from gaming to interior design to virtual film production. Therw arw many areas of life that could leverage a system that allows us to speak our environment into existwence - for the time being thsi wioll never be a system that will replaces humans - the quality of the models, for thew moment, is of very low quality and the inference latency is quite high. Who knows where this will be in a year, two years, 5 years time. Currently, (2024) the raste of progress in the indusrty is staggering. 

As a side note - the inital implementation used an extwernal service (Meshy) for the creation of 3D from text via their API (meshy.com). While this gave reasonable results the inference time (certainly on the free trial plan) nwa cripplingly slow (upwardss of e3 minutes from start to end). To it's credit the models were pretty nice with fiully textured, UV mapped meshes with a resonable poly count. But 3 minutes wait between generatiosn really killed the experience - and so I was led to my own hybrid implementation as described. Currently, start to end inference tiomes are in the range 20-30 seconds, which _just about_ fulfiulls the ambition of real-time generation.
## When?
As a prototype this needs to be less than 6 weeks in development
## Who?
Me on my lonesome

### Whisper AI

Unity Sentis gives us the opportunity to  run AI models in Unity. There are plenty to choose from on Huggingface - and eventually I want to create my own. As a POC I will try to import the shap-e generative 3D model and the whisper speech to text model to enable the user to speak a prompt that will create an object in AR.
Here is another line.

### Setup notes

- Install Conda (Anaconda or Miniconda)
- Create a new environment with `conda create -n <myenv> ipykernel` which will allow usu to access the kernel for this environment from our base environment (which is where Jupuyter will be running)
- Doing it this way means we only have to install Jupyter once in the base environment
- We can install Jupyter labs using:
```c++
conda install -c conda-forge jupyterlab
conda install -c conda-forge nb_conda_kernels
```
Now, when we launch our notebook we should be able to select the kernel from the menu or the button at top right.

New syntax for nbdev export 
```c++
from nbdev.export import nb_export
nb_export('sushi_deploy.ipynb')
#nbdev.export.nb_export('notebook-name.ipynb', 'path/where/to/save')
print('Export Success!')
```

Requirements.txt should be defined to ensure that fastai is installed on HuggingFace space 

Convert from fastai to onnx (for Unity)
```c++
import torch
from fastai.learner import load_learner

# Load the fastai model
learn = load_learner('model.pkl')

# Convert to PyTorch model
model = learn.model

# Create a dummy input that matches the input size your model expects
# For an RGB image of size 128x128, the input tensor should have the shape [1, 3, 128, 128]
dummy_input = torch.randn(1, 3, 128, 128)

# Export the model to an ONNX file
torch.onnx.export(model, dummy_input, 'model.onnx')
```

### Exporting a HuggingFace transformer to onnx

I want to use the openai/whisper model on HuggingFace so I:
- ~~Clone the repo~~ No need to do this as we can use the HuggingFace hub
- Create a new conda environment
- Install the requirements
  - Hugginface hub: `$ pip3 install huggingface_hub`
  - Optimum: `$ pip3 install optimum`
  - Onnx: `$ pip3 install onnx onnxruntime`
- Run the export script: `$ optimum-cli export onnx -m openai/whisper-tiny ./onnx/`
<div class="img_row">
	<img class="col one" src="{{ site.baseurl }}/images/whisper onnx.png" alt="" title=""/>
</div>
<div class="col one caption">
	The exported onnx model
</div>

<div class="img_row">
	<img class="col three" src="{{ site.baseurl }}/images/makeitso/flyingpig.jpg" alt="" title="Stable Diffusion generated image"/>
</div>
<div class="col three caption">
	Stable Diffusion generated image
</div>