namespace SculptorWebApi.SculptorMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PredictiveModelModelData : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PredictiveModels", "ModelData", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PredictiveModels", "ModelData");
        }
    }
}
