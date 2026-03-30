# QuantityMeasurementApp - UC18: Security, JWT & OAuth Concepts

## 🛡️ UC18 Overview

**UC18** enhances the ASP.NET Core Web API with robust security features. It introduces user authentication and authorization utilizing JSON Web Tokens (JWT), along with password hashing and AES symmetric encryption utilities.

### 🔐 Key Security Features Implemented

1. **JWT Authentication**: Implemented standard Bearer Token Auth via JWT.
2. **Authorization Profiles**: Applied `[Authorize]` globally to secure REST API endpoints like measurement conversion and history retrieval.
3. **Password Hashing**: Implemented PBKDF2 (HMACSHA256) for secure credential storage in the database.
4. **Data Encryption Utility**: Created an AES (Advanced Encryption Standard) encryption and decryption helper for confidential data handling.
5. **Auth Controller**: Added new POST endpoints for user `/api/auth/register` and `/api/auth/login`.

---

# QuantityMeasurementApp – UC17: Transformation to ASP.NET Core Web API with EF Core
## 🎯 UC17 Overview

**UC17** transforms the UC16 Console Application into a professional **ASP.NET Core Web API** with **Entity Framework Core (ORM)**, replacing manual ADO.NET SQL operations with a modern, production-grade REST API architecture.

### 🔄 From Console to Web API

| Aspect | UC16 (Console App) | UC17 (Web API) |
|---|---|---|
| **Entry Point** | Console Menu | HTTP Server |
| **User Interaction** | Text-based console input | REST API requests (JSON) |
| **Architecture** | Console → Controller → Service → ADO.NET → SQL | HTTP → Controller → Service → EF Core → SQL |
| **Database Access** | Manual SQL + ADO.NET | EF Core ORM |
| **Data Format** | In-memory objects | JSON request/response |
| **Documentation** | Code comments | Swagger/OpenAPI |
| **Scalability** | Single-user console | Multi-user, concurrent API |

### 🧪 Test Examples

**UC16 (Console)**
```
Menu opens → User types "Add 1 feet + 12 inches" → Result printed to console
```

**UC17 (Web API)**
```
POST http://localhost:5000/api/quantities/add
Content-Type: application/json

{
  "q1": { "value": 1, "unit": "feet", "measurementType": "Length" },
  "q2": { "value": 12, "unit": "inches", "measurementType": "Length" }
}

Response:
{
  "success": true,
  "message": "Addition successful",
  "data": {
    "result": 2.0,
    "unit": "feet",
    "measurementType": "Length"
  },
  "timestamp": "2026-03-20T12:34:56.789Z"
}
```

---

## 📋 UC17 Features

### ✅ RESTful API Endpoints
- **POST /api/quantities/compare** – Compare two quantities
- **POST /api/quantities/convert** – Convert quantity to different unit
- **POST /api/quantities/add** – Add two quantities
- **POST /api/quantities/subtract** – Subtract two quantities
- **POST /api/quantities/divide** – Divide two quantities
- **GET /api/quantities/history** – Retrieve operation history
- **GET /api/quantities/count** – Get total operation count
- **GET /api/quantities/health** – Health check endpoint

### ✅ Entity Framework Core (EF Core)
- **Automatic ORM mapping** – No manual SQL queries
- **DbContext abstraction** – Single point of database configuration
- **Migrations system** – Version control for database schema
- **LINQ queries** – Type-safe, compile-time checked queries
- **Lazy loading & eager loading** – Optimized data fetching
- **Transaction support** – Built-in ACID guarantees

### ✅ Data Validation
- **Fluent validation** – Data annotations on request models
- **Model binding** – Automatic request deserialization
- **Error responses** – Standardized error messages
- **HTTP status codes** – Proper REST semantics (400, 404, 500)

### ✅ Global Exception Handling
- **Middleware-based** – Catches all unhandled exceptions
- **Custom error responses** – Consistent error format
- **Logging** – All exceptions logged for debugging
- **Domain exceptions** – Special handling for business logic errors

### ✅ API Documentation
- **Swagger UI** – Interactive API exploration at `/swagger`
- **OpenAPI spec** – Machine-readable API definition
- **XML comments** – Method descriptions in responses
- **Response examples** – Sample data for each endpoint

---

## 📁 UC17 Project Structure

