using LibBooking.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibBooking.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // 生成固定的 RoleId
            var roleId = Guid.NewGuid().ToString();

            // Seed roles
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = roleId, Name = "Admin", NormalizedName = "ADMIN" }
            );

            // Seed admin user
            var adminUser = new IdentityUser
            {
                UserName = "admin@library.com",
                Email = "admin@library.com",
                EmailConfirmed = true,
                NormalizedUserName = "ADMIN@LIBRARY.COM",
                NormalizedEmail = "ADMIN@LIBRARY.COM",
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            // 密码哈希
            var passwordHasher = new PasswordHasher<IdentityUser>();
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "Admin@123");

            builder.Entity<IdentityUser>().HasData(adminUser);

            // Assign user to role
            builder.Entity<IdentityUserRole<string>>().HasKey(r => new { r.UserId, r.RoleId });

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                UserId = adminUser.Id,
                RoleId = roleId  // 使用与角色一致的 RoleId
            });

            // Seed data for rooms (existing seed data)
            builder.Entity<Room>().HasData(
                new Room { ID = 1, RoomName = "Room A", Capacity = 10, Description = "Description for Room A" },
                new Room { ID = 2, RoomName = "Room B", Capacity = 20, Description = "Description for Room B" },
                new Room { ID = 3, RoomName = "Room C", Capacity = 30, Description = "Description for Room C" },
                new Room { ID = 4, RoomName = "Room D", Capacity = 40, Description = "Description for Room D" }
            );
        }

    }
}
