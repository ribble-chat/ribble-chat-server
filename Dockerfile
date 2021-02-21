FROM mcr.microsoft.com/dotnet/sdk:5.0
WORKDIR /ribble-server

COPY *.csproj ./

RUN dotnet restore
RUN dotnet tool install --global dotnet-ef
RUN export PATH="$PATH:$HOME/.dotnet/tools"

COPY * ./

EXPOSE 5000
EXPOSE 5001

ENTRYPOINT ["dotnet", "watch", "run"]
