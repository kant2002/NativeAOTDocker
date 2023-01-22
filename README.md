Scratch docker image using C#
=============================

This repository is sample how to statically link console application into single executable and run on Docker.

# Results

This is sizes for experiment

| Experiment | Size | Embed ICU | Embed OpenSSL | 
| ------------ | ----- | --- | ---------- |
| Console + Invariant globalization | 1.56 MB | No | No |
| Console + ICU | 35.9 MB | Yes | No |
| Console + Brotli + Deflate + Gzip | 2.56 MB  | No | No |
| HttpClient + OpenSSL | 10.5 MB | No | Yes |
| Web API | 23.6 MB | No | Yes |
| Grpc API | 21.6 MB | No | Yes |
| Npgsql Cli | 66.7 MB | Yes | Yes |

Chilsed for comparison

| Experiment | Size | Embed ICU | Embed OpenSSL | 
| ------------ | ----- | --- | ---------- |
| Web API | 33.9 MB | No | No |

Resulting docker image have size of 1.56 MB. Thats after disabling reflection. That's the minimum which I can get without integrating with Docker tightly. Or is it kernel integration I'm dreaming about? Unikernels, I see unikernels around me

# Build and Run

## Embedded ICU - 35.9 MB
```shell
docker build -t nativeaot-scratch EmbeddedICU 
docker run -i nativeaot-scratch
```

30 MB is ICU data.

## Invariant globalization - 1.56 MB
```shell
docker build -t nativeaot-scratch-invariant InvariantGlobalization
docker run -i nativeaot-scratch-invariant
```

## Brotli + Deflate + Gzip - 2.56 MB
```shell
docker build -t nativeaot-scratch-compression CompressionEmbedding
docker run -i nativeaot-scratch-compression
```

## Embedded OpenSSL - 10.5 MB
with reflection unfortunately
```shell
docker build -t nativeaot-scratch-openssl OpenSslEmbedding
docker run -i nativeaot-scratch-openssl
```

## Web API - 23.6 MB
with reflection unfortunately
```shell
docker build -t nativeaot-scratch-webapi WebApi
docker run -p 5022:80 -i nativeaot-scratch-webapi
```

Web API accessible on http://localhost:5022.

For SSL configuration take a look at https://learn.microsoft.com/en-us/dotnet/core/additional-tools/self-signed-certificates-guide#with-openssl nd create `certs` folder with certificates. Then you can run following command.

```
docker run --rm -it -p 8000:80 -p 8001:443 -e ASPNETCORE_URLS="https://+;http://+" -e ASPNETCORE_HTTPS_PORT=8001 -e ASPNETCORE_ENVIRONMENT=Development -e ASPNETCORE_Kestrel__Certificates__Default__Path=/https/contoso.com.crt -e ASPNETCORE_Kestrel__Certificates__Default__KeyPath=/https/contoso.com.key -v $PWD\certs/:/https/ nativeaot-scratch-webapi
```

Web API accessible on http://localhost:8000 and https://localhost:8001. 

## NpgCli - 66.7 MB
with reflection unfortunately
```shell
docker build -t nativeaot-scratch-npgsql NpgCli
docker run -i -e ConnectionString='Host=host.docker.internal:32768;Username=postgres;Password=postgrespw' nativeaot-scratch-npgsql
```

## GrpcApi - 21.6 MB
with reflection unfortunately
```shell
docker build -t nativeaot-scratch-grpcapi GrpcApi
docker run --rm -it -p 8010:80 -p 8011:443 -e ASPNETCORE_URLS="https://+;http://+" -e ASPNETCORE_HTTPS_PORT=8001 -e ASPNETCORE_ENVIRONMENT=Development -e ASPNETCORE_Kestrel__Certificates__Default__Path=/https/contoso.com.crt -e ASPNETCORE_Kestrel__Certificates__Default__KeyPath=/https/contoso.com.key -v $PWD\certs/:/https/ nativeaot-scratch-grpcapi
```

## Web API on Chiseled - 33.9 MB
with reflection unfortunately
```shell
docker build -t nativeaot-scratch-webapi-chiseled WebApiChiseled
docker run -p 5122:80 -i nativeaot-scratch-webapi-chiseled
```

Web API accessible on http://localhost:5122.

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