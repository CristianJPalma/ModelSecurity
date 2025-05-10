using Data.GenericRepository;
using Data.IRepositories;
using Entity.Context;
using Entity.Model;
using Microsoft.Extensions.Logging;

namespace Data.Repositories
{
    public class ModuleRepository : GenericRepository<Module>, IModuleRepository
    {
        private readonly ILogger<ModuleRepository> _logger;

        public ModuleRepository(ApplicationDbContext context, ILogger<ModuleRepository> logger)
            : base(context)
        {
            _logger = logger;
        }
        public async Task<bool> PatchAsync(int id, Module module)
        {
            var existingModule = await _dbSet.FindAsync(id);
            if (existingModule == null) return false;

            if (module.Name != null) existingModule.Name = module.Name;
            if (module.Active != null) existingModule.Active = module.Active;

            return true;
        }

        public async Task<bool> DisableAsync(int id)
        {
            var module = await _dbSet.FindAsync(id);
            if (module == null) return false;
            module.Active = false;
            _dbSet.Update(module);
            return true;
        }
    }
}