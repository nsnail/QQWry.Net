FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
ENV ASPNETCORE_HTTP_PORTS=61022
EXPOSE 61022
COPY ./dist/QQWry.Net/linux-x64 .
ENTRYPOINT ["dotnet", "QQWry.Net.dll"]