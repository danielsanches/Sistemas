namespace Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PrimeiraMigracao : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cliente",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(maxLength: 60),
                        DataNascimento = c.DateTime(),
                        FoneFixo = c.String(maxLength: 12),
                        FoneMovel1 = c.String(maxLength: 12),
                        FoneMovel2 = c.String(maxLength: 12),
                        Cpf = c.String(),
                        Email = c.String(maxLength: 60),
                        Status = c.String(nullable: false, maxLength: 10),
                        DataCadastro = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Vendas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DataVenda = c.DateTime(nullable: false),
                        ValorVenda = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.String(nullable: false, maxLength: 10),
                        ClienteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cliente", t => t.ClienteId)
                .Index(t => t.ClienteId);
            
            CreateTable(
                "dbo.ItensVenda",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantidade = c.Int(nullable: false),
                        ValorItem = c.Double(nullable: false),
                        VendaId = c.Int(nullable: false),
                        ProdutoId = c.Int(),
                        Produtos_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Produto", t => t.Produtos_Id)
                .ForeignKey("dbo.Vendas", t => t.VendaId, cascadeDelete: true)
                .Index(t => t.VendaId)
                .Index(t => t.Produtos_Id);
            
            CreateTable(
                "dbo.Produto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(maxLength: 125),
                        UidadeArmazenamento = c.String(maxLength: 2),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ValorAtacado = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantidade = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.String(maxLength: 10),
                        CodigoBarra = c.String(maxLength: 255),
                        SubGrupoProdutoId = c.Int(nullable: false),
                        CompraId = c.Int(),
                        EstoqueId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Compra", t => t.CompraId)
                .ForeignKey("dbo.Estoque", t => t.EstoqueId, cascadeDelete: true)
                .ForeignKey("dbo.SubGrupoProduto", t => t.SubGrupoProdutoId, cascadeDelete: true)
                .Index(t => t.SubGrupoProdutoId)
                .Index(t => t.CompraId)
                .Index(t => t.EstoqueId);
            
            CreateTable(
                "dbo.Compra",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FornecedorId = c.Int(nullable: false),
                        DataCompra = c.DateTime(),
                        DataLancamento = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                        ValorCompra = c.Decimal(precision: 18, scale: 2),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Forncedor", t => t.FornecedorId)
                .Index(t => t.FornecedorId);
            
            CreateTable(
                "dbo.Forncedor",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NomeFantasia = c.String(nullable: false, maxLength: 100),
                        Status = c.String(),
                        FoneFixo = c.String(),
                        Celular = c.String(),
                        Email = c.String(),
                        DataCadastro = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ItensCompra",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantidade = c.Long(nullable: false),
                        ValorItem = c.Decimal(precision: 18, scale: 2),
                        ProdutoId = c.Int(),
                        CompraId = c.Int(nullable: false),
                        Produtos_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Compra", t => t.CompraId, cascadeDelete: true)
                .ForeignKey("dbo.Produto", t => t.Produtos_Id)
                .Index(t => t.CompraId)
                .Index(t => t.Produtos_Id);
            
            CreateTable(
                "dbo.Estoque",
                c => new
                    {
                        ProdutoId = c.Int(nullable: false),
                        Quantidade = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ProdutoId);
            
            CreateTable(
                "dbo.SubGrupoProduto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(),
                        Status = c.String(),
                        GrupoProdutoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GrupoProduto", t => t.GrupoProdutoId)
                .Index(t => t.GrupoProdutoId);
            
            CreateTable(
                "dbo.GrupoProduto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false, maxLength: 60),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50),
                        Senha = c.String(nullable: false),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ItensVenda", "VendaId", "dbo.Vendas");
            DropForeignKey("dbo.Produto", "SubGrupoProdutoId", "dbo.SubGrupoProduto");
            DropForeignKey("dbo.SubGrupoProduto", "GrupoProdutoId", "dbo.GrupoProduto");
            DropForeignKey("dbo.ItensVenda", "Produtos_Id", "dbo.Produto");
            DropForeignKey("dbo.Produto", "EstoqueId", "dbo.Estoque");
            DropForeignKey("dbo.Produto", "CompraId", "dbo.Compra");
            DropForeignKey("dbo.ItensCompra", "Produtos_Id", "dbo.Produto");
            DropForeignKey("dbo.ItensCompra", "CompraId", "dbo.Compra");
            DropForeignKey("dbo.Compra", "FornecedorId", "dbo.Forncedor");
            DropForeignKey("dbo.Vendas", "ClienteId", "dbo.Cliente");
            DropIndex("dbo.SubGrupoProduto", new[] { "GrupoProdutoId" });
            DropIndex("dbo.ItensCompra", new[] { "Produtos_Id" });
            DropIndex("dbo.ItensCompra", new[] { "CompraId" });
            DropIndex("dbo.Compra", new[] { "FornecedorId" });
            DropIndex("dbo.Produto", new[] { "EstoqueId" });
            DropIndex("dbo.Produto", new[] { "CompraId" });
            DropIndex("dbo.Produto", new[] { "SubGrupoProdutoId" });
            DropIndex("dbo.ItensVenda", new[] { "Produtos_Id" });
            DropIndex("dbo.ItensVenda", new[] { "VendaId" });
            DropIndex("dbo.Vendas", new[] { "ClienteId" });
            DropTable("dbo.Usuario");
            DropTable("dbo.GrupoProduto");
            DropTable("dbo.SubGrupoProduto");
            DropTable("dbo.Estoque");
            DropTable("dbo.ItensCompra");
            DropTable("dbo.Forncedor");
            DropTable("dbo.Compra");
            DropTable("dbo.Produto");
            DropTable("dbo.ItensVenda");
            DropTable("dbo.Vendas");
            DropTable("dbo.Cliente");
        }
    }
}
