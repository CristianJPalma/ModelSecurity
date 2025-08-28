using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
namespace Entity.Context
{
    public class SqlServerDbContext : ApplicationDbContext, IApplicationDbContext
    {
        public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options, IConfiguration config)
            : base(options, config) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("SqlServerConnection"));
            base.OnConfiguring(optionsBuilder);
        }
    }
}
