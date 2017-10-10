using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Reflection;

namespace MEAS.Data
{
    public class SqlServerDbContext : DbContext
    {
      

        public SqlServerDbContext() : base("name=sqlserverconnstr")
        {
            this.Database.CommandTimeout = 180;
     
            this.Database.Log = x =>
            {            
                // Console.WriteLine(x); 
            };
        }

    
        public DbSet<UserInfo> Users { get; set; }

        public DbSet<Environment> Environments { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<CustomerContact> CustomerContacts { get; set; }

        public DbSet<TorqueWrenchMeasureDao> TorqueWrenchMeasures { get; set; }

        public DbSet<TorqueWrench> TorqueWrenchs { get; set; }

        public DbSet<TorqueWrenchProduct> TorqueWrenchProducts { get; set; }

        public DbSet<TorqueStandard> TorqueStandards { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserInfoMap());
            modelBuilder.Configurations.Add(new EnvironmentMap());
            modelBuilder.Configurations.Add(new TorqueWrenchMeasureMap());
            modelBuilder.Configurations.Add(new TorqueWrenchProductMap());
            modelBuilder.Configurations.Add(new TorqueWrenchMap());
            modelBuilder.Configurations.Add(new CustomerContactMap());
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new TorqueStandardMap());
            base.OnModelCreating(modelBuilder);
        }

    }

   
}
