# HRForce Project Setup & Run Guide

## 📂 Prerequisites
- **.NET 10 SDK** installed  
- **Visual Studio 2026** (or later) with ASP.NET and Blazor workloads  
- **SQL Server** installed  
- **Microsoft SQL Server Management Studio (SSMS) 22** for database management  

---

## ⚙️ Database Setup
1. Open **SQL Server Management Studio 22**.  
2. Connect to your SQL Server instance.  
3. Create a new database named **HRForce**:  
   ```sql
   CREATE DATABASE HRForce;
   ```
4. run Migration using `dotnet ef database update` command in terminal to create tables and seed data.
5. Verify the database has been created and contains the necessary tables (`Departments`, `Employees`, etc.) after running the backend for the first time.
6. Copy the sql script from DatabaseSeeder.sql in Scripts folder and execute it in SSMS to seed the database with initial data (10 Departments and 20 Employees).
---

## 🚀 Running the Backend (API Service)
1. Open the project folder in **Visual Studio 2026**.  
2. Set **HRForce.ApiService** as the startup project.  
3. Run the backend:  
   ```bash
   dotnet run --project HRForce.ApiService
   ```

---

## 🌐 Running the Frontend (Blazor Web)
1. Ensure the **backend is running** before starting the frontend.  
2. Navigate to the **Web project** folder:  
   ```bash
   cd HRForce.Web
   ```
3. Run the frontend with HTTPS profile:  
   ```bash
   dotnet run --launch-profile https
   ```
4. Visit the development URL shown in the console (e.g., `https://localhost:5001`).  
5. You’ll see the Blazor frontend page with menus and navigation.  

---

## 📝 Usage Notes
- **Backend must be running** before the frontend can fetch data.  
- The frontend uses `DepartmentApiClient` and `EmployeeApiClient` pointing to `https://localhost:7386/`.  
- The seeded data provides initial listings for testing (Departments + Employees).  
- You can extend seeding logic in `DbSeeder.cs` for more demo data.
- Adjust the API base URL in `Program.cs` if your backend runs on a different port.

---

## ✅ Quick Checklist
- [ ] Install .NET 10 SDK  
- [ ] Install Visual Studio 2026  
- [ ] Install SQL Server + SSMS 22  
- [ ] Create `HRForce` database in SSMS  
- [ ] Run backend (`HRForce.ApiService`) → migrations + seeder  
- [ ] Run frontend (`HRForce.Web`) → `dotnet run --launch-profile https`  
- [ ] Open development URL → explore menus and listings  

---