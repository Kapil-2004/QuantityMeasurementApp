# Microservices Architecture - Quantity Measurement App

## Architecture Overview

Your application has been successfully migrated from a monolithic architecture to a **microservices architecture**. This document explains the structure, setup, and deployment.

### Services

```
┌─────────────────────────────────────────────────────────┐
│                     FRONTEND (Angular/React)             │
│              (http://localhost:4200 or :3000)           │
└──────────────────────┬──────────────────────────────────┘
                       │
                       ▼
┌─────────────────────────────────────────────────────────┐
│              API GATEWAY (Port 5000)                    │
│      Routes requests to microservices                  │
│      Handles JWT token validation                      │
│      Applies CORS policies                             │
└────┬───────────┬──────────────┬──────────────┬─────────┘
     │           │              │              │
     ▼           ▼              ▼              ▼
┌────────────┐ ┌──────────────┐ ┌──────────────┐ ┌────────────┐
│AUTH SERVICE│ │COMPARE SRVICE│ │CONVERT SRVICE│ │ARITHMETIC  │
│ (Port 5001)│ │ (Port 5002)  │ │ (Port 5003)  │ │ (Port 5004)│
└────────────┘ └──────────────┘ └──────────────┘ └────────────┘
     │
     ▼
┌─────────────────────┐
│  PostgreSQL DB      │
│  (Auth Service)     │
│  (Port 5432)        │
└─────────────────────┘
```

### Service Responsibilities

1. **API Gateway** (Port 5000)
   - Entry point for all requests
   - Routes requests to appropriate services
   - Handles JWT authentication & validation
   - Implements CORS policies
   - Aggregates responses from multiple services

2. **Auth Service** (Port 5001)
   - User registration & login
   - JWT token generation
   - Google OAuth login
   - Token validation for other services
   - Database: PostgreSQL

3. **Compare Service** (Port 5002)
   - Compares two quantities
   - Validates same measurement type
   - Uses shared conversion engine

4. **Convert Service** (Port 5003)
   - Converts units (Length, Weight, Volume, Temperature)
   - Handles base unit conversions
   - Stateless, no database required

5. **Arithmetic Service** (Port 5004)
   - Add, Subtract, Divide operations
   - Validates same measurement type
   - Converts to base unit before calculation
   - Stateless, no database required

---

## Running Locally

### Prerequisites
- .NET 8 SDK
- Docker & Docker Compose (for containerized deployment)
- PostgreSQL 15+ (for local database)
- Node.js 18+ (for frontend)

### Option 1: Running with Docker Compose (Recommended)

```bash
# Navigate to project root
cd d:\Main_Project\Github

# Build and start all services
docker-compose up --build

# Access services at:
# API Gateway:       http://localhost:5000/swagger
# Auth Service:      http://localhost:5001/swagger
# Compare Service:   http://localhost:5002/swagger
# Convert Service:   http://localhost:5003/swagger
# Arithmetic Service: http://localhost:5004/swagger
```

### Option 2: Running Individual Services

#### 1. Start PostgreSQL Database
```bash
# Using Docker
docker run --name quantity_auth_db -e POSTGRES_PASSWORD=postgres -e POSTGRES_DB=quantity_measurement_auth -p 5432:5432 postgres:15-alpine

# Or install PostgreSQL locally and create the database
createdb quantity_measurement_auth
```

#### 2. Start Auth Service
```bash
cd QuantityMeasurement.AuthService
dotnet restore
dotnet run --urls=http://localhost:5001
```

#### 3. Start Compare Service
```bash
cd QuantityMeasurement.CompareService
dotnet restore
dotnet run --urls=http://localhost:5002
```

#### 4. Start Convert Service
```bash
cd QuantityMeasurement.ConvertService
dotnet restore
dotnet run --urls=http://localhost:5003
```

#### 5. Start Arithmetic Service
```bash
cd QuantityMeasurement.ArithmeticService
dotnet restore
dotnet run --urls=http://localhost:5004
```

#### 6. Start API Gateway
```bash
cd QuantityMeasurement.ApiGateway
dotnet restore
dotnet run --urls=http://localhost:5000
```

---

## API Endpoints

### Through API Gateway (Recommended)

#### Authentication
```
POST   /api/auth/register          - Register new user
POST   /api/auth/login             - Login user
POST   /api/auth/google-login      - Login with Google OAuth
GET    /api/auth/validate-token    - Validate JWT token
```

#### Quantities (Compare, Convert, Arithmetic)
```
POST   /api/quantities/compare     - Compare two quantities
POST   /api/quantities/convert     - Convert units
POST   /api/quantities/add         - Add two quantities
POST   /api/quantities/subtract    - Subtract two quantities
POST   /api/quantities/divide      - Divide two quantities
GET    /api/quantities/history     - Get operation history
```

