using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MEAS.Data
{
    public class SqlServerDbContext : DbContext
    {
      

        public SqlServerDbContext() : base("name=sqlserverconnstr")
        {
     
        }

    
        public DbSet<UserInfoDao> Users { get; set; }

        public DbSet<Environment> Environments { get; set; }


        public DbSet<TorqueWrenchMeasureDao> TorqueWrenchMeasures { get; set; }
       

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserInfoMap());
            modelBuilder.Configurations.Add(new EnvironmentMap());
            modelBuilder.Configurations.Add(new TorqueWrenchMeasureMap());
            base.OnModelCreating(modelBuilder);
        }

    }
}