```
QuantityMeasurementApp/
│
├── QuantityMeasurementAPI/                      # NEW: Web API Layer (UC17)
│   ├── Controllers/
│   │   └── QuantitiesController.cs              # REST API endpoints
│   ├── Data/
│   │   ├── ApplicationDbContext.cs              # EF Core DbContext
│   │   └── Migrations/
│   │       ├── 20260320000000_InitialCreate.cs  # Auto-generated migration
│   │       ├── ApplicationDbContextModelSnapshot.cs
│   │       └── (future migrations here)
│   ├── Middleware/
│   │   └── GlobalExceptionHandlingMiddleware.cs # Global error handler
│   ├── Models/
│   │   ├── Request/
│   │   │   └── RequestModels.cs                 # API request DTOs
│   │   └── Response/
│   │       └── ResponseModels.cs                # API response DTOs
│   ├── Program.cs                               # DI & middleware setup
│   ├── appsettings.json                         # Configuration
│   ├── launchSettings.json                      # Profile settings
│   ├── QuantityMeasurementAPI.csproj            # Project file
│   └── obj, bin/                                # Build output
│
├── QuantityMeasurementBusinessLayer/            # UNCHANGED: Core logic
│   ├── Controllers/                             # (No longer used by API)
│   ├── Services/
│   │   ├── IQuantityMeasurementService.cs
│   │   └── QuantityMeasurementServiceImpl.cs
│   ├── Engines/
│   │   ├── ArithmeticEngine.cs
│   │   ├── ConversionEngine.cs
│   │   └── ValidationEngine.cs
│   └── Exceptions/
│       └── QuantityMeasurementException.cs
│
├── QuantityMeasurementRepositoryLayer/          # UPGRADED: EF Core support
│   ├── Interfaces/
│   │   └── IQuantityMeasurementRepository.cs
│   └── Implementations/
│       ├── QuantityMeasurementDatabaseRepository.cs  # (UC16 ADO.NET)
│       ├── QuantityMeasurementCacheRepository.cs     # (UC15)
│       └── EFCoreQuantityMeasurementRepository.cs    # NEW: EF Core implementation
│
├── QuantityMeasurementModelLayer/               # UPGRADED: EF Core entities
│   ├── DTO/
│   │   └── QuantityDTO.cs
│   ├── Entities/
│   │   └── QuantityMeasurementEntity.cs         # (Added Id, CreatedAt properties)
│   ├── Models/
│   │   └── QuantityModel.cs
│   └── Enums/
│       ├── LengthUnit.cs
│       ├── WeightUnit.cs
│       ├── VolumeUnit.cs
│       ├── TemperatureUnit.cs
│       └── OperationType.cs
│
├── QuantityMeasurementConsole/                  # OPTIONAL: Legacy console app
│   ├── Menu.cs
│   ├── Program.cs
│   └── (Can coexist with API)
│
├── QuantityMeasurementApp.Tests/                # UPGRADED: API tests
│   ├── Engines/
│   ├── Services/
│   ├── Repository/
│   └── Integration/
│
├── QuantityMeasurementApp.slnx                  # UPDATED: Includes API project
└── README.md                                    # THIS FILE
```

---

## 🔧 Technology Stack (UC17)

| Component | Technology | Purpose |
|---|---|---|
| **Web Framework** | ASP.NET Core 8.0 | RESTful API server |
| **ORM** | Entity Framework Core 8.0 | Object-relational mapping |
| **Database** | SQL Server | Persistent data store |
| **API Docs** | Swashbuckle (Swagger) | Interactive API documentation |
| **Logging** | Built-in ILogger | Operation logging |
| **Validation** | Data Annotations | Request validation |
| **Serialization** | System.Text.Json | JSON request/response handling |

---

## 🌐 API Endpoints Reference

### Operation Endpoints

#### 1. Compare Quantities
```http
POST /api/quantities/compare
Content-Type: application/json

{
  "q1": {
    "value": 10,
    "unit": "feet",
    "measurementType": "Length"
  },
  "q2": {
    "value": 120,
    "unit": "inches",
    "measurementType": "Length"
  }
}
```
**Response (200 OK):**
```json
{
  "success": true,
  "message": "Comparison successful",
  "data": {
    "areEqual": true,
    "message": "Quantities are equal"
  },
  "timestamp": "2026-03-20T12:34:56.789Z"
}
```

