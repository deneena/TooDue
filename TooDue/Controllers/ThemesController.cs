using Microsoft.AspNetCore.Mvc;
using TooDue.Data;
using TooDue.Models;

namespace TooDue.Controllers
{
    public class ThemesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ThemesController(ApplicationDbContext context)
        {
            _db = context;
        }
    }
}
