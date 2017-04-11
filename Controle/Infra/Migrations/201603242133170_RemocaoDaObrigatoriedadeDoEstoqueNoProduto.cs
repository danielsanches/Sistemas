namespace Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemocaoDaObrigatoriedadeDoEstoqueNoProduto : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Produto", "EstoqueId", "dbo.Estoque");
            DropIndex("dbo.Produto", new[] { "EstoqueId" });
            AddColumn("dbo.Produto", "Estoque_ProdutoId", c => c.Int());
            CreateIndex("dbo.Produto", "Estoque_ProdutoId");
            AddForeignKey("dbo.Produto", "Estoque_ProdutoId", "dbo.Estoque", "ProdutoId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Produto", "Estoque_ProdutoId", "dbo.Estoque");
            DropIndex("dbo.Produto", new[] { "Estoque_ProdutoId" });
            DropColumn("dbo.Produto", "Estoque_ProdutoId");
            CreateIndex("dbo.Produto", "EstoqueId");
            AddForeignKey("dbo.Produto", "EstoqueId", "dbo.Estoque", "ProdutoId", cascadeDelete: true);
        }
    }
}
