# Quantity Measurement API - Codebase Analysis

## Project Overview
This is a **monolithic ASP.NET Core API** built with Entity Framework Core that provides quantity measurement operations (comparison, conversion, arithmetic) with JWT authentication.

**Current Architecture**: Monolithic with 4-layer design:
- **Presentation Layer**: QuantityMeasurementAPI (Controllers)
- **Business Layer**: QuantityMeasurementBusinessLayer (Services + Engines)
- **Model Layer**: QuantityMeasurementModelLayer (DTOs + Entities)
- **Data Layer**: QuantityMeasurementRepositoryLayer (Repository + DbContext)

---

## 1. Current API Endpoints

### Authentication Controller (`api/auth`)
All endpoints handle user authentication and token generation:

| Endpoint | Method | Description | Auth Required |
|----------|--------|-------------|---|
| `/api/auth/register` | POST | Register new user with username/password | No |
| `/api/auth/login` | POST | Login with credentials, returns JWT token | No |
| `/api/auth/google-login` | POST | Google OAuth login with IdToken | No |

**Request Models:**
- `RegisterRequest`: { Username, Password }
- `LoginRequest`: { Username, Password }
- `GoogleLoginRequest`: { IdToken }

**Response Model:**
- `AuthResponse`: { Token (JWT), ExpiresIn, TokenType }

---

### Quantities Controller (`api/quantities`)
All endpoints (except History) allow anonymous access but save history only if authenticated:

| Endpoint | Method | Description | Auth Required | Saves History |
|----------|--------|-------------|---|---|
| `/api/quantities/compare` | POST | Compare two quantities for equality | No | If Auth |
| `/api/quantities/convert` | POST | Convert quantity to target unit | No | If Auth |
| `/api/quantities/add` | POST | Add two quantities (same type) | No | If Auth |
| `/api/quantities/subtract` | POST | Subtract second from first quantity | No | If Auth |
| `/api/quantities/divide` | POST | Divide first by second quantity | No | If Auth |
| `/api/quantities/history` | GET | Get all measurement operations (requires auth) | Yes | - |

**Request Models:**
- `BinaryOperationRequest`: { Q1: QuantityRequest, Q2: QuantityRequest }
- `ConversionRequest`: { Quantity: QuantityRequest, TargetUnit: string }
- `QuantityRequest`: { Value: double, Unit: string, MeasurementType: string }

**Response Models:**
- `ComparisonResponse`: { AreEqual: bool, Message: string }
- `ArithmeticOperationResponse`: { Result: double, Unit: string, MeasurementType: string }
- `ConversionResponse`: { Result: double, Unit: string, MeasurementType: string }
- `DivisionResponse`: { Result: double }
- `OperationHistoryResponse`: { Id, Operation, Operand1, Operand2, Result, CreatedAt }
- `ApiResponse<T>`: { Success: bool, Message: string, Data: T }

---

## 2. Database Configuration & Startup

### Startup Configuration (Program.cs)

**1. Authentication & Authorization:**
- JWT Bearer token authentication configured
- Token validation with configurable Issuer, Audience, Key, and DurationInMinutes
- Swagger configured with JWT Bearer security definition

**2. Database Setup:**
- **Primary**: Uses environment variable `DATABASE_URL` (PostgreSQL format)
- **Fallback**: Uses `appsettings.json` connection string (SQL Server)
- Dynamic connection string parsing for PostgreSQL URLs
- EF Core with Npgsql provider for PostgreSQL

**3. Dependency Injection:**
```
Services:
  - IQuantityMeasurementService → QuantityMeasurementServiceImpl
  - IAuthService → AuthServiceImpl
  
Repositories:
  - IQuantityMeasurementRepository → EFCoreQuantityMeasurementRepository
  - IUserRepository → EFCoreUserRepository
```

**4. CORS Configuration:**
- Enabled (needs to be verified in full Program.cs)

### Configuration Files

