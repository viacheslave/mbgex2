dotnet clean
dotnet restore
dotnet build --no-restore
dotnet publish -r win10-x64 --self-contained true -p:PublishSingleFile=true -p:OutputType=Exe -o publish/win10-x64 -c Release
dotnet publish -r ubuntu.20.04-x64 --self-contained true -p:PublishSingleFile=true -p:OutputType=Exe -o publish/ubuntu.20.04-x64 -c Release