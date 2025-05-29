

# 🏥 Hospital Management System - HMS (Clean Architecture - ASP.NET Core Web API)
>#### **Status: In Progress 🚧**

[![Ask DeepWiki](https://deepwiki.com/badge.svg)](https://deepwiki.com/AzzaEid/HMS)

## 📑 About the Project

This project is a **Medical Management System API** designed using **Clean Architecture Principles** and built with **ASP.NET Core Web API**.  
It simulates a real-world clinic/hospital environment by managing **Patients**, **Doctors**, **Appointments**, **Prescriptions**, **Medications**, and **Billing**.  
It demonstrates advanced backend engineering techniques covering **authentication, authorization, pagination, ordering, localization, validation, error handling, logging**, and more.

---

## 🎯 Main Features

- Full **CRUD Operations** for all entities
- **Authentication & Authorization** with JWT, Roles, and Claims
- **Role-based Access Control** (Admin, Doctor, Patient)
- **Automated Email Handling** (confirmation, password reset)
- **Secure Passwords & Refresh Tokens**
- **Logging** using Serilog (Database + Console)
- **Localization** (multi-language support)
- **Pagination**, **Ordering**, **Filtering**
- **Exception Handling** with Middlewares
- **Entity Relationships** fully implemented

---

## 🏗️ Project Architecture

This project follows a **Clean Architecture** pattern, separating concerns into:

- **Domain Layer**: Entities and Core Logic
- **Application Layer**: Use Cases, DTOs, Interfaces
- **Infrastructure Layer**: Database Access, External Services
- **Presentation Layer**: Web API Controllers

With a strong focus on:

- Dependency Injection (DI)
- Mediator Pattern
- Repository & Unit of Work Pattern
- Fluent Validation
- AutoMapper
- Custom Response Wrappers
- API Versioning & Routing Customization

---

## 🧱 Entities and Their Relationships

| Entity | Relationships |
|:---|:---|
| **Patient** | Has many Appointments, Prescriptions, and Bills |
| **Doctor** | Has many Appointments and Prescriptions |
| **Department** | Has many Doctors, Managed by one Doctor (ManagerDoctor) |
| **Appointment** | Belongs to a Patient and a Doctor |
| **Prescription** | Belongs to a Patient and a Doctor, Contains many Medications |
| **Medication** | Can belong to many Prescriptions |
| **Bill** | Belongs to a Prescription |

---

## 🔥 Authentication & Authorization

- **Register** (with roles Admin, Doctor, Patient): `POST /api/auth/register`
- **Login** (JWT + Refresh Token): `POST /api/auth/login`
- **Role and Claim Based Authorization**
- **Password Reset** and **Email Confirmation** with encryption.

---

## 📋 API Endpoints Overview

### Patients

- `POST /api/patients`
- `GET /api/patients`
- `GET /api/patients/{id}`
- `PUT /api/patients/{id}`
- `DELETE /api/patients/{id}`

### Doctors

- `POST /api/doctors`
- `GET /api/doctors`
- `GET /api/doctors/{id}`
- `PUT /api/doctors/{id}`
- `DELETE /api/doctors/{id}`

### Departments

- `POST /api/departments`
- `GET /api/departments`
- `GET /api/departments/{id}`
- `PUT /api/departments/{id}`
- `DELETE /api/departments/{id}`

### Appointments

- `POST /api/appointments`
- `GET /api/appointments?patientId={id}`
- `GET /api/appointments?doctorId={id}`
- `DELETE /api/appointments/{id}`

### Prescriptions

- `POST /api/prescriptions`
- `GET /api/prescriptions`
- `GET /api/prescriptions/{id}`
- `PUT /api/prescriptions/{id}`

### Medications

- `POST /api/medications`
- `GET /api/medications`
- `PUT /api/medications/{id}`
- `DELETE /api/medications/{id}`

### Billing

- `GET /api/bills`
- `GET /api/bills?patientId={id}`
- `PUT /api/bills/{id}`

---

## ⚙️ Technologies and Topics Covered

You will cover and implement:

- **ASP.NET Core Web API 8**
- **Entity Framework Core** (Code-First, Migrations, Fluent API)
- **SQL Server**
- **Clean Architecture Principles**
- **AutoMapper**
- **Fluent Validation**
- **Repository Pattern** & **Generic Repository**
- **Custom Response Wrappers**
- **Global Exception Handling Middleware**
- **JWT Authentication & Authorization**
- **Role & Claim-based Authorization**
- **Refresh Tokens**
- **Localization (Multi-language Support)**
- **Email Sending with MailKit & SMTP**
- **Password Reset via Email**
- **Logging with Serilog**
- **Git & GitHub** (Project versioning and push)
- **Working with Stored Procedures, Views, Functions**
- **Image Uploads and Management**

---

## 🛠️ Setup Instructions

1. Clone the repository
2. Setup your **SQL Server Database** and configure connection string
3. Run the migrations
4. Run the API
5. Use Swagger or Postman to test the endpoints

---

## 🚀 Final Words

This project is designed not just to **practice**, but to simulate **real-world backend development** with a professional architecture that is scalable, testable, and maintainable.  
By completing this system, you'll master a huge set of backend engineering concepts and build a **very strong portfolio piece**! 💪✨