### Service URLs (Direct Access)

| Service | URL | Swagger Docs |
|---------|-----|--------------|
| API Gateway | http://localhost:5000 | http://localhost:5000/swagger |
| Auth Service | http://localhost:5001 | http://localhost:5001/swagger |
| Compare Service | http://localhost:5002 | http://localhost:5002/swagger |
| Convert Service | http://localhost:5003 | http://localhost:5003/swagger |
| Arithmetic Service | http://localhost:5004 | http://localhost:5004/swagger |

---

## Example API Calls

### Register User
```bash
curl -X POST http://localhost:5000/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{
    "email": "user@example.com",
    "password": "SecurePassword123!",
    "name": "John Doe"
  }'
```

### Login
```bash
curl -X POST http://localhost:5000/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{
    "email": "user@example.com",
    "password": "SecurePassword123!"
  }'
```

### Compare Quantities
```bash
curl -X POST http://localhost:5000/api/quantities/compare \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN" \
  -d '{
    "q1": {"value": 1, "unit": "Feet", "measurementType": "Length"},
    "q2": {"value": 12, "unit": "Inch", "measurementType": "Length"}
  }'
```

### Convert Units
```bash
curl -X POST http://localhost:5000/api/quantities/convert \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN" \
  -d '{
    "quantity": {"value": 1000, "unit": "Gram", "measurementType": "Weight"},
    "targetUnit": "Kilogram"
  }'
```

### Add Quantities
```bash
curl -X POST http://localhost:5000/api/quantities/add \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN" \
  -d '{
    "q1": {"value": 5, "unit": "Foot", "measurementType": "Length"},
    "q2": {"value": 10, "unit": "Inch", "measurementType": "Length"}
  }'
```

---

## Supported Units

### Length
- Feet, Inch, Yard, Centimeter

### Weight
- Kilogram, Gram, Pound

### Volume
- Litre, Millilitre, Gallon

### Temperature
- Celsius, Fahrenheit, Kelvin
- (Note: Addition/Subtraction/Division not supported for Temperature)

---

## Configuration

### Environment Variables

#### API Gateway
```env
ASPNETCORE_ENVIRONMENT=Development
Jwt__Key=your-super-secret-key-change-this-in-production
Jwt__Issuer=QuantityMeasurementApp
Jwt__Audience=QuantityMeasurementAppUsers
Services__AuthService__Url=http://localhost:5001
Services__CompareService__Url=http://localhost:5002
Services__ConvertService__Url=http://localhost:5003
Services__ArithmeticService__Url=http://localhost:5004
```

#### Auth Service
```env
ASPNETCORE_ENVIRONMENT=Development
DATABASE_URL=postgresql://postgres:postgres@localhost:5432/quantity_measurement_auth
Jwt__Key=your-super-secret-key-change-this-in-production
Jwt__Issuer=QuantityMeasurementApp
Jwt__Audience=QuantityMeasurementAppUsers
Jwt__DurationInMinutes=60
Authentication__Google__ClientId=your-google-client-id
```

---

## Deployment to Production

### Docker Compose Deployment

1. **Update environment variables** in `docker-compose.yml`:
   - Change `Jwt__Key` to a secure key
   - Set `Google__ClientId` if using Google OAuth
   - Update `DATABASE_URL` for production database

2. **Build and push images** (if using private registry):
```bash
docker-compose build
docker tag quantity-api-gateway:latest myregistry/quantity-api-gateway:latest
docker push myregistry/quantity-api-gateway:latest
# Repeat for other services
```

3. **Deploy on production server**:
```bash
docker-compose -f docker-compose.yml up -d
```

### Kubernetes Deployment

Kubernetes manifests can be created for each service. Each service would have:
- Deployment
- Service
- ConfigMap (for non-sensitive config)
- Secret (for sensitive config like JWT key)

---

## Frontend Integration

### Update Frontend API URLs

For **Angular** (in `environment.ts`):
```typescript
export const environment = {
  apiUrl: 'http://localhost:5000/api', // For local development
  // For production:
  // apiUrl: 'https://api.yourdomain.com/api'
};
```

For **React** (in `.env`):
```
REACT_APP_API_URL=http://localhost:5000/api
# For production:
# REACT_APP_API_URL=https://api.yourdomain.com/api
```

### CORS Configuration

The API Gateway allows requests from:
- `http://localhost:4200` (Angular dev server)
- `http://localhost:3000` (React dev server)  
- `https://quantitymeasurementapp-frontend-2abn.onrender.com` (Production)

