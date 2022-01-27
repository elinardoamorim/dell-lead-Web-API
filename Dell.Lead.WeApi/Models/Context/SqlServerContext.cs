using Microsoft.EntityFrameworkCore;

namespace Dell.Lead.WeApi.Models.Context
{
    public class SqlServerContext : DbContext
    {
        public SqlServerContext() { }

        public SqlServerContext (DbContextOptions options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(a => a.Address).WithOne(a => a.Employee).HasForeignKey<Employee>(a => a.AddressId);
        }
    }
}
