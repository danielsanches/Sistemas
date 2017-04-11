namespace Infra.EntityConfiguration
{
    using Domain.Model;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    public class GrupoProdutoEntityConfig : EntityTypeConfiguration<GrupoProduto>
    {
        public GrupoProdutoEntityConfig()
        {
            ToTable("GrupoProduto");

            HasKey(x => x.Id);

            Property(x => x.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Descricao).HasMaxLength(60).IsRequired();
        }
    }
}
