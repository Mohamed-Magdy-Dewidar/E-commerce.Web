# 🛒 E-commerce API

An advanced, modular ASP.NET Core (.NET 9) API for an e-commerce platform. This backend includes Redis caching, JWT authentication, Stripe payment integration, and a clean architecture using repository and specification patterns.

---

## 🚀 Tech Stack

- **Framework:** ASP.NET Core (.NET 9)
- **Database:** SQL Server via Entity Framework Core
- **Authentication:** JWT (JSON Web Tokens)
- **Caching:** Redis (via Docker)
- **Payments:** Stripe API
- **Architecture:**
  - Clean Architecture (Modular)
  - Repository Pattern
  - Specification Pattern
- **Tooling:** AutoMapper, Docker, Swagger

---

## 📦 Features

### 🛍️ Product Module
- Advanced filtering, sorting, and pagination
- Specification pattern for clean query logic
- Consistent and well-structured API responses

### 🧺 Basket Module
- Redis-based persistent basket (cart)
- CRUD operations with a modular Redis repository

### 🔐 Authentication Module
- JWT-based user registration and login
- Identity integration with custom exception handling

### 📦 Orders Module
- Complete order management with delivery options
- Database migrations for new order and delivery structures

### 💳 Payments Module
- Stripe integration for secure payment processing
- PaymentIntent tracking and webhook handling
- Redis-based response caching with custom cache attribute

---

## 🛠️ How to Run the Project

### Prerequisites

- [Visual Studio 2022+](https://visualstudio.microsoft.com/)
- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- Stripe test account (to get your test API keys)

### Redis Setup (via Docker)

> Redis is required for the basket and cache features.

```bash
docker run -d --name redis-cart -p 6379:6379 redis
````


⚠️ Redis on Windows
Redis on Windows is not officially supported.
Using Docker is the recommended approach for local development.

💳 Stripe Setup
Go to https://dashboard.stripe.com

Navigate to Developers > API Keys

Copy your Publishable key and Secret key

Update your appsettings.json like this:

```json
"StripeSettings": {
  "SecretKey": "sk_test_your_key_here",
  "PublishableKey": "pk_test_your_key_here"
},
"JwtSettings": {
  "Key": "your_jwt_secret_here",
  "Issuer": "your_api_issuer"
}
```


▶️ Running the App (via Visual Studio)
Open the solution in Visual Studio.

Ensure the Infrastructure project is used for Entity Framework Core migrations.

Open Package Manager Console and run:

Add-Migration InitialCreate
Update-Database

Set the Presentation project as the Startup Project.

Press F5 or click Run.

Access the Swagger UI:

which will open at port localhost:7060

📌 Notes
Ensure Redis is running (via Docker) before launching the app.

Stripe and JWT secrets are required for full functionality.

Codebase is modular and organized into:

Product

Basket

Identity

Orders

Payment

🧪 Testing
Swagger UI available for interactive API testing.

Use Authorization headers for protected endpoints.

Stripe test mode supports webhooks and test cards.


