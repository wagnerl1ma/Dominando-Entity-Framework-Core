using DominandoEFCore_Modulo4_ModeloDados.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DominandoEFCore_Modulo4_ModeloDados.Configurations
{
    public class AtorFilmeConfiguration : IEntityTypeConfiguration<Ator>
    {
        public void Configure(EntityTypeBuilder<Ator> builder)
        {
            // Junçao da classes Ator e Filme
            //Configuracao muitos para muitos e criando uma tabela "FilmeAtores" automaticamente (entity) 

            /*builder
                .HasMany(p=>p.Filmes)
                .WithMany(p=>p.Atores)
                .UsingEntity(p=>p.ToTable("AtoresFilmes"));*/

            // config da tabela FilmeAtores que foi criada pelo entity
            builder
                .HasMany(p => p.Filmes)
                .WithMany(p => p.Atores)
                .UsingEntity<Dictionary<string, object>>(
                    "FilmesAtores",
                    p => p.HasOne<Filme>().WithMany().HasForeignKey("FilmeId"),
                    p => p.HasOne<Ator>().WithMany().HasForeignKey("AtorId"),
                    p => { p.Property<DateTime>("CadastradoEm").HasDefaultValueSql("GETDATE()"); }  // criacao de um novo campo eu uma tabela criada automatica pelo entity
                );
        }
    }
}