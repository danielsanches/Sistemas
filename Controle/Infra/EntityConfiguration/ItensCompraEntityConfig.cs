namespace Infra.EntityConfiguration
{
    using Domain.Model;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    public class ItensCompraEntityConfig : EntityTypeConfiguration<ItensCompra>
    {
        public ItensCompraEntityConfig()
        {
            ToTable("ItensCompra");

            HasKey(x => x.Id);

            Property(x => x.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(x => x.Produtos).WithMany(x => x.ItensCompra).HasForeignKey(x => x.ProdutoId).WillCascadeOnDelete(false);
            HasRequired(x => x.Compra).WithMany(x => x.ItensCompra).HasForeignKey(x => x.CompraId).WillCascadeOnDelete(true);
        }
    }
}
