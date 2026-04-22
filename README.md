
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
4. **Apply migrations** – run either of these commands to apply the
   Entity Framework Core migrations contained in the `Data/Migrations`
   folder.  These migrations create the ASP.NET Core Identity tables
   required by the specification:

   ```bash
   dotnet ef database update
   ```

   or in the Visual Studio Package Manager Console:

   ```powershell
   Update-Database
   ```

   These commands update the database to include the Identity schema.  When
   you run the application for the first time the `DbInitializer` will
   connect to the master database and create the database specified in your
   connection string if it does not already exist.  It subsequently ensures
   a `Tasks` table exists with columns (Id, Title, Description, DueDate,
   Priority, IsCompleted and CreatedAt) as defined in the specification.
5. **Run the app**:
   ```bash
   dotnet run
   ```
   On first run, the initializer will create the database and the `Tasks`
   table if they do not exist.

Navigate to `/` in your browser to register or log in and begin
managing tasks.

## Features

* **User authentication** – Registration, login and logout are handled via
  ASP.NET Core Identity; unauthorized users are redirected to the login
  page and only authenticated users can access task pages.

* **Task list page** – Shows tasks in a table with columns for ID, title,
  due date, priority and completion status.  Offers search by title via
  AJAX, filters for completed or pending tasks, sorting by due date and
  toggling completion without reloading.

* **Add task** – Use a form to create a new task; the title is required
  (up to 100 characters) with optional description, due date, priority (1–3) and
  completion flag.  Client‑side validation via jQuery unobtrusive validation
  complements server‑side checks.

* **Edit task** – Allows updating all fields of an existing task.

* **Delete task** – Removes a task after a confirmation prompt
  implemented with jQuery.

* **Repository & service layer** – A Dapper‑based repository encapsulates
  SQL queries, and a service layer enforces business rules and validation;
  this aligns with the architecture bonus criteria for repository and
  service layers.

* **Automatic database initialization** – At startup the initializer
  connects to the database server and creates the application database and
  the `Tasks` table if they do not exist; the table schema matches the
  specification with columns for Id, Title, Description, DueDate,
  Priority, IsCompleted and CreatedAt.

* **Logging & global error handling** – Integrated Serilog writes
  structured log files under `logs/log-<date>.txt` with daily rotation
  and retention of seven days.  A custom exception middleware logs
  unhandled exceptions and returns a JSON error response in a consistent
  format.