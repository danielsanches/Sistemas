namespace Infra.EntityConfiguration
{
    using Domain.Model;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    public class UsuarioEntityConfig : EntityTypeConfiguration<Usuario>
    {
        public UsuarioEntityConfig()
        {
            ToTable("Usuario");

            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Nome).IsRequired().HasMaxLength(50);
            Property(x => x.Senha).IsRequired();
        }
    }
}
