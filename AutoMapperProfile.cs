using AutoMapper;
using PeliculasApi.DTOs;
using PeliculasApi.Entidades;

namespace PeliculasApi
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile() 
        { 
            CreateMap<Genero, GeneroDTO>().ReverseMap();
            CreateMap<GeneroCreacionDTO, Genero>();
            CreateMap<Actor, ActorDTO>().ReverseMap();
            CreateMap<ActorCreacionDTO, Actor>().ForMember(x => x.Foto, options => options.Ignore());
        }
    }
}
