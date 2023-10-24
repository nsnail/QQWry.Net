dotnet publish -c Release -r win-x64 --sc /p:PublishSingleFile=true /p:EnableCompressionInSingleFile=true -o ./dist/QQWry.Net/win-x64
dotnet publish -c Release -r linux-x64 --sc /p:PublishSingleFile=true /p:EnableCompressionInSingleFile=true -o ./dist/QQWry.Net/linux-x64-singlefile
dotnet publish -c Release -r linux-x64 -o ./dist/QQWry.Net/linux-x64
docker build . -t nsnail/qqwry.net
docker push nsnail/qqwry.net