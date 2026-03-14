# QuantityMeasurementApp – UC15: N-Tier Architecture

## 📋 Overview

**UC15** refactors the monolithic application into a professional **N-Tier architecture** with four distinct layers, providing complete separation of concerns and improved maintainability. This enterprise-grade architecture enables independent testing, scalability, and future integration with REST APIs, Web interfaces, and mobile applications.

### The 4-Layer Architecture

1. **Application Layer** – Console entry point, initializes DI components and orchestrates application flow
2. **Controller Layer** – Processes user requests and delegates all business logic to service
3. **Service Layer** – Implements core business logic, validations, conversions, and arithmetic operations
4. **Entity/Model Layer** – DTOs, Entities, Enums, Models, and all data structures for data transfer

### Key Benefits

✅ **Separation of Concerns** – Each layer has single, well-defined responsibility  
✅ **Independent Testing** – Each layer can be unit tested in isolation  
✅ **Enhanced Maintainability** – Changes in one layer don't affect others  
✅ **SOLID Principles** – Follows industry-standard design patterns  
✅ **Foundation for Scalability** – Easy to add APIs, Web UIs, or mobile clients  
✅ **Reusable Business Logic** – Service layer can be leveraged by multiple UIs  

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
│   │   └── IQuantityMeasurementRepository.cs # Repository abstraction
│   ├── Implementations/
│   │   └── QuantityMeasurementCacheRepository.cs # Singleton in-memory cache
│   └── obj, bin/                       # Build output directories
│
├── QuantityMeasurementApp.Tests/       # Unit & Integration Tests
│   ├── Engines/
│   │   ├── ValidationEngineTests.cs
│   │   ├── ConversionEngineTests.cs
│   │   └── ArithmeticEngineTests.cs
│   ├── Services/
│   │   └── QuantityMeasurementServiceTests.cs
│   ├── Repository/
│   │   └── QuantityMeasurementRepositoryTests.cs
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

## 🏗️ Architecture Design

### Unidirectional Dependency Flow
```
Console UI (Menu.cs)
        ↓
Application Layer (Program.cs) - DI Setup & Initialization
        ↓
Controller Layer (QuantityMeasurementController.cs)
        ↓
Service Layer (QuantityMeasurementServiceImpl.cs)
        ↓
        ├─ ValidationEngine.cs
        ├─ ConversionEngine.cs
        ├─ ArithmeticEngine.cs
        ├─ IQuantityMeasurementRepository (Dependency Injection)
        │
Model/Entity Layer (DTOs, Enums, Entities)
```

### Layer Responsibilities

| Layer | Responsibility | Key Components |
|---|---|---|
| **Console/Application** | Entry point, DI setup, orchestration | Program.cs, Menu.cs, IMenu.cs |
| **Controller** | Request handling, delegation | QuantityMeasurementController.cs |
| **Service** | Business logic, validation, conversions | QuantityMeasurementServiceImpl.cs, ValidationEngine.cs, ConversionEngine.cs, ArithmeticEngine.cs |
| **Repository** | Data persistence abstraction | IQuantityMeasurementRepository.cs, QuantityMeasurementCacheRepository.cs |
| **Entity/Model** | Data structures and domains | QuantityDTO.cs, QuantityModel.cs, QuantityMeasurementEntity.cs, Enums |

---

## 🔄 Detailed Operation Workflows

### 1. **Compare Operation**
Compares two quantities of the same measurement type after normalizing to base units.

**Flow:**
1. Menu captures user input (quantity1, quantity2, measurement type)
2. Controller receives QuantityDTOs
3. Service validates measurement types match → throws exception if not
4. ConversionEngine converts both quantities to base units
5. Service compares values → returns boolean result
6. Repository persists comparison entity
7. Result displayed in console

**Example:** 1 Foot vs 12 Inches → Converts to 12 vs 12 Inches → True

### 2. **Conversion Operation**
Converts a quantity from one unit to another within same measurement type.

**Flow:**
1. Menu captures source quantity and asks for target unit
2. Controller delegates to Service
3. ConversionEngine converts source to base unit
4. ConversionEngine converts from base to target unit
5. Returns new QuantityDTO with converted value
6. Repository persists conversion entity
7. Result displayed

**Example:** 1 Foot → Convert to Centimeter → 30.48 Centimeters

### 3. **Addition Operation**
Adds two quantities of same measurement type, returns result in first quantity's unit.

**Flow:**
1. Menu captures two quantities
2. Service validates same measurement type
3. Both quantities convert to base units
4. ArithmeticEngine adds base values
5. Result converts back to original unit
6. Repository persists addition entity
7. Result displayed

**Example:** 1 Foot + 12 Inches → 12 + 12 Inches = 24 Inches → Convert to 2 Feet

### 4. **Subtraction Operation**
Subtracts second quantity from first, returns result in first quantity's unit.

**Flow:**
1. Menu captures two quantities
2. Service validates same measurement type
3. Both quantities convert to base units
4. ArithmeticEngine subtracts (first - second)
5. Result converts back to original unit
6. Repository persists subtraction entity
7. Result displayed

### 5. **Division Operation**
Divides first quantity by second, returns unitless ratio.

**Flow:**
1. Menu captures two quantities
2. Service validates same measurement type
3. Service checks divisor is non-zero → throws exception if zero
4. Both quantities convert to base units
5. ArithmeticEngine performs division
6. Returns dimensionless double result
7. Repository persists division entity
8. Result displayed

---

## 🎯 Core Design Patterns

