FROM microsoft/aspnetcore

WORKDIR /app

COPY aspnetcore/bin/release/netcoreapp1.1/publish .

ENTRYPOINT ["dotnet", "aspnetcore.dll"]