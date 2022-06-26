using Administracion_Usuarios.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Administracion_Usuarios.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        //Registrar las entidades (Tablas DB)
        public DbSet<ModuloCategoria> ModuloCategoria { get; set; }

        public DbSet<Modulo> Modulos { get; set; }

        public DbSet<Operacion> Operaciones { get; set; }

        public DbSet<Rol> Roles { get; set; }

        public DbSet<RolOperacion> RolOperaciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Indica que el nombre es único para la tabla Country
            modelBuilder.Entity<ModuloCategoria>().HasIndex(m => m.Nombre).IsUnique();

            modelBuilder.Entity<Modulo>().HasIndex(m => m.Nombre).IsUnique();

            modelBuilder.Entity<Operacion>().HasIndex(o => o.NombreClave).IsUnique();
            


            //Indices compuestos
            //modelBuilder.Entity<Operacion>().HasIndex("Nombre", "ModuloId").IsUnique();
            
        }

        
    }
}
