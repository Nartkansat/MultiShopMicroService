# 🛍️ MultiShop Microservices Architecture

[![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet)](https://dotnet.microsoft.com/)
[![Docker](https://img.shields.io/badge/Docker-2496ED?style=for-the-badge&logo=docker&logoColor=white)](https://www.docker.com/)
[![RabbitMQ](https://img.shields.io/badge/RabbitMQ-FF6600?style=for-the-badge&logo=rabbitmq&logoColor=white)](https://www.rabbitmq.com/)
[![MongoDB](https://img.shields.io/badge/MongoDB-47A248?style=for-the-badge&logo=mongodb&logoColor=white)](https://www.mongodb.com/)
[![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)](https://www.microsoft.com/en-us/sql-server)

MultiShop is a robust, scalable e-commerce platform built using a **Microservices Architecture** with **.NET 8**. The project demonstrates complex event-driven communication, centralized identity management, and decentralized database patterns.

---

## 🏗️ Architecture Overview

The project follows a **Microservices Architecture**, where each service is responsible for a specific business domain. It leverages **Ocelot API Gateway** to route traffic and **Identity Server 4** for secure, centralized authentication and authorization.

### 🧩 Services Breakdown

1.  **Catalog Service**: Manages products, categories, special offers, and feature sliders.
    -   **Tech**: ASP.NET Core Web API, **MongoDB** (NoSQL).
2.  **Order Service**: Handles complex order management using Domain-Driven Design (DDD) principles.
    -   **Tech**: ASP.NET Core Web API, **SQL Server**, **CQRS** (MediatR), **Onion Architecture**.
3.  **Discount Service**: Manages coupons and discount calculations.
    -   **Tech**: ASP.NET Core Web API, **SQL Server**, **Dapper** (Lightweight ORM).
4.  **Basket Service**: A high-performance shopping cart service.
    -   **Tech**: ASP.NET Core Web API, **Redis** (In-memory cache).
5.  **Cargo Service**: Tracks shipping status and logistics information.
    -   **Tech**: ASP.NET Core Web API, **SQL Server**, **Entity Framework Core**.
6.  **Identity Service**: The security backbone (SSO) for the entire system.
    -   **Tech**: **Identity Server 4**, ASP.NET Core Identity.
7.  **SignalR Service**: Provides real-time notifications (e.g., when an order status changes).
8.  **RabbitMQ (Message Broker)**: Facilitates asynchronous communication (Event Bus) between services like Order and Cargo.
9.  **Ocelot API Gateway**: Serves as the single entry point for all client requests, handling routing and token validation.

---

## 💻 Tech Stack

### Backend
- **Framework**: .NET 8, ASP.NET Core Web API
- **Design Patterns**: CQRS, Repository Pattern, Onion Architecture, Unit of Work.
- **Microservices Tools**: Ocelot (API Gateway), Identity Server 4 (Auth), RabbitMQ (Messaging), SignalR (Real-time).
- **ORMs**: Entity Framework Core, Dapper.
- **Validation & Mapping**: Fluent Validation, AutoMapper.

### Persistence
- **Relational**: SQL Server (Order, Cargo, Discount, Identity).
- **NoSQL**: MongoDB (Catalog).
- **Key-Value Store**: Redis (Basket).

### Frontend
- **UI**: ASP.NET Core MVC (WebUI).
- **Styling**: Bootstrap 5, Custom CSS, Modern Layouts.

---

## 🚀 Key Features

- ✅ **Distributed Data**: Each microservice manages its own database.
- ✅ **Centralized Identity**: Secure JWT-based authentication via Identity Server 4.
- ✅ **Asynchronous Messaging**: Event-driven communication for cargo updates and inventory checks.
- ✅ **Scalability**: High-performance cart management using Redis.
- ✅ **Rich UI**: Fully responsive and modern e-commerce user interface.
- ✅ **API Gateway**: Simplified client-to-microservice communication.

---

## 🛠️ Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [MongoDB](https://www.mongodb.com/try/download/community)
- [Redis](https://redis.io/download/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [RabbitMQ](https://www.rabbitmq.com/download.html)

### Installation
1.  **Clone the Repository**:
    ```bash
    git clone https://github.com/your-username/MultiShopMicroService.git
    ```
2.  **Update Connection Strings**: Check `appsettings.json` in each service folder and update DB connection strings to point to your local instances.
3.  **Run Migrations**: Apply EF Core migrations for SQL-based services (Order, Identity, Cargo).
4.  **Start Services**: Run all microservices or use the provided `.sln` file in Visual Studio.
5.  **Access the Web UI**: Navigate to the WebUI port (typically `http://localhost:5000` or as configured).

---

## 📄 License
This project is for educational purposes and is open for community contributions.

---

*Built with ❤️ using .NET 8 Microservices.*
