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
    public class TareasController : ControllerBase
    {

        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public TareasController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<TareaDTO>>> TraerTareas()
        {
            var listaTareas = await context.Tareas.ToListAsync();

            var listaDTO = mapper.Map<List<TareaDTO>>(listaTareas);

            return Ok(listaDTO);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TareaDTO>> TraerTarea(int id)
        {
            var listaTareas = await context.Tareas.FindAsync(id);


            var listaDTO = mapper.Map<TareaDTO>(listaTareas);

            return Ok(listaDTO);
        }

        [HttpGet("exacto/{titulo}")]
        public async Task<ActionResult<TareaDTO>> TraerPersonPorNombre(string titulo)
        {
            var personaEncontrada = await context.Tareas
                .FirstOrDefaultAsync(personaDb => personaDb.Titulo == titulo);


            if (personaEncontrada == null)
            {
                return NotFound("Tarea no encontrada");
            }

            var personaDto = mapper.Map<TareaDTO>(personaEncontrada);

            return Ok(personaDto);


        }

        [HttpGet("inexacto/{titulo}")]
        public async Task<ActionResult<List<TareaDTO>>> TraerListaTareaPorNombre(string titulo)
        {
            var personaEncontrada = await context.Tareas
                .Where(personaDb => personaDb.Titulo.Contains(titulo))
                .ToListAsync();


            if (personaEncontrada == null || personaEncontrada.Count <= 0)
            {
                return NotFound("Tarea no encontrada");
            }

            var personaDto = mapper.Map<List<TareaDTO>>(personaEncontrada);

            return Ok(personaDto);


        }


        [HttpPost]
        public async Task<ActionResult<TareaDTO>> CrearTareas(TareaCreacionDTO personaNuevaDTO)
        {

            //var personaNueva = new Tarea
            //{
            //    Nombre=personaNuevaDTO.Nombre,
            //    Cargo=personaNuevaDTO.Cargo,
            //    Telefono=personaNuevaDTO.Telefono,
            //    Correo=personaNuevaDTO.Correo,
            //    CI=personaNuevaDTO.CI
            //};

            var personaNueva = mapper.Map<Tarea>(personaNuevaDTO);
            await context.Tareas.AddAsync(personaNueva);
            await context.SaveChangesAsync();

            return Ok("Tarea Creada");
        }

        [HttpPut]
        public async Task<ActionResult<TareaDTO>> EditarTareas(TareaCreacionDTO personaEditadaDTO, int id)
        {


            var personaEditada = mapper.Map<Tarea>(personaEditadaDTO);

            personaEditada.Id = id;

            var existe = await context.Tareas.AnyAsync(personaEditada => personaEditada.Id == id);

            if (!existe)
            {
                return NotFound("No se puede editar este usuario por que no existe");
            }

            context.Tareas.Update(personaEditada);
            await context.SaveChangesAsync();
            var respuestaEdicion = mapper.Map<TareaDTO>(personaEditada);

            return Ok(respuestaEdicion);
        }

        [HttpDelete]
        public async Task<ActionResult> EliminarTareas(int id)
        {
            var existe = await context.Tareas.AnyAsync(persona => persona.Id == id);

            if (!existe)
            {
                return NotFound("No se puede borrar este usuario por que no existe");
            }

            var personaHaBorrar = new Tarea { Id = id };
            context.Tareas.Remove(personaHaBorrar);
            await context.SaveChangesAsync();

            return Ok("Tarea Eliminada con exito");
        }

    }
}
