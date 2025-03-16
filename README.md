# 💰 **SpendSmart – The Ultimate Financial Management API** 🚀  

![.NET 10](https://img.shields.io/badge/.NET%2010-blue?style=for-the-badge)  
![CQRS](https://img.shields.io/badge/CQRS-%F0%9F%9A%80-purple?style=for-the-badge)  
![MediatR](https://img.shields.io/badge/MediatR-%E2%9C%85-green?style=for-the-badge)  
![Secure API](https://img.shields.io/badge/Secure%20API-%F0%9F%94%92-red?style=for-the-badge)  
![Cloud Deployment](https://img.shields.io/badge/Cloud%20Ready-%F0%9F%93%A2-orange?style=for-the-badge)  

Welcome to **SpendSmart**, the ultimate financial management API designed to help users **track expenses, manage budgets, and control financial transactions** efficiently. Built with **.NET 10**, it leverages modern **CQRS architecture, MediatR for command handling, and PostgreSQL for secure data storage**.  

> **Why Use SpendSmart?**  
> - 💰 **Manage your expenses and budgets effectively**  
> - 🚀 **Optimized for high performance and scalability**  
> - 🔐 **Secure, authenticated, and cloud-ready**  
> - 🛠 **Built with the latest best practices in software architecture**  

---

## **🌟 Features**  

✅ **Expense Tracking** – Keep records of **income, expenses, and savings**.  
✅ **Budgeting System** – Create, update, and monitor **monthly budgets**.  
✅ **Transaction Management** – View **detailed reports and transaction summaries**.  
✅ **Secure Authentication** – JWT-based authentication for **secure access**.  
✅ **Multi-Currency Support** – Add and remove **multiple currencies** per user.  
✅ **Timezone Customization** – Personalize your experience with **dynamic timezones**.  
✅ **RESTful API** – Well-structured, versioned, and **easy-to-integrate API**.  
✅ **Cloud-Ready Deployment** – Supports **Docker, Kubernetes, and cloud hosting**.  

---

## **🏗️ Architecture & Design Patterns**  

📌 **CQRS (Command Query Responsibility Segregation)** – Separates **read & write** models.  
📌 **MediatR (Mediator Pattern)** – Decouples **business logic from controllers**.  
📌 **Repository Pattern** – Encapsulates **data access logic**.  
📌 **Dependency Injection (DI)** – Ensures **modular and testable code**.  
📌 **Layered Architecture** – Divided into:  
   - **Application Layer** – Business logic, commands, queries, and handlers.  
   - **Domain Layer** – Core **business models and rules**.  
   - **Persistence Layer** – Handles **data storage** with PostgreSQL.  
   - **Infrastructure Layer** – Logging, authentication, notifications.  
   - **API Layer** – RESTful endpoints for **frontend & integrations**.  

---

## **📂 Project Structure**  

📌 **src/SpendSmart.Api** – Main API layer, exposing endpoints.  
📌 **src/Application** – Business logic, MediatR commands & queries.  
📌 **src/Domain** – Core financial models and validation rules.  
📌 **src/Infrastructure** – Authentication, logging, and notifications.  
📌 **src/Persistence** – Database access and PostgreSQL integration.  
📌 **tests/SpendSmart.Tests** – Unit and integration tests.  

---

## **🚀 Getting Started**  

### **📌 Prerequisites**  
✅ [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)  
✅ [Docker](https://www.docker.com/get-started)  
✅ [PostgreSQL](https://www.postgresql.org/download/)  

### **Step 1: Clone the Repository**  
```bash
git clone https://github.com/yourusername/SpendSmart.git
cd SpendSmart
```

### **Step 2: Install Dependencies**  
```bash
dotnet restore
```

### **Step 3: Run the API Locally**  
```bash
dotnet run
```

---

## **🌍 API Endpoints**  

### **🧑 User Management**  
| Method | Endpoint | Description |
|--------|---------|-------------|
| **POST** | `/api/users/setup` | Sets up a new user |
| **POST** | `/api/users/change-timezone` | Changes the user’s timezone |
| **POST** | `/api/users/change-password` | Updates user password |

### **💳 Transaction Management**  
| Method | Endpoint | Description |
|--------|---------|-------------|
| **POST** | `/api/transactions/create` | Creates a new transaction |
| **GET**  | `/api/transactions/current-month-summary` | Fetches monthly summary |
| **GET**  | `/api/transactions/{id}` | Retrieves a transaction by ID |

### **📊 Budget Management**  
| Method | Endpoint | Description |
|--------|---------|-------------|
| **POST** | `/api/budgets/create` | Creates a new budget |
| **GET**  | `/api/budgets/active` | Lists active budgets |
| **GET**  | `/api/budgets/{id}` | Retrieves budget details |

### **🔑 Authentication**  
| Method | Endpoint | Description |
|--------|---------|-------------|
| **POST** | `/api/auth/register` | Registers a new user |
| **POST** | `/api/auth/login` | Authenticates user |
| **POST** | `/api/auth/refresh-token` | Refreshes authentication token |

---

## **🐳 Running with Docker**  

### **Step 1: Build & Run with Docker Compose**  
```bash
docker-compose up --build
```

### **Step 2: Access API & Documentation**  
🔹 **Swagger UI** – [http://localhost:5000/swagger](http://localhost:5000/swagger)  

> **🚀 The API is fully Dockerized, making deployment simple and scalable.**  

---

## **🧪 Testing**  

### **Run Automated Tests**  
```bash
dotnet test
```

### **Manual API Testing**  
📌 **Use Postman or Swagger UI** to:  
✅ **Create transactions** → `/api/transactions/create`  
✅ **Fetch budgets** → `/api/budgets/active`  
✅ **Authenticate user** → `/api/auth/login`  

---

## **🎯 Why Use SpendSmart?**  

✅ **Full-Fledged Expense Tracking & Budgeting System**  
✅ **Optimized for Performance & Scalability**  
✅ **Secure & Reliable with Authentication**  
✅ **Built on Modern Development Best Practices**  
✅ **Cloud-Ready with Docker & PostgreSQL**  

---

## **📜 License**  

This project is licensed under the **MIT License**. See [LICENSE](LICENSE) for details.  

---

## **📞 Contact**  

For feedback, contributions, or inquiries:  
📧 **Email**: [mreshboboyev@gmail.com](mailto:mreshboboyev@gmail.com)  
💻 **GitHub**: [MrEshboboyev](https://github.com/MrEshboboyev/SpendSmart)  

---

🚀 **Take control of your finances with SpendSmart!** Clone the repo & get started now!  
