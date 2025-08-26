using Data.Interfaces;
using Entity.Model;

namespace Data.Implements
{
    public abstract class ABaseData<T> : IBaseData<T> where T : BaseModel
    {
        public abstract Task<T> GetByIdAsync(int id);

        public abstract Task<IEnumerable<T>> GetAllAsync();

        public abstract Task AddAsync(T entity);

        public abstract void Update(T entity);

        public abstract void Delete(T entity);
    }
}