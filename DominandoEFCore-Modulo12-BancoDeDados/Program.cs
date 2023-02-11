using DominandoEFCore_Modulo12_BancoDeDados.Data;
using Microsoft.EntityFrameworkCore;

namespace DominandoEFCore_Modulo12_BancoDeDados
{
    class Program
    {
        static void Main(string[] args)
        {
            using var db = new ApplicationContext();

            db.Database.EnsureCreated();

            db.Pessoas.Add(new DominandoEFCore_Modulo12_BancoDeDados.Domain.Pessoa
            {
                Id = 1,
                Nome = "TESTE",
                Telefone = "999999"
            });

            db.SaveChanges();

            var pessoas = db.Pessoas.ToList();
            foreach (var item in pessoas)
            {
                Console.WriteLine($"Nome: {item.Nome}");
            }

        }
    }
}
