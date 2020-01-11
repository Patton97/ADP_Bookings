using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Text;

namespace ADP_Bookings.Models
{
    public class ADP_DBContext : DbContext, IADP_DBContext
    {
        public ADP_DBContext() : base("name=conString")
        {
            //  Database.SetInitializer(new MigrateDatabaseToLatestVersion<MyDBEntities);            
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Activity> Activities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<ADP_DBContext>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}
