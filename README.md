
# Task Tracker

**Task Tracker** is a concise ASP.NET Core MVC sample application built with
.NET 10, Dapper and ASP.NET Core Identity.  Authenticated users can manage
tasks—add, edit, delete, search, filter and mark them complete.  Identity
uses Entity Framework Core and SQL Server, while task CRUD operations
use Dapper behind a repository and service layer.  Logging is handled
via Serilog and unhandled errors are captured by a simple middleware.

## Tech Stack

* .NET 10 / ASP.NET Core MVC
* ASP.NET Core Identity + EF Core
* Dapper (for tasks)
* SQL Server
* Razor Views + jQuery/AJAX
* Serilog file logging

## Quick Start

1. **Clone or extract** the repository and open the `TaskTracker` folder.
2. **Configure the database** – edit `appsettings.json` and set
   `ConnectionStrings:DefaultConnection` to your SQL Server.
3. **Restore and build**:
   ```bash
   dotnet restore
   dotnet build
   ```
4. **Apply migrations** (Identity schema) – run either of these commands to
   apply the Entity Framework Core migrations that create the
   ASP.NET Core Identity tables as required by the test specificationfile:///home/oai/share/NET%20Developer%20Test-1(6).pdf#:~:text=0,Features:

   ```bash
   dotnet ef database update
   ```

   or in the Visual Studio Package Manager Console:

   ```powershell
   Update-Database
   ```

   This step updates the database to include the Identity schema.  The
   Dapper‐based `Tasks` table is created automatically at runtime by the
   initializer.
5. **Run the app**:
   ```bash
   dotnet run
   ```
   On first run, the initializer will create the database and the `Tasks`
   table if they do not exist.

Navigate to `/` in your browser to register or log in and begin
managing tasks.

## Main Features

* **Authentication** – register, login, logout; only logged‑in users can
  access task pages.
* **Task management** – create, edit and delete tasks with server‑side
  validation (title required up to 100 characters, priority 1–3).
* **Task list** – list of tasks with search by title, filter by
  completion status and sort by due date; completion toggling happens via
  AJAX.
* **Logging & error handling** – Serilog writes daily log files to
  `logs/`; a simple exception middleware logs unhandled errors and
  returns a JSON error response.