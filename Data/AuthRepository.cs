using System.Threading.Tasks;
using dotnet.rpg.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet.rpg.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
            _context = context;

        }

        public async Task<ServiceResponce<string>> Login(string username, string password)
        {
            var response = new ServiceResponce<string>();
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username.ToLower().Equals(username.ToLower()));
            if (user == null)
            {
                response.Success = false;
                response.Message = "User Not Found!";
            }
            else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {                
                response.Success = false;
                response.Message = "Incorrect Password!";
            }
            else
            {
                response.Data = user.Id.ToString();
            }
            return response;
        }

        public async Task<ServiceResponce<int>> Register(User user, string password)
        {
            ServiceResponce<int> response = new ServiceResponce<int>();
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            if (await UserExists(user.Username))
            {
                response.Success = false;
                response.Message = "User already exist!";
                return response;
            }

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            response.Data = user.Id;
            return response;
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(x => x.Username.ToLower().Equals(username.ToLower())))
            {
                return true;
            }
            return false;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hcmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordHash = hcmac.Key;
                passwordSalt = hcmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hcmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hcmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;

            }
        }
    }
}