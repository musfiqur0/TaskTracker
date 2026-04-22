using Microsoft.Data.SqlClient;

namespace TaskTracker.Data.Data
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var builder = new SqlConnectionStringBuilder(connectionString);
            var databaseName = builder.InitialCatalog;

            // Step 1: connect to master
            var masterBuilder = new SqlConnectionStringBuilder(connectionString)
            {
                InitialCatalog = "master"
            };

            using (var masterConnection = new SqlConnection(masterBuilder.ConnectionString))
            {
                await masterConnection.OpenAsync();

                var createDbSql = $@"
IF DB_ID('{databaseName}') IS NULL
BEGIN
    CREATE DATABASE [{databaseName}]
END";

                using var createDbCommand = new SqlCommand(createDbSql, masterConnection);
                await createDbCommand.ExecuteNonQueryAsync();
            }

            // Step 2: connect to actual DB
            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            var createTableSql = @"
IF NOT EXISTS (
    SELECT * FROM sys.objects 
    WHERE object_id = OBJECT_ID(N'[dbo].[Tasks]') AND type in (N'U')
)
BEGIN
    CREATE TABLE Tasks (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Title NVARCHAR(100) NOT NULL,
        Description NVARCHAR(MAX) NULL,
        DueDate DATETIME NULL,
        Priority INT NOT NULL,
        IsCompleted BIT NOT NULL DEFAULT 0,
        CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
    );
END";

            using var command = new SqlCommand(createTableSql, connection);
            await command.ExecuteNonQueryAsync();
        }
    }
}
