Scratch docker image using C#
=============================

This repository is sample how to statically link console application into single executable and run on Docker.

# Results

This is sizes for experiment

| Experiment | Size | Embed ICU | Embed OpenSSL | 
| ------------ | ----- | --- | ---------- |
| Console + Invariant globalization | 1.29 MB | No | No |
| Console + ICU | 35.14 MB | Yes | No |
| Console + Brotli + Deflate + Gzip | 2.22 MB  | No | No |
| HttpClient | 7.26 MB | No | No |
| HttpClient + OpenSSL | 12.16 MB | No | Yes |
| Web API | 21.94 MB | No | Yes |
| Grpc API | 23.46 MB | No | Yes |
| Npgsql ADO.NET + ICU | 50.98 MB | Yes | Yes |
| Npgsql ADO.NET + Invariant globalization | 17.08 MB | No | Yes |
| Npgsql Builder Slim + ICU | 44.1 MB | Yes | Yes |
| Npgsql Builder Slim + Invariant globalization | 10.2 MB | No | Yes |

Chilsed for comparison

| Experiment | Size | Embed ICU | Embed OpenSSL | 
| ------------ | ----- | --- | ---------- |
| Web API | 30.46 MB | No | No |

Resulting docker image have size of 1.56 MB. Thats after disabling reflection. That's the minimum which I can get without integrating with Docker tightly. Or is it kernel integration I'm dreaming about? Unikernels, I see unikernels around me

# Sizes of the components

Based on results I get approximate minimum size of code which added to your application if you using these libraries.

| Component | Size |
| ------------ | ----- |
| Barebone runtime + console | 1.28MB |
| ICU data | 29.40MB |
| Globalization support | 4.47MB |
| Brotli + Deflate + Gzip | 0.94MB |
| HttpClient | 5.98MB |
| OpenSSL | 4.26MB |
| OpenSSL certificates | 0.64MB |
| Web API | 20.66MB |
| Grpc API | 22.18MB |
| Npgsql ADO.NET | 15.82MB |
| Npgsql Builder Slim | 8.93MB |

# Build and Run

## Embedded ICU - 35.14 MB
```shell
docker build -t nativeaot-scratch EmbeddedICU 
docker run -i nativeaot-scratch
```

30 MB is ICU data.

## Invariant globalization - 1.29 MB
```shell
docker build -t nativeaot-scratch-invariant InvariantGlobalization
docker run -i nativeaot-scratch-invariant
```

## Brotli + Deflate + Gzip - 2.22 MB
```shell
docker build -t nativeaot-scratch-compression CompressionEmbedding
docker run -i nativeaot-scratch-compression
```

## HttpClient - 7.26 MB
with reflection unfortunately
```shell
docker build -t nativeaot-scratch-http-client -f OpenSslEmbedding/Dockerfile.nossl OpenSslEmbedding
docker run -i nativeaot-scratch-http-client
```

## HttpClient + OpenSSL - 12.16 MB
with reflection unfortunately
```shell
docker build -t nativeaot-scratch-openssl OpenSslEmbedding
docker run -i nativeaot-scratch-openssl
```

## Web API - 21.94 MB
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

## GrpcApi - 23.46 MB
with reflection unfortunately
```shell
docker build -t nativeaot-scratch-grpcapi GrpcApi
docker run --rm -it -p 8010:80 -p 8011:443 -e ASPNETCORE_URLS="https://+;http://+" -e ASPNETCORE_HTTPS_PORT=8001 -e ASPNETCORE_ENVIRONMENT=Development -e ASPNETCORE_Kestrel__Certificates__Default__Path=/https/contoso.com.crt -e ASPNETCORE_Kestrel__Certificates__Default__KeyPath=/https/contoso.com.key -v $PWD\certs/:/https/ nativeaot-scratch-grpcapi
``` 

## NpgCli + Globalization - 50.98 MB
with reflection unfortunately
```shell
docker build -t nativeaot-scratch-npgsql NpgCli
docker run -i -e ConnectionString='Host=172.17.0.2;Username=postgres;Password=postgrespw' nativeaot-scratch-npgsql
```

## NpgCli - 17.08 MB
with reflection unfortunately
```shell
docker build -t nativeaot-scratch-npgsql-noicu NpgCli -f NpgCli/Dockerfile.noicu
docker run -i -e ConnectionString='Host=172.17.0.2;Username=postgres;Password=postgrespw' nativeaot-scratch-npgsql-noicu
```

## Web API on Chiseled - 30.46 MB
with reflection unfortunately
```shell
docker build -t nativeaot-scratch-webapi-chiseled WebApiChiseled
docker run -p 5122:80 -i nativeaot-scratch-webapi-chiseled
```

Web API accessible on http://localhost:5122.

docker run --rm -it -p 8000:80 -p 8001:443 -e ASPNETCORE_URLS="https://+;http://+" -e ASPNETCORE_HTTPS_PORT=8001 -e ASPNETCORE_ENVIRONMENT=Development -e ASPNETCORE_Kestrel__Certificates__Default__Path=/https/contoso.com.crt -e ASPNETCORE_Kestrel__Certificates__Default__KeyPath=/https/contoso.com.key -v $PWD\certs/:/https/ nativeaot-scratch-webapi


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


```
docker run --name postgres-aot -e "POSTGRES_USER=postgres" -e "POSTGRES_PASSWORD=postgrespw" -p 5432:32768 -d postgres
```