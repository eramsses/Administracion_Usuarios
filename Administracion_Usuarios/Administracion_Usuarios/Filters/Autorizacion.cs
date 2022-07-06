
using Administracion_Usuarios.Data;
using Administracion_Usuarios.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace Administracion_Usuarios.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class Autorizacion : Attribute, IAuthorizationFilter
    {
        private readonly DataContext _context;
        private readonly string[] _permisos;

        //private readonly IDbContextFactory<DataContext> _contextFactory;


        public Autorizacion(string permisos)
        {
            _permisos = permisos.Split(',', StringSplitOptions.RemoveEmptyEntries);

            _context = GetContextDb();
        }

        private DataContext GetContextDb()
        {
            var buider = WebApplication.CreateBuilder();

            IServiceCollection services = new ServiceCollection();
            services.AddDbContext<DataContext>(option =>
            {
                option.UseSqlServer(buider.Configuration.GetConnectionString("PcConnectionSa"));
            });

            ServiceProvider serviceProvider = services.BuildServiceProvider();

            return serviceProvider.GetService<DataContext>();
        }

        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            Operacion op = null;
            string nop = "";
            bool accesoPermitido = false;
            foreach (string nc in _permisos)
            {
                op = _context.Operaciones.FirstOrDefault(o => o.NombreClave == nc.Trim() && o.Nombre == "Agregar Usuario");
                if (op != null)
                {
                    nop = op.NombreClave;
                    accesoPermitido = true;
                }

            }

            if (!accesoPermitido)
            {
                filterContext.Result = new RedirectResult("~/ModuloCategorias");
                Console.WriteLine("Acceso Denegado");
            }
            else
            {
                Console.WriteLine($"Entrando a {nop} con {op.NombreClave}");
            }


        }
    }
}
