FROM mcr.microsoft.com/dotnet/sdk:5.0
WORKDIR /ribble-server

COPY *.csproj ./

RUN dotnet restore

COPY * ./

EXPOSE 5000
EXPOSE 5001

ENTRYPOINT ["dotnet", "watch", "run"]
