using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using DomainLayer.Models;
using Microsoft.AspNetCore.Identity;

namespace Repository.context
{
    //class appuser : idenityuser{}
    public class appcontext : IdentityDbContext<applicationUser>
    {
        public appcontext()
        {

        }
        public appcontext(DbContextOptions<appcontext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source =DESKTOP-J7ATT94\\SQLEXPRESS; Initial Catalog = Vezeeta; Integrated Security = true; TrustServerCertificate = true");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "2", Name = "Patient", NormalizedName = "PATIENT" }
            );            
        }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Time> Times { get; set; }
        public DbSet<Appointement> Appointements { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        


    }
}
