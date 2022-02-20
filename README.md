Scratch docker image using C#
=============================

This repository is sample how to statically link console application into single executable and run on Docker.

# Build and Run
```
docker build -t nativeaot-scratch .
docker run -i nativeaot-scratch
```

Resulting docker image have size of 7MB. Thats after disabling reflection. That's the minimum which I can get without integrating with Docker tightly. Or is it kernel integration I'm dreaming about?

# Testing that building inside Alpine works.
docker build -t test-build -f Dockerfile.objwriter .

# Smoke test for ObjWriter build

docker build -t nativeaot-scratch-build -f Dockerfile.objwriter .
docker run -i nativeaot-scratch-build

Compiled `libobjwriter.so` is located at `artifacts/obj/InstallRoot-/lib` inside docker.