### **Dependency Injection**
- Service receives Repository abstraction through constructor
- Controller receives Service abstraction
- Enables loose coupling and testability
- Program.cs orchestrates DI setup

### **Singleton Pattern**
- QuantityMeasurementCacheRepository implements Singleton
- Ensures single in-memory cache instance throughout application
- GetInstance() factory method returns private static instance
- Private constructor prevents instantiation

### **Repository Pattern**
- IQuantityMeasurementRepository abstracts data persistence
- QuantityMeasurementCacheRepository provides in-memory implementation
- Future: Can implement SQL, NoSQL, File-based repositories without changing service layer
- Service layer remains independent of persistence mechanism

### **DTO (Data Transfer Object) Pattern**
- QuantityDTO transfers data between layers
- Prevents tight coupling to domain objects
- Each layer uses DTOs for communication
- Internal models remain encapsulated

### **Strategy Pattern**
- Unit conversion strategies (ToBaseUnit/FromBaseUnit) for each unit type
- ArithmeticEngine provides different strategies (Add, Subtract, Divide)
- ValidationEngine provides validation strategy
- Easy to extend with new strategies

### **Factory Pattern**
- EnumExtensions provide factory methods (GetMeasurementType, ToBaseUnit, FromBaseUnit)
- Centralizes object creation logic
- Cleaner API for consumers

---

## 🧩 Detailed Component Overview

### Console Layer (Application Entry Point)
- **Program.cs** – Initializes DI container, creates Repository → Service → Controller, starts interactive menu
- **Menu.cs** – Interactive console UI with colored output, captures user input, displays results
- **IMenu.cs** – Interface contract for menu implementations

### Controller Layer
- **QuantityMeasurementController.cs** – Thin wrapper delegating all operations to Service; provides clean API for UI layer

### Service Layer (Business Logic Core)
- **QuantityMeasurementServiceImpl.cs** – Orchestrates operations: validates, converts, performs arithmetic, persists results
- **ValidationEngine.cs** – Static utility ensuring quantity type compatibility for operations
- **ConversionEngine.cs** – Static utility converting between units using base unit normalization
- **ArithmeticEngine.cs** – Static utility performing Add, Subtract, Divide with validation

### Repository Layer (Data Persistence)
- **QuantityMeasurementCacheRepository.cs** – Singleton in-memory cache storing operation history
- **IQuantityMeasurementRepository.cs** – Interface for repository implementations (future: SQL, NoSQL, etc.)

### Model/Entity Layer (Data Structures)
- **QuantityDTO.cs** – Data Transfer Object: Value (double), Unit (string), MeasurementType (string)
- **QuantityModel<T>.cs** – Generic domain model for internal quantity representation
- **QuantityMeasurementEntity.cs** – Persistence entity recording Operand1, Operand2, Operation, Result, ErrorStatus
- **Unit Enums** – LengthUnit, WeightUnit, VolumeUnit, TemperatureUnit with conversion methods
- **OperationType.cs** – Enum: Compare, Convert, Add, Subtract, Divide
- **QuantityMeasurementException.cs** – Custom exception for domain-specific errors

---

## ✅ Design Principles Applied

| Principle | Application |
|---|---|
| **Single Responsibility** | Each layer and class has one, well-defined purpose |
| **Dependency Inversion** | All layers depend on abstractions (interfaces), not implementations |
| **Loose Coupling** | Layers communicate through DTOs and interfaces only |
| **Unidirectional Dependencies** | No circular references; clean hierarchy: UI → Controller → Service → Repository → Model |
| **Separation of Concerns** | Each layer handles specific aspect: UI, validation, business logic, data |
| **Extensibility** | New repositories, engines, unit types added without modifying existing code |
| **Testability** | Each component independently testable with mocked dependencies |
| **SOLID Adherence** | Follows all five SOLID principles throughout architecture |

---

## 🧪 Testing Strategy

### Unit Tests
- **ValidationEngineTests** – Validates type checking for operations
- **ConversionEngineTests** – Tests base unit conversions across all measurement types
- **ArithmeticEngineTests** – Tests Add, Subtract, Divide operations and edge cases

### Integration Tests
- **QuantityMeasurementServiceTests** – End-to-end workflows: Compare, Convert, Add, Subtract, Divide
- **QuantityMeasurementRepositoryTests** – Persistence and Singleton pattern verification

Each test follows AAA pattern: Arrange, Act, Assert

---

## 🎯 Key Outcomes

✅ **Clear Layer Separation** – Each layer has distinct, non-overlapping responsibility  
✅ **Independent Testing** – Mock dependencies; test each layer in isolation  
✅ **Business Logic Reusability** – Service layer can power REST APIs, Web UIs, Mobile apps  
✅ **Enterprise Scalability** – Foundation for adding logging, caching, authentication layers  
✅ **Professional Architecture** – Follows industry best practices and architectural patterns  
✅ **Maintainability** – Changes isolated to specific layers, reducing regression risks  
✅ **Easy Onboarding** – Clear structure helps new developers understand codebase quickly  

---

## 💡 Summary

UC15 transforms the application from a monolithic structure into a **professional N-Tier enterprise system** with:
- Four distinct, independently-deployable layers
- Clear separation of concerns and single responsibilities
- Unidirectional dependencies ensuring clean architecture
- Design patterns (DI, Singleton, Repository, DTO, Strategy, Factory)
- Comprehensive unit and integration tests
- Foundation for scalability, testability, and maintainability
- Complete adherence to SOLID principles and industry best practices

This architecture provides the perfect foundation for a production-grade application that can evolve from console to REST API, Web, and Mobile clients while maintaining code quality and professional standards.
