# Microservices Architecture - Reference Guide

## 🏗️ Complete System Architecture

```
┌─────────────────────────────────────────────────────────────────┐
│                      FRONTEND (Angular/React)                    │
│          Running on http://localhost:4200 or :3000              │
└────────────────────────────────┬────────────────────────────────┘
                                 │
                    HTTP/REST (JSON payloads)
                                 │
                                 ▼
        ┌────────────────────────────────────────────┐
        │         API GATEWAY (Port 5000)           │
        │  - JWT Token Validation                   │
        │  - Route Handling                         │
        │  - CORS Management                        │
        │  - Response Aggregation                   │
        └──┬───────────────┬──────────────┬─────────┘
           │               │              │
    ┌──────▼──┐    ┌──────▼──┐    ┌──────▼──┐    ┌──────────┐
    │  Auth   │    │ Compare │    │ Convert │    │Arithmetic│
    │Service  │    │ Service │    │ Service │    │ Service  │
    │5001     │    │ 5002    │    │ 5003    │    │ 5004     │
    └──┬──────┘    └─────────┘    └─────────┘    └──────────┘
       │
       ▼
    ┌──────────────────────┐
    │   PostgreSQL DB      │
    │  (Port 5432)         │
    │  Auth data storage   │
    └──────────────────────┘
```

---

## 📡 Service Communication Patterns

### Pattern 1: Frontend → API Gateway
```
Frontend
  │ POST /api/auth/login
  ├─── Body: {email, password}
  └──→ API Gateway
       │ Routes to Auth Service
       └──→ Auth Service (5001)
           ├─ Validates credentials
           ├─ Generates JWT token
           └──→ API Gateway
               │ Returns response
               └──→ Frontend
```

### Pattern 2: Frontend → Gateway → Multiple Services
```
Frontend
  │ POST /api/quantities/add
  ├─── Body: {q1, q2}
  └──→ API Gateway
       ├─ Validates JWT token (calls Auth Service)
       └──→ Arithmetic Service (5004)
           ├─ Calls Conversion Engine
           ├─ Converts to base units
           ├─ Performs addition
           └──→ API Gateway
               │ Returns result
               └──→ Frontend
```

---

## 🎯 Request/Response Examples

### Authentication Flow

**1. Register**
```http
POST http://localhost:5000/api/auth/register

{
  "email": "user@example.com",
  "password": "SecurePass123!",
  "name": "John Doe"
}

Response (200):
{
  "success": true,
  "message": "Authentication successful",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "user": {
    "id": 1,
    "email": "user@example.com",
    "name": "John Doe"
  }
}
```

**2. Login**
```http
POST http://localhost:5000/api/auth/login

{
  "email": "user@example.com",
  "password": "SecurePass123!"
}

Response (200):
{
  "success": true,
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  ...
}
```

### Operations Flow

**3. Compare Quantities**
```http
POST http://localhost:5000/api/quantities/compare
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...

{
  "q1": {
    "value": 1,
    "unit": "Feet",
    "measurementType": "Length"
  },
  "q2": {
    "value": 12,
    "unit": "Inch",
    "measurementType": "Length"
  }
}

Response (200):
{
  "success": true,
  "message": "Comparison successful",
  "data": {
    "areEqual": true,
    "message": "Quantities are equal"
  }
}
```

**4. Convert Units**
```http
POST http://localhost:5000/api/quantities/convert
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...

{
  "quantity": {
    "value": 1000,
    "unit": "Gram",
    "measurementType": "Weight"
  },
  "targetUnit": "Kilogram"
}

Response (200):
{
  "success": true,
  "message": "Conversion successful",
  "data": {
    "result": 1.0,
    "unit": "Kilogram",
    "measurementType": "Weight"
  }
}
```

**5. Add Quantities**
```http
POST http://localhost:5000/api/quantities/add

{
  "q1": {
    "value": 5,
    "unit": "Feet",
    "measurementType": "Length"
  },
  "q2": {
    "value": 3,
    "unit": "Feet",
    "measurementType": "Length"
  }
}

Response (200):
{
  "success": true,
  "message": "Addition successful",
  "data": {
    "result": 8.0,
    "unit": "Feet",
    "measurementType": "Length"
  }
}
```

---

## 🔐 JWT Token Structure

```
Header:
{
  "alg": "HS256",
  "typ": "JWT"
}

Payload:
{
  "nameid": "1",                    // User ID
  "email": "user@example.com",
  "name": "John Doe",
  "exp": 1234567890,                // Expiration (60 min from now)
  "iss": "QuantityMeasurementApp",
  "aud": "QuantityMeasurementAppUsers"
}

Signature:
HMACSHA256(base64UrlEncode(header) + "." + base64UrlEncode(payload), secret)
```

---

## 🗄️ Database Schema

### Users Table (PostgreSQL)
```sql
CREATE TABLE "Users" (
    "Id" SERIAL PRIMARY KEY,
    "Email" VARCHAR(100) NOT NULL UNIQUE,
    "PasswordHash" VARCHAR(256) NOT NULL,
    "Name" VARCHAR(100),
    "CreatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Example data
INSERT INTO "Users" ("Email", "PasswordHash", "Name")
VALUES (
    'user@example.com',
    'base64_salt:base64_hash',
    'John Doe'
);
```

---

## 🔄 Unit Conversion Reference

### Length (Base: Inch)
```
1 Feet = 12 Inches
1 Yard = 36 Inches
1 Centimeter = 0.393701 Inches
```

### Weight (Base: Gram)
```
1 Kilogram = 1000 Grams
1 Pound = 453.592 Grams
```

