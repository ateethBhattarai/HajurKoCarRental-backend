using HajurKoCarRental_backend.Model;
using Microsoft.EntityFrameworkCore;

namespace HajurKoCarRental_backend.DataContext
{
    public class AppDataContext : DbContext
    {
        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
        {

        }

        public DbSet<UserModel> Users { get; set; } = null!;
        public DbSet<CarsModel> Cars { get; set; } = null!;
        public DbSet<RentalModel> Rentals { get; set; } = null!;
        public DbSet<DamagedCarsModel> DamagedCars { get; set; } = null!;
        public DbSet<RentalHistory> RentalHistories { get; set; } = null!;



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>().Property(r => r.role).HasConversion<string>();
            modelBuilder.Entity<DamagedCarsModel>().Property(s => s.settlement_status).HasConversion<string>();
            modelBuilder.Entity<RentalModel>().Property(rc => rc.rental_status).HasConversion<string>();

        }


        public DbSet<HajurKoCarRental_backend.Model.RentalHistory>? RentalHistory { get; set; }
    }

}