#### 2. Convert Unit
```http
POST /api/quantities/convert
Content-Type: application/json

{
  "quantity": {
    "value": 1,
    "unit": "feet",
    "measurementType": "Length"
  },
  "targetUnit": "inches"
}
```
**Response (200 OK):**
```json
{
  "success": true,
  "message": "Conversion successful",
  "data": {
    "result": 12.0,
    "unit": "inches",
    "measurementType": "Length"
  },
  "timestamp": "2026-03-20T12:34:56.789Z"
}
```

#### 3. Add Quantities
```http
POST /api/quantities/add
Content-Type: application/json

{
  "q1": {
    "value": 1,
    "unit": "feet",
    "measurementType": "Length"
  },
  "q2": {
    "value": 12,
    "unit": "inches",
    "measurementType": "Length"
  }
}
```
**Response (200 OK):**
```json
{
  "success": true,
  "message": "Addition successful",
  "data": {
    "result": 2.0,
    "unit": "feet",
    "measurementType": "Length"
  },
  "timestamp": "2026-03-20T12:34:56.789Z"
}
```

#### 4. Get Operation History
```http
GET /api/quantities/history
```
**Response (200 OK):**
```json
{
  "success": true,
  "message": "History retrieved successfully",
  "data": [
    {
      "id": 1,
      "operation": "Add",
      "operand1": {"value": 1, "unit": "feet"},
      "operand2": {"value": 12, "unit": "inches"},
      "result": 2.0,
      "hasError": false,
      "errorMessage": null,
      "createdAt": "2026-03-20T12:34:56.789Z"
    },
    ...
  ],
  "timestamp": "2026-03-20T12:34:56.789Z"
}
```

#### 5. Get Operation Count
```http
GET /api/quantities/count
```
**Response (200 OK):**
```json
{
  "success": true,
  "message": "Count retrieved successfully",
  "data": {
    "totalOperations": 42
  },
  "timestamp": "2026-03-20T12:34:56.789Z"
}
```

#### 6. Health Check
```http
GET /api/quantities/health
```
**Response (200 OK):**
```json
{
  "status": "API is running",
  "timestamp": "2026-03-20T12:34:56.789Z"
}
```

---

## 🛠️ EF Core Configuration

### ApplicationDbContext
```csharp
public class ApplicationDbContext : DbContext
{
    public DbSet<QuantityMeasurementEntity> QuantityMeasurements { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure QuantityMeasurementEntity
        modelBuilder.Entity<QuantityMeasurementEntity>()
            .HasKey(e => e.Id);

        // JSON converters for complex types
        modelBuilder.Entity<QuantityMeasurementEntity>()
            .Property(e => e.Operand1)
            .HasConversion(...); // Serialize to JSON

        // Indexes for performance
        modelBuilder.Entity<QuantityMeasurementEntity>()
            .HasIndex(e => e.CreatedAt);

        modelBuilder.Entity<QuantityMeasurementEntity>()
            .HasIndex(e => e.Operation);
    }
}
```

### DbContext Injection
```csharp
// In Program.cs
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
```

### Connection String
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.\\SQLEXPRESS;Database=QuantityMeasurementDB;Trusted_Connection=true;Encrypt=false;"
  }
}
```

---

## 📊 EF Core vs ADO.NET Comparison

| Aspect | UC16 (ADO.NET) | UC17 (EF Core) |
|---|---|---|
| **Query Syntax** | Raw SQL strings | LINQ (Language Integrated Query) |
| **Type Safety** | Compile time: ❌ | Compile time: ✅ |
| **SQL Injection** | Manual parameterization | Automatic parameterization |
| **Boilerplate Code** | High | Low |
| **Migrations** | Manual SQL scripts | Automatic code-first migrations |
| **Performance** | Fast (direct SQL) | Very fast (optimized LINQ) |
| **Learning Curve** | Moderate | Moderate |
| **Testability** | Moderate | High (DbContext mocking) |
| **Example** | `new SqlCommand("SELECT * FROM QuantityMeasurements WHERE OperationType = @type")` | `_context.QuantityMeasurements.Where(e => e.Operation == operationType)` |

---

## 🚀 Running the UC17 API

### 1. Build the Solution
```bash
cd d:\Main_Project\Github
dotnet build
```

### 2. Apply Database Migrations
```bash
cd QuantityMeasurementAPI
dotnet ef database update
```

### 3. Run the API Server
```bash
dotnet run
```

The API will start at: **http://localhost:5000**

### 4. Access Swagger UI
Navigate to: **http://localhost:5000/swagger**

You'll see an interactive interface to test all API endpoints!

---

## 📝 Request/Response Models

### QuantityRequest (Request DTO)
```csharp
public class QuantityRequest
{
    [Required]
    [Range(0, double.MaxValue)]
    public double Value { get; set; }

