# 💳 Payment Processing System (Console App)

A modular and extensible payment processing system built with **C# (.NET 8)**, demonstrating real-world backend architecture using modern design patterns and clean architecture principles.

---

## 🚀 Overview

This project simulates a real-world payment processing system similar to platforms like:

- Stripe
- PayPal
- E-commerce payment backends

It supports multiple payment methods, fraud detection, fee calculation, transaction management, and clean separation of concerns.

---

## 🧠 Key Concepts Demonstrated

### 🔹 Factory Pattern
Used to dynamically select the correct payment processor at runtime.

```csharp
var processor = _paymentProcessorFactory.Create(methodType);
```

---

### 🔹 Strategy Pattern
Used for:
- Fee calculation per payment method
- Flexible business logic without modifying core services

---

### 🔹 Dependency Injection (DI)
All services are loosely coupled and registered via:

```csharp
services.AddSingleton<IPaymentProcessor, CreditCardPaymentProcessor>();
```

---

### 🔹 Repository Pattern
Handles transaction storage (In-Memory for simplicity).

---

### 🔹 Result Pattern
Standardized response handling using:

```csharp
OperationResult<T>
```

---

## 🏗️ Architecture

```
Presentation (Console UI)
    ↓
Application (Services / Factories / Results)
    ↓
Domain (Interfaces / Models / Enums)
    ↓
Infrastructure (Processors / Repositories / Logging / Fraud / Strategies)
```

---

## 💡 Features

- ✅ Process Payment
- 💸 Refund Payment
- 📊 Transaction History
- 🔍 Payment Status Check
- 🛡️ Fraud Detection (Mock)
- 💰 Dynamic Fee Calculation
- 🧩 Extensible Architecture (Easy to add new payment methods)

---

## 💳 Supported Payment Methods

- Credit Card
- PayPal
- Bank Transfer
- Crypto

---

## 🔄 Example Flow

1. User selects payment method
2. System validates input
3. Fraud check is performed
4. Fee is calculated using Strategy
5. Processor is selected via Factory
6. Payment is executed
7. Transaction is stored
8. Result is returned

---

## 📦 Technologies

- .NET 8
- C#
- Microsoft.Extensions.DependencyInjection

---

## 📁 Project Structure

```
PaymentProcessingSystem
│
├── Application
│   ├── Services
│   ├── Factories
│   └── Results
│
├── Domain
│   ├── Interfaces
│   ├── Models
│   └── Enums
│
├── Infrastructure
│   ├── Processors
│   ├── Repositories
│   ├── Logging
│   ├── Fraud
│   ├── FeeStrategies
│   └── Generators
│
├── Presentation
│   ├── Menus
│   └── InputHandlers
│
└── DependencyInjection
```

---

## 🖥️ Demo (Console)

Example interaction:

```
--- Payment Request ---
Enter amount: 100
Choose currency: USD
Choose payment method: Credit Card

Processing payment...
Payment succeeded.
Transaction ID: TXN-20240301-001
```

---

## 🧪 How to Run

1. Clone the repository:
```bash
git clone https://github.com/YOUR_USERNAME/YOUR_REPO.git
```

2. Open in Visual Studio

3. Run the project:
```bash
dotnet run
```

---

## 🧑‍💻 Author

Bilal

---

## 📌 Future Improvements

- Convert to ASP.NET Core Web API
- Add database (SQL Server / PostgreSQL)
- Advanced Fraud Detection (Rule-based system)
- Logging to file / external systems
- Payment Gateway integration (Stripe simulation)
- Add Unit Tests

---

## ⭐ Why this project?

This project demonstrates:

- Real-world backend architecture
- Clean code and separation of concerns
- Practical usage of design patterns
- Scalable and maintainable system design

It can be extended into a production-ready payment system.

---

## 📬 Contact

Feel free to reach out or suggest improvements 🚀
