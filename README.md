Scratch docker image using C#
=============================

This repository is sample how to statically link console application into single executable and run on Docker.

# Build and Run
docker build -t nativeaot-scratch .
docker run -i nativeaot-scratch

Resulting docker image have size of 7MB. Thats after disabling reflection. That's the minimum which I can get without integrating with Docker tightly. Or is it kernel integration I'm dreaming about?
