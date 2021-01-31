FROM mcr.microsoft.com/dotnet/sdk:5.0
WORKDIR /ribble-server

COPY *.csproj ./
RUN dotnet restore
