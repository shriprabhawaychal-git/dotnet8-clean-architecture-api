# CleanArchitecture Task Management API

A production-style backend project built with **ASP.NET Core (.NET 8)** following **Clean Architecture** principles.

This project demonstrates how to build a secure, maintainable, and scalable REST API using modern .NET development practices such as:

- Clean Architecture
- Entity Framework Core (Code First)
- SQL Server LocalDB
- Repository Pattern
- Dependency Injection
- JWT Authentication
- Role-Based Authorization
- Pagination
- Global Exception Middleware
- Request Logging Middleware
- Swagger/OpenAPI Documentation

---

## Project Features

- User Registration and Login
- JWT Token Generation
- Role-based access control using `Admin` and `User`
- CRUD operations for Tasks
- Pagination support for task listing
- Centralized exception handling
- Request and response logging
- Swagger UI integration for API testing
- Entity Framework Core migrations

---

## Technologies Used

- ASP.NET Core Web API (.NET 8)
- Entity Framework Core
- SQL Server LocalDB
- JWT Bearer Authentication
- BCrypt.Net for password hashing
- Swagger / OpenAPI
- Visual Studio 2022
- Git and GitHub

---

## Clean Architecture Structure

The solution is divided into the following layers:

### 1. API Layer (`CleanArchitecture.Api`)
Handles:
- HTTP requests and responses
- Controllers
- Middleware
- Swagger configuration
- Authentication and Authorization setup

### 2. Application Layer (`CleanArchitecture.Application`)
Handles:
- DTOs
- Interfaces
- Business contracts
- Application-level abstractions

### 3. Domain Layer (`CleanArchitecture.Domain`)
Handles:
- Core business entities
- Domain models

### 4. Infrastructure Layer (`CleanArchitecture.Infrastructure`)
Handles:
- Database access
- Entity Framework Core DbContext
- Repository implementations
- External technical concerns

### Dependency Flow

API → Application → Domain  
Infrastructure → Application + Domain

This keeps the business logic independent from frameworks and database concerns.

---

## Database Configuration

The project uses **SQL Server LocalDB** for local development.

Connection string used in `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=CleanArchitectureDb;Trusted_Connection=True;TrustServerCertificate=True;"
}

## Authentication Flow

1. User registers with email and password
2. Password is securely hashed using BCrypt
3. User logs in using credentials
4. API generates a JWT token
5. Client sends JWT token in Authorization header
6. Protected endpoints validate the token
7. Role-based authorization controls access

Example Authorization Header:

Authorization: Bearer {your_jwt_token}

---

## API Endpoints

### Authentication

POST /api/auth/register  
Register a new user

POST /api/auth/login  
Login and receive JWT token

### Tasks

GET /api/tasks  
Get paginated list of tasks

POST /api/tasks  
Create new task

PUT /api/tasks/{id}  
Update task

DELETE /api/tasks/{id}  
Delete task (Admin only)

---

## Error Handling

The API uses **Global Exception Middleware** to return standardized error responses.

Example error response:

{
  "message": "An unexpected error occurred",
  "details": "Error details"
}

Unauthorized request:

HTTP 401 Unauthorized

Forbidden request:

HTTP 403 Forbidden
