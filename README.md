# Quantity Measurement App - UC9: Weight Module Extension

## 📋 Overview

UC9 extends the system by introducing **Weight measurement support** alongside Length, maintaining architectural consistency with UC8 principles.

### Design Principles

- ✅ **Single Responsibility Principle (SRP)**
- ✅ **Base-unit normalization**
- ✅ **Immutable models**
- ✅ **Backward compatibility (UC1–UC8)**

### 🎯 Key Achievement

Weight measurement is implemented using a standalone `WeightUnit` conversion model, mirroring the clean architecture established for `LengthUnit`.

---

## ⚖️ Supported Weight Units

| Unit | Conversion to Base (Kilogram) |
|------|------|
| **Kilogram** | 1.0 |
| **Gram** | 0.001 |
| **Pound** | 0.45359237 |

**Base Unit:** Kilogram

---

## 🔧 Public API

### `WeightUnit` (UC9 New)

Standalone unit conversion for weight measurements.

```csharp
public double ConvertToBase(double value);
public double ConvertFromBase(double baseValue);
public double ConvertTo(double value, WeightUnit targetUnit);
```

### `QuantityWeight`

Immutable weight quantity model.

```csharp
// Constructor
public QuantityWeight(double value, WeightUnit unit);

// Properties
public double Value { get; }
public WeightUnit Unit { get; }

// Equality & Addition
public bool Equals(QuantityWeight other);
public QuantityWeight Add(QuantityWeight other);
public QuantityWeight Add(QuantityWeight other, WeightUnit targetUnit);
```

### `QuantityWeightService` (UC9)

Service layer for weight measurement operations.

```csharp
public bool AreEqual(double value1, WeightUnit unit1,
                     double value2, WeightUnit unit2);

public double Convert(double value, WeightUnit from,
                      WeightUnit to);

public QuantityWeight Add(double value1, WeightUnit unit1,
                          double value2, WeightUnit unit2);

public QuantityWeight Add(double value1, WeightUnit unit1,
                          double value2, WeightUnit unit2,
                          WeightUnit targetUnit);
```

---

## ✨ Key Features

- 🎯 Standalone `WeightUnit` handles conversion
- 📊 Base unit normalization (Kilogram)
- 🔄 Cross-unit equality comparison
- ➕ Smart addition (default first operand unit)
- 🎲 Explicit target unit addition
- 🔬 Floating-point tolerance support
- ⚠️ Exception handling for invalid inputs
- 📦 Clean separation between Length and Weight modules
- 🔗 Full backward compatibility with UC1–UC8

---

## 📝 Addition Rules (Weight)

1. Both operands must be Weight measurements
2. Values must be finite numbers
3. Convert both values to base unit (Kilogram)
4. Perform addition in base unit
5. Convert sum to target unit
6. Return new immutable `QuantityWeight` object

---

## 🛠️ Architecture (UC9 Extension)

### Before (UC1–UC8)

- Only Length module supported
- Standalone `LengthUnit` responsible for conversion

### After (UC9)

- `WeightUnit` introduced (parallel architecture)
- `QuantityWeight` mirrors `QuantityLength`
- `QuantityWeightService` mirrors `QuantityLengthService`
- Clean modular expansion
- Scalable for future categories (Temperature, Volume, etc.)

---

## 🧪 Test Coverage

- ✅ Weight equality (same & cross-unit)
- ✅ Weight conversion validation
- ✅ Smart addition validation
- ✅ Explicit target unit addition
- ✅ Floating-point tolerance verification
- ✅ Regression validation (UC1–UC8 unaffected)