The Bug Ticketing System is a complete ticket management platform built with ASP.NET Core Web API. It enables software teams to efficiently log, track, assign, and resolve bugs during the development lifecycle.

Whether you're a developer, tester , or project manager, this system provides:

- Bug ticketing and assignment to team members (developers/testers)

- Prioritization and severity classification for issues

- Status management (Open ➡️ In Progress ➡️ Resolved ➡️ Closed)

- User roles: Admin, Developer, Tester, Project Manager

 Upload attachments (images, files) to bugs

 JWT-based secure authentication

 Business Context
Bug tracking is a critical aspect of software development that ensures quality and reliability. This system serves:

⚡Key Features
Create, update, and delete bug tickets

Assign bugs to specific developers or testers

Add comments and attachments to bug tickets

Track bug lifecycle status transitions

Monitor team performance and bug trends

Manage projects and team members

The API supports a complete bug lifecycle from creation to resolution, with features for prioritization, assignment, categorization, and reporting.

📂 Project Structure
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
│   ├── /Validators                 # Validations classes for validate DTOs
├── /BugTrackingSystem.DAL          # Data access and external services implementation
│   ├── /Context                    # Database context
|   ├── /EntitiesConfiguration      # Entity configuration for database models
|   ├── /Enums                      # Enumeration types for data layer
|   ├── /Models                     # Database entity models
|   ├── /UnitOfWork                 # Unit of work pattern implementation
│   └── /Repositories               # Repository implementations
└── BugTrackingSystem.sln           # Solution file
🔌 API Endpoints
🔐 Authentication Flow
📁 Endpoints Summary
Action	Endpoint	Description
🔑 Login	POST /api/users/login	Authenticate user and generate JWT token
📝 Register	POST /api/users/register	Register a new user and receive JWT token
POST /api/users/login
Description: Authenticate user and generate JWT token

Request Body:

{
  "email": "user@example.com",
  "password": "securePassword123"
}
Response: JWT token and user information

  {
    {
    "data": {
        "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjBhZWQ1NjNkLTkwYmMtNDVkNC0wN2YwLTA4ZGQ4NTY5NWFkYi",
        "refreshToken": "",
        "expiration": "2025-04-30T01:41:21.2741724+03:00",
        "userId": "0aed563d-90bc-45d4-07f0-08dd85695adb",
        "email": "Kholoud@gmail.com",
        "roles": [
            "Admin"
        ]
    },
    "success": true,
    "message": "Login successful",
    "errors": null
  }
}
POST /api/users/register
Description: Register a new user account

Request Body:

{
  "email": "user@example.com",
  "password": "securePassword123",
  "firstName": "John",
  "lastName": "Doe",
}
Response: User object with JWT token

  {
    "data": {
        "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjlkYjJhM2VmLWMwMWYtNDlmOC1kMTFkLTA4ZGQ4NzVlODY1NS",
        "refreshToken": "",
        "expiration": "2025-04-30T01:43:41.6303716+03:00",
        "userId": "9db2a3ef-c01f-49f8-d11d-08dd875e8655",
        "email": "lolo@gmail.com",
        "roles": []
    },
    "success": true,
    "message": "Registration successful",
    "errors": null
} 
👥 Users
📁 Endpoints Summary
Action	Endpoint	Description
📄 Get All	GET /api/users	Retrieve all registered users
📄 Get By Id	GET /api/users/{id}	Retrieve the specific registered users
🛠 Update	PUT /api/users/{id}	Update a specific user's info
❌ Delete	DELETE /api/users/{id}	Delete a specific user
GET /api/users
Description: Get all registerd users
Response: Users objects with details about their roles and assigned bugs
DELETE /api/users/{id}
Description: delete the user with provided Id
Response: Object Represent the status of deletion.
PUT /api/users/{id}
Description: Update the data of user with provided Id

Request Body:

{
  "firstName": "John",
  "lastName": "Doe",
  "isActive": true,
}
Response: Object Represent the status of update.

GET /api/users/{id}
Description: Get specific user's data
Response: User object with details about their roles and assigned bugs.
🧩 Projects
📁 Endpoints Summary
Action	Endpoint	Description
📄 Get All	GET /api/projects	Retrieve all projects
🔍 Get One	GET /api/projects/{id}	Get specific project details
🧱 Create	POST /api/projects	Create a new project (auth)
🛠 Update	PUT /api/projects/{id}	Update project info (auth)
❌ Delete	DELETE /api/projects/{id}	Delete a project (auth)
GET /api/projects
Description: Get all projects

Response: list of projects

