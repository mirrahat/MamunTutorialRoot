using MamunTutorial.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MamunTutorial.Services
{
    public interface IJwtService
    {
      

      
        AuthenticationResponse CreateJwtToken(User user);

    }
}
