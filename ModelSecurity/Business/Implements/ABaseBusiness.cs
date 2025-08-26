using Business.Interfaces;
using Entity.DTOs;
using Entity.Model;

namespace Business.Implements
{
    public abstract class ABaseBusiness<T, D> : IBaseBusiness<T, D> where T : BaseModel where D : BaseDto
    {
        public abstract Task<IEnumerable<D>> GetAllAsync();
        public abstract Task<D> GetByIdAsync(int id);
        public abstract Task<D> CreateAsync(D dto);
        public abstract Task<D> UpdateAsync(D dto);
        public abstract Task<bool> DeleteAsync(int id);
    }
}