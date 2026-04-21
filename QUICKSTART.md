# Quick Start Guide - Microservices

## 🚀 Start All Services in 5 Minutes

### Prerequisites
- Docker & Docker Compose installed
- Git
- 5 ports available: 5000, 5001, 5002, 5003, 5004, 5432

### Fastest Method: Docker Compose

```bash
# 1. Navigate to project root
cd d:\Main_Project\Github

# 2. Build and start all services
docker-compose up --build

# 3. Wait for all services to be healthy (2-3 minutes)
# You'll see "listening on http" for each service

# 4. Access services
# API Gateway:        http://localhost:5000/swagger
# Auth Service:       http://localhost:5001/swagger
# Compare Service:    http://localhost:5002/swagger
# Convert Service:    http://localhost:5003/swagger
# Arithmetic Service: http://localhost:5004/swagger
```

### Stop All Services

```bash
# Stop without removing containers
docker-compose stop

# Stop and remove containers
docker-compose down

# Stop and remove containers + volumes (clean state)
docker-compose down -v
```

---

## 🧪 Quick Test

### 1. Register a User
```bash
curl -X POST http://localhost:5000/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{
    "email": "test@example.com",
    "password": "Test@123456",
    "name": "Test User"
  }'
```

Expected response:
```json
{
  "success": true,
  "message": "Authentication successful",
  "token": "eyJhbGciOiJIUzI1NiIs...",
  "user": {
    "id": 1,
    "email": "test@example.com",
    "name": "Test User"
  }
}
```

### 2. Save the Token
Copy the `token` value from the response. You'll use it for subsequent requests.

### 3. Compare Two Quantities
```bash
curl -X POST http://localhost:5000/api/quantities/compare \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIs..." \
  -d '{
    "q1": {"value": 1, "unit": "Feet", "measurementType": "Length"},
    "q2": {"value": 12, "unit": "Inch", "measurementType": "Length"}
  }'
```

Expected response:
```json
{
  "success": true,
  "message": "Comparison successful",
  "data": {
    "areEqual": true,
    "message": "Quantities are equal"
  }
}
```

### 4. Convert Units
```bash
curl -X POST http://localhost:5000/api/quantities/convert \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIs..." \
  -d '{
    "quantity": {"value": 1000, "unit": "Gram", "measurementType": "Weight"},
    "targetUnit": "Kilogram"
  }'
```

### 5. Add Quantities
```bash
curl -X POST http://localhost:5000/api/quantities/add \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIs..." \
  -d '{
    "q1": {"value": 5, "unit": "Feet", "measurementType": "Length"},
    "q2": {"value": 2, "unit": "Feet", "measurementType": "Length"}
  }'
```

---

## 📡 API Gateway Endpoints

**Base URL:** `http://localhost:5000/api`

### Authentication
```
POST   /auth/register       - Create new account
POST   /auth/login          - Login with email/password
POST   /auth/google-login   - Login with Google
GET    /auth/validate-token - Check if token is valid
```

### Quantities
```
POST   /quantities/compare    - Compare 2 quantities (q1 == q2?)
POST   /quantities/convert    - Convert units
POST   /quantities/add        - Add 2 quantities (q1 + q2)
POST   /quantities/subtract   - Subtract quantities (q1 - q2)
POST   /quantities/divide     - Divide quantities (q1 / q2)
GET    /quantities/history    - Get operation history
```

---

## 🔑 Environment Variables

All services share these JWT configs (in docker-compose.yml):
```
Jwt__Key: your-super-secret-key-change-this-in-production-1234567890
Jwt__Issuer: QuantityMeasurementApp
Jwt__Audience: QuantityMeasurementAppUsers
Jwt__DurationInMinutes: 60
```

### Change JWT Key
Edit `docker-compose.yml` line 127:
```yaml
Jwt__Key: YOUR_NEW_SECRET_KEY_HERE_32_CHARS_MIN
```

---

## 🐳 Docker Commands Cheat Sheet

```bash
# View running containers
docker-compose ps

# View logs from all services
docker-compose logs -f

# View logs from specific service
docker-compose logs -f auth-service

# Restart a service
docker-compose restart auth-service

# Execute command in container
docker-compose exec auth-service sh

# Remove everything and start fresh
docker-compose down -v && docker-compose up --build

# Scale services (horizontal scaling)
docker-compose up -d --scale compare-service=3
```

