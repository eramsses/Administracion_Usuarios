using Microsoft.EntityFrameworkCore;

namespace Administracion_Usuarios.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        //Registrar las entidades (Tablas DB)

    }
}
