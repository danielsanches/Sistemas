namespace Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CamposValorVendaEPercentualVendaProduto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Produto", "PercentualVenda", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Produto", "ValorVenda", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Produto", "ValorVenda");
            DropColumn("dbo.Produto", "PercentualVenda");
        }
    }
}
