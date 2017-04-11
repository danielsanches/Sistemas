namespace Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrecaoNomeCampoEAlteracaoNoTipoCampo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Produto", "UnidadeArmazenamento", c => c.String(maxLength: 2));
            AlterColumn("dbo.ItensVenda", "ValorItem", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Produto", "UidadeArmazenamento");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Produto", "UidadeArmazenamento", c => c.String(maxLength: 2));
            AlterColumn("dbo.ItensVenda", "ValorItem", c => c.Double(nullable: false));
            DropColumn("dbo.Produto", "UnidadeArmazenamento");
        }
    }
}