[
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
              "lastName": "Ahmed",
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
          },
          {
              "id": "7f4d54f1-92d0-4e4f-51f6-08dd86055e36",
              "title": "test2",
              "description": "test-Desc",
              "status": 1,
              "priority": 1
          }
      ]
  },
  {
      "projectId": "305b3cf3-4693-473c-874b-08dd8563036c",
      "name": "tt",
      "description": "Test-Desc",
      "status": 4,
      "startDate": "2021-06-05T00:00:00",
      "endDate": "2023-06-07T00:00:00",
      "isActive": false,
      "users": [],
      "bugs": [
          {
              "id": "ad6389ef-c2c6-427f-219e-08dd86b7ab0e",
              "title": "test02",
              "description": "testDesc02",
              "status": 1,
              "priority": 1
          }
      ]
  },
]
GET /api/projects/{id}
Description: Get project details by ID
Response: Project details with associated bugs
POST /api/projects
Description: Create a new project

Headers: Authorization Bearer [token]

Request Body:

{
  "name": "project name",
  "description": "project data",
  "status": 1,
  "startDate": "2022-05-06",
  "endDate": "2023-06-06",
  "isActive": true
}
Response: Created project object

PUT /api/projects/{id}
Description: Update existing project

Headers: Authorization Bearer [token]

Request Body:

{
  "name": "project name",
  "description": "project data",
  "status": 1,
  "startDate": "2022-05-06",
  "endDate": "2023-06-06",
  "isActive": true
}
Response: Updated project object

DELETE /api/projects/{id}
Description: Delete project
Headers: Authorization Bearer [token]
Response: Success message
🐞 Bugs
📁 Endpoints Summary
Action	Endpoint	Description
📄 Get All	GET /api/bugs	Retrieve all bugs
🔍 Get One	GET /api/bugs/{id}	Retrieve detailed bug info
🐛 Create	POST /api/bugs	Create a new bug report
📝 Update	PUT /api/bugs/{id}	Update existing bug info
❌ Delete	DELETE /api/bugs/{id}	Delete a bug (auth required)
GET /api/bugs
Description: Get all bugs
Response: List of bugs
GET /api/bugs/{id}
Description: Get detailed bug information
Response: Detailed bug data including its attachments
{
  "data": {
      "id": "e1904520-ea11-4c88-3cfb-08dd8785765b",
      "title": "Bug Updated",
      "description": "It's just Test",
      "status": 1,
      "priority": 1,
      "project": {
          "projectId": "17d6b305-5799-42bb-cd44-08dd8785256b",
          "name": "ProjectProgramming"
      },
      "attachments": [
          {
              "attachmentId": "38a3a21d-69b1-4b5d-b456-910e041a6b27",
              "fileName": "API Project.pdf",
              "filePath": "/attachments/b98aa617-0332-4867-92bf-7726f039a857.pdf",
              "createdDate": "2025-04-30T13:45:43.768"
          },
          {
              "attachmentId": "e85290af-e8c9-43da-bc39-fa5e08997810",
              "fileName": "SSDLC Certification.pdf",
              "filePath": "/attachments/9176f0df-bc96-4918-bae1-de154c493b1f.pdf",
              "createdDate": "2025-04-30T01:26:17.768"
          }
      ]
  },
  "success": true,
  "message": "Bug retrieved successfully.",
  "errors": null
}
POST /api/bugs
Description: Create new bug report

Request Body:

{
  "title": "Bug title",
  "description": "Detailed bug description",
  "projectId": "project123",
  "status": 1,
  "priority": 3,
}
Response: Created bug object

PUT /api/bugs/{id}
Description: Update bug information

Request Body:

{
  "title": "Bug title",
  "description": "Detailed bug description",
  "projectId": "project123",
  "status": 1,
  "priority": 3,
}
Response: Updated bug object

DELETE /api/bugs/{id}
Description: Delete bug
Headers: Authorization Bearer [token]
Response: Success message
🐞 User-Bug Relationships
📁 Endpoints Summary
Action	Endpoint	Description
➕ Assign Bug	POST /api/bugs/{id}/assignees	Assign a bug to a user
❌ Unassign Bug	DELETE /api/bugs/{id}/assignees/{id}	Unassign a specific user from a specific bug
POST /api/bugs/{id}/assignees
Description: Assign bug to user

Headers: Authorization Bearer [token]

Request Body:

{
  "userId": "45d15845-7123-472a-0ff2-08dd87844aae",
  "assignedDate": "2025-04-29"
}
Response: Object with message successfully or failed

DELETE /api/bugs/{id}/assignees/{id}
Description: Unassign specific user from a specific bug.
Headers: Authorization Bearer [token]
Response: Object with message successfully or failed
🗂️ File Management
Manage file attachments related to bug reports.

Action	Endpoint	Description
📤 Upload Attachment	POST /api/bugs/:id/attachments	Attach a file (image, log, etc.) to a bug
📂 Get Attachments	GET /api/bugs/:id/attachments	Retrieve all attachments linked to a specific bug
🗑️ Delete Attachment	DELETE /api/bugs/:id/attachments/:attachmentId	Remove a specific attachment from a bug
POST /api/bugs/{id}/attachments
Description: Attach a file (image, log, etc.) to a specific bug.

