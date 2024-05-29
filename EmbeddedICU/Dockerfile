FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /app
# these RUN lines below is optimization for current experiment to account for the fact that I have multiple similar configuration
# best practices of Docker building imply that I put all dependencies in single line
RUN apk add clang binutils musl-dev build-base zlib-static
RUN apk add cmake
RUN apk add icu-static icu-dev
COPY nuget.config .
COPY EmbeddedICU.csproj .
RUN dotnet restore --runtime linux-musl-x64 EmbeddedICU.csproj

COPY . .
RUN dotnet publish -c Release -r linux-musl-x64 -o out EmbeddedICU.csproj

FROM scratch AS runtime
WORKDIR /app
COPY --from=build /app/out/EmbeddedICU /app/
COPY --from=build /usr/share/icu/74.1/icudt74l.dat /usr/share/icu/74.1/
ENTRYPOINT ["/app/EmbeddedICU"]
