# 📏 UC2 – Inch Equality Implementation

## 📌 Overview
UC2 extends the Quantity Measurement application by introducing an **Inch** class that supports value-based equality comparison.

This use case ensures that two measurements in inches can be compared correctly using an overridden `Equals()` method while maintaining Object-Oriented principles.

---

## 🎯 Objective
- Implement equality comparison for inch measurements
- Follow value-based equality
- Maintain compatibility with UC1 (Feet implementation)
- Ensure null safety and type safety

---

## 🛠 Implementation Details
- Created `Inch` class
- Added constructor with validation
- Overridden:
  - `Equals()` method
  - `GetHashCode()` method
- Added service method for comparison
- Implemented unit test cases

---

## ✅ Functionality

| Comparison | Result |
|------------|--------|
| Inch(1.0) == Inch(1.0) | true |
| Inch(1.0) == Inch(2.0) | false |
| Inch(1.0) == null | false |

---

## 🧪 Test Cases Covered
- Same value equality
- Different value inequality
- Null comparison
- Same reference comparison
- Type safety validation

---

## 📚 Concepts Applied
- Value-Based Equality
- Equality Contract (Reflexive, Symmetric, Transitive, Consistent)
- Null Safety
- Method Overriding
- Object-Oriented Design Principles

---

## 🔒 Postconditions
- Accurate inch-to-inch comparison
- All UC1 functionality remains unchanged
- Code is validated through unit testing