using AutoMapper;

namespace BCS.Infrastructure.Dtos
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TypeCreateDto, Core.Entities.Type>().ReverseMap();
            CreateMap<TypeUpdateDto, Core.Entities.Type>().ReverseMap();

            CreateMap<StatusCreateDto, Core.Entities.Status>().ReverseMap();
            CreateMap<StatusUpdateDto, Core.Entities.Status>().ReverseMap();

            CreateMap<CityCreateDto, Core.Entities.City>().ReverseMap();
            CreateMap<CityUpdateDto, Core.Entities.City>().ReverseMap();

            CreateMap<StreetCreateDto, Core.Entities.Street>().ReverseMap();
            CreateMap<StreetUpdateDto, Core.Entities.Street>().ReverseMap();

            CreateMap<StructureCreateDto, Core.Entities.Structure>().ReverseMap();
            CreateMap<StructureUpdateDto, Core.Entities.Structure>().ReverseMap();

            CreateMap<ComplaintCreateDto, Core.Entities.Complaint>().ReverseMap();
            CreateMap<ComplaintUpdateDto, Core.Entities.Complaint>().ReverseMap();

            CreateMap<SuggestionCreateDto, Core.Entities.Suggestion>().ReverseMap();
            CreateMap<SuggestionUpdateDto, Core.Entities.Suggestion>().ReverseMap();
        }
    }
}
