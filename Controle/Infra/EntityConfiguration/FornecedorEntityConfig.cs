namespace Infra.EntityConfiguration
{
    using Domain.Model;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    public class FornecedorEntityConfig : EntityTypeConfiguration<Fornecedor>
    {
        public FornecedorEntityConfig()
        {
            ToTable("Forncedor");

            HasKey(x => x.Id);

            Property(x => x.Email).HasMaxLength(50);

            Property(x => x.CpfCnpj).HasMaxLength(14);

            Property(x => x.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.NomeFantasia).HasMaxLength(100).IsRequired();
        }
    }
}
