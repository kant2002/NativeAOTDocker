FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /app
# these RUN lines below is optimization for current experiment to account for the fact that I have multiple similar configuration
# best practices of Docker building imply that I put all dependencies in single line
RUN apk add clang binutils musl-dev build-base zlib-static
RUN apk add cmake
RUN apk add openssl-dev openssl-libs-static openssl
#RUN apk add protoc
RUN wget -q -O /etc/apk/keys/sgerrand.rsa.pub https://alpine-pkgs.sgerrand.com/sgerrand.rsa.pub
RUN wget https://github.com/sgerrand/alpine-pkg-glibc/releases/download/2.34-r0/glibc-2.34-r0.apk
RUN apk add --force-overwrite glibc-2.34-r0.apk

COPY nuget.config .
COPY GrpcApi.csproj .
RUN dotnet restore --runtime linux-musl-x64 GrpcApi.csproj

COPY . .
RUN dotnet publish -c Release -r linux-musl-x64 -o out GrpcApi.csproj

FROM scratch AS runtime
WORKDIR /app
COPY --from=build /app/out/GrpcApi /app/
COPY --from=build /etc/ssl/certs/* /etc/ssl/certs/
ENV ASPNETCORE_URLS="https://+;http://+"
EXPOSE 443
EXPOSE 80
ENTRYPOINT ["/app/GrpcApi"]  
