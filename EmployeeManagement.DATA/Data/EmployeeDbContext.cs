using EmployeeManagement.DATA.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.DATA.Data
{
    public class EmployeeDbContext : DbContext
    {

        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Specify foreign key property
            modelBuilder.Entity<Employee>()
               .HasOne(e => e.Name)
               .WithOne(n => n.Employee)
               .HasForeignKey<Name>(n => n.EmpId);
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Address)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.AddressProof)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Address>()
                .HasOne(a => a.Employee)
                .WithMany(e => e.Address)
                .HasForeignKey(a => a.EmpId)
                .OnDelete(DeleteBehavior.Cascade); // Enable cascading delete

            modelBuilder.Entity<AddressProof>()
                .HasOne(ap => ap.Employee)
                .WithMany(e => e.AddressProof)
                .HasForeignKey(ap => ap.EmpId)
                .OnDelete(DeleteBehavior.Cascade); // Enable cascading delete
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Employee>()
        //        .HasOne(e => e.Name)
        //        .WithOne(n => n.Employee)
        //        .HasForeignKey<Name>(n => n.EmployeeId);

        //    base.OnModelCreating(modelBuilder);
        //}
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Name> Names { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<AddressProof> AddressProofs { get; set; }
    }
}
