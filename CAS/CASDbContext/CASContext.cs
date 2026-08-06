using CAS.Contracts.Enums;
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
            SeedFarmerRoleData(modelBuilder);
            SeedSoilTypesData(modelBuilder);
            SeedSeasonsData(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Crop>(entity =>
            {
                entity.HasIndex(c => c.Name).IsUnique();
                entity.Property(c => c.CropStatus).HasConversion<string>();
                entity.HasQueryFilter(c => c.CropStatus == Status.Active);
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

            modelBuilder.Entity<Advisory>(entity =>
            {
                entity.Property(a => a.Advisorytatus).
               HasConversion<string>();

                entity.HasQueryFilter(a => a.Advisorytatus == Status.Active);
            });
               
             

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
                PasswordHash = "AQAAAAIAAYagAAAAEH57jLQ7uc7oKhUYtas/A3EDzs8yY13z1jMAlgZiR+WJAsOxqgsbo0y+3ztTRUCmjA==",
                RoleId = adminRoleId,
                CreatedAt = new DateTime(2026, 04, 25, 0, 0, 0, DateTimeKind.Utc),
            };

            modelBuilder.Entity<Role>().HasData(adminRole);
            modelBuilder.Entity<User>().HasData(adminUser);
        }

        private void SeedFarmerRoleData(ModelBuilder modelBuilder)
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

        private void SeedSoilTypesData(ModelBuilder modelBuilder)
        {
            var soilTypes = new List<SoilType>
            {
                new SoilType
                {
                    Id = new Guid("82907dc8-96db-429e-9550-a7fcd4f4ce6a"),
                    Name = "Sandy Soil",
                    Description = "Composed of large particles, making it gritty to the touch. It drains very quickly and warms up fast in the spring, but holds few nutrients.",
                    SoilTypeStatus = Status.Active,
                    CreatedAt = new DateTime(2026, 06, 24, 0, 0, 0, DateTimeKind.Utc)
                },
                new SoilType
                {
                    Id = new Guid("e8927783-5f40-443b-8db5-d12f42d9b399"),
                    Name = "Silty Soil",
                    Description = "Made of medium-sized particles, feeling smooth like powder. It is highly fertile, retains moisture, and is often found near water bodies.",
                    SoilTypeStatus = Status.Active,
                    CreatedAt = new DateTime(2026, 06, 24, 0, 0, 0, DateTimeKind.Utc)
                },
                new SoilType
                {
                    Id = new Guid("32b43d41-7a17-4fea-86e1-e105e89db4be"),
                    Name = "Loamy Soil",
                    Description = "The ideal agricultural soil, consisting of a balanced mixture of sand, silt, and clay. It is nutrient-rich, retains moisture effectively, and drains well.",
                    SoilTypeStatus = Status.Active,
                    CreatedAt = new DateTime(2026, 06, 24, 0, 0, 0, DateTimeKind.Utc)
                },
                new SoilType
                {
                    Id = new Guid("52f17eba-5f74-489f-89e0-e3c886146852"),
                    Name = "Clay Soil",
                    Description = "Made of the smallest particles, making it sticky when wet and rock-hard when dry. It retains nutrients and moisture well but drains slowly.",
                    SoilTypeStatus = Status.Active,
                    CreatedAt = new DateTime(2026, 06, 24, 0, 0, 0, DateTimeKind.Utc)
                },
                new SoilType
                {
                    Id = new Guid("297dac6b-ef82-429f-88bc-ef79f32c428d"),
                    Name = "Peaty Soil",
                    Description = "Contains a high amount of dead organic matter (humus) and is dark, spongy, and acidic. It acts like a sponge and holds a lot of water.",
                    SoilTypeStatus = Status.Active,
                    CreatedAt = new DateTime(2026, 06, 24, 0, 0, 0, DateTimeKind.Utc)
                },
                new SoilType
                {
                    Id = new Guid("fcc6c2a8-a714-4eee-800f-ede74c35f876"),
                    Name = "Chalky Soil",
                    Description = "Highly alkaline and contains visible stones or pieces of chalk. It is usually stony, free-draining, and requires organic matter to improve its fertility.",
                    SoilTypeStatus = Status.Active,
                    CreatedAt = new DateTime(2026, 06, 24, 0, 0, 0, DateTimeKind.Utc)
                }

            };

            modelBuilder.Entity<SoilType>().HasData(soilTypes);
        }

        private void SeedSeasonsData(ModelBuilder modelBuilder)
        {
            var seasons = new List<Season>
            {
                new Season
                {
                    Id = new Guid("813b1b0b-045e-4f12-89fb-e602dfa4e84d"),
                    Name = "Rainy",
                    SeasonStatus = Status.Active,
                    CreatedAt = new DateTime(2026, 08, 01, 0, 0, 0, DateTimeKind.Utc)
                },
                new Season
                {
                    Id = new Guid("36bf3939-a5d0-4e37-8347-dabb8c33404c"),
                    Name = "Dry",
                    SeasonStatus= Status.Active,
                    CreatedAt = new DateTime(2026, 08, 01, 0, 0, 0, DateTimeKind.Utc)
                }
            };

            modelBuilder.Entity<Season>().HasData(seasons);
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
