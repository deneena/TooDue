using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TooDue.Data;
using TooDue.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.Build.Evaluation;

namespace TooDue.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<TasksController> _logger;

        public TasksController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger<TasksController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Show(int projectId)
        {
            var userId = _userManager.GetUserId(User);
            _logger.LogInformation("User ID: {UserId}", userId);

            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogWarning("User ID is null or empty. Ensure the user is authenticated.");
                return Unauthorized();
            }

            var isAdmin = User.IsInRole("Admin");

            var tasks = isAdmin
                ? await (from t in _context.Tasks
                         join put in _context.ProjectUserTask on t.Task_id equals put.Task_id
                         where put.Project_id == projectId
                         select t).ToListAsync()
                : await (from t in _context.Tasks
                         join put in _context.ProjectUserTask on t.Task_id equals put.Task_id
                         where put.Project_id == projectId && put.User_id == userId
                         select t).ToListAsync();

            if (!tasks.Any())
            {
                ViewBag.Message = "No tasks found for this project.";
                ViewBag.Alert = "alert-info";
            }

            var project = await _context.Projects.FindAsync(projectId);
            if (project == null)
            {
                _logger.LogWarning("Project with ID {ProjectId} not found.", projectId);
                return NotFound();
            }

            ViewBag.Tasks = tasks;
            ViewBag.ProjectId = projectId;
            ViewBag.IsOrganizer = project.CreatedByUserId == userId;

            return View();
        }


        
        [Authorize(Roles = "User,Admin")]
        public IActionResult Create(int projectId)
        {
            ViewBag.ProjectId = projectId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Create(TooDue.Models.Task task, int projectId)
        {

            ModelState.Clear();
            TryValidateModel(task);

            if (ModelState.IsValid)
            {
                task.Task_create_date = DateTime.Now;
                _context.Add(task);
                await _context.SaveChangesAsync();

                var userId = _userManager.GetUserId(User);
                var projectUserTask = new Project_User_Task
                {
                    Project_id = projectId,
                    User_id = userId,
                    Task_id = task.Task_id
                };
                _context.ProjectUserTask.Add(projectUserTask);
                await _context.SaveChangesAsync();

                return RedirectToAction("Show", new { projectId = projectId });
            }
            ViewBag.ProjectId = projectId;
            return View(task);
        }

        //[HttpPost]
        //public IActionResult ToggleCompletionStatus(int id)
        //{
        //    var task = _context.Tasks.Find(id);
        //    if (task != null)
        //    {
        //        task.Task_completion = !task.Task_completion;
        //        if (task.Task_completion)
        //        {
        //            task.Task_complete_date = DateTime.Now;
        //        }

        //        var projectUserTask = _context.ProjectUserTask.FirstOrDefault(put => put.Task_id == id);
        //        if (projectUserTask != null)
        //        {
        //            _context.SaveChanges();
        //            return RedirectToAction("Show", new { projectId = projectUserTask.Project_id });
        //        }
        //    }
        //    return NotFound();
        //}

        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .FirstOrDefaultAsync(m => m.Task_id == id);
            if (task == null)
            {
                return NotFound();
            }

            var projectUserTask = await _context.ProjectUserTask
                .FirstOrDefaultAsync(put => put.Task_id == id);
            if (projectUserTask == null)
            {
                return NotFound();
            }

            ViewBag.ProjectId = projectUserTask.Project_id;
            return View(task);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            var projectUserTask = await _context.ProjectUserTask
                .FirstOrDefaultAsync(put => put.Task_id == id);
            if (projectUserTask == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            _context.ProjectUserTask.Remove(projectUserTask);
            await _context.SaveChangesAsync();
            return RedirectToAction("Show", new { projectId = projectUserTask.Project_id });
        }

        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            var projectUserTask = await _context.ProjectUserTask
                .FirstOrDefaultAsync(put => put.Task_id == id);
            if (projectUserTask == null)
            {
                return NotFound();
            }

            ViewBag.ProjectId = projectUserTask.Project_id;
            _logger.LogWarning("transmitted {ViewBag.ProjectId}", (object)ViewBag.ProjectId);
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Edit(int id, TooDue.Models.Task task, int projectId)
        {
            if (id != task.Task_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(task);
                await _context.SaveChangesAsync();
                return RedirectToAction("Show", new { projectId = projectId });
            }
            else
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        _logger.LogError("ModelState Error: {ErrorMessage}", error.ErrorMessage);
                    }
                }
            }
            ViewBag.ProjectId = projectId;
            return View(task);
        }




        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Assign(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            var projectUserTask = await _context.ProjectUserTask.FirstOrDefaultAsync(put => put.Task_id == id);
            if (projectUserTask == null)
            {
                return NotFound();
            }

            ViewBag.ProjectId = projectUserTask.Project_id;
            ViewBag.Users = new SelectList(_context.Users, "Id", "UserName");
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Assign(int id, string selectedUser)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            var projectUserTask = await _context.ProjectUserTask.FirstOrDefaultAsync(put => put.Task_id == id);
            if (projectUserTask == null)
            {
                return NotFound();
            }

            var projectId = projectUserTask.Project_id;

            if (ModelState.IsValid)
            {
                //var existingEntries = _context.ProjectUserTask.Where(put => put.Task_id == id);
                //_context.ProjectUserTask.RemoveRange(existingEntries);

                var newProjectUserTask = new Project_User_Task
                {
                    Project_id = projectId,
                    User_id = selectedUser,
                    Task_id = task.Task_id
                };
                _context.ProjectUserTask.Add(newProjectUserTask);

                await _context.SaveChangesAsync();
                return RedirectToAction("Show", new { projectId = projectId });
            }

            ViewBag.ProjectId = projectId;
            ViewBag.Users = new SelectList(_context.Users, "Id", "UserName", selectedUser);
            return View(task);
        }


        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Mark(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            var projectUserTask = await _context.ProjectUserTask.FirstOrDefaultAsync(put => put.Task_id == id);
            if (projectUserTask == null)
            {
                return NotFound();
            }

            ViewBag.ProjectId = projectUserTask.Project_id;
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Mark(int id, TooDue.Models.Task task)
        {
            if (id != task.Task_id)
            {
                _logger.LogWarning("Task ID mismatch: {Id} != {TaskId}", id, task.Task_id);
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                    var existingTask = await _context.Tasks.FindAsync(id);
                    if (existingTask == null)
                    {
                        _logger.LogWarning("Task with ID {Id} not found.", id);
                        return NotFound();
                    }

                    existingTask.Task_completion = task.Task_completion;
                    if (task.Task_completion == "Completed")
                    {
                        existingTask.Task_complete_date = DateTime.Now;
                    }

                    _context.Update(existingTask);
                    await _context.SaveChangesAsync();

                    var project_User_Task = await _context.ProjectUserTask.FirstOrDefaultAsync(put => put.Task_id == id);
                    if (project_User_Task == null)
                    {
                        _logger.LogWarning("ProjectUserTask with Task ID {Id} not found.", id);
                        return NotFound();
                    }

                    return RedirectToAction("Show", new { projectId = project_User_Task.Project_id });
            }
            else
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        _logger.LogError("ModelState Error: {ErrorMessage}", error.ErrorMessage);
                    }
                }
            }

            var projectUserTask = await _context.ProjectUserTask.FirstOrDefaultAsync(put => put.Task_id == id);
            if (projectUserTask != null)
            {
                ViewBag.ProjectId = projectUserTask.Project_id;
            }
            else
            {
                _logger.LogWarning("ProjectUserTask with Task ID {Id} not found.", id);
            }
            return View(task);
        }





    }
}
