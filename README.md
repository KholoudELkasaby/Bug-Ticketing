
# Bug Ticketing System

The Bug Ticketing System is a comprehensive platform built with ASP.NET Core Web API. It enables software teams to efficiently manage the lifecycle of bugs through structured ticketing, assignment, and resolution workflows.

## 🎯 Purpose
This system supports teams in identifying, prioritizing, and resolving bugs during the development lifecycle. It caters to developers, testers, project managers, and admins.

## 🔐 Authentication
- JWT-based secure authentication
- Role-based authorization (Admin, Developer, Tester, Project Manager)

## ⚙️ Key Features
- Create, update, and delete bug tickets
- Assign bugs to specific team members
- Classify issues by priority and severity
- Manage bug statuses (Open → In Progress → Resolved → Closed)
- Comment on bugs and upload attachments (images/files)
- Track bug lifecycle and trends
- Manage projects and their associated users and bugs


📂 **Project Structure**

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
│   └── /Validators                 # Validation classes for DTOs
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

### Authentication
| Action    | Endpoint              | Description                             |
|-----------|------------------------|-----------------------------------------|
| Login     | `POST /api/users/login` | Authenticate user and return JWT token |
| Register  | `POST /api/users/register` | Register a new user                    |

#### Example: Login
**Request**:
```json
{
  "email": "user@example.com",
  "password": "securePassword123"
}
```
**Response**:
```json
{
  "data": {
    "token": "<JWT_TOKEN>",
    "userId": "<USER_ID>",
    "email": "user@example.com",
    "roles": ["Admin"]
  },
  "success": true,
  "message": "Login successful"
}
```

### Users
| Action    | Endpoint                 | Description                     |
|-----------|---------------------------|---------------------------------|
| Get All   | `GET /api/users`          | Retrieve all users              |
| Get By ID | `GET /api/users/{id}`     | Retrieve user by ID             |
| Update    | `PUT /api/users/{id}`     | Update user info                |
| Delete    | `DELETE /api/users/{id}`  | Delete user                     |

### Projects
| Action    | Endpoint                  | Description                     |
|-----------|----------------------------|---------------------------------|
| Get All   | `GET /api/projects`        | Retrieve all projects           |
| Get One   | `GET /api/projects/{id}`   | Retrieve project by ID          |
| Create    | `POST /api/projects`       | Create new project (auth)       |
| Update    | `PUT /api/projects/{id}`   | Update project info (auth)      |
| Delete    | `DELETE /api/projects/{id}`| Delete a project (auth)         |

### Bugs
| Action    | Endpoint               | Description                     |
|-----------|-------------------------|---------------------------------|
| Get All   | `GET /api/bugs`         | Retrieve all bugs               |
| Get One   | `GET /api/bugs/{id}`    | Retrieve detailed bug info      |
| Create    | `POST /api/bugs`        | Create a new bug report         |
| Update    | `PUT /api/bugs/{id}`    | Update existing bug             |
| Delete    | `DELETE /api/bugs/{id}` | Delete bug (auth required)      |

## 🧠 Business Context
Bug tracking is essential for maintaining high software quality and ensuring that issues are systematically addressed. This system streamlines collaboration across development and QA teams.

⚙️ Getting Started
📦 Prerequisites
.NET 6 SDK or later

SQL Server or SQL Server Express

Visual Studio 2022 or VS Code

⚙️ Setup Instructions
Clone the repository

bash
Copy
Edit
git clone https://github.com/KholoudELkasaby/Bug-Ticketing.git
Update the database connection string in appsettings.json

Run database migrations

bash
Copy
Edit
dotnet ef database update


Run the application

bash
Copy
Edit
dotnet run --project BugTrackingSystem.API
The API will be available at https://localhost:5279 or http://localhost:5000

📄 License
This project is licensed under the MIT License - see the LICENSE file for details.





