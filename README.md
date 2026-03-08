# QuantityMeasurementApp – UC12 Arithmetic Operations (Subtract & Divide)

## 📋 Overview

UC12 extends the system by introducing **additional arithmetic operations** for measurement quantities.

This use case enables the system to perform:
- **Subtraction** between quantities
- **Division** between quantities

These operations are supported for all existing measurement categories:
- 📏 **Length**
- ⚖️ **Weight**
- 🧪 **Volume**

The implementation builds on the architecture established in earlier use cases and maintains full compatibility with the existing service-based design.

All operations internally normalize values using their respective base units, ensuring **consistent and accurate calculations** across different units.

---

## 🎯 Objectives

- ✓ Introduce subtraction operations for measurement quantities
- ✓ Introduce division operations for measurement quantities
- ✓ Support arithmetic across different units within the same category
- ✓ Maintain architectural consistency with UC1–UC11
- ✓ Ensure calculations remain accurate through base unit normalization
- ✓ Extend the system without modifying existing functionality

---

## 📏 Supported Measurement Categories

UC12 enables arithmetic operations across all supported measurement types.

| Measurement Type | Base Unit | Supported Units |
|------------------|-----------|------------------|
| **Length** | Feet | Feet, Inch, Yard, Centimeter |
| **Weight** | Kilogram | Kilogram, Gram, Pound |
| **Volume** | Liter | Liter, Milliliter, Gallon |
---

## 🏗️ Architectural Design

UC12 integrates seamlessly into the existing layered architecture used throughout the application.

No structural changes were required. Instead, additional arithmetic methods were introduced within the service layer.

### Project Structure

```
QuantityMeasurementApp/
├── Models/
│   ├── IMeasurable.cs
│   ├── Quantity.cs
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
|-----------|----------------|
| **Quantity.cs** | Defines the generic quantity model and arithmetic behavior |
| **Service Classes** | Perform measurement operations using base unit normalization |
| **Program.cs** | Provides console-based user interaction |
| **Tests** | Validate correctness of arithmetic operations |
---

## ⚙️ Core Functionalities

### 1️⃣ Subtraction of Quantities

Allows subtraction between two measurement quantities even when they are expressed in different units.

**Examples:**
- `2 Feet − 12 Inch` → `1 Feet`
- `5 Kilogram − 500 Gram` → `4.5 Kilogram`
- `2 Liter − 500 Milliliter` → `1.5 Liter`

The result is returned in the unit of the **first operand**, ensuring consistent behavior across all operations.

### 2️⃣ Division of Quantities

Divides one quantity by another quantity of the same measurement category.

**Examples:**
- `10 Feet ÷ 2 Feet` → `5`
- `4 Kilogram ÷ 500 Gram` → `8`
- `2 Liter ÷ 500 Milliliter` → `4`

Division produces a **dimensionless numeric result**, representing the ratio between the two quantities.

---

## 🔄 Internal Conversion Strategy

All arithmetic operations follow the same **normalization workflow** used in previous use cases:

```
Input Value → Convert to Base Unit → Perform Calculation → Convert to Result Unit
```

### Example: 2 Liter − 500 Milliliter

**Step 1: Convert to Base Unit (Liter)**
- `2 Liter` → `2 Liter`
- `500 Milliliter` → `0.5 Liter`

**Step 2: Perform Calculation**
- `2 − 0.5 = 1.5 Liter`
---

## 🧪 Test Coverage

UC12 expands the existing test suite to validate subtraction and division operations across all measurement categories.

### Covered Scenarios

- ✅ Subtraction between different units
- ✅ Subtraction returning correct result unit
- ✅ Division between quantities
- ✅ Division producing correct ratio
- ✅ Handling of invalid unit inputs
- ✅ Validation against edge cases

### Example Test Cases

| Test Case | Expected Result |
|-----------|----------------|
| `2 Feet − 12 Inch` | `1 Feet` |
| `5 Kilogram − 500 Gram` | `4.5 Kilogram` |
| `2 Liter − 500 Milliliter` | `1.5 Liter` |
| `10 Feet ÷ 2 Feet` | `5` |
| `4 Kilogram ÷ 500 Gram` | `8` |
---

## 🔒 Design Principles Applied

UC12 continues to follow the same **core software engineering principles** used throughout the project.

| Principle | Description |
|-----------|-------------|
| **Consistency** | Arithmetic operations follow the same logic across all measurement types |
| **Single Responsibility** | Service classes handle only their respective measurement categories |
| **Extensibility** | Additional operations can be introduced without modifying existing logic |
| **Reusability** | Base unit conversion logic is reused across all services |
| **Domain Normalization** | Calculations are performed using base units for accuracy |
---

## 🚀 Outcome

UC12 enhances the system by introducing **full arithmetic capability** across measurement quantities.

The application now supports:
- 📏 **Length Measurements**
- ⚖️ **Weight Measurements**
- 🧪 **Volume Measurements**
- ➕ **Addition**
- ➖ **Subtraction**
- ➗ **Division**
- 🔄 **Unit Conversion**
- ✔️ **Equality Comparison**

### ✅ System Capability After UC12

| Measurement Type | Operations Supported |
|------------------|---------------------|
| **Length** | Compare, Convert, Add, Subtract, Divide |
| **Weight** | Compare, Convert, Add, Subtract, Divide |
| **Volume** | Compare, Convert, Add, Subtract, Divide |

This use case demonstrates the **scalability** of the system architecture, allowing new operations to be integrated seamlessly without affecting previously implemented functionality.