using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace APIMDS.Controllers
{

    public class AuthentificationHelper
    {
        private readonly IConfiguration _configuration;

        public AuthentificationHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // Méthode pour vérifier le mot de passe de l'utilisateur
        public bool VerifyPassword(string password, string passwordHash)
        {
            return password == passwordHash;
        }

        // Méthode pour générer le jeton d'authentification
        public string GenerateAuthToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
