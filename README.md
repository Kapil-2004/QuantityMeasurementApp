# QuantityMeasurementApp – UC13: Centralized Arithmetic Logic

## 📋 Overview

**UC13** improves the internal implementation of arithmetic operations by refactoring duplicated logic into centralized helper methods.

### The Problem (UC12)

In UC12, arithmetic operations were implemented separately with repeated logic:

| Task | Status |
|------|--------|
| ➕ **Addition** | Repeated logic |
| ➖ **Subtraction** | Repeated logic |
| ➗ **Division** | Repeated logic |

Each method independently handled:
- Input validation
- Unit compatibility checks
- Base unit conversion  
- Arithmetic execution

### The Solution (UC13)

UC13 eliminates duplication by introducing **centralized arithmetic and validation helpers** inside the `Quantity` class.

**Key Benefits:**
- Enforces the **DRY** (Don't Repeat Yourself) principle
- Easier to maintain, extend, and debug
- ✅ All public APIs remain identical
- ✅ No external behavior changes
- ✅ Full backward compatibility with UC12

---

## ## 🎯 Objectives

✅ Eliminate duplicated logic across arithmetic methods  
✅ Introduce a centralized helper for arithmetic execution  
✅ Introduce centralized validation for arithmetic operands  
✅ Maintain backward compatibility with UC12 behavior  
✅ Improve maintainability and readability of the `Quantity` class  
✅ Prepare the system for future arithmetic operations  

---

## 📏 Supported Measurement Categories

UC13 does not introduce new measurement types but improves the internal arithmetic logic used across all existing categories.

| Measurement Type | Base Unit | Supported Units |
|---|---|---|
| **Length** | Feet | Feet, Inch, Yard, Centimeter |
| **Weight** | Kilogram | Kilogram, Gram, Pound |
| **Volume** | Liter | Liter, Milliliter, Gallon |

All arithmetic operations continue to operate using **base unit normalization**.

---

## 🏗️ Architectural Design

UC13 performs **internal refactoring only**. The existing project architecture remains unchanged.

### Project Structure

```
QuantityMeasurementApp/
├── Models/
│   ├── IMeasurable.cs
│   ├── Quantity.cs              ← Refactored in UC13
│   ├── LengthUnit.cs
│   ├── WeightUnit.cs
│   └── VolumeUnit.cs
├── Services/
│   ├── IQuantityService.cs
│   ├── QuantityService.cs
│   ├── QuantityLengthService.cs
│   ├── QuantityWeightService.cs
│   └── QuantityVolumeService.cs
├── Program.cs
└── QuantityMeasurementApp.csproj
```

### Component Responsibilities

| Component | Responsibility |
|---|---|
| `Quantity.cs` | Handles arithmetic logic and validation |
| Service Classes | Provide measurement operations to the application |
| `Program.cs` | Console UI for user interaction |
| Tests | Validate arithmetic correctness and behavior |

---

## ⚙️ Core Refactoring Improvements

UC13 introduces two major internal improvements to enhance code quality.

### 1️⃣ Centralized Arithmetic Helper

A new helper method performs arithmetic operations in base units, ensuring consistent behavior across all operations.

**Method:** `PerformBaseArithmetic(Quantity<U> other, ArithmeticOperation operation)`

**Responsibilities:**
- Convert both quantities to base units
- Execute the arithmetic operation
- Return the computed base-unit result

**Used by:**
- `Add()`
- `Subtract()`
- `Divide()`

### 2️⃣ Centralized Validation Helper

UC13 introduces a dedicated validation method to ensure consistent input validation.

**Method:** `ValidateArithmeticOperands(Quantity<U> other)`

**Validations Performed:**
- ✅ Null operand validation
- ✅ Measurement category compatibility  
- ✅ Numeric finiteness checks
- ✅ Prevention of invalid arithmetic operations

This ensures that all arithmetic methods fail **consistently** for invalid inputs.

---

## 🔄 Arithmetic Operation Dispatch

UC13 introduces an internal enum used to determine which arithmetic operation should be performed.

### ArithmeticOperation Enum

```csharp
private enum ArithmeticOperation
{
    ADD,
    SUBTRACT,
    DIVIDE
}
```

This allows the helper method to execute operations in a **type-safe** and **scalable** way without relying on multiple conditional statements.

---

## 🔄 Internal Execution Flow

All arithmetic operations now follow a unified execution flow:

```
Public Arithmetic Method
         ↓
 Validate Operands
         ↓
Convert Quantities to Base Units
         ↓
 Perform Arithmetic Operation
         ↓
Convert Result to Target Unit
         ↓
   Return Result
```

### Example: Subtraction Operation

```
q1.Subtract(q2)
         ↓
ValidateArithmeticOperands(q2)
         ↓
PerformBaseArithmetic(q2, SUBTRACT)
         ↓
Convert result back to q1 unit
         ↓
Return new Quantity
```

---

## 🧪 Test Coverage

UC13 ensures that all existing UC12 tests continue to pass without modification.

This confirms that the refactoring **does not alter external behavior**.

### Validation Scenarios Covered

✅ Null operand validation  
✅ Cross-category arithmetic prevention  
✅ Finiteness validation  
✅ Division by zero detection  

### Arithmetic Scenarios Covered

| Operation | Example | Expected Result |
|---|---|---|
| **Addition** | 1 Feet + 12 Inch | 2 Feet |
| **Subtraction** | 2 Liter − 500 Milliliter | 1.5 Liter |
| **Division** | 10 Feet ÷ 2 Feet | 5 |

---

## 🔒 Design Principles Applied

UC13 strengthens the application by reinforcing key software design principles.

| Principle | Description |
|---|---|
| **DRY Principle** | Eliminates duplicated validation and conversion logic |
| **Single Responsibility** | Helper methods isolate arithmetic responsibilities |
| **Encapsulation** | Arithmetic implementation hidden inside `Quantity` |
| **Maintainability** | Changes to validation or arithmetic occur in one place |
| **Scalability** | Future arithmetic operations can reuse the same helper |

---

## 🚀 Outcome

UC13 significantly improves the **internal design** of the system without changing its **external behavior**.

### Improvements Achieved

✓ Reduced code duplication  
✓ Improved readability of arithmetic methods  
✓ Centralized validation and arithmetic execution  
✓ Simplified debugging and maintenance  
✓ Prepared architecture for future operations  

---

## 📊 System Capability After UC13

The application now supports the following capabilities across all measurement types.

| Measurement Type | Supported Operations |
|---|---|
| **Length** | Compare, Convert, Add, Subtract, Divide |
| **Weight** | Compare, Convert, Add, Subtract, Divide |
| **Volume** | Compare, Convert, Add, Subtract, Divide |

Although UC13 does not add new features, it **strengthens the architecture** by ensuring arithmetic logic is centralized, maintainable, and scalable.

---

## 💡 Summary

UC13 demonstrates an important software engineering practice:

> **Improving internal code quality through refactoring while preserving existing functionality.**