using DominandoEFCore_Modulo7_Interceptacao.Data;
using DominandoEFCore_Modulo7_Interceptacao.Domain;
using Microsoft.EntityFrameworkCore;

namespace DominandoEFCore_Modulo7_Interceptacao
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TesteInterceptacao();
            //TesteInterceptacaoSaveChanges();
        }

        static void TesteInterceptacaoSaveChanges()
        {
            using (var db = new ApplicationContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                db.Funcoes.Add(new Funcao
                {
                    Descricao1 = "Teste"
                });

                db.SaveChanges();
            }
        }

        static void TesteInterceptacao()
        {
            using (var db = new ApplicationContext())
            {
                var consulta = db
                    .Funcoes
                    .TagWith("Use NOLOCK")
                    .FirstOrDefault();

                Console.WriteLine($"Consulta: {consulta?.Descricao1}");
            }
        }
    }
}