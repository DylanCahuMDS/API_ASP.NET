using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIMDS.Controllers
{
    [ApiController]
    [Route("univers")]
    public class UniversController : ControllerBase
    {
        private readonly IUniversRepository _universRepository;

        public UniversController(IUniversRepository universRepository)
        {
            _universRepository = universRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Univers>>> GetUnivers()
        {
            var univers = await _universRepository.GetUnivers();
            return Ok(univers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Univers>> GetUniversById(int id)
        {
            var univers = await _universRepository.GetUniversById(id);
            if (univers == null)
                return NotFound();

            return Ok(univers);
        }

        [HttpPost]
        public async Task<ActionResult<Univers>> CreateUnivers(Univers univers)
        {
            var createdUnivers = await _universRepository.CreateUnivers(univers);
            return CreatedAtAction(nameof(GetUniversById), new { id = createdUnivers.Id }, createdUnivers);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Univers>> UpdateUnivers(int id, Univers univers)
        {
            if (id != univers.Id)
                return BadRequest();

            var existingUnivers = await _universRepository.GetUniversById(id);
            if (existingUnivers == null)
                return NotFound();

            var updatedUnivers = await _universRepository.UpdateUnivers(univers);
            return Ok(updatedUnivers);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUnivers(int id)
        {
            var deleted = await _universRepository.DeleteUnivers(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
