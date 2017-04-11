namespace Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteracaoTabelaUsuarioCampoNomeUsuario : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuario", "NomeLogin", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Usuario", "NomeLogin");
        }
    }
}
