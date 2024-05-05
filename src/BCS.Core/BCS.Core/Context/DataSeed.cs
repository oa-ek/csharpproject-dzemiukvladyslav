using BCS.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BCS.Core.Context
{
    public static class DataSeed
    {
        public static void Seed(this ModelBuilder builder)
        {
            var (arID, wrID, urID) = _seedRoles(builder);

            _seedWorkers(builder, wrID, arID);
        }

        private static (Guid, Guid, Guid) _seedRoles(ModelBuilder builder)
        {
            var ADMIN_ROLE_ID = Guid.NewGuid();
            var WORKER_ROLE_ID = Guid.NewGuid();
            var USER_ROLE_ID = Guid.NewGuid();

            builder.Entity<IdentityRole<Guid>>()
               .HasData(
                   new IdentityRole<Guid>
                   {
                       Id = ADMIN_ROLE_ID,
                       Name = "Admin",
                       NormalizedName = "ADMIN",
                       ConcurrencyStamp = ADMIN_ROLE_ID.ToString()
                   },
                   new IdentityRole<Guid>
                   {
                       Id = WORKER_ROLE_ID,
                       Name = "Worker",
                       NormalizedName = "WORKER",
                       ConcurrencyStamp = WORKER_ROLE_ID.ToString()
                   },
                   new IdentityRole<Guid>
                   {
                       Id = USER_ROLE_ID,
                       Name = "User",
                       NormalizedName = "USER",
                       ConcurrencyStamp = USER_ROLE_ID.ToString()
                   }
               );


            return (ADMIN_ROLE_ID, WORKER_ROLE_ID, USER_ROLE_ID);
        }

        private static Guid _seedWorkers(ModelBuilder builder, Guid WORKER_ROLE_ID, Guid ADMIN_ROLE_ID)
        {
            var workerId = Guid.NewGuid();

            var worker = new AppUser
            {
                Id = workerId,
                UserName = "admin@gmail.com",
                EmailConfirmed = true,
                NormalizedUserName = "admin@gmail.com".ToUpper(),
                Email = "admin@gmail.com",
                NormalizedEmail = "admin@gmail.com".ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString(),
                FullName = "Власник сайту"
            };

            var worker2 = new AppUser
            {
                Id = Guid.NewGuid(),
                UserName = "vlad.dzemyuk@gmail.com",
                EmailConfirmed = true,
                NormalizedUserName = "vlad.dzemyuk@gmail.com".ToUpper(),
                Email = "vlad.dzemyuk@gmail.com",
                NormalizedEmail = "vlad.dzemyuk@gmail.com".ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString(),
                FullName = "Владислав Дзем'юк"
            };

            PasswordHasher<AppUser> passwordHasher = new PasswordHasher<AppUser>();
            worker.PasswordHash = passwordHasher.HashPassword(worker, "tVAz_Az.@_Hnx4+");
            worker2.PasswordHash = passwordHasher.HashPassword(worker2, "LwPV+%4*M9jsU6R");

            builder.Entity<AppUser>()
                .HasData(worker, worker2);

            builder.Entity<IdentityUserRole<Guid>>()
              .HasData(
                  new IdentityUserRole<Guid>
                  {
                      RoleId = ADMIN_ROLE_ID,
                      UserId = workerId
                  },
                  new IdentityUserRole<Guid>
                  {
                      RoleId = WORKER_ROLE_ID,
                      UserId = workerId
                  }
              );

            return workerId;
        }
    }
}
