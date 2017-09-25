namespace MEAS.Data.SqlClient.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fghfgh : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TorqueWrenchMeasures", "AcceptTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TorqueWrenchMeasures", "AcceptTime");
        }
    }
}
