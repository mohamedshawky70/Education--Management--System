# Education Management System API

---

# Project Mind Map
![Image](https://github.com/user-attachments/assets/dc084464-29ae-433f-9855-3930cbfa38b0)
## Glimpse of the working solution
![Image](https://github.com/user-attachments/assets/07e1b3f9-5083-4816-9414-50cfac01124a)

![Image](https://github.com/user-attachments/assets/614a67a4-bd03-4cfb-b96a-26417c6ac2b1)

![Image](https://github.com/user-attachments/assets/353b566b-9231-41ab-90e2-8debe7af91bb)

![Image](https://github.com/user-attachments/assets/804afc04-4b70-4d00-af2d-74ae6b9ced33)

![Image](https://github.com/user-attachments/assets/04c20fd4-3c4a-4a94-8aec-b1e734c8dc33)

![Image](https://github.com/user-attachments/assets/cd68ef27-ca4d-42f0-a5a6-1fe0a57b9e98)

![Image](https://github.com/user-attachments/assets/3189144a-b7d8-44f8-83ab-0c67b2fc2e0e)

![Image](https://github.com/user-attachments/assets/d5325550-4b04-444b-a2e0-c90f6fd13504)

![Image](https://github.com/user-attachments/assets/d1aaf990-f5dd-4a76-8bb7-da587904eb28)

![Image](https://github.com/user-attachments/assets/85de0725-8049-4bb4-8565-45afe84754e2)

![Image](https://github.com/user-attachments/assets/3a0fb740-682a-45f5-bc1e-d4613ff222e5)

![Image](https://github.com/user-attachments/assets/634e4137-dee6-447c-9362-8a3d8ad84bb4)

![Image](https://github.com/user-attachments/assets/0b8fbc8c-ac28-4a36-8580-2a13c2230a01)

![Image](https://github.com/user-attachments/assets/17b3adc3-acf0-4b10-9e90-ea73ac5ba90c)

## 📌 Features
- **User Authentication**: Secure JWT-based login, registration, and password management.
- **Role-Based Access Control**: Manage user roles and permissions.
- **Multi-Entity Management**: CRUD operations for Students, Teachers, Departments, and Subjects.
- **Enrollment System**: Track student-subject enrollments and department-subject associations.
- **Versioned APIs**: Supports API versioning (e.g., Student endpoints).
- **User Status Control**: Activate/deactivate users or unlock accounts.

---

## 🔑 Authentication & User Management

### Auth Endpoints (`/Auth`)
| Method | Endpoint                     | Description                          |
|--------|------------------------------|--------------------------------------|
| POST   | `/Auth`                      | Login                                |
| POST   | `/Auth/refresh`              | Refresh JWT token                    |
| PUT    | `/Auth/revoke-refresh-token` | Revoke refresh token                 |
| POST   | `/Auth/register`             | Register new user                    |
| POST   | `/Auth/confirm-email`        | Confirm email address                |
| POST   | `/Auth/forget-password`      | Initiate password reset              |
| POST   | `/Auth/reset-password`       | Complete password reset              |

### Profile Management (`/me`)
| Method | Endpoint                   | Description                      |
|--------|----------------------------|----------------------------------|
| GET    | `/me`                      | Get current user profile         |
| PUT    | `/me`                      | Update profile                   |
| PUT    | `/me/change-password`      | Change password                  |

### Admin User Management (`/api/User`)
| Method | Endpoint                         | Description                      |
|--------|----------------------------------|----------------------------------|
| GET    | `/api/User`                     | List all users                   |
| POST   | `/api/User`                     | Create new user                  |
| PUT    | `/api/User/(id)`                | Update user by ID                |
| PUT    | `/api/User/toggle-status/(id)`  | Activate/Deactivate user         |
| PUT    | `/api/User/UnLock/(id)`         | Unlock user account              |

---

## 🛠️ Core Entities

### Students (`/api/v(v)/Student`)
| Method | Endpoint                     | Description                      |
|--------|------------------------------|----------------------------------|
| GET    | `/api/v(v)/Student`          | List all students               |
| POST   | `/api/v(v)/Student`          | Create student                  |
| GET    | `/api/v(v)/Student/(id)`     | Get student by ID               |
| PUT    | `/api/v(v)/Student/(id)`     | Update student                  |
| DELETE | `/api/v(v)/Student/(id)`     | Delete student                  |

### Teachers (`/api/Teacher`)
| Method | Endpoint                 | Description                      |
|--------|--------------------------|----------------------------------|
| GET    | `/api/Teacher`           | List all teachers               |
| POST   | `/api/Teacher`           | Create teacher                  |
| GET    | `/api/Teacher/(id)`      | Get teacher by ID               |
| PUT    | `/api/Teacher/(id)`      | Update teacher                  |
| DELETE | `/api/Teacher/(id)`      | Delete teacher                  |

### Assignment (`/api/Assignment`)
| Method | Endpoint                 | Description                      |
|--------|--------------------------|----------------------------------|
| GET    | `/api/Assignment`           | List all Assignment               |
| POST   | `/api/Assignment`           | Create Assignment                  |
| GET    | `/api/Assignment/(id)`      | Get Assignment by ID               |
| PUT    | `/api/Assignment/(id)`      | Update Assignment                  |
| DELETE | `/api/Assignment/(id)`      | Delete Assignment                  |

### Departments & Subjects
| Entity          | Endpoints                                                                 |
|-----------------|---------------------------------------------------------------------------|
| **Department**  | CRUD operations via `/api/Department`                                    |
| **Subject**     | CRUD operations via `/api/subject`                                       |
| **Enrollment**  | Manage student-subject relationships via `/api/Enrollment`               |
| **Department-Subject** | Link departments to subjects via `/api/DepartmentSubject`              |

---

## 🚀 Getting Started
1. Clone the repository
2. Configure database connection in `appsettings.json`
3. Run migrations: `dotnet ef database update`
4. Start the API: `dotnet run`

---

## Project Overview

**Objective:** 
A comprehensive backend API for managing educational institutions, including students, teachers, departments, subjects, Assignment and enrollments. Built with RESTful principles and JWT authentication.


## Tech Stack
-**Backend**: .NET 9 (Web API)

-**Database**: SQL Server 

-**Authentication**: Secure access to surveys and responses using JWT token and refresh token authentication 

-**ORM**: Entity Framework Core for database interactions

## Key Features
-**🔒 User and Role Management**: Leveraged JWT for secure authentication and authorization, allowing for seamless and secure access control.

-**📝 Audit Logging**: Implemented audit logging to track changes on resources, ensuring transparency and accountability in user actions.

-**🚨 Exception Handling**: Integrated centralized exception handling to manage errors gracefully, significantly enhancing the user experience.

-**⚠️ Error Handling with Result Pattern**: Employed a result pattern for structured error handling, providing clear and actionable feedback to users.

-**🚦CORS (Cross-Origin Resource Sharing)**: a security feature implemented by web browsers to prevent web pages from making requests to a different domain than the one that served the web page. 

-**🔄 Automapper/Mapster**: Utilized for efficient object mapping between models, improving data handling and reducing boilerplate code.

-**✅ Fluent Validation**: Ensured data integrity by effectively validating inputs, leading to user-friendly error messages.

-**🔑 Account Management**: Implemented features for user account management, including change password and reset password functionalities.

-**🚦 Rate Limiting**: Controlled the number of requests to prevent abuse, ensuring fair usage across all users.

-**🛠️ Background Jobs**: Used Hangfire for managing background tasks like sending confirmation emails and processing password resets seamlessly.

-**🗃️ Hybrid Caching**: Optimized performance with caching for frequently accessed data, significantly improving response times.

-**📧 Email Confirmation**: Managed user email confirmations, password changes, and resets seamlessly to enhance security.

-**📈 API Versioning**: Supported multiple versions of the API to ensure backward compatibility and smooth transitions as the project evolves.


## Development Focus

### 1. [Genaric Repository Pattern](#repository-pattern)
- **Description:** Implement the Repository Pattern to abstract data access logic, making the code more testable and maintainable. 
- **Functionality:**
  - **Genaric Repository Pattern:** Simplifies data access by providing a consistent API for CRUD operations.
  - **Unit of Work:** Manages transactions across multiple repositories, ensuring data integrity.


### 2. [Entity Framework Core](#entity-framework-core)
- **Description:** Handle database interactions using Entity Framework Core, allowing for seamless integration with the database. The use of code-first migrations ensures that the database schema is in sync with the application models.
- **Features:**
  - **Code-First Migrations:** Automatically generate database schema from your code.
  - **Entity Mapping:** Ensure proper mapping of domain entities to database tables.

### 3. [Auth Section](#auth-section)
- **Login:** Secure user authentication.
- **Reset Password:** Provide password recovery options.
- **Confirm Email** Sent Email virefication to user to avoid fake emails.
- **Edit Profile:** Enable users to update their personal information and settings.


### 4. [Data Seeding](#data-seeding)
- **Description:** Seed initial data for the admin role and users to ensure the system starts with essential data, improving ease of testing and initial use.
- **Seeded Data:**
- **Admin Role:** Pre-configured admin role with full access.
- **Sample Users:** Initial users with different roles for testing purposes.

## Links
- **[Project Repository](https://github.com/mohamedshawky70/School)**
