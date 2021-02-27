FROM mcr.microsoft.com/dotnet/sdk:5.0
WORKDIR /ribble-server

COPY *.csproj ./

RUN dotnet restore
RUN dotnet tool install --global dotnet-ef
ENV PATH="/root/.dotnet/tools:${PATH}"

COPY * ./

EXPOSE 5000
EXPOSE 5001

ENTRYPOINT ["dotnet", "run"]
