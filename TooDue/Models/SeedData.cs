using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TooDue.Data;
using TooDue.Models;


// PASUL 4: useri si roluri

namespace TooDue.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
            serviceProvider.GetRequiredService
            <DbContextOptions<ApplicationDbContext>>()))
            {

                if (context.Roles.Any())
                {

                }
                else
                {
                    context.Roles.AddRange(
                        new IdentityRole { Id = "2c5e174e-3b0e-446f-86af-483d56fd7210", Name = "Admin", NormalizedName = "Admin".ToUpper() },
                        new IdentityRole { Id = "2c5e174e-3b0e-446f-86af-483d56fd7212", Name = "User", NormalizedName = "User".ToUpper() }
                    );
                }

                var hasher = new PasswordHasher<ApplicationUser>();


                context.Users.AddRange(
                    new ApplicationUser
                    {
                        Id = "8e445865-a24d-4543-a6c6-9443d048cdb0",
                        UserName = "admin@test.com",
                        EmailConfirmed = true,
                        NormalizedEmail = "ADMIN@TEST.COM",
                        Email = "admin@test.com",
                        NormalizedUserName = "ADMIN@TEST.COM",
                        PasswordHash = hasher.HashPassword(new ApplicationUser(), "Admin1!")
                    },

                    new ApplicationUser
                    {
                        Id = "8e445865-a24d-4543-a6c6-9443d048cdb1",
                        UserName = "matei@test.com",
                        EmailConfirmed = true,
                        NormalizedEmail = "matei@TEST.COM",
                        Email = "matei@test.com",
                        NormalizedUserName = "MATEI@TEST.COM",
                        PasswordHash = hasher.HashPassword(new ApplicationUser(), "Matei1!")
                    },

                    new ApplicationUser
                    {
                        Id = "8e445865-a24d-4543-a6c6-9443d048cdb2",
                        UserName = "user@test.com",
                        EmailConfirmed = true,
                        NormalizedEmail = "USER@TEST.COM",
                        Email = "user@test.com",
                        NormalizedUserName = "USER@TEST.COM",
                        PasswordHash = hasher.HashPassword(new ApplicationUser(), "User1!")
                    },

                    new ApplicationUser
                    {
                        Id = "8e445865-a24d-4543-a6c6-9443d048cdb3",
                        UserName = "denisa@test.com",
                        EmailConfirmed = true,
                        NormalizedEmail = "DENISA@TEST.COM",
                        Email = "denisa@test.com",
                        NormalizedUserName = "DENISA@TEST.COM",
                        PasswordHash = hasher.HashPassword(new ApplicationUser(), "Denisa1!")
                    },
                    new ApplicationUser
                    {
                        Id = "8e445865-a24d-4543-a6c6-9443d048cdb4",
                        UserName = "andrei@test.com",
                        EmailConfirmed = true,
                        NormalizedEmail = "ANDREI@TEST.COM",
                        Email = "andrei@test.com",
                        NormalizedUserName = "ANDREI@TEST.COM",
                        PasswordHash = hasher.HashPassword(new ApplicationUser(), "Andrei1!")
                    }
                );



                context.UserRoles.AddRange(
                new IdentityUserRole<string>
                {
                    RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                    UserId = "8e445865-a24d-4543-a6c6-9443d048cdb0"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7212",
                    UserId = "8e445865-a24d-4543-a6c6-9443d048cdb1"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7212",
                    UserId = "8e445865-a24d-4543-a6c6-9443d048cdb2"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7212",
                    UserId = "8e445865-a24d-4543-a6c6-9443d048cdb3"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7212",
                    UserId = "8e445865-a24d-4543-a6c6-9443d048cdb4"
                }
                );
                
                
                context.SaveChanges();
                }
            }
        }
    }

