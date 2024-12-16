using Microsoft.AspNetCore.Mvc;
using TooDue.Models;

namespace TooDue.Controllers
{
    public class ThemesController : Controller
    {
        private readonly AppDbContext _db;

        public ThemesController(AppDbContext context)
        {
            _db = context;
        }
    }
}
