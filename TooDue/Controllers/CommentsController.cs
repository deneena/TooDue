using Microsoft.AspNetCore.Mvc;
using TooDue.Models;

namespace TooDue.Controllers
{
    public class CommentsController : Controller
    {
        private readonly AppDbContext _db;

        public CommentsController(AppDbContext context)
        {
            _db = context;
        }
    }
}
