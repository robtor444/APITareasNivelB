using ApiTareasNivelB.DbContextClass;
using ApiTareasNivelB.DTO;
using ApiTareasNivelB.Modelo;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiTareasNivelB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectosController : ControllerBase
    {

        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ProyectosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<ProyectoDTO>>> TraerProyectos()
        {
            var listaProyectos = await context.Proyectos.ToListAsync();

            var listaDTO = mapper.Map<List<ProyectoDTO>>(listaProyectos);

            return Ok(listaDTO);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProyectoDTO>> TraerProyecto(int id)
        {
            var listaProyectos = await context.Proyectos.FindAsync(id);


            var listaDTO = mapper.Map<ProyectoDTO>(listaProyectos);

            return Ok(listaDTO);
        }

        [HttpGet("exacto/{nombre}")]
        public async Task<ActionResult<ProyectoDTO>> TraerPersonPorNombre(string nombre)
        {
            var personaEncontrada = await context.Proyectos
                .FirstOrDefaultAsync(personaDb => personaDb.Nombre == nombre);


            if (personaEncontrada == null)
            {
                return NotFound("Proyecto no encontrada");
            }

            var personaDto = mapper.Map<ProyectoDTO>(personaEncontrada);

            return Ok(personaDto);


        }

        [HttpGet("inexacto/{nombre}")]
        public async Task<ActionResult<List<ProyectoDTO>>> TraerListaProyectoPorNombre(string nombre)
        {
            var personaEncontrada = await context.Proyectos
                .Where(personaDb => personaDb.Nombre.Contains(nombre))
                .ToListAsync();


            if (personaEncontrada == null || personaEncontrada.Count <= 0)
            {
                return NotFound("Proyecto no encontrada");
            }

            var personaDto = mapper.Map<List<ProyectoDTO>>(personaEncontrada);

            return Ok(personaDto);


        }


        [HttpPost]
        public async Task<ActionResult<ProyectoDTO>> CrearProyectos(ProyectoCreacionDTO personaNuevaDTO)
        {

            //var personaNueva = new Proyecto
            //{
            //    Nombre=personaNuevaDTO.Nombre,
            //    Cargo=personaNuevaDTO.Cargo,
            //    Telefono=personaNuevaDTO.Telefono,
            //    Correo=personaNuevaDTO.Correo,
            //    CI=personaNuevaDTO.CI
            //};

            var personaNueva = mapper.Map<Proyecto>(personaNuevaDTO);
            await context.Proyectos.AddAsync(personaNueva);
            await context.SaveChangesAsync();

            return Ok("Proyecto Creada");
        }

        [HttpPut]
        public async Task<ActionResult<ProyectoDTO>> EditarProyectos(ProyectoCreacionDTO personaEditadaDTO, int id)
        {


            var personaEditada = mapper.Map<Proyecto>(personaEditadaDTO);

            personaEditada.Id = id;

            var existe = await context.Proyectos.AnyAsync(personaEditada => personaEditada.Id == id);

            if (!existe)
            {
                return NotFound("No se puede editar este usuario por que no existe");
            }

            context.Proyectos.Update(personaEditada);
            await context.SaveChangesAsync();
            var respuestaEdicion = mapper.Map<ProyectoDTO>(personaEditada);

            return Ok(respuestaEdicion);
        }

        [HttpDelete]
        public async Task<ActionResult> EliminarProyectos(int id)
        {
            var existe = await context.Proyectos.AnyAsync(persona => persona.Id == id);

            if (!existe)
            {
                return NotFound("No se puede borrar este usuario por que no existe");
            }

            var personaHaBorrar = new Proyecto { Id = id };
            context.Proyectos.Remove(personaHaBorrar);
            await context.SaveChangesAsync();

            return Ok("Proyecto Eliminada con exito");
        }
    }
}
