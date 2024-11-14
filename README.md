Here’s a README file in English for the **Hospital Management System** project.

---

# **Hospital Management System**

This project is a simulation of a hospital management system. The system includes features such as doctor assignments to departments, patient registration, user management, and authentication. It is built using a REST API architecture and incorporates modern software development practices like security, data validation, and error management.

## **Table of Contents**
1. [About the Project](#about-the-project)
2. [Features](#features)
3. [Installation](#installation)
4. [Usage](#usage)
5. [API Endpoints](#api-endpoints)
6. [Contributors](#contributors)
7. [License](#license)

## **About the Project**
This project is a hospital management system developed using Entity Framework Core with a Code-First approach and a multi-layered architecture. The project allows the management of doctor, patient, and department data, with features like user authentication, data encryption, error handling, and data validation for security and reliability.

## **Features**
- **Layered Architecture:** Structured into Presentation, Business, and Data Access layers.
- **CRUD Operations:** Full CRUD support for entities (Doctor, Patient, Department).
- **Authentication & Authorization:** JWT-based security for protecting endpoints.
- **Global Exception Handling:** A centralized error management system.
- **Model Validation:** Input validation for incoming data to ensure data integrity.
- **Data Protection:** Sensitive data encryption using the Data Protection API.

## **Installation**
To get started with the project, follow these steps:

### **Prerequisites**
Make sure you have the following installed:
- [.NET Core SDK](https://dotnet.microsoft.com/download) (version 6.0 or higher)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or any database that supports Entity Framework)
- [Postman](https://www.postman.com/) (optional, for testing API endpoints)

### **Steps to Install**
1. Clone the repository:
    ```bash
    git clone https://github.com/your-username/HospitalManagementSystem.git
    ```

2. Navigate to the project folder:
    ```bash
    cd HospitalManagementSystem
    ```

3. Restore the project dependencies:
    ```bash
    dotnet restore
    ```

4. Apply migrations to the database:
    ```bash
    dotnet ef database update
    ```

5. Run the application:
    ```bash
    dotnet run
    ```

The application should now be running locally. You can test it by accessing the API endpoints.

## **Usage**
Once the project is running, you can interact with it through the exposed REST API endpoints. The system allows you to:
- **Create, read, update, and delete doctors, patients, and departments.**
- **Authenticate users** using JWT and secure the API routes.
- **Handle errors** gracefully using global exception handling.

### **Testing API Endpoints**
You can test the API endpoints using tools like Postman or cURL. Here are some of the available API routes:

## **API Endpoints**
- **POST /api/auth/register** – Register a new user.
- **POST /api/auth/login** – Authenticate a user and receive a JWT token.
- **GET /api/doctors** – Get a list of all doctors.
- **POST /api/doctors** – Create a new doctor.
- **PUT /api/doctors/{id}** – Update doctor details.
- **DELETE /api/doctors/{id}** – Delete a doctor.
- **GET /api/patients** – Get a list of all patients.
- **POST /api/patients** – Register a new patient.





