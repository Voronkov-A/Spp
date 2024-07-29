FROM localhost:51443/mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY ./bin/Release/net8.0/publish .
COPY ./docker-healthcheck.sh .
RUN chmod -R 555 /app/Certificates
RUN apt update -y
RUN apt install curl -y
EXPOSE 32005
USER $APP_UID
ENTRYPOINT ["dotnet", "Spp.IdentityProvider.dll"]
HEALTHCHECK --interval=10s CMD ["bash", "docker-healthcheck.sh", "32005"]