**appsettings.json:**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.\\SQLEXPRESS;Database=QuantityMeasurementDB;..."
  },
  "Jwt": {
    "Key": "SuperSecretKeyForQuantityMeasurementApiUseCase18!$@2026",
    "Issuer": "QuantityMeasurementAPI",
    "Audience": "QuantityMeasurementAPIUsers",
    "DurationInMinutes": 60
  },
  "Authentication": {
    "Google": {
      "ClientId": "407408718192.apps.googleusercontent.com"
    }
  }
}
```

---

## 3. Business Logic in Each Layer

### Service Layer: `IQuantityMeasurementService`

**Methods:**
1. `Compare(QuantityDTO q1, QuantityDTO q2, bool saveHistory)` 
   - Returns: bool (true if equal)
   - Validates same measurement type
   - Converts to base unit and compares

2. `Convert(QuantityDTO input, string targetUnit, bool saveHistory)`
   - Returns: QuantityDTO with converted value
   - Converts input to base unit, then to target unit

3. `Add(QuantityDTO q1, QuantityDTO q2, bool saveHistory)`
   - Returns: QuantityDTO sum
   - Validates same measurement type
   - Throws exception if Temperature (not supported)
   - Returns result in Q1's unit

4. `Subtract(QuantityDTO q1, QuantityDTO q2, bool saveHistory)`
   - Returns: QuantityDTO difference
   - Same constraints as Add

5. `Divide(QuantityDTO q1, QuantityDTO q2, bool saveHistory)`
   - Returns: double (scalar result, no unit)
   - Throws exception if divisor is zero

6. `GetHistory()`
   - Returns: List<QuantityMeasurementEntity>
   - Retrieves all saved operations

**Implementation: `QuantityMeasurementServiceImpl`**
- Uses `IQuantityMeasurementRepository` for persistence
- Handles unit conversions through enum extension methods (ToBaseUnit, FromBaseUnit)
- Maps QuantityDTOs to internal QuantityModel for storage
- Conditionally saves operations to database via `repository.Save()`

---

### Service Layer: `IAuthService`

**Methods:**
1. `RegisterAsync(RegisterRequest request)`
   - Validates username uniqueness
   - Hashes password using SecurityHelper
   - Creates UserEntity and saves via repository
   - Generates JWT token

2. `LoginAsync(LoginRequest request)`
   - Retrieves user by username
   - Verifies password hash
   - Generates JWT token

3. `GoogleLoginAsync(GoogleLoginRequest request)`
   - Validates Google IdToken
   - Finds user by Google email or creates new user
   - Stores placeholder "GOOGLE_OAUTH_LOGIN" as password
   - Generates JWT token

**Implementation: `AuthServiceImpl`**
- Uses `IUserRepository` for user CRUD
- Uses `SecurityHelper` for password hashing/verification
- Uses Google.Apis.Auth for token validation
- Reads JWT configuration from IConfiguration

---

### Engine Layer (Business Logic Utilities)

#### 1. **ConversionEngine** (Static Helper)
```
ConvertToBase(QuantityDTO) → double
  - Parses unit enum by measurement type
  - Calls ToBaseUnit() extension on enum
  - Converts Length, Weight, Volume, Temperature

ConvertFromBase(measurementType, unit, baseValue) → double
  - Calls FromBaseUnit() extension on parsed enum
  - Converts base unit back to target unit
```

#### 2. **ArithmeticEngine** (Static Helper)
```
Add(v1, v2, measurementType) → double
  - Throws if Temperature
  - Returns v1 + v2

Subtract(v1, v2, measurementType) → double
  - Throws if Temperature
  - Returns v1 - v2

Divide(v1, v2, measurementType) → double
  - Throws if Temperature
  - Throws if v2 == 0
  - Returns v1 / v2
```

#### 3. **ValidationEngine** (Static Helper)
```
ValidateSameMeasurement(q1, q2)
  - Throws if measurement types differ
