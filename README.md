# 🛍️ E-Commerce REST API

![.NET Core](https://img.shields.io/badge/.NET-8.0-purple)
![Entity Framework](https://img.shields.io/badge/Entity%20Framework%20Core-7.0-blue)

A modern RESTful API for e-commerce applications built with ASP.NET Core, Entity Framework Core, and Sql Server as Database and Redis as In Memory Data Store for cache.

## ✨ Features

- **Product Management**: CRUD operations for products with categories and brands
- **User Authentication**: JWT-based authentication system
- **Shopping Cart**: Persistent cart functionality
- **Order Processing**: Complete order lifecycle management
- **Search & Filtering**: Advanced product search capabilities
- **Pagination**: Efficient data retrieval
- **Swagger Documentation**: Interactive API documentation

## 🛠️ Technology Stack

- **Backend**: ASP.NET Core 8.0
- **Database**: PostgreSQL with Entity Framework Core
- **Authentication**: JWT Bearer Tokens
- **API Documentation**: Swagger/OpenAPI
- **Testing**: xUnit (optional)
- **CI/CD**: GitHub Actions (optional)

## 🚀 Getting Started

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/Mohamed-Magdy-Dewidar/E-commerce.Web.git
Navigate to project directory:

bash
cd E-commerce.Web
Configure database:

Update connection string in appsettings.json

Run migrations:

bash
dotnet ef database update
Run the application:

bash
dotnet run
📚 API Documentation
Access interactive API documentation at:

http://localhost:5000/swagger
🌱 Seed Data
The application comes with seed data for:

Product brands

Product types

Sample products

To seed the database, run:

bash
dotnet run --seed
🤝 Contributing
Contributions are welcome! Please follow these steps:

Fork the repository

Create your feature branch (git checkout -b feature/AmazingFeature)

Commit your changes (git commit -m 'Add some AmazingFeature')

Push to the branch (git push origin feature/AmazingFeature)

Open a Pull Request

📄 License
This project is licensed under the GNU Affero General Public License v3.0 - see the LICENSE file for details.

📧 Contact
Mohamed Magdy Dewidar - mohamed.magdy@example.com

Project Link: https://github.com/Mohamed-Magdy-Dewidar/E-commerce.Web
