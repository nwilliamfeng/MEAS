namespace MEAS.Data.SqlClient.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gn : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TorqueStandards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Model = c.String(),
                        MinRange = c.Double(nullable: false),
                        MaxRange = c.Double(nullable: false),
                        CertificateName = c.String(),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.TorqueWrenchMeasures", "Data", c => c.String());
            AddColumn("dbo.TorqueWrenchMeasures", "MeasurandId", c => c.Int(nullable: false));
            AddColumn("dbo.TorqueWrenchMeasures", "StandardId", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "Roles", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Mobile", c => c.String());
            CreateIndex("dbo.TorqueWrenchMeasures", "MeasurandId");
            CreateIndex("dbo.TorqueWrenchMeasures", "StandardId");
            AddForeignKey("dbo.TorqueWrenchMeasures", "MeasurandId", "dbo.TorqueWrenchs", "Id");
            AddForeignKey("dbo.TorqueWrenchMeasures", "StandardId", "dbo.TorqueStandards", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TorqueWrenchMeasures", "StandardId", "dbo.TorqueStandards");
            DropForeignKey("dbo.TorqueWrenchMeasures", "MeasurandId", "dbo.TorqueWrenchs");
            DropIndex("dbo.TorqueWrenchMeasures", new[] { "StandardId" });
            DropIndex("dbo.TorqueWrenchMeasures", new[] { "MeasurandId" });
            AlterColumn("dbo.Users", "Mobile", c => c.String(maxLength: 11));
            AlterColumn("dbo.Users", "Roles", c => c.String());
            DropColumn("dbo.TorqueWrenchMeasures", "StandardId");
            DropColumn("dbo.TorqueWrenchMeasures", "MeasurandId");
            DropColumn("dbo.TorqueWrenchMeasures", "Data");
            DropTable("dbo.TorqueStandards");
        }
    }
}
