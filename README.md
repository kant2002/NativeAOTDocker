Scratch docker image using C#
=============================

This repository is sample how to statically link console application into single executable and run on Docker.

# Build and Run

ICU support - 47.9 Mb
```shell
docker build -t nativeaot-scratch -f Dockerfile . 
docker run -i nativeaot-scratch
```

Invariant globalization - 8.9 Mb 
```shell
docker build -f Dockerfile.invariant -t nativeaot-scratch-invariant .
docker run -i nativeaot-scratch-invariant
```

Resulting docker image have size of 8.9MB. Thats after disabling reflection. That's the minimum which I can get without integrating with Docker tightly. Or is it kernel integration I'm dreaming about?

# Inspeciting scratch container

You can use dive
```shell
docker run --rm -it `
     -v /var/run/docker.sock:/var/run/docker.sock `
     wagoodman/dive:latest nativeaot-scratch
```

or export tar with container

```
docker save nativeaot-scratch -o nativeaot-scratch.tar
```

# Testing that building inside Alpine works.

That's was needed when NativeAOT does not have objwriter for musl. Still interesting if you need to build augment ObjWriter.
```
docker build -t test-build -f Dockerfile.objwriter .
```

# Smoke test for ObjWriter build

```
docker build -t nativeaot-scratch-build -f Dockerfile.objwriter .
docker run -i nativeaot-scratch-build
```

Compiled `libobjwriter.so` is located at `artifacts/obj/InstallRoot-/lib` inside docker.