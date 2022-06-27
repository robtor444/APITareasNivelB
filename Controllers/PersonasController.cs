using ApiTareasNivelB.DbContextClass;
using ApiTareasNivelB.DTO;
using ApiTareasNivelB.Modelo;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ApiTareasNivelB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public PersonasController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<PersonaDTO>>> TraerPersonas()
        {
            var listaPersonas = await context.Personas.ToListAsync();

            var listaDTO = mapper.Map<List<PersonaDTO>>(listaPersonas);

            return Ok(listaDTO);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PersonaDTO>> TraerPersona(int id)
        {
            var listaPersonas = await context.Personas.FindAsync(id);


            var listaDTO = mapper.Map<PersonaDTO>(listaPersonas);

            return Ok(listaDTO);
        }

        [HttpGet("exacto/{nombre}")]
        public async Task<ActionResult<PersonaDTO>> TraerPersonPorNombre(string nombre)
        {
            var personaEncontrada = await context.Personas
                .FirstOrDefaultAsync(personaDb => personaDb.Nombre == nombre);

           
            if (personaEncontrada==null)
            {
                return NotFound("Persona no encontrada");
            }

            var personaDto = mapper.Map<PersonaDTO>(personaEncontrada);

            return Ok(personaDto);


        }

        [HttpGet("inexacto/{nombre}")]
        public async Task<ActionResult<List<PersonaDTO>>> TraerListaPersonaPorNombre(string nombre)
        {
            var personaEncontrada = await context.Personas
                .Where(personaDb=> personaDb.Nombre.Contains(nombre))
                .ToListAsync();


            if (personaEncontrada == null || personaEncontrada.Count<=0)
            {
                return NotFound("Persona no encontrada");
            }

            var personaDto = mapper.Map<List<PersonaDTO>>(personaEncontrada);

            return Ok(personaDto);


        }


        [HttpPost]
        public async Task<ActionResult<PersonaDTO>> CrearPersonas(PersonaCreacionDTO personaNuevaDTO)
        {

            //var personaNueva = new Persona
            //{
            //    Nombre=personaNuevaDTO.Nombre,
            //    Cargo=personaNuevaDTO.Cargo,
            //    Telefono=personaNuevaDTO.Telefono,
            //    Correo=personaNuevaDTO.Correo,
            //    CI=personaNuevaDTO.CI
            //};

            var personaNueva = mapper.Map<Persona>(personaNuevaDTO);
            await context.Personas.AddAsync(personaNueva);
            await context.SaveChangesAsync();

            return Ok("Persona Creada");
        }

        [HttpPut]
        public async Task<ActionResult<PersonaDTO>> EditarPersonas(PersonaCreacionDTO personaEditadaDTO, int id)
        {
            

            var personaEditada = mapper.Map<Persona>(personaEditadaDTO);
            
            personaEditada.Id=id;

            var existe = await context.Personas.AnyAsync(personaEditada => personaEditada.Id == id);

            if (!existe)
            {
                return NotFound("No se puede editar este usuario por que no existe");
            }

            context.Personas.Update(personaEditada);
            await context.SaveChangesAsync();
            var respuestaEdicion = mapper.Map<PersonaDTO>(personaEditada);

            return Ok(respuestaEdicion);
        }

        [HttpDelete]
        public async Task<ActionResult> EliminarPersonas(int id)
        {
            var existe =await context.Personas.AnyAsync(persona => persona.Id == id);

            if (!existe)
            {
                return NotFound("No se puede borrar este usuario por que no existe");
            }

           var personaHaBorrar= new Persona { Id=id };
            context.Personas.Remove(personaHaBorrar);
            await context.SaveChangesAsync();

            return Ok("Persona Eliminada con exito");
        }

    }
}
