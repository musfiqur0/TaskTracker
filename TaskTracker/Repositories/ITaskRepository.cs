using TaskTracker.Models.Task;

namespace TaskTracker.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskItem>> GetAllAsync();
        Task<IEnumerable<TaskItem>> SearchAsync(string? term, bool? isCompleted = null, string? sortOrder = null);
        Task<TaskItem?> GetByIdAsync(int id);
        Task<int> CreateAsync(TaskItem task);
        Task<int> UpdateAsync(TaskItem task);
        Task<int> DeleteAsync(int id);
        Task<int> ToggleStatusAsync(int id);
    }
}
