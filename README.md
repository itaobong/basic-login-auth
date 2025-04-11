# Login Authentication System

A secure authentication system built with ASP.NET Core Web API that provides JWT-based authentication.

## Table of Contents
- [Features](#features)
- [Prerequisites](#prerequisites)
- [Dependencies](#dependencies)
- [Project Structure](#project-structure)
- [Setup](#setup)
- [Configuration](#configuration)
- [Usage](#usage)
- [Development](#development)
- [Security Considerations](#security-considerations)

## Features

- User registration and login
- JWT token-based authentication
- Secure password hashing using ASP.NET Core Identity
- SQL Server database for user storage
- Swagger/OpenAPI documentation
- Input validation and error handling

## Prerequisites

- .NET 7.0 SDK or later
- SQL Server (LocalDB or full instance)
- Visual Studio 2022 (optional)
- Postman or similar API testing tool (optional)

## Dependencies

The project uses the following NuGet packages:

```xml
<PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="7.0.20" />
<PackageReference Include="Microsoft.AspNetCore.Identity.EntityFrameworkCore" Version="7.0.20" />
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="7.0.20" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="7.0.20" />
```

Each package serves a specific purpose:
- `JwtBearer`: JWT authentication middleware
- `Identity.EntityFrameworkCore`: User identity management
- `EntityFrameworkCore.SqlServer`: SQL Server database provider
- `EntityFrameworkCore.Tools`: EF Core CLI tools for migrations

## Project Structure

```
LoginAuth/
├── Controllers/
│   └── AuthController.cs       # Authentication endpoints
├── Models/
│   ├── ApplicationUser.cs      # Custom user model
│   └── AuthModel.cs           # Authentication DTOs
├── Data/
│   └── ApplicationDbContext.cs # Database context
├── appsettings.json           # Application configuration
└── Program.cs                 # Application startup and DI
```

## Setup

1. Clone the repository:
   ```bash
   git clone https://github.com/itaobong/basic-login-auth
   cd LoginAuth
   ```

2. Install the .NET EF Core tools (if not already installed):
   ```bash
   dotnet tool install --global dotnet-ef
   ```

3. Install dependencies:
   ```bash
   dotnet restore
   ```

4. Update the database connection string in `appsettings.json` if needed:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=LoginAuthDb;..."
   }
   ```

5. Update JWT settings in `appsettings.json`:
   ```json
   "JWT": {
     "Secret": "<your-secret-key>",
     "ValidIssuer": "https://your-domain",
     "ValidAudience": "https://your-domain"
   }
   ```
   Note: Generate a strong secret key (at least 32 characters) for production use.

6. Create and apply database migrations:
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

## Configuration

### Database
- Default configuration uses SQL Server LocalDB
- Supports any SQL Server instance
- Connection string can be modified in `appsettings.json`

### JWT Settings
- `Secret`: Key for signing tokens (min 32 chars recommended)
- `ValidIssuer`: Token issuer URL
- `ValidAudience`: Token audience URL
- Token expiration: 3 hours (configurable in `AuthController.cs`)

### Security
- HTTPS enabled by default
- Password requirements configurable via Identity options
- Cross-Origin Resource Sharing (CORS) can be configured in `Program.cs`

## Usage

1. Start the application:
   ```bash
   dotnet run
   ```

2. Access the API:
   - Swagger UI: https://localhost:5134/swagger
   - API Base URL: https://localhost:5134/api

3. Test the authentication:
   - Register a user
   - Login to get a JWT token
   - Use the token in the Authorization header for protected endpoints

See [API_REFERENCE.md](./API_REFERENCE.md) for detailed endpoint documentation.

## Development

### Adding New Endpoints
1. Create new controller in `Controllers/`
2. Add authorization attributes as needed
3. Update API documentation

### Database Changes
1. Modify model classes
2. Create new migration:
   ```bash
   dotnet ef migrations add YourMigrationName
   ```
3. Apply migration:
   ```bash
   dotnet ef database update
   ```

### Running Tests
```bash
dotnet test
```

## Security Considerations

1. JWT Token Security:
   - Use strong secret keys
   - Keep expiration times reasonable
   - Store secrets in secure configuration

2. Password Security:
   - Passwords are hashed using Identity's default hasher
   - Configure password requirements in Program.cs
   - Never store plain-text passwords

3. Database Security:
   - Use connection string encryption
   - Limit database user permissions
   - Regular security audits

For more details about the API endpoints and request/response formats, see [API_REFERENCE.md](./API_REFERENCE.md).
