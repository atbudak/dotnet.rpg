using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet.rpg.Dtos.Character;
using dotnet.rpg.Models;

namespace dotnet.rpg.Services.CharacterServices
{
    public class CharacterServices : ICharacterServices
    {
        private static List<Character> characters = new List<Character>
        {
            new Character(),
            new Character {Id = 1, Name="Ertuğrul"}
        };
        private readonly IMapper _mapper;

        public CharacterServices(IMapper mapper)
        {
            _mapper = mapper;

        }

        public async Task<ServiceResponce<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceResponce = new ServiceResponce<List<GetCharacterDto>>();
            Character character = _mapper.Map<Character>(newCharacter);
            character.Id = characters.Max(x => x.Id) + 1;
            characters.Add(character);
            serviceResponce.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponce;
        }

        public async Task<ServiceResponce<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
             var serviceResponce = new ServiceResponce<List<GetCharacterDto>>();
            try
            {
                Character character = characters.First(x => x.Id == id);

                characters.Remove(character);

                serviceResponce.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponce.Success = false;
                serviceResponce.Message = ex.Message;
            }
            return serviceResponce;
        }

        public async Task<ServiceResponce<List<GetCharacterDto>>> GetAllCharacters()
        {
            var serviceResponce = new ServiceResponce<List<GetCharacterDto>>();
            serviceResponce.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponce;
        }

        public async Task<ServiceResponce<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponce = new ServiceResponce<GetCharacterDto>();
            serviceResponce.Data = _mapper.Map<GetCharacterDto>(characters.FirstOrDefault(x => x.Id == id));
            return serviceResponce;
        }

        public async Task<ServiceResponce<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            var serviceResponce = new ServiceResponce<GetCharacterDto>();
            try
            {
                Character character = characters.FirstOrDefault(x => x.Id == updatedCharacter.Id);
                character.HitPoints = updatedCharacter.HitPoints;
                character.Deffense = updatedCharacter.Deffense;
                character.Intelligence = updatedCharacter.Intelligence;
                character.Name = updatedCharacter.Name;
                character.Strength = updatedCharacter.Strength;
                character.Class = updatedCharacter.Class;

                serviceResponce.Data = _mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception ex)
            {
                serviceResponce.Success = false;
                serviceResponce.Message = ex.Message;
            }
            return serviceResponce;
        }
    }
}