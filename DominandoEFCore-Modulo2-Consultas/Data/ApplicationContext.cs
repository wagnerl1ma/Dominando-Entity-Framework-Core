
using DominandoEFCore_Modulo2_Consultas.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DominandoEFCore_Modulo2_Consultas.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string strConnection = "Data source=(localdb)\\mssqllocaldb; Initial Catalog=DevIO-02;Integrated Security=true;pooling=true;";
            optionsBuilder
                //.UseSqlServer(strConnection,p=>p.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)) // Config Global -  para todas as query que tem relacionamento dividir a consulta
                .UseSqlServer(strConnection)
                .EnableSensitiveDataLogging()
                .LogTo(Console.WriteLine, LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Departamento>().HasQueryFilter(p=>!p.Excluido); //filtro global - nao trazer registros com excluido == true
        }
    }
}