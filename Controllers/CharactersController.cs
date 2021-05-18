using System.Collections.Generic;
using dotnet.rpg.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using dotnet.rpg.Services.CharacterServices;
using System.Threading.Tasks;
using dotnet.rpg.Dtos.Character;

namespace dotnet.rpg.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharactersController : ControllerBase
    {

        private readonly ICharacterServices _characterservices;
        public CharactersController(ICharacterServices characterservices)
        {
            _characterservices = characterservices;

        }

        //Route metodunu kullanmadan http nin yanına da yazılabilir alttaki gibi
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponce<List<GetCharacterDto>>>> Get()
        {
            return Ok(await _characterservices.GetAllCharacters());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponce<GetCharacterDto>>> GetSingle(int id)
        {
            return Ok(await _characterservices.GetCharacterById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponce<List<GetCharacterDto>>>> AddCharacter(AddCharacterDto newCharacter)
        {
            return Ok(await _characterservices.AddCharacter(newCharacter));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponce<GetCharacterDto>>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            return Ok(await _characterservices.UpdateCharacter(updatedCharacter));

            /* var response = await _characterservices.UpdateCharacter(updatedCharacter)); if(response == null){ return NotFound();}else{return Ok(response);}*/
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponce<List<GetCharacterDto>>>> Delete(int id)
        {
            var response = await _characterservices.DeleteCharacter(id);
            if (response == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(response);
            }
        }
    }
}