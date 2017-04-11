namespace Infra.EntityConfiguration
{
    using Domain.Model;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    public class SubGrupoProdutoEntityConfig : EntityTypeConfiguration<SubGrupoProduto>
    {
        public SubGrupoProdutoEntityConfig()
        {
            ToTable("SubGrupoProduto");

            HasKey(x => x.Id);

            Property(x => x.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(x => x.GrupoProduto).WithMany(x => x.SubGrupoProduto).HasForeignKey(x => x.GrupoProdutoId).WillCascadeOnDelete(false);
        }
    }
}
