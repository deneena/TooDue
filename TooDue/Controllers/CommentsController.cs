using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TooDue.Data;
using TooDue.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace TooDue.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CommentsController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public CommentsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger<CommentsController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Show(int? taskId)
        {
            if (taskId == null)
            {
                return NotFound();
            }
            var task = await _context.Tasks
            .Where(t => t.Task_id == taskId)
            .Select(t => new { t.Project_Id })
            .FirstOrDefaultAsync();

            if (task == null)
            {
                return NotFound();
            }

            var projectId = task.Project_Id;

            var comments = await _context.Comments
               .Where(c => c.TaskId == taskId)
               .Include(c => c.User) 
               .ToListAsync();

            //var projectUserTask = await _context.ProjectUserTask
            //    .FirstOrDefaultAsync(put => put.Task_id == taskId);

            //if (projectUserTask == null)
            //{
            //    return NotFound();
            //}

            if (!comments.Any())
            {
                ViewBag.Message = "No comments found for this project.";
                ViewBag.Alert = "alert-info";
            }

            ViewBag.Comments = comments;
            ViewBag.TaskId = taskId;
            ViewBag.ProjectId = task.Project_Id;
            ViewBag.IsAdmin = User.IsInRole("Admin");
            var currentUser = await _userManager.GetUserAsync(User);
            ViewBag.CurrentUserId = currentUser?.Id;
            ViewBag.UserMail = currentUser.Email;

            return View();
        }

        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Create(int taskId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            ViewBag.TaskId = taskId;
            ViewBag.UserId = user.Id;
            ViewBag.UserMail = user.Email;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Create(Comment comment, int taskId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Unauthorized();
            }

            comment.UserId = currentUser.Id;
            comment.Comment_Date = DateTime.Now;
            comment.TaskId = taskId;

            if (ModelState.IsValid)
            {
                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Show", new { taskId = taskId });
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
            ViewBag.TaskId = taskId;
            return View(comment);
        }


        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var currentUser = await _userManager.GetUserAsync(User);

            var comment = await _context.Comments
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Comment_id == id);
            if (comment == null)
            {
                return NotFound();
            }

            
            if (comment.UserId != currentUser?.Id && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            return View(comment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Delete(int id, Comment comment)
        {
            var commentToDelete = await _context.Comments.FindAsync(id);
            if (commentToDelete == null)
            {
                return NotFound();
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (commentToDelete.UserId != currentUser.Id && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            _context.Comments.Remove(commentToDelete);
            await _context.SaveChangesAsync();
            return RedirectToAction("Show", new { taskId = commentToDelete.TaskId });
        }


        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (comment.UserId != currentUser.Id && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            ViewBag.TaskId = comment.TaskId;
            ViewBag.UserId = currentUser.Id;
            return View(comment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Edit(int id, Comment comment)
        {
            if (id != comment.Comment_id)
            {
                return NotFound();
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Unauthorized();
            }

            if (comment.UserId != currentUser.Id && !User.IsInRole("Admin"))
            {
                _logger.LogWarning("The currentuserID {currentUser.Id} and the userId of the comment {comment.UserId}", currentUser.Id, comment.UserId);
                return Forbid();
            }

            var existingComment = await _context.Comments.AsNoTracking().FirstOrDefaultAsync(c => c.Comment_id == id);
            if (existingComment == null)
            {
                return NotFound();
            }

            existingComment.Comment_text = comment.Comment_text;
            existingComment.Comment_Date = DateTime.Now;
            existingComment.isEdited = true;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(existingComment).State = EntityState.Detached;
                    _context.Update(comment);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Show", new { taskId = comment.TaskId });
                }
                catch (DbUpdateException ex)
                {
                    _logger.LogError(ex, "An error occurred while updating the comment.");
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

            ViewBag.TaskId = comment.TaskId;
            ViewBag.UserId = currentUser.Id;
            return View(comment);
        }

    }
}

