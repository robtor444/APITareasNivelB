using ApiTareasNivelB.Modelo;
using Microsoft.EntityFrameworkCore;

namespace ApiTareasNivelB.DbContextClass
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }

        public DbSet<Persona> Personas { get; set; }

        public DbSet<Proyecto> Proyectos{ get; set; }

        public DbSet<Tarea> Tareas{ get; set; }

    }
}
