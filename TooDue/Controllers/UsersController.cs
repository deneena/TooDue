using Microsoft.AspNetCore.Mvc;
using TooDue.Models;

namespace TooDue.Controllers
{
    public class UsersController : Controller
    {
        private readonly AppDbContext _db;

        public UsersController(AppDbContext context)
        {
            _db = context;
        }
    }
}
