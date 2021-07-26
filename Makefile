.DEFAULT_GOAL := all

.PHONY: all
all: clean build publish

.PHONY: clean
clean:
	dotnet clean

.PHONY: build
build:
	dotnet restore && \
	dotnet build --no-restore

.PHONY: publish
publish:
	dotnet publish \
		-r ubuntu.20.04-x64 \
		--self-contained true \
		-p:PublishSingleFile=true \
		-p:OutputType=Exe \
		-o publish/ubuntu.20.04-x64 \
		-c Release

