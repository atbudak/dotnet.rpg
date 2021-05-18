using AutoMapper;
using dotnet.rpg.Dtos.Character;
using dotnet.rpg.Models;

namespace dotnet.rpg
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character,GetCharacterDto>();
            CreateMap<AddCharacterDto,Character>();
        }
    }
}