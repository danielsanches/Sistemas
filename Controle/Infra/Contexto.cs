namespace Infra
{
    using EntityConfiguration;
    using System.Data.Entity;

    public class Contexto : DbContext
    {
        public Contexto() : base("Name=ConnectionString") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ClienteEntytiConfig());
            modelBuilder.Configurations.Add(new EstoqueEntytiConfig());
            modelBuilder.Configurations.Add(new GrupoProdutoEntityConfig());
            modelBuilder.Configurations.Add(new SubGrupoProdutoEntityConfig());
            modelBuilder.Configurations.Add(new ProdutoEntityConfig());
            modelBuilder.Configurations.Add(new UsuarioEntityConfig());
            modelBuilder.Configurations.Add(new CompraEntityConfig());
            modelBuilder.Configurations.Add(new VendaEntityConfig());
            modelBuilder.Configurations.Add(new FornecedorEntityConfig());
            modelBuilder.Configurations.Add(new ItensVendaEntityConfig());
            modelBuilder.Configurations.Add(new ItensCompraEntityConfig());

            base.OnModelCreating(modelBuilder);
        }
    }
}
