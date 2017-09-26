namespace MEAS.Data.SqlClient.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fkg : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TorqueWrenchMeasures", "AcceptTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TorqueWrenchMeasures", "AcceptTime", c => c.DateTime(nullable: false));
        }
    }
}
