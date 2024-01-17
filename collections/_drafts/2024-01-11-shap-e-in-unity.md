---
title: Using shap-e in Unity - holodeck prototype
project: false
layout: post
categories:
  - projects
  - unity
  - realtime
  - machine learning
tags:
  - projects
description: Learning how to export a model as onnx from Huggingface and import into Unity
video: 395757574
tech:
  - Unity
  - Python

---


<!--div class="img_row">
	<img class="col three" src="{{ site.baseurl }}/images/2sugars/2sugars.jpg" alt="" title=""/>
</div>
<div class="col three caption">
	Bright future: Experts predict the live events industry will remain popular. © AFP
</div-->


### Shap-e into Unity

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