namespace MEAS.Data.SqlClient.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _45 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerContacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Sex = c.Boolean(nullable: false),
                        Phone = c.String(),
                        Mobile = c.String(),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        ContactPhone = c.String(),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Environments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Time = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Humidity = c.Double(nullable: false),
                        Temperature = c.Double(nullable: false),
                        Address = c.String(nullable: false),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TorqueWrenchMeasures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TestCode = c.String(maxLength: 12),
                        Tester = c.String(nullable: false),
                        Checker = c.String(),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        EnvironmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Environments", t => t.EnvironmentId)
                .Index(t => t.TestCode, unique: true, name: "Idx_TestCode")
                .Index(t => t.EnvironmentId);
            
            CreateTable(
                "dbo.TorqueWrenchProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Model = c.String(nullable: false),
                        Manufacturer = c.String(nullable: false),
                        MinRange = c.Double(nullable: false),
                        MaxRange = c.Double(nullable: false),
                        WorkDirection = c.Int(nullable: false),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TorqueWrenchs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ManufactureDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        SerialNumber = c.String(nullable: false),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        OwnerId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.OwnerId)
                .ForeignKey("dbo.TorqueWrenchProducts", t => t.ProductId)
                .Index(t => t.OwnerId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LoginName = c.String(nullable: false, maxLength: 12),
                        UserName = c.String(nullable: false, maxLength: 12),
                        Password = c.String(nullable: false, maxLength: 15),
                        Roles = c.String(),
                        Avatar = c.Binary(),
                        Phone = c.String(),
                        Mobile = c.String(maxLength: 11),
                        EmailAddress = c.String(),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TorqueWrenchs", "ProductId", "dbo.TorqueWrenchProducts");
            DropForeignKey("dbo.TorqueWrenchs", "OwnerId", "dbo.Customers");
            DropForeignKey("dbo.TorqueWrenchMeasures", "EnvironmentId", "dbo.Environments");
            DropForeignKey("dbo.CustomerContacts", "CustomerId", "dbo.Customers");
            DropIndex("dbo.TorqueWrenchs", new[] { "ProductId" });
            DropIndex("dbo.TorqueWrenchs", new[] { "OwnerId" });
            DropIndex("dbo.TorqueWrenchMeasures", new[] { "EnvironmentId" });
            DropIndex("dbo.TorqueWrenchMeasures", "Idx_TestCode");
            DropIndex("dbo.CustomerContacts", new[] { "CustomerId" });
            DropTable("dbo.Users");
            DropTable("dbo.TorqueWrenchs");
            DropTable("dbo.TorqueWrenchProducts");
            DropTable("dbo.TorqueWrenchMeasures");
            DropTable("dbo.Environments");
            DropTable("dbo.Customers");
            DropTable("dbo.CustomerContacts");
        }
    }
}
