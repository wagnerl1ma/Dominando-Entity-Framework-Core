using DominandoEFCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominandoEFCore.Data
{
    public class ApplicationContextCidade : DbContext
    {
        public DbSet<Cidade> Cidades { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string strConnection = "Data source=(localdb)\\mssqllocaldb; Initial Catalog=C002;Integrated Security=true;pooling=true;";

            optionsBuilder
                .UseSqlServer(strConnection)
                .EnableSensitiveDataLogging()
                //.UseLazyLoadingProxies()
                .LogTo(Console.WriteLine, LogLevel.Information);
        }
    }
}
