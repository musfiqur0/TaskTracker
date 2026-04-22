using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using TaskTracker.Models.Task;

namespace TaskTracker.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IConfiguration _configuration;

        public TaskRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            using var connection = CreateConnection();

            var sql = @"
                SELECT Id, Title, Description, DueDate, Priority, IsCompleted, CreatedAt
                FROM Tasks
                ORDER BY Id DESC";

            return await connection.QueryAsync<TaskItem>(sql);
        }

        public async Task<IEnumerable<TaskItem>> SearchAsync(string? term, bool? isCompleted = null, string? sortOrder = null)
        {
            using var connection = CreateConnection();

            var sql = @"
                SELECT Id, Title, Description, DueDate, Priority, IsCompleted, CreatedAt
                FROM Tasks
                WHERE (@Term IS NULL OR Title LIKE '%' + @Term + '%')
                  AND (@IsCompleted IS NULL OR IsCompleted = @IsCompleted)
            ";

            sql += sortOrder == "due_desc"
                ? " ORDER BY DueDate DESC, Id DESC"
                : sortOrder == "due_asc"
                    ? " ORDER BY DueDate ASC, Id DESC"
                    : " ORDER BY Id DESC";

            return await connection.QueryAsync<TaskItem>(sql, new
            {
                Term = string.IsNullOrWhiteSpace(term) ? null : term,
                IsCompleted = isCompleted
            });
        }

        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            using var connection = CreateConnection();

            var sql = @"
                SELECT Id, Title, Description, DueDate, Priority, IsCompleted, CreatedAt
                FROM Tasks
                WHERE Id = @Id";

            return await connection.QueryFirstOrDefaultAsync<TaskItem>(sql, new { Id = id });
        }

        public async Task<int> CreateAsync(TaskItem task)
        {
            using var connection = CreateConnection();

            var sql = @"
                INSERT INTO Tasks (Title, Description, DueDate, Priority, IsCompleted)
                VALUES (@Title, @Description, @DueDate, @Priority, @IsCompleted)";

            return await connection.ExecuteAsync(sql, task);
        }

        public async Task<int> UpdateAsync(TaskItem task)
        {
            using var connection = CreateConnection();

            var sql = @"
                UPDATE Tasks
                SET Title = @Title,
                    Description = @Description,
                    DueDate = @DueDate,
                    Priority = @Priority,
                    IsCompleted = @IsCompleted
                WHERE Id = @Id";

            return await connection.ExecuteAsync(sql, task);
        }

        public async Task<int> DeleteAsync(int id)
        {
            using var connection = CreateConnection();

            var sql = "DELETE FROM Tasks WHERE Id = @Id";
            return await connection.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<int> ToggleStatusAsync(int id)
        {
            using var connection = CreateConnection();

            var sql = @"
                UPDATE Tasks
                SET IsCompleted = CASE WHEN IsCompleted = 1 THEN 0 ELSE 1 END
                WHERE Id = @Id";

            return await connection.ExecuteAsync(sql, new { Id = id });
        }
    }
}
