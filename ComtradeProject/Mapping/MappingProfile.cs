using AutoMapper;
using ComtradeProject.DTOs;
using ComtradeProject.Model;
using ServiceReference;

namespace ComtradeProject.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        { 
            CreateMap<Agent, RegisterResponseDTO>().ReverseMap();
            CreateMap<Agent, RegisterDTO>().ReverseMap();

            CreateMap<Model.Person, ServiceReference.Person>().ReverseMap();
            CreateMap<Model.Person, PersonDTO>().ReverseMap();
            
        }
    }
}
