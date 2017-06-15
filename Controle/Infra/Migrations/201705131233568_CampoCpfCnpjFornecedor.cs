namespace Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CampoCpfCnpjFornecedor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Forncedor", "CpfCnpj", c => c.String(maxLength: 14));
            AlterColumn("dbo.Forncedor", "Email", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Forncedor", "Email", c => c.String());
            DropColumn("dbo.Forncedor", "CpfCnpj");
        }
    }
}