Headers: Authorization Bearer [token], Content-Type: multipart/form-data

Request Form Data:

file: The file to upload
Response:

{
    "data": {
        "attachmentId": "024ebc69-a038-434c-82a4-5bf5fcdf0512",
        "fileName": "WhatsApp Image 2024-12-19 at 8.14.38 PM.jpeg",
        "filePath": "http://localhost:5279/attachments/a71c0da4-1846-472e-8543-78574222ed4f.jpeg",
        "createdDate": "2025-04-29T20:54:38.3461079Z"
    },
    "success": true,
    "message": "Attachment uploaded successfully",
    "errors": null
}
DELETE /api/Bugs/{bugId}/attachments/{attachmentId}
Description: Delete attachment from the bug.

Response:

{
  "success": true,
  "message": "Attachment deleted successfully",
  "errors": null
}
GET /api/Bugs/{bugId}/attachments
Description: Get all attachment of the specific bug.
Response: List of all attachments of specific bug
🛡️ Role Management
📁 Endpoints Summary
Action	Endpoint	Description
➕ Create Role	POST /api/Roles	Create a new role
📄 Get All Roles	GET /api/Roles	Retrieve a list of all roles
POST /api/Roles
Description: Create a new role in the system.

Headers: Authorization Bearer [token]

Request Body:

{
  "name": "Admin",
  "description": "Administrator role with full permissions"
}
GET /api/Roles
Description: GET all roles in the system.

Headers: Authorization Bearer [token]

Request Body:

[
  {
      "id": "8ff8905b-e969-49e2-6134-08dd81dcd4d9",
      "name": "Developer",
      "description": "Work with codes, and make debuging and tracing for it"
  },
  {
      "id": "cf810305-be5d-46c3-737c-08dd84d0eef1",
      "name": "Admin",
      "description": "It's Super Admin Test"
  }
]
👥 User Role Assignment
📁 Endpoints Summary
Action	Endpoint	Description
➕ Assign Role	POST /api/UserRoles	Assign a role to a user
📄 Get User Roles	GET /api/UserRoles	Get list of all user-role mappings
❌ Unassign Role	DELETE /api/UserRoles	Remove a role from a user
POST /api/UserRoles
Description: Assign a role to a user.

Headers: Authorization Bearer [token]

Request Body:

{
  "userId": "user123",
  "roleId": "role456",
  "assignedDate": "2025-04-29T12:00:00Z"
}
GET /api/UserRoles
Description: Get all data of the UserRoles tabl.

Headers: Authorization Bearer [token]

Request Body:

{
  "data": [
      {
          "userId": "6bc0f5d0-b34a-4304-59aa-08dd81b764ce",
          "roleId": "8ff8905b-e969-49e2-6134-08dd81dcd4d9",
          "assignedDate": "2020-06-08T00:00:00"
      },
      {
          "userId": "6bc0f5d0-b34a-4304-59aa-08dd81b764ce",
          "roleId": "cf810305-be5d-46c3-737c-08dd84d0eef1",
          "assignedDate": "2025-04-27T00:00:00"
      },
      {
          "userId": "4fdcb2c5-da53-4652-336b-08dd84d1cddd",
          "roleId": "cf810305-be5d-46c3-737c-08dd84d0eef1",
          "assignedDate": "2022-01-01T00:00:00"
      },
      {
          "userId": "0aed563d-90bc-45d4-07f0-08dd85695adb",
          "roleId": "cf810305-be5d-46c3-737c-08dd84d0eef1",
          "assignedDate": "2025-04-27T00:00:00"
      }
  ],
  "success": true,
  "message": "User roles retrieved successfully",
  "errors": null
}
DELETE /api/UserRoles
Description: Delete a specific role from a specific user.

Headers: Authorization Bearer [token]

Request Body:

{
  "userId": "user123",
  "roleId": "role456",
}
⚠️ Error Handling
All API endpoints follow a consistent error response format:

{
  "data": [], //Return List of the data if exist any data need to return.
  "message": "message", // Custom message
  "success": true // true if no error and false if there is an error,
  "errors": [] //List of All errors if exist or null if there is not error
}
Common HTTP status codes:

200: Success
201: Resource created
400: Bad request / Invalid input
401: Unauthorized / Invalid token
403: Forbidden / Insufficient permissions
404: Resource not found
500: Server error
🚀 Getting Started
📦Prerequisites
.NET 6 SDK or later
SQL Server or SQL Server Express
Visual Studio 2022 or VS Code
⚙️Setup Instructions
Clone the repository

git clone https://github.com/KholoudELkasaby/Bug-Ticketing.git
Update database connection string in appsettings.json

Run database migrations

dotnet ef database update
Run the application

dotnet run --project BugTrackingSystem.API
API will be available at https://localhost:5279 or http://localhost:5000

📄 License
This project is licensed under the MIT License - see the LICENSE file for details.
