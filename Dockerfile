FROM mcr.microsoft.com/dotnet/sdk:5.0 as build

COPY . /usr/src/app

WORKDIR /usr/src/app

# Publish artifacts
RUN dotnet restore && \
    dotnet build -c Release && \
    dotnet publish \
      -o "artifact" \
      -c Release \
      -r ubuntu.20.04-x64 \
      --self-contained true \
      -p:PublishSingleFile=true \
      -p:OutputType=Exe

FROM build as release

CMD [ "artifact/mbgex2" ]