---

## 🛠️ Manual Setup (Without Docker)

If you prefer running services directly:

### 1. Create PostgreSQL Database
```bash
# Using psql
createdb quantity_measurement_auth
```

### 2. Start Services (in separate terminals)

**Terminal 1: Auth Service**
```bash
cd QuantityMeasurement.AuthService
dotnet restore
dotnet run --urls=http://localhost:5001
```

**Terminal 2: Compare Service**
```bash
cd QuantityMeasurement.CompareService
dotnet restore
dotnet run --urls=http://localhost:5002
```

**Terminal 3: Convert Service**
```bash
cd QuantityMeasurement.ConvertService
dotnet restore
dotnet run --urls=http://localhost:5003
```

**Terminal 4: Arithmetic Service**
```bash
cd QuantityMeasurement.ArithmeticService
dotnet restore
dotnet run --urls=http://localhost:5004
```

**Terminal 5: API Gateway**
```bash
cd QuantityMeasurement.ApiGateway
dotnet restore
dotnet run --urls=http://localhost:5000
```

---

## 🔍 Health Checks

Check if all services are running:

```bash
# API Gateway
curl http://localhost:5000/swagger

# Auth Service
curl http://localhost:5001/swagger

# Compare Service
curl http://localhost:5002/swagger

# Convert Service
curl http://localhost:5003/swagger

# Arithmetic Service
curl http://localhost:5004/swagger
```

All should return HTML with Swagger UI.

---

## 🧩 Service Architecture

```
Frontend (Angular/React)
        ↓
API Gateway (5000) - Routes & Auth
    ↓  ↓  ↓  ↓
Auth Compare Convert Arithmetic
5001  5002  5003    5004
 ↓
PostgreSQL Database
```

---

## 📊 Supported Units

### Length
- Feet, Inch, Yard, Centimeter

### Weight  
- Kilogram, Gram, Pound

### Volume
- Litre, Millilitre, Gallon

### Temperature
- Celsius, Fahrenheit, Kelvin
- (No arithmetic operations for Temperature)

---

## ✅ Verification Checklist

After starting services, verify:

- [ ] All 5 services show as healthy in `docker-compose ps`
- [ ] API Gateway Swagger loads at http://localhost:5000/swagger
- [ ] Can register a user: `/api/auth/register`
- [ ] Can login: `/api/auth/login`
- [ ] Can compare quantities: `/api/quantities/compare`
- [ ] Can convert units: `/api/quantities/convert`
- [ ] Can add quantities: `/api/quantities/add`
- [ ] Database has created users table

---

## 🚨 Troubleshooting

### Port Already in Use
```bash
# Find process using port 5000
netstat -ano | findstr :5000

# Kill process (Windows)
taskkill /PID <PID> /F

# Or change port in appsettings
```

### Service Not Starting
```bash
# Check logs
docker-compose logs auth-service

# Rebuild image
docker-compose build --no-cache auth-service
```

### Database Connection Failed
```bash
# Verify PostgreSQL is running
docker ps | grep postgres

# Check connection
psql -U postgres -d quantity_measurement_auth
```

### CORS Issues
Update allowed origins in `QuantityMeasurement.ApiGateway/Program.cs`:
```csharp
policy.WithOrigins(
    "http://localhost:4200",  // Angular
    "http://localhost:3000",  // React
    "your-frontend-url.com"   // Production
)
```

---

## 📖 Full Documentation

See `MICROSERVICES_DEPLOYMENT.md` for:
- Complete API reference
- Deployment to production
- Database schema
- Performance optimization
- Security considerations
- Advanced configuration

---

## 🎯 Next Steps

1. **Start services:** `docker-compose up --build`
2. **Test endpoints:** See "Quick Test" section above
3. **Connect frontend:** Update API URL to `http://localhost:5000/api`
4. **Deploy to cloud:** See production deployment section in full docs

---

## 💡 Tips

- Use Swagger UI at `/swagger` to test endpoints
- All requests except `/auth/register`, `/auth/login`, `/auth/google-login` require Bearer token
- Check service health: `docker-compose ps`
- View logs: `docker-compose logs -f`
- Services auto-restart if they crash (Docker healthchecks)

---

Good luck! Your microservices are ready to go. 🚀
