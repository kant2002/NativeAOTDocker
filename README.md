Scratch docker image using C#
=============================

This repository is sample how to statically link console application into single executable and run on Docker.

# Build and Run

Embedded ICU - 35.9 MB
```shell
docker build -t nativeaot-scratch EmbeddedICU 
docker run -i nativeaot-scratch
```

30 MB is ICU data.

Invariant globalization - 1.56 MB
```shell
docker build -t nativeaot-scratch-invariant InvariantGlobalization
docker run -i nativeaot-scratch-invariant
```

Brotli + Deflate + Gzip - 2.56 MB
```shell
docker build -t nativeaot-scratch-compression CompressionEmbedding
docker run -i nativeaot-scratch-compression
```

Embedded OpenSSL - 10.5 MB (with reflection unfortunately)
```shell
docker build -t nativeaot-scratch-openssl OpenSslEmbedding
docker run -i nativeaot-scratch-openssl
```

Web API - 23.6 MB (with reflection unfortunately and SSL not yet configured)
```shell
docker build -t nativeaot-scratch-webapi WebApi
docker run -p 5022:80 -i nativeaot-scratch-webapi
```

Web API accessible on http://localhost:5022. For SSL configuration take a look at https://learn.microsoft.com/en-us/dotnet/core/additional-tools/self-signed-certificates-guide#with-openssl

Resulting docker image have size of 1.56 MB. Thats after disabling reflection. That's the minimum which I can get without integrating with Docker tightly. Or is it kernel integration I'm dreaming about? Unikernels, I see unikernels around me.

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