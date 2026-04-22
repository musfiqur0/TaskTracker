using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Models.Task;
using TaskTracker.Services;

namespace TaskTracker.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        // GET /Task
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var tasks = await _taskService.GetAllAsync();
            return View(tasks);
        }

        // GET /Task/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST /Task/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaskItem task)
        {
            if (!ModelState.IsValid)
                return View(task);

            var success = await _taskService.CreateAsync(task);

            if (!success)
            {
                ModelState.AddModelError(string.Empty, "Failed to create task.");
                return View(task);
            }

            TempData["Success"] = "Task created successfully.";
            return RedirectToAction(nameof(Index));
        }

        // GET /Task/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var task = await _taskService.GetByIdAsync(id);

            if (task == null)
                return NotFound();

            return View(task);
        }

        // POST /Task/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TaskItem task)
        {
            if (!ModelState.IsValid)
                return View(task);

            var success = await _taskService.UpdateAsync(task);

            if (!success)
            {
                ModelState.AddModelError(string.Empty, "Failed to update task.");
                return View(task);
            }

            TempData["Success"] = "Task updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        // POST /Task/Delete/{id}
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _taskService.DeleteAsync(id);

            if (!success)
            {
                return Json(new
                {
                    success = false,
                    message = "Task not found or delete failed."
                });
            }

            return Json(new
            {
                success = true,
                message = "Task deleted successfully."
            });
        }

        // POST /Task/ToggleStatus/{id}
        [HttpPost]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var success = await _taskService.ToggleStatusAsync(id);

            if (!success)
            {
                return Json(new
                {
                    success = false,
                    message = "Task not found or status update failed."
                });
            }

            return Json(new
            {
                success = true,
                message = "Task status updated successfully."
            });
        }

        // GET /Task/Search?term=test
        [HttpGet]
        public async Task<IActionResult> Search(string? term, bool? isCompleted, string? sortOrder)
        {
            var tasks = await _taskService.SearchAsync(term, isCompleted, sortOrder);
            return PartialView("_TaskTablePartial", tasks);
        }
    }
}
