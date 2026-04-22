using TaskTracker.Models.Task;

namespace TaskTracker.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskItem>> GetAllAsync();
        Task<IEnumerable<TaskItem>> SearchAsync(string? term, bool? isCompleted = null, string? sortOrder = null);
        Task<TaskItem?> GetByIdAsync(int id);
        Task<bool> CreateAsync(TaskItem task);
        Task<bool> UpdateAsync(TaskItem task);
        Task<bool> DeleteAsync(int id);
        Task<bool> ToggleStatusAsync(int id);
    }
}
