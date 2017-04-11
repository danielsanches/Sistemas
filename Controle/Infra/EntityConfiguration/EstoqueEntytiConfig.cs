namespace Infra.EntityConfiguration
{
    using Domain.Model;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    public class EstoqueEntytiConfig : EntityTypeConfiguration<Estoque>
    {
        public EstoqueEntytiConfig()
        {
            ToTable("Estoque");

            HasKey(x => x.ProdutoId);

            Property(x => x.ProdutoId).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}
