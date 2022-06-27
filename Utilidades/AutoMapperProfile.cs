using ApiTareasNivelB.DTO;
using ApiTareasNivelB.Modelo;
using AutoMapper;

namespace ApiTareasNivelB.Utilidades
{
    public class AutoMapperProfile:Profile
    {

        public AutoMapperProfile()
        {
            CreateMap<PersonaCreacionDTO, Persona>();
            CreateMap<Persona,PersonaDTO>();

            CreateMap<ProyectoCreacionDTO, Proyecto>();
            CreateMap<Proyecto, ProyectoDTO>();

            CreateMap<TareaCreacionDTO, Tarea>();
            CreateMap<Tarea, TareaDTO>();

        }
    }
}
