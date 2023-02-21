using System.Threading.Tasks;
using DominandoEFCore_Modulo14_RepositoryUow.Data.Repositories.Base;
using DominandoEFCore_Modulo14_RepositoryUow.Domain;

namespace DominandoEFCore_Modulo14_RepositoryUow.Data.Repositories
{
    public interface IDepartamentoRepository : IGenericRepository<Departamento>
    {
        //Task<Departamento> GetByIdAsync(int id); 
        //void Add(Departamento departamento);
        //bool Save();
    }
}