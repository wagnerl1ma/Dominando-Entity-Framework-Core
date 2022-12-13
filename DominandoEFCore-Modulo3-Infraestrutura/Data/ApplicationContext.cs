using DominandoEFCore_Modulo3_Infraestrutura.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace DominandoEFCore_Modulo3_Infraestrutura.Data
{
    public class ApplicationContext : DbContext
    {
        //static string? caminho = Path.GetDirectoryName(Directory.GetCurrentDirectory());
        static string? caminho = "C:\\Users\\Wagner\\Desktop\\DESENVOLVEDOR.IO\\ENTITY FRAMEWORK CORE\\Dominando o Entity Framework Core\\Projeto\\DominandoEFCore\\DominandoEFCore-Modulo3-Infraestrutura";
        private readonly StreamWriter _writer = new StreamWriter($"{caminho}\\meu_log_do_ef_core.txt", append: true); // append: acrescenta todo vez mais informaçoes no mesmo arquivo
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string strConnection = "Data source=(localdb)\\mssqllocaldb; Initial Catalog=DevIO-02;Integrated Security=true;pooling=true;";

            optionsBuilder
                .UseSqlServer(strConnection, o => o.MaxBatchSize(100).CommandTimeout(5).EnableRetryOnFailure(4, TimeSpan.FromSeconds(10), null))
                //.LogTo(Console.WriteLine, LogLevel.Information); //inserindo log no console
                .LogTo(_writer.WriteLine, LogLevel.Information); //Gravando logs em um arquivo de texto
                /*.LogTo(Console.WriteLine, new[] { CoreEventId.ContextInitialized, RelationalEventId.CommandExecuted},
                                                  LogLevel.Information, DbContextLoggerOptions.LocalTime | DbContextLoggerOptions.SingleLine) //filtrando eventos do log
                .EnableSensitiveDataLogging();*/
        }

        public override void Dispose()
        {
            base.Dispose();
            _writer.Dispose();
        }
    }
}