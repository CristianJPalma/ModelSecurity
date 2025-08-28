using Entity.Model;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Entity.Context
{
    public interface IApplicationDbContext
    {
        DbSet<Rol> Rol { get; }
        DbSet<Form> Form { get; }
        DbSet<FormModule> FormModule { get; }
        DbSet<Module> Module { get; }
        DbSet<Permission> Permission { get; }
        DbSet<Person> Person { get; }
        DbSet<RolFormPermission> RolFormPermission { get; }
        DbSet<RolUser> RolUser { get; }
        DbSet<User> User { get; }
        DbSet<T> Set<T>() where T : class;
        int SaveChanges();
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

        Task<IEnumerable<T>> QueryAsync<T>(string text, object parameters = null, int? timeout = null, CommandType? type = null);
        Task<T?> QueryFirstOrDefaultAsync<T>(string text, object parameters = null, int? timeout = null, CommandType? type = null);
        Task<int> ExecuteAsync(string text, object parameters = null, int? timeout = null, CommandType? type = null);
        Task<T?> ExecuteScalarAsync<T>(string query, object parameters = null, int? timeout = null, CommandType? type = null);
    }
}
