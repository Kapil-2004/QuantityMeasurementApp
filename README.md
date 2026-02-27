# 📏 Quantity Measurement App – UC7 Addition with Target Unit Specification

## Overview

UC7 extends UC6 by introducing addition with explicit target unit selection. The system supports adding two `QuantityLength` values— even if they are in different units— and allows the caller to specify the desired unit for the result.

All addition operations are normalized through a common base unit (Feet), ensuring mathematical accuracy, consistency, and reuse of the UC5 conversion infrastructure.

**Key Point:** This enhancement builds on UC6 without modifying any existing UC1–UC6 logic and maintains full backward compatibility.

---

## 📐 Supported Units

| Unit        | Conversion to Feet     |
|-------------|------------------------|
| Feet        | 1.0                    |
| Inch        | 1 / 12                 |
| Yard        | 3.0                    |
| Centimeter  | 0.0328084167           |

---

## 🔧 Public API

```csharp
// Add two QuantityLength objects with explicit target unit
public QuantityLength Add(QuantityLength other, LengthUnit targetUnit);

// Add two length values with explicit units and target unit
public QuantityLength Add(
    double value1, LengthUnit unit1,
    double value2, LengthUnit unit2,
    LengthUnit targetUnit
);
```

---

## ✅ Key Features

- Same-unit addition with custom result unit  
- Cross-unit addition with explicit target unit  
- Result returned in user-specified unit  
- Base unit normalization (Feet)  
- Immutability preserved (original objects remain unchanged)  
- Zero and negative value handling  
- Large and small magnitude value support  
- Floating-point precision tolerance  
- Exception handling for invalid numeric inputs  
- Full backward compatibility with UC1–UC6  

---

## 📝 Addition Rules

1. **Category Check:** Both operands must belong to the same measurement category (Length)  
2. **Validity Check:** Values must be finite numbers  
3. **Target Validation:** Target unit must be a valid `LengthUnit`  
4. **Normalization:** Both values are converted to the base unit (Feet)  
5. **Operation:** Values are added in the base unit  
6. **Conversion:** The sum is converted to the specified target unit  
7. **Return:** A new `QuantityLength` object is returned  

---

## 💡 Concepts Demonstrated

- Arithmetic operations with flexible output representation  
- Base-unit normalization strategy  
- Explicit result-unit control  
- Immutability principle  
- Reuse of conversion infrastructure (UC5 & UC6)  
- Floating-point precision handling  
- Mathematical commutativity (`a + b = b + a`)  
- Identity property (`a + 0 = a`)  
- Defensive programming and validation  
- Scalable domain-driven design  

---

## 🧪 Test Coverage

### Addition Operations

- Same-unit addition with custom target  
- Cross-unit addition with target conversion  
- Feet + Inch → Yard  
- Inch + Centimeter → Feet  
- Yard + Feet → Inch  

### Mathematical Properties

- ✓ Commutativity validation  
- ✓ Identity element validation (adding zero)  
- ✓ Negative value addition  
- ✓ Large magnitude values  
- ✓ Small magnitude values  

### Error Handling

- ✓ NaN and invalid numeric exception handling  
- ✓ Invalid target unit handling
