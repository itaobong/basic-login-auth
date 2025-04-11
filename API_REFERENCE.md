# API Reference

This document provides detailed information about the available API endpoints, their request/response formats, and example usage.

## Base URL

```
https://localhost:5134/api
```

## Authentication

All protected endpoints require a valid JWT token in the Authorization header:

```
Authorization: Bearer <your-jwt-token>
```

## Endpoints

### Register User

Register a new user in the system.

- **URL**: `/auth/register`
- **Method**: `POST`
- **Auth required**: No

#### Request Body

```json
{
    "email": "string",
    "password": "string",
    "firstName": "string",
    "lastName": "string"
}
```

##### Field Requirements

- `email`: Valid email format
- `password`: Must meet the following requirements:
  - At least 8 characters
  - At least one uppercase letter
  - At least one lowercase letter
  - At least one number
  - At least one special character
- `firstName`: Optional, string
- `lastName`: Optional, string

#### Success Response

- **Code**: `200 OK`
- **Content**:
```json
{
    "message": "User created successfully"
}
```

#### Error Responses

- **Code**: `400 BAD REQUEST`
- **Content**:
```json
{
    "errors": [
        {
            "code": "string",
            "description": "string"
        }
    ]
}
```

Common error codes:
- `DuplicateEmail`: Email is already registered
- `PasswordTooShort`: Password doesn't meet length requirement
- `PasswordRequiresNonAlphanumeric`: Password requires at least one special character
- `PasswordRequiresDigit`: Password requires at least one number
- `PasswordRequiresUpper`: Password requires at least one uppercase letter
- `PasswordRequiresLower`: Password requires at least one lowercase letter

### Login

Authenticate a user and receive a JWT token.

- **URL**: `/auth/login`
- **Method**: `POST`
- **Auth required**: No

#### Request Body

```json
{
    "email": "string",
    "password": "string"
}
```

#### Success Response

- **Code**: `200 OK`
- **Content**:
```json
{
    "token": "string"
}
```

The token is a JWT token that should be included in subsequent requests.

#### Error Response

- **Code**: `400 BAD REQUEST`
- **Content**:
```json
{
    "message": "Invalid credentials"
}
```

## Using the API with cURL

### Register a New User

```bash
curl -X POST https://localhost:5134/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{
    "email": "user@example.com",
    "password": "YourPassword123!",
    "firstName": "John",
    "lastName": "Doe"
  }'
```

### Login

```bash
curl -X POST https://localhost:5134/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{
    "email": "user@example.com",
    "password": "YourPassword123!"
  }'
```

## Using the API with Postman

1. Import the following collection:
```json
{
    "info": {
        "name": "Login Auth API",
        "schema": "https://schema.getpostman.com/json/collection/v2.1.0/collection.json"
    },
    "item": [
        {
            "name": "Register",
            "request": {
                "method": "POST",
                "header": [
                    {
                        "key": "Content-Type",
                        "value": "application/json"
                    }
                ],
                "url": {
                    "raw": "https://localhost:5134/api/auth/register",
                    "protocol": "https",
                    "host": ["localhost"],
                    "port": "5134",
                    "path": ["api", "auth", "register"]
                },
                "body": {
                    "mode": "raw",
                    "raw": "{\n    \"email\": \"user@example.com\",\n    \"password\": \"YourPassword123!\",\n    \"firstName\": \"John\",\n    \"lastName\": \"Doe\"\n}"
                }
            }
        },
        {
            "name": "Login",
            "request": {
                "method": "POST",
                "header": [
                    {
                        "key": "Content-Type",
                        "value": "application/json"
                    }
                ],
                "url": {
                    "raw": "https://localhost:5134/api/auth/login",
                    "protocol": "https",
                    "host": ["localhost"],
                    "port": "5134",
                    "path": ["api", "auth", "login"]
                },
                "body": {
                    "mode": "raw",
                    "raw": "{\n    \"email\": \"user@example.com\",\n    \"password\": \"YourPassword123!\"\n}"
                }
            }
        }
    ]
}
```

2. Set up environment variables:
   - `baseUrl`: `https://localhost:5134`
   - `token`: Will be automatically set after successful login

## Rate Limiting

The API implements rate limiting to prevent abuse:
- 100 requests per minute per IP address for registration
- 5 failed login attempts per minute per IP address
- 1000 requests per minute per IP address for authenticated endpoints

## Error Handling

All endpoints follow a consistent error response format:

```json
{
    "errors": [
        {
            "code": "string",
            "description": "string"
        }
    ]
}
```

Common HTTP status codes:
- `200 OK`: Request successful
- `400 Bad Request`: Invalid input
- `401 Unauthorized`: Missing or invalid authentication
- `403 Forbidden`: Insufficient permissions
- `404 Not Found`: Resource not found
- `429 Too Many Requests`: Rate limit exceeded
- `500 Internal Server Error`: Server error

## Swagger Documentation

The API documentation is also available in Swagger format at:
```
https://localhost:5134/swagger
```

This provides an interactive interface to:
- View all available endpoints
- Test endpoints directly in the browser
- View request/response schemas
- Download OpenAPI specification
