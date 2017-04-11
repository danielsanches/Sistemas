namespace Infra.EntityConfiguration
{
    using Domain.Model;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    public class ProdutoEntityConfig : EntityTypeConfiguration<Produtos>
    {
        public ProdutoEntityConfig()
        {
            ToTable("Produto");

            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.UnidadeArmazenamento).HasMaxLength(2);
            Property(x => x.Status).HasMaxLength(10);
            Property(x => x.Descricao).HasMaxLength(125);
            Property(x => x.CodigoBarra).HasMaxLength(255);
        }
    }
}
