namespace Infra.EntityConfiguration
{
    using Domain.Model;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    public class ClienteEntytiConfig : EntityTypeConfiguration<Cliente>
    {
        public ClienteEntytiConfig()
        {
            ToTable("Cliente");
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Nome).HasMaxLength(60);
            Property(x => x.Status).HasMaxLength(10).IsRequired();
            Property(x => x.FoneMovel1).HasMaxLength(12);
            Property(x => x.FoneMovel2).HasMaxLength(12);
            Property(x => x.FoneFixo).HasMaxLength(12);
            Property(x => x.Email).HasMaxLength(60);
        }
    }
}
