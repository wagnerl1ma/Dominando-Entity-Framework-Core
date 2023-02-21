using System;
using DominandoEFCore_Modulo14_RepositoryUow.Data.Repositories;

namespace DominandoEFCore_Modulo14_RepositoryUow.Data
{
    public interface IUnitOfWork : IDisposable
    {
         bool Commit();
         IDepartamentoRepository DepartamentoRepository{get;}
    }
}