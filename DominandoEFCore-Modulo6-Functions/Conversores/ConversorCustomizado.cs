using System;
using System.Linq;
using DominandoEFCore_Modulo6_Functions.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DominandoEFCore_Modulo6_Functions.Conversores
{
    public class ConversorCustomizado : ValueConverter<Status, string>
    {
        public ConversorCustomizado() : base(
            p=>ConverterParaOhBancoDeDados(p),
            value => ConverterParaAplicacao(value),
            new ConverterMappingHints(1))
        {
            
        }

        static string ConverterParaOhBancoDeDados(Status status)
        {
            return status.ToString()[0..1];
        }

        static Status ConverterParaAplicacao(string value)
        {
            var status = Enum
                .GetValues<Status>()
                .FirstOrDefault(p=>p.ToString()[0..1] == value);

            return status;
        }
    }
}