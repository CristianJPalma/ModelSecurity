using AutoMapper;
using Entity.DTOs;
using Entity.Model;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Form, FormDto>().ReverseMap();
        CreateMap<Module, ModuleDto>().ReverseMap();
    }
}
