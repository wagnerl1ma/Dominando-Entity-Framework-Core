using DominandoEFCore_Modulo4_ModeloDados.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DominandoEFCore_Modulo4_ModeloDados.Conversores
{
    public class ConversorCustomizado : ValueConverter<Status, string>
    {
        public ConversorCustomizado() : base(p => ConverterParaOhBancoDeDados(p), value => ConverterParaAplicacao(value), new ConverterMappingHints(1)) //ConverterMappingHints = tamanho da propriedade customizada
        {

        }

        static string ConverterParaOhBancoDeDados(Status status)
        {
            return status.ToString()[0..1]; //capturando a primeira letra
        }

        static Status ConverterParaAplicacao(string value)
        {
            var status = Enum.GetValues<Status>().FirstOrDefault(p => p.ToString()[0..1] == value);

            return status;
        }
    }
}