using System;
using DominandoEFCore_Modulo11_Migrations.Data;
using Microsoft.EntityFrameworkCore;

namespace DominandoEFCore_Modulo11_Migrations
{
    class Program
    {
        static void Main(string[] args)
        {
            using var db = new ApplicationContext();

            //db.Database.Migrate();

            var migracoes = db.Database.GetPendingMigrations();
            foreach (var migracao in migracoes)
            {
                Console.WriteLine(migracao);
            }

            Console.WriteLine("Hello World!");
        }
    }
}
