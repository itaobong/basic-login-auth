# System Architecture

This document provides UML diagrams illustrating the system's architecture and interactions.

## Class Diagram

```mermaid
classDiagram
    class ApplicationUser {
        +string Id
        +string Email
        +string? FirstName
        +string? LastName
        +string UserName
    }
    
    class AuthController {
        -UserManager<ApplicationUser> _userManager
        -IConfiguration _configuration
        +Register(RegisterModel) Task<IActionResult>
        +Login(LoginModel) Task<IActionResult>
        -GenerateJwtToken(ApplicationUser) string
    }
    
    class ApplicationDbContext {
        +DbSet<ApplicationUser> Users
        +OnModelCreating(ModelBuilder)
    }
    
    class RegisterModel {
        +string? Email
        +string? Password
        +string? FirstName
        +string? LastName
    }
    
    class LoginModel {
        +string? Email
        +string? Password
    }
    
    ApplicationUser --|> IdentityUser : inherits
    ApplicationDbContext --|> IdentityDbContext : inherits
    AuthController ..> RegisterModel : uses
    AuthController ..> LoginModel : uses
    AuthController ..> ApplicationUser : manages
    ApplicationDbContext ..> ApplicationUser : contains
```

## Sequence Diagrams

### User Registration Flow

```mermaid
sequenceDiagram
    participant Client
    participant AuthController
    participant UserManager
    participant DbContext
    participant Database
    
    Client->>AuthController: POST /api/auth/register
    Note over Client,AuthController: RegisterModel payload
    
    AuthController->>AuthController: Validate input
    
    AuthController->>UserManager: CreateAsync(user, password)
    UserManager->>UserManager: Hash password
    UserManager->>DbContext: Add user
    DbContext->>Database: Save changes
    
    alt Success
        Database-->>DbContext: Confirm save
        DbContext-->>UserManager: Success
        UserManager-->>AuthController: Success result
        AuthController-->>Client: 200 OK
    else Failure
        Database-->>DbContext: Error
        DbContext-->>UserManager: Error
        UserManager-->>AuthController: Error result
        AuthController-->>Client: 400 Bad Request
    end
```

### User Login Flow

```mermaid
sequenceDiagram
    participant Client
    participant AuthController
    participant UserManager
    participant DbContext
    participant Database
    
    Client->>AuthController: POST /api/auth/login
    Note over Client,AuthController: LoginModel payload
    
    AuthController->>UserManager: FindByEmailAsync(email)
    UserManager->>DbContext: Query user
    DbContext->>Database: Select user
    Database-->>DbContext: User data
    DbContext-->>UserManager: User entity
    UserManager-->>AuthController: User result
    
    AuthController->>UserManager: CheckPasswordAsync(user, password)
    UserManager->>UserManager: Verify hash
    
    alt Valid Credentials
        UserManager-->>AuthController: Password valid
        AuthController->>AuthController: Generate JWT
        AuthController-->>Client: 200 OK with token
    else Invalid Credentials
        UserManager-->>AuthController: Password invalid
        AuthController-->>Client: 400 Bad Request
    end
```

## Component Diagram

```mermaid
graph TB
    subgraph Client Layer
        Client[Web/Mobile Client]
    end
    
    subgraph API Layer
        Auth[Auth Controller]
        JWT[JWT Service]
    end
    
    subgraph Service Layer
        Identity[Identity Service]
        UserMgmt[User Management]
    end
    
    subgraph Data Layer
        Context[DB Context]
        DB[(SQL Server)]
    end
    
    Client -->|HTTP| Auth
    Auth -->|Uses| JWT
    Auth -->|Uses| Identity
    Identity -->|Uses| UserMgmt
    UserMgmt -->|Uses| Context
    Context -->|CRUD| DB
```

## Security Flow

```mermaid
sequenceDiagram
    participant Client
    participant API
    participant Auth
    participant DB
    
    Client->>API: Request with JWT
    API->>Auth: Validate JWT
    
    alt Valid Token
        Auth-->>API: Token valid
        API->>DB: Process request
        DB-->>API: Data
        API-->>Client: 200 OK with data
    else Invalid Token
        Auth-->>API: Token invalid
        API-->>Client: 401 Unauthorized
    end
```

## System Interactions

The authentication system follows these key interaction patterns:

1. **Registration Flow**:
   - Client sends registration data
   - System validates input
   - Password is hashed
   - User data is stored
   - Success/failure response returned

2. **Login Flow**:
   - Client sends credentials
   - System verifies user exists
   - Password is verified
   - JWT token generated
   - Token returned to client

3. **Authentication Flow**:
   - Client includes JWT in requests
   - System validates token
   - Access granted/denied based on validation

4. **Data Security**:
   - Passwords are hashed using Identity framework
   - JWTs are signed with private key
   - Database access is controlled via Entity Framework
   - HTTPS ensures transport security

The diagrams above illustrate these interactions in detail, showing how different components of the system work together to provide secure authentication functionality.
