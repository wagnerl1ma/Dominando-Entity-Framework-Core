using System;
using DominandoEFCore_Modulo6_Functions.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DominandoEFCore_Modulo6_Functions.Configurations
{
    public class DocumentoConfiguration : IEntityTypeConfiguration<Documento>
    {
        public void Configure(EntityTypeBuilder<Documento> builder)
        {
             builder.Property("_cpf").HasColumnName("CPF").HasMaxLength(11);
                //.HasField("_cpf");
        }
    }
}