using Dapper;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using System.Data;
using Module = Entity.Model.Module;

using System.Reflection;

namespace Entity.Context
{
    /// <summary>
    /// Representa el contexto de la base de datos de la aplicación, proporcionando configuraciones y métodos
    /// para la gestión de entidades y consultas personalizadas con Dapper.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        protected readonly IConfiguration _configuration;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }
//tablas de la base de datos
        public DbSet<Rol> Rol { get; set; }
        public DbSet<Form> Form {get;set;}

        public DbSet<FormModule> FormModule  {get;set;}
        public DbSet<Module> Module  {get;set;}
        public DbSet<Permission> Permission  {get;set;}
        public DbSet<Person> Person  {get;set;}
        public DbSet<RolFormPermission> RolFormPermission  {get;set;}
        public DbSet<RolUser> RolUser  {get;set;}
        public DbSet<User> User  {get;set;}

              

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Person ↔ User (1:1)
            modelBuilder.Entity<Person>()
                .HasOne(p => p.User)
                .WithOne(u => u.Person)
                .HasForeignKey<User>(u => u.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            // User ↔ Rol (N:N) → RolUser
            modelBuilder.Entity<RolUser>()
                .HasOne(ru => ru.User)
                .WithMany(u => u.RolUsers)
                .HasForeignKey(ru => ru.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RolUser>()
                .HasOne(ru => ru.Rol)
                .WithMany(r => r.RolUsers)
                .HasForeignKey(ru => ru.RolId)
                .OnDelete(DeleteBehavior.Cascade);

            // Rol ↔ Form ↔ Permission (N:N con tabla intermedia RolFormPermission)
            modelBuilder.Entity<RolFormPermission>()
                .HasOne(rfp => rfp.Rol)
                .WithMany(r => r.RolFormPermissions)
                .HasForeignKey(rfp => rfp.RolId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RolFormPermission>()
                .HasOne(rfp => rfp.Form)
                .WithMany(f => f.RolFormPermissions)
                .HasForeignKey(rfp => rfp.FormId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RolFormPermission>()
                .HasOne(rfp => rfp.Permission)
                .WithMany(p => p.RolFormPermissions)
                .HasForeignKey(rfp => rfp.PermissionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Form ↔ Module (N:N con FormModule)
            modelBuilder.Entity<FormModule>()
                .HasOne(fm => fm.Form)
                .WithMany(f => f.FormModules)
                .HasForeignKey(fm => fm.FormId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FormModule>()
                .HasOne(fm => fm.Module)
                .WithMany(m => m.FormModules)
                .HasForeignKey(fm => fm.ModuleId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);

            // Aplica configuraciones con IEntityTypeConfiguration<T>
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<decimal>().HavePrecision(18, 2);
        }

        public override int SaveChanges()
        {
            EnsureAudit();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            EnsureAudit();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string text, object parameters = null, int? timeout = null, CommandType? type = null)
        {
            using var command = new DapperEFCoreCommand(this, text, parameters, timeout, type, CancellationToken.None);
            var connection = this.Database.GetDbConnection();
            return await connection.QueryAsync<T>(command.Definition);
        }

        public async Task<T?> QueryFirstOrDefaultAsync<T>(string text, object parameters = null, int? timeout = null, CommandType? type = null)
        {
            using var command = new DapperEFCoreCommand(this, text, parameters, timeout, type, CancellationToken.None);
            var connection = this.Database.GetDbConnection();
            return await connection.QueryFirstOrDefaultAsync<T>(command.Definition);
        }

        public async Task<int> ExecuteAsync(string text, object parameters = null, int? timeout = null, CommandType? type = null)
        {
            using var command = new DapperEFCoreCommand(this, text, parameters, timeout, type, CancellationToken.None);
            var connection = this.Database.GetDbConnection();
            return await connection.ExecuteAsync(command.Definition);
        }

        public async Task<T?> ExecuteScalarAsync<T>(string query, object parameters = null, int? timeout = null, CommandType? type = null)
        {
            using var command = new DapperEFCoreCommand(this, query, parameters, timeout, type, CancellationToken.None);
            var connection = this.Database.GetDbConnection();
            return await connection.ExecuteScalarAsync<T>(command.Definition);
        }

        private void EnsureAudit()
        {
            ChangeTracker.DetectChanges();
        }

        public readonly struct DapperEFCoreCommand : IDisposable
        {
            public DapperEFCoreCommand(DbContext context, string text, object parameters, int? timeout, CommandType? type, CancellationToken ct)
            {
                var transaction = context.Database.CurrentTransaction?.GetDbTransaction();
                var commandType = type ?? CommandType.Text;
                var commandTimeout = timeout ?? context.Database.GetCommandTimeout() ?? 30;

                Definition = new CommandDefinition(text, parameters, transaction, commandTimeout, commandType, cancellationToken: ct);
            }

            public CommandDefinition Definition { get; }

            public void Dispose() { }
        }
    }
}