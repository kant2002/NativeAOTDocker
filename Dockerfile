FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
WORKDIR /app
RUN apk add clang libexecinfo binutils musl-dev build-base zlib-static
COPY nuget.config .
COPY HelloWorldStatic.csproj .
RUN dotnet restore --runtime linux-musl-x64 HelloWorldStatic.csproj

COPY . .
RUN dotnet publish -c Release -r linux-musl-x64 -o out HelloWorldStatic.csproj

FROM scratch AS runtime
WORKDIR /app
COPY --from=build /app/out/* /app/
ENTRYPOINT ["/app/HelloWorldStatic"]  
