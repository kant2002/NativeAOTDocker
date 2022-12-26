FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
WORKDIR /app
RUN apk add clang libexecinfo binutils musl-dev build-base zlib-static cmake icu-static icu-dev
COPY nuget.config .
COPY HelloWorldStatic.csproj .
RUN dotnet restore --runtime linux-musl-x64 HelloWorldStatic.csproj

COPY . .
RUN dotnet publish -c Release -r linux-musl-x64 -o out HelloWorldStatic.csproj /p:InvariantGlobalization=false /bl

FROM scratch AS runtime
WORKDIR /app
COPY --from=build /app/out/* /app/
COPY --from=build /usr/share/icu/71.1/icudt71l.dat /usr/share/icu/71.1/
ENTRYPOINT ["/app/HelloWorldStatic"]  
