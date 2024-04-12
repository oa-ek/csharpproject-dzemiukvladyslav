using AutoMapper;

namespace BCS.WebUI.Dtos
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TypeCreateDto, Core.Entities.Type>();
            CreateMap<TypeUpdateDto, Core.Entities.Type>();
        }
    }
}
