using Entity.Context;
using Data.IRepositories;
using Data.Repositories;
using Data.GenericRepository;
using Microsoft.Extensions.Logging;
namespace Data.UnitOfWork
{ 
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly ILoggerFactory _loggerFactory;

        public IFormRepository Forms { get; }
        public IModuleRepository Modules { get; }

        public UnitOfWork(ApplicationDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _loggerFactory = loggerFactory;
            Forms = new FormRepository(_context, _loggerFactory.CreateLogger<FormRepository>());
            Modules = new ModuleRepository(_context, _loggerFactory.CreateLogger<ModuleRepository>());
        }

        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}