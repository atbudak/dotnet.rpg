using System.Threading.Tasks;
using dotnet.rpg.Data;
using dotnet.rpg.Dtos.User;
using dotnet.rpg.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet.rpg.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _AuthRepo;
        public AuthController(IAuthRepository AuthRepo)
        {
            _AuthRepo = AuthRepo;

        }

        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponce<int>>> Register(UserRegisterDto request)
        {

          var response = await _AuthRepo.Register(
              new User {Username = request.Username} ,request.Password
          );

            if(!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponce<string>>> Login(UserLoginDto request)
        {
          var response = await _AuthRepo.Login(
              request.Username, request.Password
          );

            if(!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);   
        } 

    }
}