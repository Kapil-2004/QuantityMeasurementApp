# 📏 QuantityMeasurementApp – UC6 Addition of Two Length Units

## 📋 Overview

**UC6** extends UC5 by introducing arithmetic addition between two length measurements.

The system now supports adding two **QuantityLength** values — even if they are in different units — and returns the result in the unit of the first operand.

> All addition operations are normalized through a common base unit (**Feet**), ensuring mathematical accuracy, consistency, and reuse of the UC5 conversion infrastructure.

✨ **Key Point:** This enhancement does not modify any existing UC1–UC5 logic and maintains full backward compatibility.

---

## 📐 Supported Units

| Unit | Conversion to Feet |
|:---|---:|
| **Feet** | 1.0 |
| **Inch** | 1 / 12 |
| **Yard** | 3.0 |
| **Centimeter** | 0.0328084167 |

---

## 🔧 Public API Added

```csharp
// Add two QuantityLength objects
public QuantityLength Add(QuantityLength other);

// Add two length values with explicit units
public QuantityLength Add(
    double value1, LengthUnit unit1,
    double value2, LengthUnit unit2
);
```

---

## ✅ Key Features

- ✔️ Same-unit addition (Feet + Feet, Inch + Inch)
- ✔️ Cross-unit addition (Feet + Inch, Yard + Feet, etc.)
- ✔️ Result returned in the unit of the first operand
- ✔️ Base unit normalization (Feet)
- ✔️ Immutability preserved (original objects remain unchanged)
- ✔️ Zero and negative value handling
- ✔️ Large and small magnitude value support
- ✔️ Floating-point precision tolerance
- ✔️ Exception handling for invalid numeric inputs
- ✔️ Full backward compatibility with UC1–UC5

---

## 📝 Addition Rules

1. **Category Check:** Both operands must belong to the same measurement category (Length)
2. **Validity Check:** Values must be finite numbers
3. **Normalization:** Both values are converted to the base unit (Feet)
4. **Operation:** Values are added in the base unit
5. **Conversion:** The sum is converted back to the unit of the first operand
6. **Return:** A new QuantityLength object is returned

---

## 💡 Concepts Demonstrated

- 🎯 Arithmetic operations on Value Objects
- 🎯 Base-unit normalization strategy
- 🎯 Immutability principle
- 🎯 Reuse of conversion infrastructure (UC5)
- 🎯 Floating-point precision handling
- 🎯 Mathematical commutativity (a + b = b + a)
- 🎯 Identity property (a + 0 = a)
- 🎯 Defensive programming and validation
- 🎯 Scalable domain-driven design

---

## 🧪 Test Coverage

### Addition Operations
- Same-unit addition (Feet + Feet)
- Same-unit addition (Inch + Inch)
- Cross-unit addition (Feet + Inch)
- Cross-unit addition (Inch + Feet)
- Yard + Feet conversion
- Centimeter + Inch conversion

### Mathematical Properties
- ✓ Commutativity validation
- ✓ Identity element validation (adding zero)
- ✓ Negative value addition
- ✓ Large magnitude values
- ✓ Small magnitude values

### Error Handling
- ✓ NaN and invalid numeric exception handling

---

## 🔄 Compatibility

✅ All UC1–UC5 tests continue to pass without modification.