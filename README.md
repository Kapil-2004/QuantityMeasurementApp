# QuantityMeasurementApp – UC11 Volume Measurement Support

## 📋 Overview

UC11 extends the system by introducing **Volume measurement operations** into the application.

This use case enables the system to perform:
- **Comparison** between volume quantities
- **Conversion** across different volume units
- **Addition** operations with automatic unit handling

The implementation maintains the same architecture used for Length (UC1–UC8) and Weight (UC9).

### Supported Volume Units
- **Liter** (L) - Base Unit
- **Milliliter** (mL) - 1 mL = 0.001 L
- **Gallon** (gal) - 1 gal = 3.78 L

All calculations are internally normalized using **Liter** as the base unit to ensure consistent and accurate conversions.

---

## 🎯 Objectives

- ✓ Introduce Volume measurement support
- ✓ Maintain architectural consistency with previous UCs
- ✓ Enable equality comparison between volume quantities
- ✓ Support unit conversion across volume units
- ✓ Implement addition operations between different units
- ✓ Allow addition with explicit target unit selection

---

## 📏 Supported Volume Units

| Unit | Symbol | Conversion |
|------|--------|------------|
| Liter | L | Base Unit |
| Milliliter | mL | 1 mL = 0.001 L |
| Gallon | gal | 1 gal = 3.78 L |

---

## 🏗️ Architectural Design

UC11 follows the same layered architecture used in earlier use cases, ensuring **consistency** and **maintainability** across the application.

### Project Structure

```
QuantityMeasurementApp/
├── Models/
│   ├── IMeasurable.cs
│   ├── VolumeUnit.cs
│   ├── Quantity.cs
│   ├── LengthUnit.cs
│   └── WeightUnit.cs
├── Services/
│   ├── IQuantityService.cs
│   ├── QuantityService.cs
│   ├── QuantityVolumeService.cs
│   ├── QuantityLengthService.cs
│   └── QuantityWeightService.cs
├── Program.cs
└── QuantityMeasurementApp.csproj
```

### Component Responsibilities

| Component | Responsibility |
|-----------|-----------------|
| **VolumeUnit.cs** | Defines supported volume units |
| **QuantityVolumeService.cs** | Handles comparison, conversion, and addition |
| **Program.cs** | Provides console-based interaction |
| **Tests** | Verifies system correctness |
---

## ⚙️ Core Functionalities

### 1️⃣ Volume Equality Comparison

Determines if two quantities represent the same volume.

**Examples:**
- `1 Liter == 1000 Milliliter` → ✅ True
- `1 Gallon == 3.78 Liter` → ✅ True

### 2️⃣ Volume Conversion

Converts volume from one unit to another, normalizing internally through the base unit.

**Examples:**
- `2 Liter` → `2000 Milliliter`
- `1 Gallon` → `3.78 Liter`

### 3️⃣ Addition of Volumes

Adds two volume quantities and returns the result in the unit of the first operand.

**Examples:**
- `1 Liter + 500 Milliliter` = `1.5 Liter`
- `2 Liter + 1000 Milliliter` = `3 Liter`

### 4️⃣ Addition with Target Unit

Allows addition of two quantities while specifying a desired output unit.

**Examples:**
- `1 Liter + 1 Gallon` → `4.78 Liter`
- `1 Liter + 1 Gallon` → `4780 Milliliter`
---

## 🔄 Internal Conversion Strategy

All operations follow a **two-step normalization process** for accuracy and consistency:

```
Input Value → Convert to Base Unit (Liter) → Convert to Target Unit
```

### Example: 1 Gallon + 500 Milliliter

**Step 1: Normalize to Base Unit (Liter)**
- 1 Gallon → 3.78 Liter
- 500 mL → 0.5 Liter

**Step 2: Perform Calculation**
- 3.78 + 0.5 = **4.28 Liter**

---

## 🧪 Test Coverage

UC11 introduces comprehensive tests for the Volume service layer, following the same testing patterns established in previous use cases.

### Covered Scenarios

- ✅ Equality comparison between units
- ✅ Liter to Milliliter conversion
- ✅ Gallon to Liter conversion
- ✅ Addition across different units
- ✅ Addition with explicit target unit
- ✅ Validation for invalid units

### Example Test Cases

| Test Case | Expected Result |
|-----------|-----------------|
| 1 Liter == 1000 Milliliter | ✅ True |
| 1 Gallon == 3.78 Liter | ✅ True |
| 1 Liter + 500 Milliliter = 1.5 Liter | ✅ True |
---

## 🔒 Design Principles Applied

UC11 continues to follow the same **architectural principles** established in previous use cases, ensuring code quality and maintainability.

| Principle | Description |
|-----------|-------------|
| **Single Responsibility** | Each service handles a single measurement category |
| **Consistency Across Modules** | Volume logic mirrors Length and Weight services |
| **Extensibility** | New measurement categories can be added easily |
| **Encapsulation** | Conversion logic is contained inside service classes |
| **Domain Normalization** | All operations rely on a common base unit |

---

## 🚀 Outcome

UC11 enhances the application by introducing **volume measurement capabilities** while maintaining a clean and scalable architecture.

The system is now capable of handling:
- 📏 **Length Measurements** (Feet, Inch, Yard, Centimeter)
- ⚖️ **Weight Measurements** (Kilogram, Gram, Pound)
- 🧪 **Volume Measurements** (Liter, Milliliter, Gallon)

### ✅ System Capability After UC11

| Measurement Type | Units Supported |
|------------------|-----------------|
| **Length** | Feet, Inch, Yard, Centimeter |
| **Weight** | Kilogram, Gram, Pound |
| **Volume** | Liter, Milliliter, Gallon |

This expansion demonstrates how the architecture supports **incremental feature growth** without modifying existing logic, reinforcing strong software design practices.