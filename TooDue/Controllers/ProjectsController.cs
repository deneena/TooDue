using Microsoft.AspNetCore.Mvc;
using TooDue.Models;

namespace TooDue.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly AppDbContext _db;

        public ProjectsController(AppDbContext context)
        {
            _db = context;
        }
    }
}
