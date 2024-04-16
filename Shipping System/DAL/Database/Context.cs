using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shipping_System.DAL.Entites;

namespace Shipping_System.DAL.Database
{
    public class Context : IdentityDbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
          
        }

        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<Governate> Governates { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ShippingSetting> ShippingSettings { get; set; }
        public virtual DbSet<WeightSetting> WeightSettings{ get ; set; }
        public virtual DbSet<VillageShipping>  VillageSettings { get; set; }
        public virtual DbSet<Order_Status> Order_Statuses { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure cascade delete behavior for Order-Product relationship
            modelBuilder.Entity<Order>()
                .HasMany(o => o.Products)
                .WithOne(p => p.Order)
                .OnDelete(DeleteBehavior.Cascade);

            // Disable cascade delete for relationships involving Trader and Representitive
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.RepresentiveOrders)
                .WithOne(o => o.Representitive)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.TraderOrders)
                .WithOne(o => o.Trader)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
            .Property(o => o.Order_Total_Cost)
            .HasComputedColumnSql("[Shipping_Total_Cost] + [Products_Total_Cost]"); // Specify the calculation
        }


    }
}

