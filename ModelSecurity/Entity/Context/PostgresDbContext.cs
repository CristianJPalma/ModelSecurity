using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Microsoft.Extensions.Configuration;
namespace Entity.Context
{
    public class PostgresDbContext : ApplicationDbContext, IApplicationDbContext
    {
        public PostgresDbContext(DbContextOptions<PostgresDbContext> options, IConfiguration config)
            : base(options, config) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("PostgresConnection"));
            base.OnConfiguring(optionsBuilder);
        }
    }
}
