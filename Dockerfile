FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
WORKDIR /app
RUN apk add clang libexecinfo binutils musl-dev build-base zlib-static cmake icu-static icu-dev
COPY nuget.config .
COPY HelloWorldStatic.csproj .
RUN dotnet restore --runtime linux-musl-x64 HelloWorldStatic.csproj

# Remove once https://github.com/dotnet/runtime/pull/79501 lands and propagates.
COPY Microsoft.NETCore.Native.Unix.targets /root/.nuget/packages/microsoft.dotnet.ilcompiler/8.0.0-alpha.1.22609.9/build/Microsoft.NETCore.Native.Unix.targets
COPY . .
RUN dotnet publish -c Release -r linux-musl-x64 -o out HelloWorldStatic.csproj /p:InvariantGlobalization=false /bl

FROM scratch AS runtime
WORKDIR /app
COPY --from=build /app/out/* /app/
COPY --from=build /usr/share/icu/71.1/icudt71l.dat /usr/share/icu/71.1/
ENTRYPOINT ["/app/HelloWorldStatic"]  
