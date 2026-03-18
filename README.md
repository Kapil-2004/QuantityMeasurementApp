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
