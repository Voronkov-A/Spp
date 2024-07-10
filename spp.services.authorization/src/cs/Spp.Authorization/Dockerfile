FROM localhost:51443/mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY ./bin/Release/net8.0/publish .
RUN chmod -R 555 /app/Certificates
EXPOSE 34009
USER $APP_UID
ENTRYPOINT ["dotnet", "Spp.Authorization.dll"]
