FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
ENV ASPNETCORE_HTTP_PORTS=61022
EXPOSE 61022
COPY ./dist/backend/QQWry.Net.WebApi/bin/Release/net8.0/publish .
ENTRYPOINT ["dotnet", "QQWry.Net.WebApi.dll"]