namespace Infra.EntityConfiguration
{
    using Domain.Model;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    public class ItensVendaEntityConfig : EntityTypeConfiguration<ItensVenda>
    {
        public ItensVendaEntityConfig()
        {
            ToTable("ItensVenda");

            HasKey(x => x.Id);

            Property(x => x.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(x => x.Produtos).WithMany(x => x.ItensVenda).HasForeignKey(x => x.ProdutoId).WillCascadeOnDelete(false);
            HasRequired(x => x.Vendas).WithMany(x => x.ItensVenda).HasForeignKey(x => x.VendaId).WillCascadeOnDelete(true);
        }
    }
}
