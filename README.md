
```markdown
# 🐞 Bug Ticketing System

A comprehensive ticket management platform built with ASP.NET Core Web API for efficient bug tracking and resolution.

## 📚 Table of Contents
- [📖 Overview](#-overview)
- [💼 Business Context](#-business-context)
- [⚡ Key Features](#-key-features)
- [📂 Project Structure](#-project-structure)
- [🔌 API Endpoints](#-api-endpoints)
  - [🔐 Authentication](#-authentication)
  - [👥 Users](#-users)
  - [🧩 Projects](#-projects)
  - [🐞 Bugs](#-bugs)
  - [🐞 User-Bug Relationships](#-user-bug-relationships)
  - [🗂️ File Management](#️-file-management)
  - [🛡️ Role Management](#️-role-management)
  - [👥 User Role Assignment](#-user-role-assignment)
- [⚠️ Error Handling](#️-error-handling)
- [🚀 Getting Started](#-getting-started)
- [📄 License](#-license)

## 📖 Overview
Welcome to The Bug Ticketing System - a complete ticket management platform built with ASP.NET Core Web API. It enables software teams to efficiently log, track, assign, and resolve bugs during the development lifecycle.

Whether you're a developer, tester, QA engineer, or project manager, this system provides:

- 🎯 Bug ticketing and assignment to team members (developers/testers)
- 🔥 Prioritization and severity classification for issues
- 🛠️ Status management (Open → In Progress → Resolved → Closed)
- 👥 User roles: Admin, Developer, Tester, Project Manager
- 📈 Upload attachments (images, files) to bugs
- 🔐 JWT-based secure authentication

## 💼 Business Context
Bug tracking is a critical aspect of software development that ensures quality and reliability. This system serves development teams by:

- Providing a centralized platform for bug reporting
- Enabling clear communication between team members
- Tracking resolution progress and team performance
- Maintaining historical records of issues and fixes

## ⚡ Key Features
- Create, update, and delete bug tickets
- Assign bugs to specific developers or testers
- Add comments and attachments to bug tickets
- Track bug lifecycle status transitions
- Monitor team performance and bug trends
- Manage projects and team members
- Role-based access control
- Comprehensive API for integration

## 📂 Project Structure

```
├── /BugTicketingSystem.API         # API project containing controllers and startup configuration
│   ├── /Controllers                # API endpoint controllers
│   ├── /Extensions                 # Extension methods for services configuration
│   ├── /Middlewares                # Custom middleware components
│   ├── Program.cs                  # Application entry point
│   └── appsettings.json            # Configuration settings
├── /BugTicketingSystem.BL          # Core business logic and domain models
│   ├── /DTOs                       # Data Transfer Objects
│   ├── /Managers                   # Business logic implementation
│   ├── /Enums                      # Enumeration types
│   ├── /Validators                 # Validation classes for DTOs
├── /BugTrackingSystem.DAL          # Data access and external services implementation
│   ├── /Context                    # Database context
│   ├── /EntitiesConfiguration      # Entity configuration for database models
│   ├── /Enums                      # Enumeration types for data layer
│   ├── /Models                     # Database entity models
│   ├── /UnitOfWork                 # Unit of work pattern implementation
│   └── /Repositories               # Repository implementations
└── BugTrackingSystem.sln           # Solution file
```

## 🔌 API Endpoints

### 🔐 Authentication

| Action    | Endpoint                  | Description                             |
|-----------|---------------------------|-----------------------------------------|
| Login     | `POST /api/users/login`    | Authenticate user and return JWT token  |
| Register  | `POST /api/users/register` | Register a new user                     |

**Example Login Request:**
```json
{
  "email": "user@example.com",
  "password": "securePassword123"
}
```

**Example Login Response:**
```json
{
  "data": {
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
    "refreshToken": "",
    "expiration": "2025-04-30T01:41:21.2741724+03:00",
    "userId": "0aed563d-90bc-45d4-07f0-08dd85695adb",
    "email": "kholoud@gmail.com",
    "roles": ["Admin"]
  },
  "success": true,
  "message": "Login successful",
  "errors": null
}
```

### 👥 Users

| Action    | Endpoint                 | Description                     |
|-----------|---------------------------|---------------------------------|
| Get All   | `GET /api/users`          | Retrieve all registered users   |
| Get By ID | `GET /api/users/{id}`     | Retrieve specific user          |
| Update    | `PUT /api/users/{id}`     | Update user information         |
| Delete    | `DELETE /api/users/{id}`  | Delete a user                   |

### 🧩 Projects

| Action    | Endpoint                  | Description                     |
|-----------|----------------------------|---------------------------------|
| Get All   | `GET /api/projects`        | Retrieve all projects           |
| Get One   | `GET /api/projects/{id}`   | Get specific project details    |
| Create    | `POST /api/projects`       | Create new project (auth)       |
| Update    | `PUT /api/projects/{id}`   | Update project info (auth)      |
| Delete    | `DELETE /api/projects/{id}`| Delete a project (auth)         |

**Example Project Response:**
```json
{
  "projectId": "7e07d988-e45c-451d-6d1e-08dd8270c6e4",
  "name": "Project1",
  "description": "TestProject",
  "status": 1,
  "startDate": "2022-08-01T00:00:00",
  "endDate": "2023-04-06T00:00:00",
  "isActive": true,
  "users": [
    {
      "id": "6bc0f5d0-b34a-4304-59aa-08dd81b764ce",
      "firstName": "kholoud",
      "lastName": "ahmed",
      "email": "kholoud@gmail.com",
      "isActive": true
    }
  ],
  "bugs": [
    {
      "id": "f2087d96-cdac-4e57-d380-08dd82738c67",
      "title": "gg",
      "description": "ggDec",
      "status": 1,
      "priority": 1
    }
  ]
}
```

### 🐞 Bugs

| Action    | Endpoint               | Description                     |
|-----------|-------------------------|---------------------------------|
| Get All   | `GET /api/bugs`         | Retrieve all bugs               |
| Get One   | `GET /api/bugs/{id}`    | Retrieve detailed bug info      |
| Create    | `POST /api/bugs`        | Create a new bug report         |
| Update    | `PUT /api/bugs/{id}`    | Update existing bug             |
| Delete    | `DELETE /api/bugs/{id}` | Delete bug (auth required)      |

### 🐞 User-Bug Relationships

| Action          | Endpoint                                | Description                         |
|-----------------|-----------------------------------------|-------------------------------------|
| Assign Bug      | `POST /api/bugs/{id}/assignees`        | Assign a bug to a user              |
| Unassign Bug    | `DELETE /api/bugs/{id}/assignees/{id}` | Unassign user from a bug            |

### 🗂️ File Management

| Action            | Endpoint                                  | Description                         |
|-------------------|-------------------------------------------|-------------------------------------|
| Upload Attachment | `POST /api/bugs/{id}/attachments`        | Attach file to bug                  |
| Get Attachments   | `GET /api/bugs/{id}/attachments`         | Retrieve bug attachments            |
| Delete Attachment | `DELETE /api/bugs/{id}/attachments/{id}` | Remove attachment from bug          |

### 🛡️ Role Management

| Action        | Endpoint          | Description                     |
|---------------|-------------------|---------------------------------|
| Create Role   | `POST /api/Roles` | Create a new role               |
| Get All Roles | `GET /api/Roles`  | Retrieve all roles              |

### 👥 User Role Assignment

| Action        | Endpoint              | Description                     |
|---------------|-----------------------|---------------------------------|
| Assign Role   | `POST /api/UserRoles` | Assign role to user             |
| Get User Roles| `GET /api/UserRoles`  | Get user-role mappings          |
| Unassign Role | `DELETE /api/UserRoles` | Remove role from user          |

## ⚠️ Error Handling
All API endpoints follow a consistent error response format:

```json
{
  "data": [],
  "message": "Error message",
  "success": false,
  "errors": ["Detailed error description"]
}
```

Common HTTP status codes:
- 200: Success
- 201: Resource created
- 400: Bad request / Invalid input
- 401: Unauthorized / Invalid token
- 403: Forbidden / Insufficient permissions
- 404: Resource not found
- 500: Server error

## 🚀 Getting Started

### 📦 Prerequisites
- .NET 6 SDK or later
- SQL Server or SQL Server Express
- Visual Studio 2022 or VS Code

### ⚙️ Setup Instructions
1. Clone the repository:
   ```bash
    git clone https://github.com/Ereh11/Bug-Ticketing-System.git](https://github.com/KholoudELkasaby/Bug-Ticketing.git   
   ```
2. Update database connection string in `appsettings.json`
3. Run database migrations:
   ```bash
   dotnet ef database update
   ```
4. Run the application:
   ```bash
   dotnet run --project BugTrackingSystem.API
   ```
5. The API will be available at:
   - https://localhost:5279 or 
   - http://localhost:5000

## 📄 License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
```
