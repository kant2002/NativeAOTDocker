FROM mcr.microsoft.com/dotnet/sdk:8.0-jammy AS build
WORKDIR /app
COPY nuget.config .
COPY WebApi.csproj .
RUN dotnet restore --runtime linux-x64 WebApi.csproj

COPY . .
RUN apt update && apt-get install -y clang zlib1g-dev
RUN dotnet publish -c Release -r linux-x64 -o out WebApi.csproj

FROM mcr.microsoft.com/dotnet/nightly/runtime-deps:8.0-jammy-chiseled AS runtime
WORKDIR /app
COPY --from=build /app/out/WebApi /app/
ENV ASPNETCORE_URLS="https://+;http://+"
EXPOSE 443
EXPOSE 80
ENTRYPOINT ["/app/WebApi"]  
