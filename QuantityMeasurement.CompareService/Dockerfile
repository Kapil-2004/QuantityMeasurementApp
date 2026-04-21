# Compare Service Dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["QuantityMeasurement.CompareService/QuantityMeasurement.CompareService.csproj", "QuantityMeasurement.CompareService/"]
COPY ["QuantityMeasurement.SharedModels/QuantityMeasurement.SharedModels.csproj", "QuantityMeasurement.SharedModels/"]
RUN dotnet restore "QuantityMeasurement.CompareService/QuantityMeasurement.CompareService.csproj"
COPY . .
WORKDIR "/src/QuantityMeasurement.CompareService"
RUN dotnet build "QuantityMeasurement.CompareService.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "QuantityMeasurement.CompareService.csproj" -c Release -o /app/publish
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "QuantityMeasurement.CompareService.dll"]