    [Required]
    public string Unit { get; set; }

    [Required]
    public string MeasurementType { get; set; }
}
```

### BinaryOperationRequest (Request DTO)
```csharp
public class BinaryOperationRequest
{
    [Required]
    public QuantityRequest Q1 { get; set; }

    [Required]
    public QuantityRequest Q2 { get; set; }
}
```

### ApiResponse<T> (Generic Response Wrapper)
```csharp
public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
    public DateTime Timestamp { get; set; }
}
```

---

## 🧪 Testing UC17 API

### Using cURL
```bash
curl -X POST http://localhost:5000/api/quantities/add \
  -H "Content-Type: application/json" \
  -d '{
    "q1": {"value": 1, "unit": "feet", "measurementType": "Length"},
    "q2": {"value": 12, "unit": "inches", "measurementType": "Length"}
  }'
```

### Using Postman
1. Create POST request to `http://localhost:5000/api/quantities/add`
2. Set header: `Content-Type: application/json`
3. Body (raw JSON):
```json
{
  "q1": {"value": 1, "unit": "feet", "measurementType": "Length"},
  "q2": {"value": 12, "unit": "inches", "measurementType": "Length"}
}
```
4. Send and view response

### Using Swagger UI
1. Navigate to `http://localhost:5000/swagger`
2. Click on the endpoint (e.g., "POST /api/quantities/add")
3. Click "Try it out"
4. Fill in the request body
5. Click "Execute"

---

## 🔄 Dependency Injection Setup (Program.cs)

```csharp
// EF Core DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Service Layer
builder.Services.AddScoped<IQuantityMeasurementService, QuantityMeasurementServiceImpl>();

// Repository Layer (EF Core implementation)
builder.Services.AddScoped<IQuantityMeasurementRepository, EFCoreQuantityMeasurementRepository>();

// Add Swagger
builder.Services.AddSwaggerGen();

// Add CORS (for frontend integration)
builder.Services.AddCors(options => {...});
```

---

## ⚠️ Global Exception Handler Middleware

Catches all unhandled exceptions and returns standardized error responses:

```csharp
public class GlobalExceptionHandlingMiddleware
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (QuantityMeasurementException qEx)
        {
            // Return 400 Bad Request for domain errors
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync(JsonSerializer.Serialize(new ErrorResponse { ... }));
        }
        catch (Exception ex)
        {
            // Return 500 Internal Server Error for unexpected errors
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync(JsonSerializer.Serialize(new ErrorResponse { ... }));
        }
    }
}
```

---

## 🔍 Database Schema (EF Core)

After migrations, the `QuantityMeasurements` table will have:

```
Columns:
- Id (int) – Primary Key, auto-increment
- Operand1 (nvarchar(max)) – JSON serialized first operand
- Operand2 (nvarchar(max)) – JSON serialized second operand
- Operation (nvarchar) – Enum: Compare, Convert, Add, Subtract, Divide
- Result (nvarchar(max)) – JSON serialized result
- HasError (bit) – Success/failure flag
- ErrorMessage (nvarchar(500)) – Error details if failed
- CreatedAt (datetime2) – Timestamp of operation

Indexes:
- PK_QuantityMeasurements (Id)
- IX_QuantityMeasurements_CreatedAt (for sorting)
- IX_QuantityMeasurements_Operation (for filtering)
```

---

## 📚 Design Patterns in UC17

### 1. **Repository Pattern** (Abstraction)
- IQuantityMeasurementRepository defines contract
- EFCoreQuantityMeasurementRepository implements EF Core logic
- Service layer remains agnostic to persistence mechanism

### 2. **Dependency Injection (DI)**
- Tight coupling eliminated
- Services injected via constructor
- Easy to mock for unit testing

### 3. **Data Transfer Objects (DTOs)**
- Separate request/response models from entities
- Validates incoming data
- Decouples API contracts from persistence models

