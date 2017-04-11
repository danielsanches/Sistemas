namespace Infra.EntityConfiguration
{
    using Domain.Model;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    public class VendaEntityConfig : EntityTypeConfiguration<Vendas>
    {
        public VendaEntityConfig()
        {
            ToTable("Vendas");

            HasKey(x => x.Id);

            Property(x => x.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Status).HasMaxLength(10).IsRequired();

            HasRequired(x => x.Cliente).WithMany(x => x.Vendas).HasForeignKey(x => x.ClienteId).WillCascadeOnDelete(false);

            HasRequired(x => x.ItensVenda);
        }
    }
}
