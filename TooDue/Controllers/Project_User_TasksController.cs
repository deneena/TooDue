using Microsoft.AspNetCore.Mvc;
using TooDue.Models;

namespace TooDue.Controllers
{
    public class Project_User_TasksController : Controller
    {
        private readonly AppDbContext _db;

        public Project_User_TasksController(AppDbContext context)
        {
            _db = context;
        }
    }
}
