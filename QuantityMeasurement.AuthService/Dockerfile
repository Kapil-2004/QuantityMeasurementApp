# Auth Service Dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["QuantityMeasurement.AuthService/QuantityMeasurement.AuthService.csproj", "QuantityMeasurement.AuthService/"]
COPY ["QuantityMeasurement.SharedModels/QuantityMeasurement.SharedModels.csproj", "QuantityMeasurement.SharedModels/"]
RUN dotnet restore "QuantityMeasurement.AuthService/QuantityMeasurement.AuthService.csproj"
COPY . .
WORKDIR "/src/QuantityMeasurement.AuthService"
RUN dotnet build "QuantityMeasurement.AuthService.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "QuantityMeasurement.AuthService.csproj" -c Release -o /app/publish
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "QuantityMeasurement.AuthService.dll"]