### 4. **Service Layer Pattern**
- Business logic encapsulated in IQuantityMeasurementService
- Reusable across Console and Web API
- Easy to test in isolation

### 5. **Middleware Architecture**
- GlobalExceptionHandlingMiddleware for cross-cutting concerns
- Centralized error handling
- Consistent error responses

### 6. **Configuration as Code**
- appsettings.json for configuration management
- Environment-specific settings (Development, Production)
- Easy to deploy to different environments

---

## 🎓 Key Takeaways (UC17)

✅ **From Console to HTTP** – User interaction moved from text input to REST API  
✅ **From ADO.NET to EF Core** – Manual SQL replaced with ORM  
✅ **From Single-tier to N-tier** – Proper separation of concerns  
✅ **From Monolith to Microservices-ready** – API-first architecture  
✅ **From Documentation to Swagger** – Auto-generated interactive API docs  
✅ **From Manual Testing to API Testing** – Testable HTTP contracts  
✅ **From Local Execution to Production-ready** – Scalable backend system  

---

## 📋 What Remains Unchanged

All UC1-UC16 business logic is fully preserved:
- ✔ Conversion engines
- ✔ Validation logic
- ✔ Arithmetic operations
- ✔ Service layer implementation
- ✔ Database persistence (now via EF Core)

The API is simply a **new presentation layer** that wraps the existing business logic.

---

## 🚀 Next Steps After UC17

1. **Unit Tests** – Add API controller tests
2. **Integration Tests** – Test API endpoints with real database
3. **Frontend** – Build React/Angular UI consuming these APIs
4. **Authentication** – Add JWT/OAuth for security
5. **Database Optimization** – Add more indexes for performance
6. **Containerization** – Docker for deployment
7. **Microservices** – Split into separate services by domain

# QuantityMeasurementApp – UC16: Database Integration & Persistence

## 📋 Overview

**UC16** extends the N-Tier architecture with **SQL Server database integration**, replacing the in-memory cache repository with persistent, relational database storage. This enterprise-grade persistence layer ensures data durability, enables complex queries, provides audit trails, and supports multi-user scenarios with concurrent access control and transaction management.

### Database Integration Architecture

1. **Persistent Storage** – SQL Server QuantityMeasurements table with normalized schema
2. **Repository Abstraction** – IQuantityMeasurementRepository enables database or cache swapping
3. **Database Repository** – QuantityMeasurementDatabaseRepository implements ADO.NET data access
4. **Transaction Management** – Ensures data consistency across multi-step operations
5. **Query Optimization** – Indexed columns for fast searches by operation type, category, date

### Key Benefits

✅ **Data Durability** – All operation history persisted permanently in SQL Server  
✅ **Complex Queries** – Filter by operation type, category, date ranges, success/failure status  
✅ **Audit Trail** – Complete record of every measurement operation with timestamps  
✅ **Multi-User Support** – Database handles concurrent access and transaction isolation  
✅ **Scalability** – Supports unlimited operation history without memory constraints  
✅ **Flexible Persistence** – Swappable repository implementations (SQL, Cache, File, NoSQL)  

---

## 📁 Project Structure

