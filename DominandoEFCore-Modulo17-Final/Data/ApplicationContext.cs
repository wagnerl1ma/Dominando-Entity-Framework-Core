using System;
using DominandoEFCore_Modulo17_Final.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace DominandoEFCore_Modulo17_Final.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Departamento> Departamentos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                //.LogTo(Console.WriteLine)
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=sobrescrevendo_comportamento;Integrated Security=true")
                //.ReplaceService<IQuerySqlGeneratorFactory, MySqlServerQuerySqlGeneratorFactory>()
                .EnableSensitiveDataLogging();
        }
    }
}