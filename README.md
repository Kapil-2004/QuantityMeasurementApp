# QuantityMeasurementApp – UC5 Unit-to-Unit Conversion

## Overview

UC5 extends UC4 by introducing explicit unit-to-unit conversion functionality.

In addition to equality comparison, the system now allows converting a numeric value from one `LengthUnit` to another using centralized conversion factors.

All conversions are normalized through a common base unit (Feet), ensuring mathematical consistency and scalability.

This enhancement does not modify any existing UC1–UC4 logic and maintains full backward compatibility.

---

## Supported Units

| Unit        | Conversion to Feet |
|------------|-------------------|
| Feet       | 1.0               |
| Inch       | 1 / 12            |
| Yard       | 3.0               |
| Centimeter | 0.0328084167      |

---

## Public API Added

---

## Key Features

- CDirect unit-to-unit conversion
- Base unit normalization (Feet)
- Same-unit conversion support
- Zero and negative value handling
- Round-trip conversion accuracy
- Floating-point precision tolerance
- Exception handling for invalid inputs
- Full backward compatibility with UC1–UC4


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

- Feet ↔ Inch conversions
- Yard ↔ Feet conversions
- Centimeter ↔ Inch conversions
- Cross-unit conversions
- Same-unit conversion
- Zero and negative values
- Round-trip validation
- NaN and Infinity exception handling

All UC1–UC4 tests continue to pass without modification.