Update CORS policy in `QuantityMeasurement.ApiGateway/Program.cs` for additional domains.

---

## Inter-Service Communication

Services communicate via HTTP/REST through the API Gateway. Direct service-to-service communication is possible but should be avoided for better decoupling.

### Adding New Direct Service-to-Service Call:

```csharp
using (var client = new HttpClient())
{
    var response = await client.GetAsync("http://auth-service:8080/api/auth/validate-token");
    // Handle response
}
```

---

## Monitoring & Logging

### Swagger/OpenAPI Documentation

Each service has Swagger documentation available at `/swagger`:
- API Gateway: http://localhost:5000/swagger
- Auth Service: http://localhost:5001/swagger
- Compare Service: http://localhost:5002/swagger
- Convert Service: http://localhost:5003/swagger
- Arithmetic Service: http://localhost:5004/swagger

### Application Logs

Logs are output to console in Docker containers. To view logs:

```bash
# View all services
docker-compose logs -f

# View specific service
docker-compose logs -f auth-service
```

---

## Database

### Auth Service Database Schema

```sql
CREATE TABLE "Users" (
    "Id" SERIAL PRIMARY KEY,
    "Email" VARCHAR(100) NOT NULL UNIQUE,
    "PasswordHash" VARCHAR(256) NOT NULL,
    "Name" VARCHAR(100),
    "CreatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
```

### Database Migrations

Migrations are applied automatically on service startup. To create new migrations:

```bash
cd QuantityMeasurement.AuthService
dotnet ef migrations add MigrationName
dotnet ef database update
```

---

## Scaling

### Horizontal Scaling

Each microservice can be scaled independently. Example with Docker Compose:

```yaml
# Scale services
docker-compose up -d --scale compare-service=3 --scale arithmetic-service=2
```

### Load Balancing

Use a load balancer (Nginx, HAProxy, or cloud provider's load balancer) to distribute traffic to multiple instances:

```nginx
upstream api_gateway {
    server localhost:5000;
    server localhost:5001; # Another instance
}
```

---

## Security Considerations

1. **Change JWT Secret**: Replace default JWT key in all services before production
2. **Enable HTTPS**: Use certificates in production
3. **Database Credentials**: Use secure credential management (AWS Secrets Manager, Azure Key Vault)
4. **API Rate Limiting**: Implement rate limiting on API Gateway
5. **Input Validation**: All services validate input data
6. **CORS**: Update CORS policies for production domains only

---

## Troubleshooting

### Services Not Starting

1. Check Docker daemon is running: `docker ps`
2. Check ports are available: `netstat -ano | findstr :5000`
3. View service logs: `docker-compose logs service-name`

### Database Connection Error

1. Verify PostgreSQL is running
2. Check connection string in environment variables
3. Ensure database exists: `psql -l`

### Token Validation Failed

1. Ensure `Jwt__Key` is same across all services
2. Check token expiration: `Jwt__DurationInMinutes`
3. Verify token format: `Bearer <token>`

### Service Discovery Issues (Docker)

1. Ensure all services are on same network: `quantity-network`
2. Use service names for internal communication
3. Check DNS resolution: `docker exec service_name nslookup auth-service`

---

## Development Guidelines

### Adding a New Microservice

1. Create new project folder: `QuantityMeasurement.NewService`
2. Copy project structure from existing service
3. Update `docker-compose.yml`
4. Add route in API Gateway controller
5. Update appsettings with service URL
6. Create Dockerfile for new service

### Shared Models

Common DTOs and models are in `QuantityMeasurement.SharedModels`. All services reference this project.

### Adding New Unit Type

1. Create new enum in `QuantityMeasurement.SharedModels/Enums/`
2. Add extension methods for conversion
3. Update `ConversionEngine.cs` to handle new type
4. Rebuild services

---

## Performance Optimization

1. **Enable Caching**: Implement Redis for frequently accessed data
2. **Async/Await**: All services use async operations
3. **Connection Pooling**: Database connections are pooled
4. **Compression**: Response compression is enabled in HTTP pipeline
5. **Service Isolation**: Independent scaling and deployment

---

## Disaster Recovery

### Backup Strategy

- Database: Regular PostgreSQL backups
- Configuration: Store env variables in secure vaults
- Code: Git repository with version control

### Recovery Procedure

1. Restore database from backup
2. Redeploy services with latest code
3. Verify all endpoints are responding
4. Test end-to-end workflow

---

## Support & Maintenance

For issues or questions:
1. Check Swagger documentation at `/swagger` endpoints
2. Review application logs
3. Verify service health: `docker ps` / `docker-compose ps`
4. Test individual services via Swagger UI

---

## License

This project is part of the Quantity Measurement Application.
