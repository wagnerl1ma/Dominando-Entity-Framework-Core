using DominandoEFCore_Modulo4_ModeloDados.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DominandoEFCore_Modulo4_ModeloDados.Configurations
{
    public class EstadoConfiguration : IEntityTypeConfiguration<Estado>
    {
        public void Configure(EntityTypeBuilder<Estado> builder)
        {
            //Relacionamento um para um - Tem um Governador com um Estado
            builder.HasOne(p=>p.Governador)
                .WithOne(p=>p.Estado)
                .HasForeignKey<Governador>(p=>p.EstadoId);

            //Relacionamento de Forma Automatica
            builder.Navigation(p=>p.Governador).AutoInclude(); //Inlcuindo o AutoInlcuide() na chamada da consulta de um estado para já vir relacionado um Governado. Sendo assim nao é necessário informar o include em outras chamadas

            //Relacionamento de muitos para um 
             builder.HasMany(p=>p.Cidades)
                .WithOne(p=>p.Estado)
                .IsRequired(false);
                //.OnDelete(DeleteBehavior.Restrict);
        }
    }
}