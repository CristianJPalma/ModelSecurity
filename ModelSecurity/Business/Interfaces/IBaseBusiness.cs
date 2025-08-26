using Entity.DTOs;
using Entity.Model;

namespace Business.Interfaces
{
    public interface IBaseBusiness<T, D> where T : BaseModel where D : BaseDto
    {
        Task<IEnumerable<D>> GetAllAsync();
        Task<D> GetByIdAsync(int id);
        Task<D> CreateAsync(D dto);
        Task<D> UpdateAsync(D dto);
        Task<bool> DeleteAsync(int id);
    }
    
}