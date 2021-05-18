using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet.rpg.Data;
using dotnet.rpg.Dtos.Character;
using dotnet.rpg.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet.rpg.Services.CharacterServices
{
    public class CharacterServices : ICharacterServices
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CharacterServices(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;

        }

        public async Task<ServiceResponce<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceResponce = new ServiceResponce<List<GetCharacterDto>>();            
            Character character = _mapper.Map<Character>(newCharacter);
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            serviceResponce.Data = await _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
            return serviceResponce;
        }

        public async Task<ServiceResponce<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var serviceResponce = new ServiceResponce<List<GetCharacterDto>>();
            try
            {
                Character character = await _context.Characters.FirstAsync(x => x.Id == id);

                _context.Characters.Remove(character);
                await _context.SaveChangesAsync();

                serviceResponce.Data = _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
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
            var dbCharacters = await _context.Characters.ToArrayAsync();
            serviceResponce.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponce;
        }

        public async Task<ServiceResponce<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponce = new ServiceResponce<GetCharacterDto>();
            var dbCharacters = await _context.Characters.FirstOrDefaultAsync(x => x.Id == id);
            serviceResponce.Data = _mapper.Map<GetCharacterDto>(dbCharacters);
            return serviceResponce;
        }

        public async Task<ServiceResponce<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            var serviceResponce = new ServiceResponce<GetCharacterDto>();
            try
            {
                Character character = await _context.Characters.FirstOrDefaultAsync(x => x.Id == updatedCharacter.Id);

                character.HitPoints = updatedCharacter.HitPoints;
                character.Deffense = updatedCharacter.Deffense;
                character.Intelligence = updatedCharacter.Intelligence;
                character.Name = updatedCharacter.Name;
                character.Strength = updatedCharacter.Strength;
                character.Class = updatedCharacter.Class;

                await _context.SaveChangesAsync();

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