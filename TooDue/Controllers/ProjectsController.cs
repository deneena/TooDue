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
using Microsoft.Build.Framework;

namespace TooDue.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ProjectsController> _logger;

        public ProjectsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger<ProjectsController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        private List<ApplicationUser> GetUsersNotInProject(int projectId)
        {
            var usersInProject = _context.ProjectUserRoles
       .Where(pur => pur.Related_project_id == projectId)
       .Select(pur => pur.Related_user_id)
       .ToList();

            _logger.LogInformation("Users in project {ProjectId}: {UsersInProject}", projectId, string.Join(", ", usersInProject));

            var usersNotInProject = _userManager.Users
                .Where(u => !usersInProject.Contains(u.Id))
                .ToList();

            _logger.LogInformation("Users not in project {ProjectId}: {UsersNotInProject}", projectId, string.Join(", ", usersNotInProject.Select(u => u.Id)));

            return usersNotInProject;
        }


        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Index()
        {
            if (TempData.ContainsKey("message"))
            {
                ViewBag.Message = TempData["message"];
                ViewBag.Alert = TempData["messageType"];
            }

            SetAccessRights();

            var userId = _userManager.GetUserId(User);
            _logger.LogInformation("User ID: {UserId}", userId);

            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogWarning("User ID is null or empty. Ensure the user is authenticated.");
                return Unauthorized();
            }

            var isAdmin = User.IsInRole("Admin");

            var projects = isAdmin
               ? await _context.Projects.Include(p => p.CreatedByUser).ToListAsync()
               : await _context.Projects
                   .Where(p => p.CreatedByUserId == userId || _context.ProjectUserRoles.Any(pur => pur.Related_user_id == userId && pur.Related_project_id == p.Project_id))
                   .Include(p => p.CreatedByUser)
                   .ToListAsync();

            if (!projects.Any())
            {
                ViewBag.Message = "You have no projects.";
                ViewBag.Alert = "alert-info";
            }

            ViewBag.Projects = projects;

            return View();
        }

       
        [Authorize(Roles = "User,Admin")]
        public IActionResult Create()
        {
            Project project = new Project();
            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Create(Project project)
        {
            _logger.LogInformation("Create method started.");

            var userId = _userManager.GetUserId(User);
            _logger.LogInformation("User ID: {UserId}", userId);

            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogWarning("User ID is null or empty. Ensure the user is authenticated.");
                return Unauthorized();
            }

            project.CreatedByUserId = userId;
            project.CreatedByUser = await _userManager.FindByIdAsync(userId);

            project.Project_create_date = DateTime.Now; // Set the create date to the current date and time
            _logger.LogInformation("Project create date set to: {CreateDate}", project.Project_create_date);

            project.Project_status = "in work"; // Set the project status to "in work"
            _logger.LogInformation("Project status set to: {Status}", project.Project_status);


            ModelState.Clear();
            TryValidateModel(project);

            if (ModelState.IsValid)
            {
                _logger.LogInformation("Model state is valid.");
                _context.Add(project);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Project saved to database.");

                var projectUserRole = new Project_User_Role
                {
                    Related_project_id = project.Project_id,
                    Related_user_id = userId,
                    Role = "organiser"
                };
                _context.Add(projectUserRole);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Project_User_Role entry saved to database.");

                TempData["message"] = "Project has been created.";
                TempData["messageType"] = "alert-success";
                return RedirectToAction("Index");
            }
            else
            {
                _logger.LogWarning("Model state is invalid.");
            }

            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                _logger.LogError("Model state error: {ErrorMessage}", error.ErrorMessage);
            }

            return View(project);
        }


        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            ViewBag.ProjectName = project.Project_name;
            ViewBag.Users = GetUsersNotInProject(id)
                .Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.UserName
                }).ToList();

            return View(project);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Edit(int id, Project project, string? userIdToAdd)
        {
            if (id != project.Project_id)
            {
                _logger.LogWarning("what the heck, id {id} and pojectId {project.Project_id} ", id, project.Project_id);
                return NotFound();
            }

            var existingProject = await _context.Projects.AsNoTracking().FirstOrDefaultAsync(p => p.Project_id == id);
            if (existingProject == null)
            {
                _logger.LogWarning("but i can");
                return NotFound();
            }
            project.CreatedByUserId = existingProject.CreatedByUserId;

            ModelState.Clear();
            TryValidateModel(project);

            if (ModelState.IsValid)
            {
                _context.Update(project);
                await _context.SaveChangesAsync();

                if (!string.IsNullOrEmpty(userIdToAdd))
                {
                    var projectUserRole = new Project_User_Role
                    {
                        Related_project_id = project.Project_id,
                        Related_user_id = userIdToAdd,
                        Role = "member"
                    };
                    _context.Add(projectUserRole);
                    await _context.SaveChangesAsync();
                }

                TempData["message"] = "Project has been updated.";
                TempData["messageType"] = "alert-success";
                return RedirectToAction("Index");
            }
            else
            {
                _logger.LogWarning("Model state is invalid.");
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        _logger.LogError("Model state error in {Key}: {ErrorMessage}", state.Key, error.ErrorMessage);
                    }
                }
            }

            ViewBag.Users = _context.Users.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.UserName
            }).ToList();

            return View(project);
        }



        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.CreatedByUser)
                .FirstOrDefaultAsync(m => m.Project_id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User,Editor,Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var projectToDelete = await _context.Projects.FindAsync(id);
            if (projectToDelete == null)
            {
                return NotFound();
            }

            var projectUserRoles = _context.ProjectUserRoles.Where(pur => pur.Related_project_id == id);
            _context.ProjectUserRoles.RemoveRange(projectUserRoles);

            var projectUserTasks = _context.ProjectUserTask.Where(put => put.Project_id == id);
            _context.ProjectUserTask.RemoveRange(projectUserTasks);

            var tasks = _context.Tasks.Where(t => t.Project_Id == id).ToList();
            foreach (var task in tasks)
            {
                var comments = _context.Comments.Where(c => c.TaskId == task.Task_id);
                _context.Comments.RemoveRange(comments);

                _context.Tasks.Remove(task);
            }

            _context.Projects.Remove(projectToDelete);

            await _context.SaveChangesAsync();

            ViewBag.Message = "Project has been deleted.";
            ViewBag.MessageType = "alert-success";
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeOrganiser(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            ViewBag.ProjectName = project.Project_name;

            var usersInProject = _context.ProjectUserRoles
                .Where(pur => pur.Related_project_id == id)
                .Select(pur => pur.Related_user_id)
                .ToList();

            var admins = await _userManager.GetUsersInRoleAsync("Admin");

            var allUsers = _userManager.Users
                .Where(u => usersInProject.Contains(u.Id) || admins.Select(a => a.Id).Contains(u.Id))
                .Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.UserName
                }).ToList();

            ViewBag.AllUsers = allUsers;

            return View(project);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeOrganiser(int id, string newOwnerId)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(newOwnerId) && newOwnerId != project.CreatedByUserId)
            {
                // Update the project to set the new organizer
                project.CreatedByUserId = newOwnerId;
                _context.Update(project);

                await _context.SaveChangesAsync();

                TempData["message"] = "Project organiser has been changed.";
                TempData["messageType"] = "alert-success";
            }

            return RedirectToAction("Index");
        }


        private void SetAccessRights()
        {
            //ViewBag.AfisareButoane = User.IsInRole("Editor") || User.IsInRole("User");
            ViewBag.EsteAdmin = User.IsInRole("Admin");
            ViewBag.UserCurent = _userManager.GetUserId(User);
        }
    }
}
