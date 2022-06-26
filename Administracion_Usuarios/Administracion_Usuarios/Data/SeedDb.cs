using Administracion_Usuarios.Data.Entities;

namespace Administracion_Usuarios.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            //Crear la base de datos en la primera ejecución
            await _context.Database.EnsureCreatedAsync();
            await CheckCategoriasModulosAsync();
            //await CheckModulosAsync();
            //await CheckOperacionesAsync();
        }

        private Task CheckOperacionesAsync()
        {
            throw new NotImplementedException();
        }

        private Task CheckModulosAsync()
        {
            throw new NotImplementedException();
        }

        private async Task CheckCategoriasModulosAsync()
        {
            if (!_context.ModuloCategoria.Any())
            {
                _context.ModuloCategoria.Add(
                    new ModuloCategoria()
                    {
                        Nombre = "Sistema",
                        Descripcion = "Permisos asociados a la administración del sistema.",
                        Modulos = new List<Modulo>()
                        {
                            new Modulo()
                            {
                                Nombre = "Categorías de Módulos",
                                Descripcion = "Se definen las categorías para los módulos del sistema.",
                                Operaciones = new List<Operacion>()
                                {
                                    new Operacion()
                                    {
                                        Nombre = "Ver",
                                        NombreClave = "categoria_modulo_ver",
                                        Descripcion = "Permite visualizar el catálogo de categorías de módulos."
                                    },
                                    new Operacion()
                                    {
                                        Nombre="Agregar",
                                        NombreClave = "categoria_modulo_agregar",
                                        Descripcion = "Permite agregar una nueva categoría de módulos."
                                    },
                                    new Operacion()
                                    {
                                        Nombre="Modificar",
                                        NombreClave = "categoria_modulo_modificar",
                                        Descripcion = "Permite modificar la información de una categoría."
                                    },
                                    new Operacion()
                                    {
                                        Nombre="Eliminar",
                                        NombreClave = "categoria_modulo_eliminar",
                                        Descripcion = "Permite eliminar una categoría que no está en uso."
                                    }
                                }
                            },
                            new Modulo()
                            {
                                Nombre = "Módulos",
                                Descripcion = "Se definen los módulos desarrollados para el sistema.",
                                Operaciones = new List<Operacion>()
                                {
                                    new Operacion()
                                    {
                                        Nombre="Ver",
                                        NombreClave = "modulos_ver",
                                        Descripcion = "Permite visualizar el catálogo de módulos."
                                    },
                                    new Operacion()
                                    {
                                        Nombre="Agregar",
                                        NombreClave = "modulos_agregar",
                                        Descripcion = "Permite agregar un nuevo módulos."
                                    },
                                    new Operacion()
                                    {
                                        Nombre="Modificar",
                                        NombreClave = "modulos_modificar",
                                        Descripcion = "Permite modificar la información de un módulo."
                                    },
                                    new Operacion()
                                    {
                                        Nombre="Eliminar",
                                        NombreClave = "modulos_eliminar",
                                        Descripcion = "Permite eliminar un módulo que no está en uso."
                                    }
                                }
                            },

                            new Modulo()
                            {
                                Nombre = "Operaciones",
                                Descripcion = "Se definen operaciones de los módulos desarrollados para el sistema.",
                                Operaciones = new List<Operacion>()
                                {
                                    new Operacion()
                                    {
                                        Nombre="Ver",
                                        NombreClave = "operaciones_ver",
                                        Descripcion = "Permite visualizar el catálogo de operaciones de un módulo."
                                    },
                                    new Operacion()
                                    {
                                        Nombre="Agregar",
                                        NombreClave = "operaciones_agregar",
                                        Descripcion = "Permite agregar una nueva operación."
                                    },
                                    new Operacion()
                                    {
                                        Nombre="Modificar",
                                        NombreClave = "operaciones_modificar",
                                        Descripcion = "Permite modificar la información de una operación."
                                    },
                                    new Operacion()
                                    {
                                        Nombre="Eliminar",
                                        NombreClave = "operaciones_eliminar",
                                        Descripcion = "Permite eliminar una operación que no está en uso."
                                    }
                                }
                            },

                        }
                    }
                );









            }


            await _context.SaveChangesAsync();

        }
    }
}
