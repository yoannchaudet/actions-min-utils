# Build a small image for .NET 9
# Largely inspired by https://www.thorsten-hans.com/how-to-build-smaller-and-secure-docker-images-for-net5

ARG RUNTIME=linux-x64
ARG BUILD_CONFIGURATION=Release
ARG PROJECT_FILE=ActionsMinUtils.Sample.csproj

# Builder
FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS builder
ARG RUNTIME
ARG BUILD_CONFIGURATION
ARG PROJECT_FILE
WORKDIR /src
COPY . .
RUN dotnet restore --runtime $RUNTIME
RUN dotnet publish $PROJECT_FILE \
    --configuration $BUILD_CONFIGURATION \
    --output /app/publish \
    --no-restore \
    --runtime $RUNTIME \
    --self-contained true \
    /p:PublishSingleFile=true

# Runtime
FROM mcr.microsoft.com/dotnet/runtime-deps:9.0-alpine
ARG PROJECT_FILE
ENV \
    DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false \
    LC_ALL=en_US.UTF-8 \
    LANG=en_US.UTF-8
RUN apk add --no-cache \
    icu-data-full \
    icu-libs \
    gcompat
RUN adduser --disabled-password \
    --home /app \
    --gecos '' dotnetuser \
    && chown -R dotnetuser /app
ENV PROJECT_FILE=$PROJECT_FILE
USER dotnetuser
WORKDIR /app
COPY --from=builder /app/publish .
RUN ln -s /app/$(basename $PROJECT_FILE .csproj) /app/__entrypoint
ENTRYPOINT ["/app/__entrypoint"]