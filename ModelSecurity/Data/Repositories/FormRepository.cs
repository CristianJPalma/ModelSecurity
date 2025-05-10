using Data.GenericRepository;
using Data.IRepositories;
using Entity.Context;
using Entity.Model;
using Microsoft.Extensions.Logging;

namespace Data.Repositories
{ 
    public class FormRepository : GenericRepository<Form>, IFormRepository
    {
        private readonly ILogger<FormRepository> _logger;

        public FormRepository(ApplicationDbContext context, ILogger<FormRepository> logger)
            : base(context)
        {
            _logger = logger;
        }

        public async Task<bool> PatchAsync(int id, Form form)
        {
            var existing = await _dbSet.FindAsync(id);
            if (existing == null) return false;

            if (form.Name != null) existing.Name = form.Name;
            if (form.Code != null) existing.Code = form.Code;
            if (form.Active != null) existing.Active = form.Active;

            return true;
        }

        public async Task<bool> DisableAsync(int id)
        {
            var form = await _dbSet.FindAsync(id);
            if (form == null) return false;
            form.Active = false;
            _dbSet.Update(form);
            return true;
        }
    }
}