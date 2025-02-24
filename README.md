# AuthWithIdentity

## Introduction

**AuthWithIdentity** is an authentication system built using **ASP.NET Core Identity**, supporting **JWT Authentication** and **Role-Based Authorization**. The project includes user registration, login, email confirmation, password reset, and role management.

---

## Technologies Used

- **ASP.NET Core 6+**
- **Entity Framework Core**
- **Identity Framework**
- **JWT Authentication**
- **Microsoft SQL Server**


---

## Key Features

✅ **User Registration**
✅ **JWT-based Login**
✅ **Email Confirmation**
✅ **Password Reset**
✅ **Role & Permission Management**
✅ **JWT Token Generation & Refreshing**
✅ **Refresh Tokens Support**

---

## Running the Project Locally

### 1. Prerequisites:

Before running the project, ensure you have the following installed:
- **.NET SDK** (Version 6.0 or higher)
- **SQL Server**
- **Visual Studio** (or any compatible .NET Core IDE)

### 2. Database Setup

1. Open `appsettings.json` and configure the **database connection**.
2. Run the following commands to apply migrations and create the database:

```sh
# Create the database
dotnet ef database update
```

### 3. Run the Application

1. Open the project in **Visual Studio**.
2. Set the project as the startup project.
3. Run the project using the following command:

```sh
dotnet run
```

---

## API Documentation & Usage

Automatic API documentation is provided using **Swagger**.

### 📌 Access Swagger UI:

Once the project is running, visit the following URL to explore the API:
```
http://localhost:<port>/swagger/index.html
```

---

## Key API Endpoints

### 🔐 Authentication & Registration:

- **POST** `/api/auth/register` ➝ Register a new user
- **POST** `/api/auth/login` ➝ Login and receive a JWT token
- **GET** `/api/auth/confirmemail` ➝ Confirm email using verification code
- **POST** `/api/auth/forgot-password` ➝ Request password reset via email
- **POST** `/api/auth/reset-password` ➝ Reset password

### 🎭 Role & Permission Management:

- **POST** `/api/auth/create-role` ➝ Create a new role
- **POST** `/api/auth/add-user-to-role` ➝ Assign a user to a role
- **POST** `/api/auth/remove-user-from-role` ➝ Remove a user from a role

---

## Contributors

This project was developed by **[Mohammed Baasheem]** 🚀

---

## Additional Notes

- The project can be enhanced by adding **Google & Facebook Authentication**.
- Support for **Two-Factor Authentication (2FA)** can be added as a future enhancement.

😊 **Thanks for using this project! Feel free to contribute or open issues for any questions.**

