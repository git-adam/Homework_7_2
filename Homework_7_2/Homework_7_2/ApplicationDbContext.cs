using Homework_7_2.Models.Configurations;
using Homework_7_2.Models.Domains;
using Homework_7_2.Properties;
using System.Data.Entity;

namespace Homework_7_2
{
    public class ApplicationDbContext : DbContext
    {
        public static string _connectionString =
            $@"Server={Settings.Default.ServerAddress}{Settings.Default.ServerName};
            Database={Settings.Default.DatabaseName};
            User Id={Settings.Default.DatabaseLogin};
            Password={Settings.Default.DatabasePassword}";

        public ApplicationDbContext()
            : base(_connectionString)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Status> Statuses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EmployeeConfiguration());
            modelBuilder.Configurations.Add(new StatusConfiguration());
        }
    }
}