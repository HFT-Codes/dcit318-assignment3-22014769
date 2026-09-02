# DCIT318 Assignment 3 - C# Programming Solutions

This repository contains solutions for 5 comprehensive C# programming assignments covering interfaces, records, sealed classes, generics, collections, exception handling, and file operations.

## Project Overview

| Question | Topic | Folder | Key Concepts |
|----------|-------|--------|--------------|
| Q1 | Finance Management System | `Q1FinanceApp` | Records, Interfaces, Sealed Classes |
| Q2 | Healthcare Management System | `Q2HealthSystem` | Generics, Collections, Dictionary |
| Q3 | Warehouse Inventory Management | `Q3WarehouseInventory` | Custom Exceptions, Generics, Collections |
| Q4 | Student Result Processing | `Q4StudentResults` | File I/O, Custom Exceptions |
| Q5 | Inventory Record System | `Q5InventoryRecords` | Records, JSON Serialization, File Operations |

---

## Getting Started

### Clone the Repository

```bash
git clone https://github.com/HFT-Codes/dcit318-assignment3-22014769.git
cd dcit318-assignment3-22014769
```

### Navigate to a Question

```bash
cd Q1FinanceApp  # or Q2HealthSystem, Q3WarehouseInventory, etc.
```

---

## Question 1: Finance Management System

**Folder:** `Q1FinanceApp`

### Concepts
- **Records** for immutable data
- **Interfaces** for transaction processing
- **Sealed classes** to prevent inheritance
- **Virtual methods** for transaction application

### Components
- `Transaction` - Immutable record with Id, Date, Amount, Category
- `ITransactionProcessor` - Interface for processing transactions
- `BankTransferProcessor`, `MobileMoneyProcessor`, `CryptoWalletProcessor` - Concrete implementations
- `Account` - Base account class with virtual method
- `SavingsAccount` - Sealed account with balance validation
- `FinanceApp` - Main application managing transactions

### Run
```bash
cd Q1FinanceApp
dotnet run
```

---

## Question 2: Healthcare Management System

**Folder:** `Q2HealthSystem`

### Concepts
- **Generic classes** for type-safe repository pattern
- **Collections** (List, Dictionary)
- **Lambda expressions** for LINQ queries
- **Data grouping** using Dictionary

### Components
- `Patient` - Represents patient record
- `Prescription` - Represents medication prescription
- `Repository<T>` - Generic repository with Add, GetAll, GetById, Remove methods
- `HealthSystemApp` - Manages patients and prescriptions, builds prescription map

### Run
```bash
cd Q2HealthSystem
dotnet run
```

---

## Question 3: Warehouse Inventory Management

**Folder:** `Q3WarehouseInventory`

### Concepts
- **Marker interfaces** for consistency
- **Custom exceptions** (DuplicateItemException, ItemNotFoundException, InvalidQuantityException)
- **Generic constraints** with where clause
- **Dictionary-based inventory** storage
- **Exception handling** with try-catch blocks

### Components
- `IInventoryItem` - Marker interface
- `ElectronicItem`, `GroceryItem` - Product classes
- Custom exceptions
- `InventoryRepository<T>` - Generic repository with validation
- `WareHouseManager` - Manages electronics and groceries

### Run
```bash
cd Q3WarehouseInventory
dotnet run
```

---

## Question 4: Student Result Processing

**Folder:** `Q4StudentResults`

### Concepts
- **File I/O** with StreamReader/StreamWriter
- **Custom exceptions** (InvalidScoreFormatException, MissingFieldException)
- **Data validation** during parsing
- **Grade calculation** logic
- **Exception handling** for robust error management

### Components
- `Student` - Student record with grade calculation
- Custom exceptions
- `StudentResultProcessor` - Reads from file, writes report to file
- Grade mapping: 80-100→A, 70-79→B, 60-69→C, 50-59→D, <50→F

### Run
```bash
cd Q4StudentResults
dotnet run
```

---

## Question 5: Inventory Record System

**Folder:** `Q5InventoryRecords`

### Concepts
- **C# Records** for immutable data
- **JSON serialization** (System.Text.Json)
- **File operations** with using statements
- **Generic classes** with interface constraints
- **Data persistence** and recovery

### Components
- `IInventoryEntity` - Marker interface
- `InventoryItem` - Immutable record implementing IInventoryEntity
- `InventoryLogger<T>` - Generic logger with Save/Load capabilities
- `InventoryApp` - Manages inventory lifecycle

### Run
```bash
cd Q5InventoryRecords
dotnet run
```

---

## Requirements

- **.NET 10.0** or later
- **C# 12** or later
- Windows/Linux/macOS with .NET SDK installed

## Building All Projects

```bash
# Build all solutions
cd Q1FinanceApp && dotnet build
cd ../Q2HealthSystem && dotnet build
cd ../Q3WarehouseInventory && dotnet build
cd ../Q4StudentResults && dotnet build
cd ../Q5InventoryRecords && dotnet build
```

## Running All Projects

```bash
# Run each project individually
cd Q1FinanceApp && dotnet run
cd Q2HealthSystem && dotnet run
cd Q3WarehouseInventory && dotnet run
cd Q4StudentResults && dotnet run
cd Q5InventoryRecords && dotnet run
```

## Project Structure

```
dcit318-assignment3-22014769/
├── Q1FinanceApp/
│   ├── Program.cs
│   └── Q1FinanceApp.csproj
├── Q2HealthSystem/
│   ├── Program.cs
│   └── Q2HealthSystem.csproj
├── Q3WarehouseInventory/
│   ├── Program.cs
│   └── Q3WarehouseInventory.csproj
├── Q4StudentResults/
│   ├── Program.cs
│   └── Q4StudentResults.csproj
├── Q5InventoryRecords/
│   ├── Program.cs
│   └── Q5InventoryRecords.csproj
├── .gitignore
└── README.md
```