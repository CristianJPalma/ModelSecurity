using AutoMapper;
using Entity.DTOs;
using Entity.Model;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Form, FormDto>().ReverseMap();
        CreateMap<FormModule, FormModuleDto>().ReverseMap();
        CreateMap<Module, ModuleDto>().ReverseMap();
        CreateMap<Permission, PermissionDto>().ReverseMap();
        CreateMap<Person, PersonDto>().ReverseMap();
        CreateMap<Rol, RolDto>().ReverseMap();
        CreateMap<RolFormPermission, RolFormPermissionDto>().ReverseMap();
        CreateMap<RolUser, RolUserDto>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();
    }
}
