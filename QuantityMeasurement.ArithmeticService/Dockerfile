# Arithmetic Service Dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["QuantityMeasurement.ArithmeticService/QuantityMeasurement.ArithmeticService.csproj", "QuantityMeasurement.ArithmeticService/"]
COPY ["QuantityMeasurement.SharedModels/QuantityMeasurement.SharedModels.csproj", "QuantityMeasurement.SharedModels/"]
RUN dotnet restore "QuantityMeasurement.ArithmeticService/QuantityMeasurement.ArithmeticService.csproj"
COPY . .
WORKDIR "/src/QuantityMeasurement.ArithmeticService"
RUN dotnet build "QuantityMeasurement.ArithmeticService.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "QuantityMeasurement.ArithmeticService.csproj" -c Release -o /app/publish
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "QuantityMeasurement.ArithmeticService.dll"]
