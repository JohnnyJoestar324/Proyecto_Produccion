using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace Proyecto_Produccion.Models
{
    public static class MolderBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // Crear ROLES
            List<IdentityRole> roles = new List<IdentityRole>() {
                new IdentityRole { Name = "Administrador", NormalizedName = "ADMINISTRADOR" }


            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);

            // Crear USERS
            List<ApplicationUser> users = new List<ApplicationUser>() {
                new ApplicationUser {
                    FirstName = "Johnny",
                    LastName = "Eduardo",
                    UserName = "martinezjohnny324@gmail.com",
                    NormalizedUserName = "MARTINEZJOHNNY324@GMAIL.COM",
                    Email = "martinezjohnny324@gmail.com",
                    NormalizedEmail = "MARTINEZJOHNNY324@GMAIL.COM",
                    EmailConfirmed = true
                },
                new ApplicationUser {
                    UserName = "Rcairo09@gmail.com",
                    NormalizedUserName = "RCAIRO09@GMAIL.COM",
                    Email = "Rcairo09@gmail.com",
                    NormalizedEmail = "RCAIRO09@GMAIL.COM",
                    EmailConfirmed = true
                },
                new ApplicationUser {
                    UserName = "My10@gmail.com",
                    NormalizedUserName = "MY10@GMAIL.COM",
                    Email = "My10@gmail.com",
                    NormalizedEmail = "MY10@GMAIL.COM",
                    EmailConfirmed = true
                },
                new ApplicationUser {
                    UserName = "Oteroanielka7@gmail.com",
                    NormalizedUserName = "OTEROANIELKA7@GMAIL.COM",
                    Email = "Oteroanielka7@gmail.com",
                    NormalizedEmail = "OTEROANIELKA7@GMAIL.COM",
                    EmailConfirmed = true
                }

            };
            modelBuilder.Entity<ApplicationUser>().HasData(users);

            // Agregar contraseña a los usuarios
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            users[0].PasswordHash = passwordHasher.HashPassword(users[0], "edu2002");
            users[1].PasswordHash = passwordHasher.HashPassword(users[1], "rocha156");
            users[2].PasswordHash = passwordHasher.HashPassword(users[2], "milo150");
            users[3].PasswordHash = passwordHasher.HashPassword(users[2], "anielka677");




            // Agregar roles a usuario
            List<IdentityUserRole<string>> userRoles = new List<IdentityUserRole<string>>();
            userRoles.Add(new IdentityUserRole<string>
            {
                UserId = users[0].Id,
                RoleId = roles.First(q => q.Name == "Administrador").Id
            });
            userRoles.Add(new IdentityUserRole<string>
            {
                UserId = users[1].Id,
                RoleId = roles.First(q => q.Name == "Administrador").Id
            });
            userRoles.Add(new IdentityUserRole<string>
            {
                UserId = users[2].Id,
                RoleId = roles.First(q => q.Name == "Administrador").Id
            });
            userRoles.Add(new IdentityUserRole<string>
            {
                UserId = users[3].Id,
                RoleId = roles.First(q => q.Name == "Administrador").Id
            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(userRoles);

        }


    }
}

