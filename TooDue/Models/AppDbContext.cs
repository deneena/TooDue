using Microsoft.EntityFrameworkCore;

namespace TooDue.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>
        options)
        : base(options)
        {
        }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Project_User_Task> Project_User_Tasks { get; set; }
        public DbSet<Project_User_Role> Project_User_Roles { get; set; }
    }
}
