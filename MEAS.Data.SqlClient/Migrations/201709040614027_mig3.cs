namespace MEAS.Data.SqlClient.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TorqueWrenchMeasures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TestCode = c.String(),
                        TestDate = c.DateTime(nullable: false),
                        TeserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.TeserId)
                .Index(t => t.TeserId);
            
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Avatar = c.Binary(),
                        Phone = c.String(),
                        Mobile = c.String(),
                        EmailAddress = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TorqueWrenchMeasures", "TeserId", "dbo.Users");
            DropIndex("dbo.TorqueWrenchMeasures", new[] { "TeserId" });
            DropTable("dbo.UserProfiles");
            DropTable("dbo.TorqueWrenchMeasures");
        }
    }
}
