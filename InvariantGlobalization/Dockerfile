FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /app
RUN apk add clang binutils musl-dev build-base zlib-static
COPY nuget.config .
COPY InvariantGlobalization.csproj .
RUN dotnet restore --runtime linux-musl-x64 InvariantGlobalization.csproj

COPY . .
RUN dotnet publish -c Release -r linux-musl-x64 -o out InvariantGlobalization.csproj

FROM scratch AS runtime
WORKDIR /app
COPY --from=build /app/out/InvariantGlobalization /app/
ENTRYPOINT ["/app/InvariantGlobalization"]  