```

---

### Repository Layer

#### Interfaces:

**IQuantityMeasurementRepository:**
- `Save(QuantityMeasurementEntity)` - Persist operation
- `GetAll()` - Get all operations
- `GetMeasurementsByOperation(OperationType)` - Filter by operation type
- `GetTotalCount()` - Count operations
- `DeleteAll()` - Clear all data
- `CloseResources()` - Cleanup

**IUserRepository:**
- `GetUserByUsernameAsync(string)` - Find user by username
- `AddUserAsync(UserEntity)` - Create new user

#### Implementations:
- `EFCoreQuantityMeasurementRepository` - EF Core implementation with DbContext
- `EFCoreUserRepository` - EF Core implementation with DbContext
- `QuantityMeasurementCacheRepository` - Caching implementation (details not examined)

---

### Data Model Layer

#### Entities:

**UserEntity:**
```csharp
{
  Id (PK),
  Username (string, required, max 100),
  PasswordHash (string, required)
}
```

**QuantityMeasurementEntity:**
```csharp
{
  Id (PK),
  Operand1 (QuantityModel<object>),
  Operand2 (QuantityModel<object>),
  Operation (OperationType enum),
  Result (object),
  HasError (bool),
  ErrorMessage (string),
  CreatedAt (DateTime)
}
```

#### DTOs:

**QuantityDTO:**
```csharp
{
  Value (double),
  Unit (string),
  MeasurementType (string) // "Length", "Weight", "Volume", "Temperature"
}
```

#### Enums:
- `LengthUnit` - Meter, Kilometer, Centimeter, Mile, Yard, Foot, Inch
- `WeightUnit` - Kilogram, Gram, Milligram, Pound, Ounce
- `VolumeUnit` - Liter, Milliliter, Gallon, Pint, Cup
- `TemperatureUnit` - Celsius, Fahrenheit, Kelvin
- `OperationType` - Compare, Convert, Add, Subtract, Divide

---

## 4. Service Communication Flow

```
REQUEST FLOW:
┌─────────────────────────────────────────────────────────────┐
│ Client (HTTP)                                               │
└──────────────────────┬──────────────────────────────────────┘
                       │
                       ↓
        ┌──────────────────────────────────┐
        │ API Controllers                  │
        │ ├─ AuthController                │
        │ └─ QuantitiesController          │
        └──────────────────┬───────────────┘
                           │
                ┌──────────┴──────────┐
                │                     │
                ↓                     ↓
        ┌───────────────┐    ┌─────────────────┐
        │ IAuthService  │    │ IQuantityService│
        └───────┬───────┘    └────────┬────────┘
                │                     │
                │                ┌────┴────────────┐
                │                │                 │
                │                ↓                 ↓
                │         ┌──────────────┐  ┌────────────┐
                │         │ Engines      │  │ Repository │
                │         │ ├─ Arithmetic│  │ (Quantity) │
                │         │ ├─ Conversion│  └────┬───────┘
                │         │ └─Validation │       │
                │         └──────────────┘       │
                │                                 │
                ↓                                 ↓
        ┌─────────────────┐            ┌──────────────────┐
        │ IUserRepository │            │ ApplicationDb    │
        │ (User CRUD)     │            │ Context (EF)     │
        └────────┬────────┘            └────────┬─────────┘
                 │                              │
                 └──────────────┬───────────────┘
                                │
                                ↓
                    ┌────────────────────────┐
                    │ PostgreSQL/SQL Server  │
                    └────────────────────────┘
```

### Authentication Flow:
1. Client sends RegisterRequest/LoginRequest/GoogleLoginRequest
2. AuthController delegates to IAuthService
3. IAuthService validates input, manages user via IUserRepository
4. Returns JWT token via AuthResponse
5. Client uses token in Authorization header for subsequent requests

### Quantity Operation Flow:
1. Client sends operation request with authentication (optional)
2. QuantitiesController validates request
3. Creates QuantityDTOs from request
4. Calls IQuantityMeasurementService method
5. Service applies business logic (validation, conversion, arithmetic)
6. If authenticated, saves operation to repository
7. Returns response with result

---

## 5. What Needs to be Split into Separate Microservices

### Current Monolith Issues:
1. **Mixed Responsibilities**: Auth and Quantity operations in one API
2. **Single Database**: User data and operation history tightly coupled
3. **Deployment Coupling**: Any change requires redeploying entire application
4. **Scaling Issues**: Can't scale specific features independently
5. **Test Coupling**: Auth tests run with quantity tests

### Recommended Microservices Split:

#### **Option A: Domain-Based Split (Recommended)**

```
┌──────────────────────────────────┐
│  API Gateway                     │
│  (Routes & Auth Validation)      │
└─────┬──────────────────────┬─────┘
      │                      │
      ↓                      ↓
  ┌─────────────┐    ┌──────────────────┐
  │ Auth        │    │ Quantity         │
  │ Service     │    │ Measurement      │
  │             │    │ Service          │
  │ ├─ Register │    │                  │
  │ ├─ Login    │    │ ├─ Compare       │
  │ └─ Google   │    │ ├─ Convert       │
  │   OAuth     │    │ ├─ Add           │
  │             │    │ ├─ Subtract      │
  │ DB: Users   │    │ ├─ Divide        │
  │             │    │ └─ History       │
  │             │    │                  │
  │             │    │ DB: Operations   │
  └─────────────┘    └──────────────────┘
