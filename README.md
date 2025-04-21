
# 🏥 Hospital Management System (HMS) - Web API  
>#### **Status: In Progress 🚧**

## 📌 Project Overview

The **Hospital Management System (HMS)** is a RESTful Web API built with **.NET 8** and **Entity Framework Core**, designed to manage hospital operations such as:

- Patient and doctor management  
- Appointment scheduling  
- Prescription and medication tracking  
- Automatic billing  
- Role-based access with secure authentication  

This project started **a Web API**, following a clean architecture pattern with **Dependency Injection**, **JWT Authentication**, and **Swagger** for documentation.

---

## 🚀 Features Overview 

### ✅ Core Modules & Endpoints

| Module | Description |
|--------|-------------|
| **Authentication & Authorization** | JWT login & registration with roles: Admin, Doctor, Patient |
| **Patients** | CRUD operations for managing patient records |
| **Doctors** | CRUD operations + specialty management |
| **Appointments** | Schedule, view, and cancel appointments |
| **Prescriptions** | Issue and manage prescriptions (linked to appointments & medications) |
| **Medications** | Manage inventory and prices of medications |
| **Billing** | Auto-generate bills when prescriptions are issued |

---

## 🛡️ Authentication & Authorization

- **JWT Authentication** implemented with secure token generation.
- **Role-based access control** for:
  - `Admin`: Full access to all modules
  - `Doctor`: Manage own patients and prescriptions
  - `Patient`: View personal data, appointments, and bills

---

## 📂 API Endpoints Summary

### 🔐 Auth
- `POST /api/auth/register` – Register new users  
- `POST /api/auth/login` – Login and receive JWT  

### 👤 Patient Management
- `POST /api/patients`
- `GET /api/patients`
- `GET /api/patients/{id}`
- `PUT /api/patients/{id}`
- `DELETE /api/patients/{id}`

### 👨‍⚕️ Doctor Management
- `POST /api/doctors`
- `GET /api/doctors`
- `GET /api/doctors/{id}`
- `PUT /api/doctors/{id}`
- `DELETE /api/doctors/{id}`

### 📅 Appointment Management
- `POST /api/appointments`
- `GET /api/appointments?patientId={id}` or `?doctorId={id}`
- `DELETE /api/appointments/{id}`

### 💊 Prescription Management
- `POST /api/prescriptions`
- `GET /api/prescriptions`
- `GET /api/prescriptions/{id}`
- `PUT /api/prescriptions/{id}`

### 🧪 Medication Management
- `POST /api/medications`
- `GET /api/medications`
- `PUT /api/medications/{id}`
- `DELETE /api/medications/{id}`

### 💵 Billing Management
- `GET /api/bills`
- `GET /api/bills?patientId={id}`
- `PUT /api/bills/{id}`

---

## ⚙️ Technologies Used

- ASP.NET Core 8 Web API  
- Entity Framework Core (Code-First, SQL Server)  
- JWT Authentication  
- Role-based Authorization  
- Swagger / Swashbuckle  
- Dependency Injection  

---

## 📌 Status & To-Do

- [x] Set up API structure  
- [x] Patient & Doctor CRUD  
- [x] Appointment module  
- [ ] JWT authentication  
- [ ] Billing logic automation  
- [ ] Admin dashboard features  

---

## 📬 Contributions

This project is under active development. Contributions, suggestions, or feedback are welcome!
