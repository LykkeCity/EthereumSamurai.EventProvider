FROM microsoft/dotnet:2.0-runtime
ARG source
WORKDIR /app
COPY ${source} .
ENTRYPOINT ["dotnet", "EthereumSamurai.EventProvider.dll"]
EXPOSE 5000/tcp