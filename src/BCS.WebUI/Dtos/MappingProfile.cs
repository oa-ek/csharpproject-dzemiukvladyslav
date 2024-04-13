using AutoMapper;

namespace BCS.WebUI.Dtos
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TypeCreateDto, Core.Entities.Type>();
            CreateMap<TypeUpdateDto, Core.Entities.Type>();

            CreateMap<StatusCreateDto, Core.Entities.Status>();
            CreateMap<StatusUpdateDto, Core.Entities.Status>();

            CreateMap<CityCreateDto, Core.Entities.City>();
            CreateMap<CityUpdateDto, Core.Entities.City>();

            CreateMap<StreetCreateDto, Core.Entities.Street>();
            CreateMap<StreetUpdateDto, Core.Entities.Street>();
        }
    }
}
