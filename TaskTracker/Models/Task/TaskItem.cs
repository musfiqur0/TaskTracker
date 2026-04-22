using System.ComponentModel.DataAnnotations;

namespace TaskTracker.Models.Task
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Display(Name = "Due Date")]
        public DateTime? DueDate { get; set; }

        [Range(1, 3, ErrorMessage = "Priority must be between 1 and 3.")]
        public int Priority { get; set; } = 1;

        [Display(Name = "Is Completed")]
        public bool IsCompleted { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