```
QuantityMeasurementApp/
│
├── QuantityMeasurementConsole/         # Application Layer (Entry Point)
│   ├── Program.cs                      # Main entry point with DI setup
│   ├── Menu.cs                         # Interactive console UI with colored output
│   ├── IMenu.cs                        # Menu interface contract
│   ├── QuantityMeasurementController.cs# Console layer controller wrapper
│   └── obj, bin/                       # Build output directories
│
├── QuantityMeasurementBusinessLayer/   # Controller & Service Layer
│   ├── Controllers/
│   │   └── QuantityMeasurementController.cs  # Delegates to service layer
│   ├── Services/
│   │   ├── IQuantityMeasurementService.cs    # Service interface contract
│   │   └── QuantityMeasurementServiceImpl.cs  # Core business logic implementation
│   ├── Engines/
│   │   ├── ValidationEngine.cs         # Validates measurement type compatibility
│   │   ├── ConversionEngine.cs         # Performs base unit conversions
│   │   └── ArithmeticEngine.cs         # Executes arithmetic operations
│   ├── Exceptions/
│   │   └── QuantityMeasurementException.cs   # Custom domain exception
│   └── obj, bin/                       # Build output directories
│
├── QuantityMeasurementModelLayer/      # Entity/Model Layer
│   ├── DTO/
│   │   └── QuantityDTO.cs              # Data transfer object between layers
│   ├── Entities/
│   │   └── QuantityMeasurementEntity.cs # Persistence entity with operation record
│   ├── Models/
│   │   └── QuantityModel.cs            # Generic internal domain model
│   ├── Enums/
│   │   ├── LengthUnit.cs               # Length units (Feet, Inch, Yard, Centimeter)
│   │   ├── WeightUnit.cs               # Weight units (Kilogram, Gram, Pound)
│   │   ├── VolumeUnit.cs               # Volume units (Litre, Millilitre, Gallon)
│   │   ├── TemperatureUnit.cs          # Temperature units (Celsius, Fahrenheit, Kelvin)
│   │   └── OperationType.cs            # Operation types (Compare, Convert, Add, Subtract, Divide)
│   └── obj, bin/                       # Build output directories
│
├── QuantityMeasurementRepositoryLayer/ # Data Persistence Layer
│   ├── Interfaces/
│   │   └── IQuantityMeasurementRepository.cs # Abstraction for both repository types
│   ├── Implementations/
│   │   ├── QuantityMeasurementDatabaseRepository.cs # SQL Server persistent storage (UC16)
│   │   └── QuantityMeasurementCacheRepository.cs    # Legacy in-memory cache (UC15)
│   └── obj, bin/                       # Build output directories
│
├── QuantityMeasurementDB/              # Database Scripts
│   ├── QuantityMeasurementDB.sql       # SQL schema, tables, indexes, stored procedures
│   └── (Execution creates SqlServer database)
│
├── QuantityMeasurementApp.Tests/       # Unit & Integration Tests
│   ├── Engines/
│   ├── Services/
│   ├── Repository/                     # Tests both Cache and Database repositories
│   ├── Integration/                    # Database integration tests
│   └── obj, bin/                       # Build output directories
│
├── QuantityMeasurementApp.slnx         # Visual Studio solution file
└── README.md                            # This documentation
```

---

## 📏 Supported Operations

| Measurement Type | Units | Operations |
|---|---|---|
| **Length** | Feet, Inch, Yard, Centimeter | Compare, Convert, Add, Subtract, Divide |
| **Weight** | Kilogram, Gram, Pound | Compare, Convert, Add, Subtract, Divide |
| **Volume** | Litre, Millilitre, Gallon | Compare, Convert, Add, Subtract, Divide |
| **Temperature** | Celsius, Fahrenheit, Kelvin | Compare, Convert |

---

## 🏗️ Database Architecture

### QuantityMeasurements Table Schema
```sql
CREATE TABLE QuantityMeasurements (
    MeasurementId NVARCHAR(50) PRIMARY KEY,      -- Unique operation identifier
    CreatedAt DATETIME2 NOT NULL,                -- Operation timestamp
    OperationType INT NOT NULL,                  -- Compare, Convert, Add, Subtract, Divide
    
    -- First Operand (for binary operations)
    FirstOperandValue FLOAT NULL,
    FirstOperandUnit NVARCHAR(20) NULL,
    FirstOperandCategory NVARCHAR(20) NULL,     -- Length, Weight, Volume, Temperature
    
    -- Second Operand (for binary operations)
    SecondOperandValue FLOAT NULL,
    SecondOperandUnit NVARCHAR(20) NULL,
    SecondOperandCategory NVARCHAR(20) NULL,
    
    -- Source Operand (for conversion operations)
    SourceOperandValue FLOAT NULL,
    SourceOperandUnit NVARCHAR(20) NULL,
    SourceOperandCategory NVARCHAR(20) NULL,
    
    -- Result Fields
    TargetUnit NVARCHAR(20) NULL,
    ResultValue FLOAT NULL,
    ResultUnit NVARCHAR(20) NULL,
    FormattedResult NVARCHAR(200) NULL,         -- Human-readable result
    IsSuccessful BIT NOT NULL,                  -- Operation success flag
    ErrorDetails NVARCHAR(MAX) NULL             -- Error messages if failed
);
```

### Performance Optimization
```sql
-- Indexes for fast queries by operation type and category
CREATE INDEX IX_OperationType ON QuantityMeasurements(OperationType);
CREATE INDEX IX_CreatedAt ON QuantityMeasurements(CreatedAt);
CREATE INDEX IX_FirstCategory ON QuantityMeasurements(FirstOperandCategory);
CREATE INDEX IX_SourceCategory ON QuantityMeasurements(SourceOperandCategory);
```

