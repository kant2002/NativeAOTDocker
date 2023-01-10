FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
WORKDIR /app
RUN apk add clang libexecinfo binutils musl-dev build-base zlib-static
COPY nuget.config .
COPY CompressionEmbedding.csproj .
RUN dotnet restore --runtime linux-musl-x64 CompressionEmbedding.csproj

COPY . .
RUN dotnet publish -c Release -r linux-musl-x64 -o out CompressionEmbedding.csproj

FROM scratch AS runtime
WORKDIR /app
COPY --from=build /app/out/* /app/
ENTRYPOINT ["/app/CompressionEmbedding"]  