### Volume (Base: Millilitre)
```
1 Litre = 1000 Millilitres
1 Gallon = 3785.41 Millilitres
```

### Temperature (Base: Celsius)
```
Celsius → Fahrenheit: (C × 9/5) + 32
Celsius → Kelvin: C + 273.15
```

---

## 🚀 Deployment Checklist

### Before Going Live

- [ ] Change JWT secret key in docker-compose.yml
- [ ] Update Google OAuth Client ID
- [ ] Configure database credentials
- [ ] Enable HTTPS/SSL certificates
- [ ] Set up monitoring and logging
- [ ] Configure firewall rules
- [ ] Set up database backups
- [ ] Enable CORS for production domain only
- [ ] Set up load balancer
- [ ] Configure auto-scaling policies
- [ ] Test end-to-end workflows
- [ ] Load test individual services
- [ ] Set up health monitoring
- [ ] Configure alerts for failures
- [ ] Document runbooks for operations

---

## 📊 Service Health Indicators

```bash
# Check all services
docker-compose ps

# Output:
NAME                  STATUS              PORTS
quantity_api_gateway  Up (healthy)        0.0.0.0:5000->8080/tcp
quantity_auth_service Up (healthy)        0.0.0.0:5001->8080/tcp
quantity_compare_svc  Up (healthy)        0.0.0.0:5002->8080/tcp
quantity_convert_svc  Up (healthy)        0.0.0.0:5003->8080/tcp
quantity_arithmetic_svc Up (healthy)      0.0.0.0:5004->8080/tcp
quantity_auth_db      Up (healthy)        0.0.0.0:5432->5432/tcp
```

---

## 🔍 Debugging Commands

```bash
# View logs from all services
docker-compose logs -f

# View specific service logs
docker-compose logs -f auth-service

# Execute command in container
docker-compose exec auth-service sh

# Check service health
curl http://localhost:5000/swagger

# Test service connectivity
docker-compose exec auth-service ping compare-service

# View network
docker network ls

# Inspect network
docker network inspect quantity-network
```

---

## 💾 Backup & Restore

### Backup Database
```bash
docker-compose exec auth-db pg_dump -U postgres quantity_measurement_auth > backup.sql
```

### Restore Database
```bash
cat backup.sql | docker-compose exec -T auth-db psql -U postgres quantity_measurement_auth
```

---

## 🔄 Blue-Green Deployment

```bash
# Current running (Blue)
docker-compose -f docker-compose.blue.yml up -d

# Deploy new version (Green)
docker-compose -f docker-compose.green.yml up -d

# Test green version

# Switch traffic to green
# (Update load balancer DNS to point to green)

# Keep blue running as rollback point
```

---

## 📈 Performance Optimization Tips

1. **Enable Redis Caching** - Cache frequently converted units
2. **Database Indexing** - Index on User.Email for faster lookups
3. **API Response Compression** - Enabled by default (gzip)
4. **Connection Pooling** - Configured by default in DbContext
5. **Async All The Way** - All I/O operations are async
6. **Horizontal Scaling** - Add more instances of hot services
7. **Load Balancing** - Distribute load across instances

---

## 🎯 Monitoring Setup Examples

### Prometheus Metrics
```csharp
// Add to each service's Program.cs
builder.Services.AddOpenTelemetry()
    .WithMetrics(builder => builder
        .AddAspNetCoreInstrumentation()
        .AddPrometheusExporter());
```

### ELK Stack Integration
```csharp
// Centralized logging
builder.Services.AddSerilog(new LoggerConfiguration()
    .WriteTo.Elasticsearch(...)
    .CreateLogger());
```

---

## 🚀 Production Deployment Steps

1. **Build & Push Images**
   ```bash
   docker build -t myregistry/api-gateway:v1 -f QuantityMeasurement.ApiGateway/Dockerfile .
   docker push myregistry/api-gateway:v1
   ```

2. **Deploy to Kubernetes**
   ```bash
   kubectl apply -f k8s/deployment.yml
   ```

3. **Configure Ingress**
   ```yaml
   apiVersion: networking.k8s.io/v1
   kind: Ingress
   metadata:
     name: quantity-ingress
   spec:
     rules:
     - host: api.yourdomain.com
       http:
         paths:
         - path: /
           backend:
             serviceName: api-gateway
             servicePort: 8080
   ```

4. **Monitor & Scale**
   ```bash
   kubectl autoscale deployment compare-service --min=1 --max=5 --cpu-percent=80
   ```

---

## 📚 Additional Resources

- **QUICKSTART.md** - Get started in 5 minutes
- **MICROSERVICES_DEPLOYMENT.md** - Complete deployment guide
- **MIGRATION_SUMMARY.md** - What was changed
- Swagger UI - `/swagger` endpoint on each service
- Docker Compose Docs - https://docs.docker.com/compose
- .NET Documentation - https://docs.microsoft.com/dotnet

---

## 🎓 Learning Paths

### For Developers
1. Understand microservices architecture (this document)
2. Run locally with Docker Compose (QUICKSTART.md)
3. Test endpoints using Swagger
4. Modify code and redeploy
5. Monitor logs and debug

### For DevOps
1. Review docker-compose.yml
2. Set up production environment
3. Configure CI/CD pipeline
4. Set up monitoring
5. Implement backup/restore

### For Architects
1. Review service responsibilities
2. Analyze scalability patterns
3. Plan disaster recovery
4. Design API versioning strategy
5. Plan migration to Kubernetes

---

**Ready to deploy? Start with QUICKSTART.md! 🚀**
