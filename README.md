
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
   required by the specificationfile:///home/oai/share/NET%20Developer%20Test-1(6).pdf#:~:text=Functional%20Requirements%200,Features:

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
   Priority, IsCompleted and CreatedAt) as defined in the specificationfile:///home/oai/share/NET%20Developer%20Test-1(6).pdf#:~:text=Database%20Design%20Table%3A%20Tasks%20CREATE,DATETIME%20NOT%20NULL%20DEFAULT%20GETDATE.
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
  page and only authenticated users can access task pagesfile:///home/oai/share/NET%20Developer%20Test-1(6).pdf#:~:text=Functional%20Requirements%200,Features.

* **Task list page** – Displays all tasks in a table with columns for ID,
  title, due date, priority and completion statusfile:///home/oai/share/NET%20Developer%20Test-1(6).pdf#:~:text=1,%E2%97%8F%20Sort%20by%20due%20date.  The
  page supports searching by title via AJAXfile:///home/oai/share/NET%20Developer%20Test-1(6).pdf#:~:text=Features%3A%20%E2%97%8F%20Search%20by%20title,Filter%20by%20completed%20%2F%20pending, filtering
  tasks by completed or pending statusfile:///home/oai/share/NET%20Developer%20Test-1(6).pdf#:~:text=%E2%97%8F%20Filter%20by%20completed%20%2F,%E2%97%8F%20Sort%20by%20due%20date, sorting by due date
  ascending or descendingfile:///home/oai/share/NET%20Developer%20Test-1(6).pdf#:~:text=%E2%97%8F%20Sort%20by%20due%20date and toggling completion status without
  a full page reloadfile:///home/oai/share/NET%20Developer%20Test-1(6).pdf#:~:text=5,status%20without%20full%20page%20reload.

* **Add task** – A form for creating a new task with server‑side
  validation; the title is required (up to 100 characters) and you can
  optionally provide a description, due date, priority (1–3) and completion
  flagfile:///home/oai/share/NET%20Developer%20Test-1(6).pdf#:~:text=2,required%2C%20max%20100.  Client‑side validation is enabled via jQuery
  unobtrusive validation.

* **Edit task** – Allows updating all fields of an existing taskfile:///home/oai/share/NET%20Developer%20Test-1(6).pdf#:~:text=3,all%20fields.

* **Delete task** – Removes a task after a confirmation prompt
  implemented with jQueryfile:///home/oai/share/NET%20Developer%20Test-1(6).pdf#:~:text=4,confirmation%20popup%20using%20jQuery.

* **Repository & service layer** – A Dapper‑based repository encapsulates
  SQL queries, and a service layer enforces business rules and validation;
  this aligns with the architecture bonus criteria for repository and
  service layersfile:///home/oai/share/NET%20Developer%20Test-1(6).pdf#:~:text=Architecture%20Bonus%20%E2%97%8F%20Repository%20pattern,%E2%97%8F%20Async%20methods%20%E2%97%8F%20Logging.

* **Automatic database initialization** – At startup the initializer
  connects to the database server and creates the application database and
  the `Tasks` table if they do not exist; the table schema matches the
  specification with columns for Id, Title, Description, DueDate,
  Priority, IsCompleted and CreatedAtfile:///home/oai/share/NET%20Developer%20Test-1(6).pdf#:~:text=Database%20Design%20Table%3A%20Tasks%20CREATE,DATETIME%20NOT%20NULL%20DEFAULT%20GETDATE.

* **Logging & global error handling** – Integrated Serilog writes
  structured log files under `logs/log-<date>.txt` with daily rotation
  and retention of seven days.  A custom exception middleware logs
  unhandled exceptions and returns a JSON error response in a consistent
  format.