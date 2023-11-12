using System.Reflection;
using ClassManager.Business.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClassManager.Data
{
    public class ClassManagerDbContext : DbContext
    {
        public DbSet<Usuario>? Usuarios;

        public ClassManagerDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // foreach(var x in modelBuilder.Model.GetEntityTypes())
            // {
            //     var properties = x.GetProperties().Where(p => p.GetMaxLength() == null && p.ClrType == typeof(string));
            //     foreach(var property in properties)
            //         property.SetMaxLength(100);
            // }
        }
    }
}