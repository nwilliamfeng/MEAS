namespace MEAS.Data.SqlClient.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fdsfds : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TorqueWrenchMeasureSettings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TestCount = c.Int(nullable: false),
                        NominalValuesString = c.String(),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.TorqueWrenchMeasures", "Setting_Id", c => c.Int());
            CreateIndex("dbo.TorqueWrenchMeasures", "Setting_Id");
            AddForeignKey("dbo.TorqueWrenchMeasures", "Setting_Id", "dbo.TorqueWrenchMeasureSettings", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TorqueWrenchMeasures", "Setting_Id", "dbo.TorqueWrenchMeasureSettings");
            DropIndex("dbo.TorqueWrenchMeasures", new[] { "Setting_Id" });
            DropColumn("dbo.TorqueWrenchMeasures", "Setting_Id");
            DropTable("dbo.TorqueWrenchMeasureSettings");
        }
    }
}
