namespace MEAS.Data.SqlClient.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class mig4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TorqueWrenchMeasures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TestCode = c.String(maxLength: 10,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "Idx_TestCode",
                                    new AnnotationValues(oldValue: null, newValue: "IndexAnnotation: { IsUnique: True }")
                                },
                            }),
                        TestDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        TeserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.TeserId)
                .Index(t => t.TeserId);
            
            AddColumn("dbo.Users", "Avatar", c => c.Binary());
            AddColumn("dbo.Users", "Phone", c => c.String());
            AddColumn("dbo.Users", "Mobile", c => c.String(maxLength: 11));
            AddColumn("dbo.Users", "EmailAddress", c => c.String());
            AlterColumn("dbo.Users", "LoginName", c => c.String(nullable: false, maxLength: 12));
            AlterColumn("dbo.Users", "UserName", c => c.String(nullable: false, maxLength: 12));
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false, maxLength: 15));
            DropTable("dbo.UserProfiles");
        }
        
        public override void Down()
        {
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
            
            DropForeignKey("dbo.TorqueWrenchMeasures", "TeserId", "dbo.Users");
            DropIndex("dbo.TorqueWrenchMeasures", new[] { "TeserId" });
            AlterColumn("dbo.Users", "Password", c => c.String());
            AlterColumn("dbo.Users", "UserName", c => c.String());
            AlterColumn("dbo.Users", "LoginName", c => c.String());
            DropColumn("dbo.Users", "EmailAddress");
            DropColumn("dbo.Users", "Mobile");
            DropColumn("dbo.Users", "Phone");
            DropColumn("dbo.Users", "Avatar");
            DropTable("dbo.TorqueWrenchMeasures",
                removedColumnAnnotations: new Dictionary<string, IDictionary<string, object>>
                {
                    {
                        "TestCode",
                        new Dictionary<string, object>
                        {
                            { "Idx_TestCode", "IndexAnnotation: { IsUnique: True }" },
                        }
                    },
                });
        }
    }
}
