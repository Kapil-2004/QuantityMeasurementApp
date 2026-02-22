# 📏 UC3 – Generic QuantityLength (DRY Implementation)

## 📌 Overview
UC3 introduces a single **QuantityLength** class to eliminate duplication between Feet and Inch implementations from UC1 and UC2.

It applies the **DRY principle** while preserving all existing functionality.

---

## 🎯 Key Features
- Single reusable `QuantityLength` model
- `LengthUnit` enum for type-safe unit handling
- Base unit conversion (Feet)
- Cross-unit equality support (1 ft == 12 in)
- Backward compatibility with UC1 & UC2

---

## 🧪 Test Coverage
- Feet-to-Feet equality
- Inch-to-Inch equality
- Feet-to-Inch equivalence
- Different value comparison
- Null and same reference checks

---

## ✅ Outcome
- Code duplication removed
- Scalable design for adding new units
- Clean Models + Services architecture
- Equality contract fully maintained