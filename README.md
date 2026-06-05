# OrbitBook

![Status](https://img.shields.io/badge/Status-Completed-success)
![.NET Version](https://img.shields.io/badge/.NET-9.0-blue)
![Architecture](https://img.shields.io/badge/Architecture-Clean%20Architecture-brightgreen)
![Database](https://img.shields.io/badge/Database-Oracle%2019c%2B-red)

## The Problem & Solution

The space economy has left science fiction behind — but there's still no centralized platform to find, compare, and book space travel experiences in a simple, secure, and automated way.

**OrbitBook** is a web and mobile booking platform for space tourism, covering Suborbital Flights, Low Earth Orbit (LEO), Lunar Missions, and Mars Colonization. The API handles the full lifecycle of travelers, operators, and reservations with intelligent booking capabilities.

---

## Architecture & Design Patterns

Built with **C# / .NET 9** following **Clean Architecture** principles and **SOLID** design. Dependency injection is handled natively by ASP.NET Core.

### Solution Projects

| Project | Responsibility |
|---|---|
| `OrbitBook.Domain` | Business rules and entities (`User`, `Destination`, `Booking`, etc.). No external dependencies. |
| `OrbitBook.Application` | Use cases, service/repository interfaces, and DTOs. |
| `OrbitBook.Infrastructure` | Data persistence (`DbContext` with EF Core on Oracle), mappings, and repository implementations. |
| `OrbitBook.API` | Controllers, Swagger, middlewares, and JWT configuration. |
| `OrbitBook.Tests` | Unit tests focused on the Application layer using **xUnit** and **Moq**. |

### Entity Relationships

- `Users` (1:N) `Bookings` — every booking is tied to a user
- `Bookings` (N:1) `Destinations` — base catalog of available destinations
- `Bookings` (1:N) `Passengers` — passengers linked to each booking

---

## Features

- ✅ **REST API** with ASP.NET Core and clean architectural patterns
- ✅ **Relational persistence** via Oracle and Entity Framework Core
- ✅ **Entity relationships** with explicit `1:N` mappings using `HasMany` / `WithOne`
- ✅ **Global exception handling** via `GlobalExceptionHandlerMiddleware`
- ✅ **EF Core Migrations** for database schema management
- ✅ **Health Checks** for Oracle connectivity at `/health`
- ✅ **JWT Authentication** protecting sensitive routes (bookings, user history)
- ✅ **Swagger documentation** auto-generated
- ✅ **Unit tests** following the AAA pattern (Arrange, Act, Assert) with xUnit + Moq

---

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- Oracle Database 19c or higher
- Git

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/joaoGFG/OrbitBook.git
   cd OrbitBook
   ```

2. Set your Oracle connection string in `OrbitBook.API/appsettings.json`:
   ```json
   "ConnectionStrings": {
     "OracleConnection": "DATA SOURCE=your_server;USER ID=your_user;PASSWORD=your_password;"
   }
   ```
    You must set the connection string in `Program.cs` too:
    ```json
    var connectionString = builder.Configuration.GetConnectionString("OracleConnection") ?? "Insert your connection string here";
    ```

3. Run EF Core migrations to create the database schema:
   ```bash
   dotnet ef database update --project OrbitBook.Infrastructure --startup-project OrbitBook.API
   ```

4. (Optional) Populate the database with sample data using the SQL/DML scripts located in the repository root.

5. Build and run:
   ```bash
   dotnet build
   dotnet run --project OrbitBook.API/OrbitBook.API.csproj
   ```

The API will be available at `https://localhost:XXXX/swagger`.

---

## API Usage Examples

### 1. Public — List Travel Catalog
```
GET /api/destinations
```
Returns available trips from SpaceX, Blue Origin, and others. No authentication required.

### 2. Authentication — Generate JWT Token
```
POST /api/auth/login
```
```json
{
  "email": "carlos.souza@email.com",
  "password": "yourpassword"
}
```
Returns a `Bearer Token` to use in protected routes.

### 3. Register a New User
```
POST /api/auth/register
```
```json
{
  "name": "João Silva",
  "email": "joao@email.com",
  "password": "yourpassword",
  "documentNumber": "123.456.789-00"
}
```

### 4. Protected — View My Bookings
```
GET /api/bookings
Authorization: Bearer <your_token>
```
Extracts the user identity from the JWT claims and returns only that user's bookings.

### 5. Run Unit Tests
```bash
dotnet test
```
Validates authentication scenarios using the AAA pattern (Arrange, Act, Assert).

---

**Author:** JoaoGFG
