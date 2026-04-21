# Convert Service Dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["QuantityMeasurement.ConvertService/QuantityMeasurement.ConvertService.csproj", "QuantityMeasurement.ConvertService/"]
COPY ["QuantityMeasurement.SharedModels/QuantityMeasurement.SharedModels.csproj", "QuantityMeasurement.SharedModels/"]
RUN dotnet restore "QuantityMeasurement.ConvertService/QuantityMeasurement.ConvertService.csproj"
COPY . .
WORKDIR "/src/QuantityMeasurement.ConvertService"
RUN dotnet build "QuantityMeasurement.ConvertService.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "QuantityMeasurement.ConvertService.csproj" -c Release -o /app/publish
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "QuantityMeasurement.ConvertService.dll"]
