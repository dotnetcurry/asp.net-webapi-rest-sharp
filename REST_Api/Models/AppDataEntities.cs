namespace REST_Api.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AppDataEntities : DbContext
    {
        public DbSet<EmployeeInfo> Employees { get; set; }
        public AppDataEntities()
            : base("name=AppDataEntities")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
