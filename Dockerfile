# Base image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["QuantityMeasurementAPI/QuantityMeasurementAPI.csproj", "QuantityMeasurementAPI/"]
COPY ["QuantityMeasurementBusinessLayer/QuantityMeasurementBusinessLayer.csproj", "QuantityMeasurementBusinessLayer/"]
COPY ["QuantityMeasurementModelLayer/QuantityMeasurementModelLayer.csproj", "QuantityMeasurementModelLayer/"]
COPY ["QuantityMeasurementRepositoryLayer/QuantityMeasurementRepositoryLayer.csproj", "QuantityMeasurementRepositoryLayer/"]

RUN dotnet restore "QuantityMeasurementAPI/QuantityMeasurementAPI.csproj"

# Copy the rest of the code
COPY . .

# Build the application
WORKDIR "/src/QuantityMeasurementAPI"
RUN dotnet build "QuantityMeasurementAPI.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "QuantityMeasurementAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Expose the port the app listens on
EXPOSE 80
EXPOSE 443

# Start the application
ENTRYPOINT ["dotnet", "QuantityMeasurementAPI.dll"]
