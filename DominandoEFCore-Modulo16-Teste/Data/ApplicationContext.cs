using DominandoEFCore_Modulo15_Teste.Entities;
using Microsoft.EntityFrameworkCore;

namespace DominandoEFCore_Modulo15_Teste.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Departamento> Departamentos { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }
    }
}