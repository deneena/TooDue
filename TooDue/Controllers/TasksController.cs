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


        private List<ApplicationUser> GetUsersInProject(int projectId)
        {
            var usersInProject = _context.ProjectUserRoles
                .Where(pur => pur.Related_project_id == projectId)
                .Select(pur => pur.Related_user_id)
                .ToList();

            return _userManager.Users
                .Where(u => usersInProject.Contains(u.Id))
                .ToList();
        }

        private List<ApplicationUser> GetUsersWithTask(int projectId, int taskId)
        {
            var usersWithTask = _context.ProjectUserTask
                .Where(put => put.Project_id == projectId && put.Task_id == taskId)
                .Select(put => put.User_id)
                .ToList();

            return _userManager.Users
                .Where(u => usersWithTask.Contains(u.Id))
                .ToList();
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
            var project = await _context.Projects.FindAsync(projectId);
            if (project == null)
            {
                _logger.LogWarning("Project with ID {ProjectId} not found.", projectId);
                return NotFound();
            }
            var isAdmin = User.IsInRole("Admin");

            var isOrganizer = project.CreatedByUserId == userId;

            _logger.LogInformation("Fetching tasks for project ID: {ProjectId}", projectId);

            var tasks = (isAdmin || isOrganizer)
                ? await _context.Tasks
                    .Where(t => t.Project_Id == projectId)
                    .ToListAsync()
                : await (from t in _context.Tasks
                         join put in _context.ProjectUserTask on t.Task_id equals put.Task_id
                         where put.Project_id == projectId && put.User_id == userId
                         select t).ToListAsync();


            _logger.LogInformation("Tasks fetched: {TaskCount}", tasks.Count);

            if (!tasks.Any())
            {
                _logger.LogInformation("No tasks found for project ID: {ProjectId}", projectId);
                ViewBag.Message = "No tasks found for this project.";
                ViewBag.Alert = "alert-info";
            }


            ViewBag.Tasks = tasks;
            ViewBag.ProjectId = projectId;
            ViewBag.IsOrganizer = project.CreatedByUserId == userId;
            ViewBag.ProjectName = project.Project_name;

            return View();
        }





        [Authorize(Roles = "User,Admin")]
        public IActionResult Create(int projectId)
        {
            var project = _context.Projects.Find(projectId);
            if (project == null)
            {
                return NotFound();
            }

            var task = new TooDue.Models.Task
            {
                Project_Id = projectId,
                Task_create_date = DateTime.Now // Set the create date to the current date and time
            };

            ViewBag.ProjectId = projectId;
            ViewBag.ProjectName = project.Project_name;
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Create(TooDue.Models.Task task)
        {
            if (ModelState.IsValid)
            {
                var project = await _context.Projects.FindAsync(task.Project_Id);
                if (project == null)
                {
                    return NotFound();
                }

                _context.Tasks.Add(task);
                await _context.SaveChangesAsync();

                TempData["message"] = "Task has been created.";
                TempData["messageType"] = "alert-success";
                return RedirectToAction("Show", "Tasks", new { projectId = task.Project_Id });
            }

            var projectForView = await _context.Projects.FindAsync(task.Project_Id);
            if (projectForView != null)
            {
                ViewBag.ProjectName = projectForView.Project_name;
            }

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

            //var projectUserTask = await _context.ProjectUserTask
            //    .FirstOrDefaultAsync(put => put.Task_id == id);
            //if (projectUserTask == null)
            //{
            //    return NotFound();
            //}

            ViewBag.ProjectId = task.Project_Id;
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
            //if (projectUserTask == null)
            //{
            //    return NotFound();
            //}

            var comments = _context.Comments.Where(c => c.TaskId == id);
            _context.Comments.RemoveRange(comments);

            _context.Tasks.Remove(task);
            _context.ProjectUserTask.Remove(projectUserTask);
            await _context.SaveChangesAsync();
            ViewBag.Message = "Task has been deleted.";
            ViewBag.MessageType = "alert-success";
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

            //var projectUserTask = await _context.ProjectUserTask
            //    .FirstOrDefaultAsync(put => put.Task_id == id);
            //if (projectUserTask == null)
            //{
            //    return NotFound();
            //}

            ViewBag.ProjectId = task.Project_Id;
            _logger.LogWarning("transmitted {ViewBag.ProjectId}", (object)ViewBag.ProjectId);
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Edit(int id, TooDue.Models.Task task)
        {
            if (id != task.Task_id)
            {
                return NotFound();
            }

            if (task.Task_complete_date <= task.Task_create_date)
            {
                ModelState.AddModelError("Task_complete_date", "Deadline must be greater than Start Date.");
                ViewBag.ProjectId = task.Project_Id;
                return View(task);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(task);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Show", new { projectId = task.Project_Id });
                }
                catch (DbUpdateException ex)
                {
                    _logger.LogError(ex, "An error occurred while updating the task.");
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
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
            ViewBag.ProjectId = task.Project_Id;
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

            //var projectUserTask = await _context.ProjectUserTask.FirstOrDefaultAsync(put => put.Task_id == id);
            //if (projectUserTask == null)
            //{
            //    return NotFound();
            //}

            var usersInProject = GetUsersInProject(task.Project_Id);
            var usersWithTask = GetUsersWithTask(task.Project_Id, task.Task_id);

           
            var project = await _context.Projects.FindAsync(task.Project_Id);
            if (project == null)
            {
                return NotFound();
            }
            var organizerId = project.CreatedByUserId;
            var availableUsers = usersInProject
                .Where(u => !usersWithTask.Any(ut => ut.Id == u.Id) && u.Id != organizerId)
                .ToList();

            ViewBag.ProjectId = task.Project_Id;
            ViewBag.TaskId = task.Task_id;
            ViewBag.AvailableUsers = availableUsers;
            ViewBag.UsersWithTask = usersWithTask;

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

            //var projectUserTask = await _context.ProjectUserTask.FirstOrDefaultAsync(put => put.Task_id == id);
            //if (projectUserTask == null)
            //{
            //    return NotFound();
            //}

            var projectId = task.Project_Id;

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
            ViewBag.UsersInProject = GetUsersInProject(task.Project_Id);

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

            //var projectUserTask = await _context.ProjectUserTask.FirstOrDefaultAsync(put => put.Task_id == id);
            //if (projectUserTask == null)
            //{
            //    return NotFound();
            //}

            ViewBag.ProjectId = task.Project_Id;
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

                    //var project_User_Task = await _context.ProjectUserTask.FirstOrDefaultAsync(put => put.Task_id == id);
                    //if (project_User_Task == null)
                    //{
                    //    _logger.LogWarning("ProjectUserTask with Task ID {Id} not found.", id);
                    //    return NotFound();
                    //}

                    return RedirectToAction("Show", new { projectId = existingTask.Project_Id });
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

           
            ViewBag.ProjectId = task.Project_Id;
            return View(task);
        }





    }
}