```

**Microservice 1: Auth Service**
- **Responsibility**: User management, token generation, OAuth
- **Endpoints**: /auth/*
- **Database**: Users table only
- **Dependencies**: JWT library, Google.Apis.Auth
- **Current Artifacts**:
  - AuthController
  - IAuthService, AuthServiceImpl
  - UserEntity, IUserRepository, EFCoreUserRepository

**Microservice 2: Quantity Measurement Service**
- **Responsibility**: All measurement operations, history tracking
- **Endpoints**: /api/quantities/*
- **Database**: Operations table only
- **Dependencies**: Calculation engines
- **Current Artifacts**:
  - QuantitiesController
  - IQuantityMeasurementService, QuantityMeasurementServiceImpl
  - ArithmeticEngine, ConversionEngine, ValidationEngine
  - QuantityMeasurementEntity, IQuantityMeasurementRepository

---

#### **Option B: Feature-Based Split (More Granular)**

```
┌─────────────────────┐
│ API Gateway         │
└─────────────────────┘
        ↓
  ┌─────┬─────┬──────┬──────┐
  ↓     ↓     ↓      ↓      ↓
┌────┐┌────┐┌─────┐┌─────┐┌─────┐
│Auth││User││Conv-││Arith-││Hist-│
│Svc ││Svc ││Svc  ││Svc   ││Svc  │
└────┘└────┘└─────┘└─────┘└─────┘
```

More complex but allows independent scaling of conversion operations, arithmetic, etc.

---

## 6. Data Requirements for Microservices

### Auth Service Database Schema:
```sql
-- User table only
CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY,
    Username NVARCHAR(100) UNIQUE NOT NULL,
    PasswordHash NVARCHAR(MAX) NOT NULL
)
```

### Quantity Service Database Schema:
```sql
-- Operations table (current QuantityMeasurementEntity mapping)
CREATE TABLE QuantityMeasurements (
    Id INT PRIMARY KEY IDENTITY,
    Operation NVARCHAR(50) NOT NULL,     -- Compare, Convert, Add, etc.
    Operand1 NVARCHAR(MAX),               -- JSON serialized
    Operand2 NVARCHAR(MAX),               -- JSON serialized
    Result NVARCHAR(MAX),                 -- JSON serialized
    HasError BIT,
    ErrorMessage NVARCHAR(MAX),
    CreatedAt DATETIME2
)
```

### Service-to-Service Communication:
- **Auth → Quantity**: Quantity service validates JWT tokens (public key from Auth)
- **Quantity → Auth**: Optional - call Auth service for token introspection
- **API Gateway** (if added): Handles auth middleware, routes to services

---

## 7. Current Technology Stack

| Layer | Technology |
|-------|-----------|
| **Framework** | ASP.NET Core 6+ |
| **Database** | PostgreSQL (primary), SQL Server (dev) |
| **ORM** | Entity Framework Core with Npgsql |
| **Authentication** | JWT Bearer + Google OAuth |
| **API Documentation** | Swagger/OpenAPI |
| **Testing** | xUnit (test project present) |
| **Architecture** | Monolithic, 4-layer design |

---

## 8. Key Implementation Details

### Unit Conversion System:
- Uses **base unit pattern**: Convert input to base unit → convert base to target
- Enum extensions with `ToBaseUnit()` and `FromBaseUnit()` methods
- Supported measurement types: Length, Weight, Volume, Temperature
- Temperature addition/subtraction/division explicitly blocked

### History Tracking:
- Optional per-operation: `saveHistory` parameter
- Only saved when user is authenticated
- Stores operation type, operands, result, timestamp
- Queryable by operation type

### Error Handling:
- Custom `QuantityMeasurementException` for business logic errors
- Global Exception Handling Middleware
- Custom error response models

---

## 9. Deployment Considerations

**Current (Monolithic):**
- Single deployment artifact
- Environment variable `DATABASE_URL` for PostgreSQL URL
- Fallback to appsettings.json for SQL Server

**After Microservices Split:**
- Each service deployed independently
- Shared appsettings or config service (e.g., Spring Cloud Config)
- API Gateway for request routing
- Service discovery for inter-service communication
- Each service independently scalable
