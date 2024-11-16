$containerTool="docker"
&$containerTool build -t nativeaot-scratch EmbeddedICU 
&$containerTool build -t nativeaot-scratch-invariant InvariantGlobalization
&$containerTool build -t nativeaot-scratch-compression CompressionEmbedding
&$containerTool build -t nativeaot-scratch-openssl OpenSslEmbedding
&$containerTool build -t nativeaot-scratch-http-client -f OpenSslEmbedding/Dockerfile.nossl OpenSslEmbedding
&$containerTool build -t nativeaot-scratch-webapi WebApi
&$containerTool build -t nativeaot-scratch-grpcapi GrpcApi
&$containerTool build -t nativeaot-scratch-npgsql NpgCli
&$containerTool build -t nativeaot-scratch-npgsql-noicu NpgCli -f NpgCli/Dockerfile.noicu
&$containerTool build -t nativeaot-scratch-npgsql-aot NpgCliSlim
&$containerTool build -t nativeaot-scratch-npgsql-aot-noicu NpgCliSlim -f NpgCliSlim/Dockerfile.noicu

&$containerTool build -t nativeaot-scratch-webapi-chiseled WebApiChiseled
