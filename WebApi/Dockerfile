FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine3.16 AS build
WORKDIR /app
# these RUN lines below is optimization for current experiment to account for the fact that I have multiple similar configuration
# best practices of Docker building imply that I put all dependencies in single line
RUN apk add clang libexecinfo binutils musl-dev build-base zlib-static
RUN apk add cmake
RUN apk add openssl-dev openssl-libs-static openssl
COPY nuget.config .
COPY WebApi.csproj .
RUN dotnet restore --runtime linux-musl-x64 WebApi.csproj

# Remove once System.Security.Cryptography.Native can be statically linked using default build process
COPY Microsoft.NETCore.Native.Unix.targets /root/.nuget/packages/microsoft.dotnet.ilcompiler/8.0.0-alpha.1.23058.2/build/Microsoft.NETCore.Native.Unix.targets
COPY System.Security.Cryptography.Native /root/.nuget/packages/runtime.linux-musl-x64.microsoft.dotnet.ilcompiler/8.0.0-alpha.1.23058.2/native/src/libs/System.Security.Cryptography.Native
COPY build-local.sh /root/.nuget/packages/runtime.linux-musl-x64.microsoft.dotnet.ilcompiler/8.0.0-alpha.1.23058.2/native/src/libs/build-local.sh
COPY . .
RUN dotnet publish -c Release -r linux-musl-x64 -o out WebApi.csproj

FROM scratch AS runtime
WORKDIR /app
COPY --from=build /app/out/WebApi /app/
COPY --from=build /etc/ssl/certs/* /etc/ssl/certs/
ENV ASPNETCORE_URLS="https://+;http://+"
EXPOSE 443
EXPOSE 80
ENTRYPOINT ["/app/WebApi"]  
