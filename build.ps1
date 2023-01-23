docker build -t nativeaot-scratch EmbeddedICU 
docker build -t nativeaot-scratch-invariant InvariantGlobalization
docker build -t nativeaot-scratch-compression CompressionEmbedding
docker build -t nativeaot-scratch-openssl OpenSslEmbedding
docker build -t nativeaot-scratch-http-client -f OpenSslEmbedding/Dockerfile.nossl OpenSslEmbedding
docker build -t nativeaot-scratch-webapi WebApi
docker build -t nativeaot-scratch-grpcapi GrpcApi
docker build -t nativeaot-scratch-npgsql NpgCli
docker build -t nativeaot-scratch-npgsql-noicu NpgCli -f NpgCli/Dockerfile.noicu
