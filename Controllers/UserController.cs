using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace APIMDS.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UsersController(IUserRepository userRepository, IConfiguration configuration)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Dictionary<string, string> credentials)
        {
            if (!credentials.ContainsKey("username") || !credentials.ContainsKey("password"))
            {
                return Unauthorized("Missing credentials");
            }

            var storedUser = await _userRepository.GetUserByUsername(credentials["username"]);

            if (storedUser == null)
            {
                return Unauthorized("Unknown user");
            }

            var authHelper = new AuthentificationHelper(_configuration);

            if (!authHelper.VerifyPassword(HashPassword(credentials["password"]), storedUser.Password))
            {
                return Unauthorized("Invalid password.");
            }

            var token = authHelper.GenerateAuthToken(storedUser);
            return Ok(new { token });
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _userRepository.GetUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(Dictionary<string, string> userData)
        {
            var passwordHash = HashPassword(userData["password"]);
            var user = new User(userData["username"], passwordHash);


            // Check if the username is already taken
            if (await _userRepository.GetUserByUsername(userData["username"]) != null)
            {
                return BadRequest("Username is already taken");
            }

            var createdUser = await _userRepository.CreateUser(user);

            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        }
        public static string HashPassword(string password)
        {
            //simple hash
            var hash = new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hash);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateUser(int id, Dictionary<string, string> userData)
        {
            var existingUser = await _userRepository.GetUserById(id);
            if (existingUser == null)
                return NotFound();

            if (userData.ContainsKey("username"))
            {
                // Check if the new username is already taken
                var newUsername = userData["username"];
                if (await _userRepository.GetUserByUsername(newUsername) != null)
                {
                    return BadRequest("Username is already taken");
                }

                existingUser.Username = newUsername;
            }

            if (userData.ContainsKey("password"))
            {
                // Hash the new password
                var newPassword = userData["password"];
                existingUser.Password = HashPassword(newPassword);
            }

            var updatedUser = await _userRepository.UpdateUser(existingUser);
            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var deleted = await _userRepository.DeleteUser(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }

}
