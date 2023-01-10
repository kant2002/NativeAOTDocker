FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
WORKDIR /app
RUN apk add clang libexecinfo binutils musl-dev build-base zlib-static cmake openssl-dev openssl-libs-static
COPY nuget.config .
COPY OpenSslEmbedding.csproj .
RUN dotnet restore --runtime linux-musl-x64 OpenSslEmbedding.csproj

# Remove once System.Security.Cryptography.Native can be statically linked using default build process
COPY Microsoft.NETCore.Native.Unix.targets /root/.nuget/packages/microsoft.dotnet.ilcompiler/8.0.0-alpha.1.23058.2/build/Microsoft.NETCore.Native.Unix.targets
COPY System.Security.Cryptography.Native /root/.nuget/packages/runtime.linux-musl-x64.microsoft.dotnet.ilcompiler/8.0.0-alpha.1.23058.2/native/src/libs/System.Security.Cryptography.Native
COPY build-local.sh /root/.nuget/packages/runtime.linux-musl-x64.microsoft.dotnet.ilcompiler/8.0.0-alpha.1.23058.2/native/src/libs/build-local.sh
COPY . .
RUN dotnet publish -c Release -r linux-musl-x64 -o out OpenSslEmbedding.csproj

FROM scratch AS runtime
WORKDIR /app
COPY --from=build /app/out/* /app/
COPY --from=build /etc/ssl/certs/* /etc/ssl/certs/
ENTRYPOINT ["/app/OpenSslEmbedding"]  
