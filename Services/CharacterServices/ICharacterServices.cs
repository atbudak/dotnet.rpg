using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet.rpg.Dtos.Character;
using dotnet.rpg.Models;

namespace dotnet.rpg.Services.CharacterServices
{
    public interface ICharacterServices
    {
         Task<ServiceResponce<List<GetCharacterDto>>> GetAllCharacters();

         Task<ServiceResponce<GetCharacterDto>> GetCharacterById(int id);

         Task<ServiceResponce<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter);

         Task<ServiceResponce<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter);

         Task<ServiceResponce<List<GetCharacterDto>>> DeleteCharacter(int id);
    }
}