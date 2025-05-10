using Data.GenericRepository;
using Entity.Model;

namespace Data.IRepositories
{ 
    public interface IFormRepository : IGenericRepository<Form>
    {
        Task<bool> PatchAsync(int id, Form form);
        Task<bool> DisableAsync(int id);
    }
}