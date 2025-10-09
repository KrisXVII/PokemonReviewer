using PokemonReviewer.Dto;
using Profile = AutoMapper.Profile;

namespace PokemonReviewer.lib;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Pokemon, PokemonDto>();
    }
}