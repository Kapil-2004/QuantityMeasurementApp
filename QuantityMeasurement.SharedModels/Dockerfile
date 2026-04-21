# Shared Models Dockerfile (optional - for library distribution)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["QuantityMeasurement.SharedModels/QuantityMeasurement.SharedModels.csproj", "QuantityMeasurement.SharedModels/"]
RUN dotnet restore "QuantityMeasurement.SharedModels/QuantityMeasurement.SharedModels.csproj"
COPY . .
WORKDIR "/src/QuantityMeasurement.SharedModels"
RUN dotnet build "QuantityMeasurement.SharedModels.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "QuantityMeasurement.SharedModels.csproj" -c Release -o /app/publish
