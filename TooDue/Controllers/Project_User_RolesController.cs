using Microsoft.AspNetCore.Mvc;
using TooDue.Models;

namespace TooDue.Controllers
{
    public class Project_User_RolesController : Controller
    {
        private readonly AppDbContext _db;

        public Project_User_RolesController(AppDbContext context)
        {
            _db = context;
        }
    }
}
