using TaskTracker.Models.Task;
using TaskTracker.Repositories;

namespace TaskTracker.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            return await _taskRepository.GetAllAsync();
        }

        public async Task<IEnumerable<TaskItem>> SearchAsync(string? term, bool? isCompleted = null, string? sortOrder = null)
        {
            return await _taskRepository.SearchAsync(term, isCompleted, sortOrder);
        }

        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            return await _taskRepository.GetByIdAsync(id);
        }

        public async Task<bool> CreateAsync(TaskItem task)
        {
            if (task == null)
                return false;

            task.Title = task.Title?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(task.Title))
                return false;

            if (task.Title.Length > 100)
                return false;

            if (task.Priority < 1 || task.Priority > 3)
                return false;

            var affectedRows = await _taskRepository.CreateAsync(task);
            return affectedRows > 0;
        }

        public async Task<bool> UpdateAsync(TaskItem task)
        {
            if (task == null || task.Id <= 0)
                return false;

            task.Title = task.Title?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(task.Title))
                return false;

            if (task.Title.Length > 100)
                return false;

            if (task.Priority < 1 || task.Priority > 3)
                return false;

            var existingTask = await _taskRepository.GetByIdAsync(task.Id);
            if (existingTask == null)
                return false;

            var affectedRows = await _taskRepository.UpdateAsync(task);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
                return false;

            var affectedRows = await _taskRepository.DeleteAsync(id);
            return affectedRows > 0;
        }

        public async Task<bool> ToggleStatusAsync(int id)
        {
            if (id <= 0)
                return false;

            var affectedRows = await _taskRepository.ToggleStatusAsync(id);
            return affectedRows > 0;
        }
    }
}
