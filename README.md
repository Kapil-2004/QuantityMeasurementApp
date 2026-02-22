# QuantityMeasurementApp – UC4 Extended Unit Support

## Overview

UC4 extends the scalable design from UC3 by introducing additional length units:

- Yard
- Centimeter

The system continues to use a single generic `QuantityLength` class with a base unit (Feet) for all conversions and equality comparisons.

This demonstrates extensibility without modifying core business logic.

---

## Supported Units

| Unit        | Conversion to Feet |
|------------|-------------------|
| Feet       | 1.0               |
| Inch       | 1 / 12            |
| Yard       | 3.0               |
| Centimeter | 0.0328084167      |

---

## Key Features

- Cross-unit equality (e.g., 1 Yard == 3 Feet)
- Centimeter conversions (1 cm == 0.393701 inch)
- Tolerance-based floating-point comparison
- Full backward compatibility with UC1–UC3
- No code duplication (DRY compliant)
- Centralized conversion logic in enum extension


---

## Concepts Demonstrated

- Open/Closed Principle
- Enum extensibility
- Conversion factor management
- Mathematical precision handling
- Scalable generic design
- Unit-safe architecture

---

## Test Coverage

- Yard-to-yard equality
- Yard-to-feet conversion
- Yard-to-inch conversion
- Centimeter cross-unit comparison
- Transitive property validation
- Null and reference checks
- Complex multi-unit scenarios

All previous UC1–UC3 tests continue to pass.
