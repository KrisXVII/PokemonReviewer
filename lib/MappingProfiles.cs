using Profile = AutoMapper.Profile;

namespace PokemonReviewer.lib;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Pokemon, PokemonDto>();
        CreateMap<Category, CategoryDto>().ReverseMap();;
        CreateMap<Country, CountryDto>().ReverseMap();;
        CreateMap<Owner, OwnerDto>().ReverseMap();;
        CreateMap<Review, ReviewDto>();
        CreateMap<Reviewer, ReviewerDto>();
    }
}