### Persistence Flow
```
Service Layer (Business Logic)
        ↓
QuantityMeasurementEntity (Object representation)
        ↓
IQuantityMeasurementRepository (Abstraction)
        ↓
QuantityMeasurementDatabaseRepository (ADO.NET implementation)
        ↓
SQL Server QuantityMeasurements Table (Persistent Storage)
```

---

## 🔄 Database Repository Methods

### IQuantityMeasurementRepository Interface
```csharp
public interface IQuantityMeasurementRepository {
    void Save(QuantityMeasurementEntity entity);           // Persist single operation
    List<QuantityMeasurementEntity> GetAll();             // Retrieve all operations
    List<QuantityMeasurementEntity> GetMeasurementsByOperation(OperationType type);
    int GetTotalCount();                                   // Get total operation count
    void DeleteAll();                                      // Clear all operations
    void CloseResources();                                 // Close DB connections
}
```

### QuantityMeasurementDatabaseRepository (UC16)
- **ADO.NET SqlConnection** for direct SQL Server connectivity
- **Parameterized queries** to prevent SQL injection
- **Connection pooling** for performance optimization
- **Error handling & logging** for database operations
- **Transaction support** ensuring atomic operations
- **Stored procedures** for complex business queries
- **Swappable with IQuantityMeasurementRepository** via Dependency Injection

### Configuration
- **Database Name:** QuantityMeasurementDB
- **Connection String:** Stored in appsettings.json
- **Initialization:** SQL script auto-creates schema on first run
- **Connection Timeout:** 30 seconds (configurable)

---

## 🎯 Core Design Patterns

### **Repository Pattern (UC16 Focus)**
- **Abstraction:** IQuantityMeasurementRepository defines contract
- **Implementation:** QuantityMeasurementDatabaseRepository encapsulates SQL Server logic
- **Benefit:** Service layer remains unaware of persistence mechanism
- **Flexibility:** Swap SqlServer ↔ Cache ↔ File-based repository via DI without code changes
- **Testability:** Mock repositories enable unit testing without database

### **Dependency Injection**
- Program.cs configures which repository implementation to use
- Service receives IQuantityMeasurementRepository abstraction
- Loose coupling between business logic and data access layers

### **Data Access Object (DAO)**
- QuantityMeasurementDatabaseRepository serves as DAO pattern
- Encapsulates CRUD operations and SQL query construction
- Returns domain entities, not raw DataSets

### **Singleton Pattern** (Legacy - UC15)
- QuantityMeasurementCacheRepository implements Singleton
- In-memory cache alternative for non-persistent scenarios

### **DTO (Data Transfer Object) Pattern**
- QuantityDTO transfers data between Console and Service layers
- QuantityMeasurementEntity transfers data between Service and Repository layers
- Prevents tight coupling to presentation objects

---

## 🧩 Repository Layer Details (UC16)

### QuantityMeasurementDatabaseRepository.cs
**Characteristics:**
- Direct SQL Server connectivity via SqlConnection (ADO.NET)
- Parameterized INSERT queries prevent SQL injection attacks
- Connection pooling from SQL Server for performance
- Implements IQuantityMeasurementRepository interface
- Handles database errors, timeouts, and resource cleanup

**Key Methods:**
- `Save()` – Inserts QuantityMeasurementEntity into QuantityMeasurements table
- `GetAll()` – SELECT * retrieves all operation history
- `GetMeasurementsByOperation()` – Filters by OperationType using indexed column
- `GetTotalCount()` – Returns COUNT(*) for statistics
- `DeleteAll()` – TRUNCATE TABLE for testing cleanup
- `CloseResources()` – Properly closes SqlConnection and frees resources

**Query Examples:**
```sql
-- Insert new measurement result
INSERT INTO QuantityMeasurements (MeasurementId, CreatedAt, OperationType, 
    FirstOperandValue, FirstOperandUnit, ResultValue, IsSuccessful)
VALUES (@id, @createdAt, @opType, @val1, @unit1, @result, @success);

-- Get all additions
SELECT * FROM QuantityMeasurements 
WHERE OperationType = 1 ORDER BY CreatedAt DESC;

-- Get count by category
SELECT COUNT(*) FROM QuantityMeasurements 
WHERE FirstOperandCategory = 'Length';
```

