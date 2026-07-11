using CAS.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CAS.CASDbContext
{
    public class CASContext(DbContextOptions<CASContext> options) : DbContext(options)
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(w =>
                w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedAdminData(modelBuilder);
            SeedRoleData(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email.ToLowerInvariant())
                .IsUnique();

            modelBuilder.Entity<Crop>(entity =>
            {
                entity.HasIndex(c => c.Name).IsUnique();
                entity.Property(c => c.CropStatus).HasConversion<string>();
            });


            modelBuilder.Entity<SoilType>(entity =>
            {
                entity.HasIndex(st => st.Name).IsUnique();
                entity.Property(st => st.SoilTypeStatus).HasConversion<string>();
            });

            modelBuilder.Entity<Season>(entity =>
            {
                entity.HasIndex(s => s.Name).IsUnique();
                entity.Property(s => s.SeasonStatus).HasConversion<string>();
            });

            modelBuilder.Entity<Advisory>()
               .Property(a => a.Advisorytatus).
               HasConversion<string>();
               

            modelBuilder.Entity<Role>()
                .HasIndex(c => c.Name)
                .IsUnique();
        }

        private void SeedAdminData(ModelBuilder modelBuilder)
        {
            var adminRoleId = new Guid("202d1e4d-4423-468f-9b78-84d2ee041b8b");
            var adminUserId = new Guid("f3c9e1b2-5d4a-4e6b-8f1a-2c3d4e5f6a7b");

            var adminRole = new Role
            {
                Id = adminRoleId,
                Name = "Admin",
                Description = "Has full access to application resources",
                CreatedAt = new DateTime(2026, 06, 24, 0, 0, 0, DateTimeKind.Utc),

            };

            var adminUser = new User
            {
                Id = adminUserId,
                Email = "cas@admin.com",
                FullName = "CAS Admin",
                PhoneNumber = "09117690426",
                Location = "Lagos",
                PasswordHash = "AQAAAAEAACcQAAAAEP55WXadi3LFl/WUHm61QFIdM7BF33w0jUBWix6x/RFfzvK8F0VN4/KNkLFlDuMdEg==",
                RoleId = adminRoleId,
                CreatedAt = new DateTime(2026, 04, 25, 0, 0, 0, DateTimeKind.Utc),
            };

            modelBuilder.Entity<Role>().HasData(adminRole);
            modelBuilder.Entity<User>().HasData(adminUser);
        }

        private void SeedRoleData(ModelBuilder modelBuilder)
        {
            var role = new Role
            {
                Id = new Guid("57bfb05d-063b-4e84-86dd-76f90d83b6ac"),
                Name = "Farmer",
                Description = "Has full access to application resources",
                CreatedAt = new DateTime(2026, 06, 24, 0, 0, 0, DateTimeKind.Utc),

            };

            modelBuilder.Entity<Role>().HasData(role);
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Crop> Crops { get; set; }
        public DbSet<Advisory> Advisories { get; set; }
        public DbSet<SaveGuide> SaveGuides { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<SoilType> SoilTypes { get; set; }
        public DbSet<WeatherLog> WeatherLogs { get; set; }

    }
}
