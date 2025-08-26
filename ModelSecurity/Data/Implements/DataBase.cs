using Data.Implements;
using Entity.Context;
using Entity.Model;
using Microsoft.EntityFrameworkCore;

namespace Data.Implements
{
    public class DataBase<T> : ABaseData<T> where T : BaseModel
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;
        public DataBase(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public override async Task<T> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        public override async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public override async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

        public override void Update(T entity) => _dbSet.Update(entity);

        public  override void Delete(T entity) => _dbSet.Remove(entity);
    } 
}