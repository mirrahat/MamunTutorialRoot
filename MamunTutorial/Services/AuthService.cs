using MamunTutorial.Data;
using MamunTutorial.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MamunTutorial.Services
{
    public class AuthService
    {
        private readonly ApplicationDBContext _dBcontext;

        public AuthService(ApplicationDBContext dBcontext)
        {
            _dBcontext = dBcontext;
        }

        public async Task<User> ValidateUserAsync(string identifier, string password)
        {
            // Check if the identifier is email, phone number, or username
            var user = await _dBcontext.UsersInfo
                .FirstOrDefaultAsync(u =>  u.Email == identifier || u.PhoneNumber == identifier);

            // If user doesn't exist or password doesn't match, return null (invalid credentials)
            if (user == null || user.Password != password)
            {
                return null; // Invalid credentials
            }

            return user;
        }
    }
}
