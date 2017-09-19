namespace MEAS.Data.SqlClient.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmigrationbnbng : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.TorqueWrenchMeasures", name: "Measurand_Id", newName: "MeasurandId");
            RenameIndex(table: "dbo.TorqueWrenchMeasures", name: "IX_Measurand_Id", newName: "IX_MeasurandId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.TorqueWrenchMeasures", name: "IX_MeasurandId", newName: "IX_Measurand_Id");
            RenameColumn(table: "dbo.TorqueWrenchMeasures", name: "MeasurandId", newName: "Measurand_Id");
        }
    }
}