### Connection Management
```csharp
string connectionString = "Server=localhost;Database=QuantityMeasurementDB;..";
using (SqlConnection conn = new SqlConnection(connectionString)) {
    conn.Open();
    // Execute queries with try-catch-finally for resource cleanup
    SqlCommand cmd = new SqlCommand("INSERT INTO QuantityMeasurements...");
    cmd.ExecuteNonQuery();
}
```

---

## ✅ Design Principles Applied

| Principle | Application |
|---|---|
| **Single Responsibility** | Repository handles only data persistence; Service handles only business logic |
| **Dependency Inversion** | Service depends on IQuantityMeasurementRepository, not concrete DatabaseRepository |
| **Loose Coupling** | Database details hidden from service; swappable repository implementations |
| **Separation of Concerns** | UI ↔ Controller ↔ Service ↔ Repository ↔ Database; each layer independent |
| **DRY (Don't Repeat Yourself)** | Shared QuantityMeasurementEntity used across layers |
| **Open/Closed Principle** | Open for extension (new repository implementations), closed for modification |
| **Resource Management** | Proper connection pooling and `using` statements prevent resource leaks |

---

## 🧪 Testing Strategy (UC16)

### Unit Tests
- **ValidationEngineTests** – Type validation logic isolated from database
- **ConversionEngineTests** – Unit conversion math independent of persistence
- **ArithmeticEngineTests** – Add/Subtract/Divide operations in isolation

### Integration Tests
- **QuantityMeasurementDatabaseRepositoryTests** – Database CRUD operations, connection handling
- **QuantityMeasurementServiceTests** – End-to-end workflows with mocked repository

### Test Database
- **Separate test database** with identical schema for integration testing
- **Cleanup between tests** ensures test isolation via DeleteAll()
- **Connection strings at runtime** selected based on environment (dev/test/prod)

---

## 📊 UC15 vs UC16 Comparison

| Feature | UC15 (Cache) | UC16 (Database) |
|---|---|---|
| **Storage** | In-memory (ephemeral) | SQL Server (persistent) |
| **Data Retention** | Lost on app shutdown | Permanently stored |
| **Concurrency** | Single-thread safe only | Multi-user with isolation levels |
| **Query Capability** | Linear search O(n) | Indexed SQL queries O(log n) |
| **Reporting** | Limited to runtime data | Complex queries, statistics, audit |
| **Scalability** | Limited by RAM | Unlimited (disk-based) |
| **Use Case** | Testing, demo | Production, audit trails, analytics |

---

## 🚀 Getting Started

1. **Create Database:** Execute QuantityMeasurementDB.sql in SQL Server
2. **Update Connection String:** Set server/database in appsettings.json
3. **Configure DI:** In Program.cs, register QuantityMeasurementDatabaseRepository
4. **Run Application:** All operations now persist to SQL Server
5. **View Results:** Query QuantityMeasurements table for operation history

---

## 🎯 UC16 Key Outcomes

✅ **Persistent Data Storage** – Operation history survives application restarts  
✅ **Enterprise-Grade Queries** – Filter, sort, and analyze operation history with SQL  
✅ **Audit Compliance** – Complete timestamps and error tracking for every operation  
✅ **Production Ready** – Database backing ensures data integrity and ACID properties  
✅ **Repository Abstraction** – Easy swap between Cache and Database implementations  
✅ **Connection Pooling** – Optimized database performance through sql Server pooling  
✅ **Error Resilience** – Graceful handling of connection timeouts and SQL errors

---

## 📈 Summary

**UC16 upgrades the application persistence layer** from in-memory cache (UC15) to **SQL Server database integration**. By implementing the Repository Pattern with `QuantityMeasurementDatabaseRepository`, the system now:

- ✅ Persists all measurement operations permanently
- ✅ Supports complex queries and analytics on operation history
- ✅ Maintains audit trails with timestamps and error tracking
- ✅ Scales to unlimited operation history
- ✅ Enables multi-user concurrent access
- ✅ Provides swappable repository implementations via Dependency Injection
- ✅ Follows enterprise database best practices and design patterns

This UC extends the foundational N-Tier architecture (UC15) with **production-ready database persistence**, creating an enterprise-grade application suitable for real-world deployment with regulatory compliance and reporting requirements.
