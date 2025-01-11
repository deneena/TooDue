using Microsoft.AspNetCore.Mvc;
using TooDue.Data;
using TooDue.Models;

namespace TooDue.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _db;

        public UsersController(ApplicationDbContext context)
        {
            _db = context;
        }
    }
}
