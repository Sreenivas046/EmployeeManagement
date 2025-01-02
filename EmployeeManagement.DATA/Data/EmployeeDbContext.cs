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
            modelBuilder.Entity<Name>()
                .HasOne(c => c.Employee)               // Specify navigation property
                .WithMany(p => p.Name)           // Specify inverse navigation
                .HasForeignKey(c => c.EmpId);     // Specify foreign key property
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Name> Names { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<AddressProof> AddressProofs { get; set; }
    }
}
