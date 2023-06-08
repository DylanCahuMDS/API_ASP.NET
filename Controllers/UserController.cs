using Microsoft.AspNetCore.Mvc;

namespace APIMDS.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            var createdUser = await _userRepository.CreateUser(user);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateUser(int id, User user)
        {
            if (id != user.Id)
                return BadRequest();

            var existingUser = await _userRepository.GetUserById(id);
            if (existingUser == null)
                return NotFound();

            var updatedUser = await _userRepository.UpdateUser(user);
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
