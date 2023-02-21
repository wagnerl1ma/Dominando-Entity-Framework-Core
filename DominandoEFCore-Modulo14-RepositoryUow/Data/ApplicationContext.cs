using DominandoEFCore_Modulo14_RepositoryUow.Domain;
using Microsoft.EntityFrameworkCore;

namespace DominandoEFCore_Modulo14_RepositoryUow.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Departamento> Departamentos {get;set;}
        public DbSet<Colaborador> Colaboradores {get;set;}

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {            
        }
    }
}