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




    }
}

