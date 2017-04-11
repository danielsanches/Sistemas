namespace Infra.EntityConfiguration
{
    using Domain.Model;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    public class CompraEntityConfig : EntityTypeConfiguration<Compra>
    {
        public CompraEntityConfig()
        {
            ToTable("Compra");

            HasKey(x => x.Id);

            Property(x => x.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.DataLancamento).IsRequired();

            HasRequired(x => x.ItensCompra);

            HasRequired(x => x.Fornecedor).WithMany(x => x.Compra).HasForeignKey(x => x.FornecedorId).WillCascadeOnDelete(false);
        }
    }
}
