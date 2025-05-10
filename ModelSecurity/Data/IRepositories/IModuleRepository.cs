using Data.GenericRepository;
using Entity.Model;

namespace Data.IRepositories
{ 
    public interface IModuleRepository : IGenericRepository<Module>
    {
        Task<bool> PatchAsync(int id, Module module);
        Task<bool> DisableAsync(int id);
    }
}