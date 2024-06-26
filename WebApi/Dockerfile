FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /app
# these RUN lines below is optimization for current experiment to account for the fact that I have multiple similar configuration
# best practices of Docker building imply that I put all dependencies in single line
RUN apk add clang binutils musl-dev build-base zlib-static
RUN apk add cmake
RUN apk add openssl-dev openssl-libs-static openssl
COPY nuget.config .
COPY WebApi.csproj .
RUN dotnet restore --runtime linux-musl-x64 WebApi.csproj

COPY . .
RUN dotnet publish -c Release -r linux-musl-x64 -o out WebApi.csproj

FROM scratch AS runtime
WORKDIR /app
COPY --from=build /app/out/WebApi /app/
COPY --from=build /etc/ssl/certs/* /etc/ssl/certs/
ENV ASPNETCORE_URLS="http://+"
EXPOSE 443
EXPOSE 80
ENTRYPOINT ["/app/WebApi"]  
