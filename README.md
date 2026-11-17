# SalesWebMvc

A sales management web app built with **ASP.NET Core MVC**. It lets you manage departments and sellers and search sales by period, either as a simple list or grouped by department.

It started as a project from a 2018 C# course and has been updated to .NET 10, modern C# and a new UI.

## Features

- **Dashboard** with total billed, counts and latest sales
- **CRUD** for departments and sellers, with validation
- **Sales search** by period: simple or grouped by department
- **Light/dark** theme and responsive layout (the sidebar becomes a menu on mobile)

## Tech stack

- .NET 10 / ASP.NET Core MVC
- Entity Framework Core 10
- SQL Server Express (database)
- Bootstrap 5.3, Bootstrap Icons, Inter font

## Getting started

**Prerequisites:** [.NET 10 SDK](https://dotnet.microsoft.com/download) and [SQL Server Express](https://www.microsoft.com/sql-server/sql-server-downloads), the database used in development (the default connection string points to the `.\SQLEXPRESS` instance with Windows authentication).

1. Adjust the `SalesWebMvcContext` connection string in `SalesWebMvc/appsettings.json` if needed.
2. Create the database by applying the migrations:
   ```bash
   dotnet tool install --global dotnet-ef
   ```
   ```bash
   dotnet ef database update --project SalesWebMvc
   ```
3. Run the app:
   ```bash
   dotnet run --project SalesWebMvc
   ```
4. Open `http://localhost:5274`.

On first run, `SeedingService` fills the database with sample data (departments, sellers and sales from 2018). To see them in the searches, filter by that year.

## Project structure

```
SalesWebMvc/
├── Controllers/   # Home (dashboard), Departments, Sellers, SalesRecords
├── Data/          # DbContext and seeding
├── Migrations/    # EF Core migrations
├── Models/        # Entities, enums and ViewModels
├── Services/      # Data access logic and service exceptions
├── Views/         # Razor views and partials
└── wwwroot/       # CSS, JS and front-end libraries
```
