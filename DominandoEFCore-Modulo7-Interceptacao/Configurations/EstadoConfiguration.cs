using System;
using DominandoEFCore_Modulo7_Interceptacao.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DominandoEFCore_Modulo7_Interceptacao.Configurations
{
    public class EstadoConfiguration : IEntityTypeConfiguration<Estado>
    {
        public void Configure(EntityTypeBuilder<Estado> builder)
        {
            builder
                .HasOne(p=>p.Governador)
                .WithOne(p=>p.Estado)
                .HasForeignKey<Governador>(p=>p.EstadoReference);

            builder.Navigation(p=>p.Governador).AutoInclude();

             builder
                .HasMany(p=>p.Cidades)
                .WithOne(p=>p.Estado)
                .IsRequired(false);
                //.OnDelete(DeleteBehavior.Restrict);
        }
    }
}