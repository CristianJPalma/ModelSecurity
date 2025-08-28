using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
namespace Entity.Context
{
    public class MySqlDbContext : ApplicationDbContext, IApplicationDbContext
    {
        public MySqlDbContext(DbContextOptions<MySqlDbContext> options, IConfiguration config)
            : base(options, config) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                _configuration.GetConnectionString("MySqlConnection"),
                new MySqlServerVersion(new Version(8, 0, 36))
            );
            base.OnConfiguring(optionsBuilder);
        }
    }
}
