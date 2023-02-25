using System.Threading.Tasks;
using DominandoEFCore_Modulo14_RepositoryUow.Data.Repositories.Base;
using DominandoEFCore_Modulo14_RepositoryUow.Domain;
using Microsoft.EntityFrameworkCore;

namespace DominandoEFCore_Modulo14_RepositoryUow.Data.Repositories
{
    public class DepartamentoRepository : GenericRepository<Departamento>, IDepartamentoRepository
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<Departamento> _dbSet;

        public DepartamentoRepository(ApplicationContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Departamento>();
        }

        public void Add(Departamento departamento)
        {
            _dbSet.Add(departamento);
        }

        public async Task<Departamento> GetByIdAsync(int id)
        {
            return await _dbSet.Include(p => p.Colaboradores).FirstOrDefaultAsync(p => p.Id == id);
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }


    }
}