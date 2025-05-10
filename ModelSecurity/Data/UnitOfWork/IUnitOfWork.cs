
using Data.IRepositories;

namespace Data.UnitOfWork
{ 
    public interface IUnitOfWork : IDisposable
    {
        IFormRepository Forms { get; }
        IModuleRepository Modules { get; }
        Task<int> SaveAsync();
    }
}