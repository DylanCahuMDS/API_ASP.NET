using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIMDS.Controllers
{
    [ApiController]
    [Route("personnages")]
    public class PersonnageController : ControllerBase
    {
        private readonly IPersonnageRepository _personnageRepository;

        public PersonnageController(IPersonnageRepository personnageRepository)
        {
            _personnageRepository = personnageRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Personnage>>> GetPersonnages()
        {
            var personnages = await _personnageRepository.GetPersonnages();
            return Ok(personnages);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Personnage>> GetPersonnageById(int id)
        {
            var personnage = await _personnageRepository.GetPersonnageById(id);
            if (personnage == null)
                return NotFound();

            return Ok(personnage);
        }

        [HttpPost]
        public async Task<ActionResult<Personnage>> CreatePersonnage(Personnage personnage)
        {
            var createdPersonnage = await _personnageRepository.CreatePersonnage(personnage);
            return CreatedAtAction(nameof(GetPersonnageById), new { id = createdPersonnage.Id }, createdPersonnage);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Personnage>> UpdatePersonnage(int id, Personnage personnage)
        {
            if (id != personnage.Id)
                return BadRequest();

            var existingPersonnage = await _personnageRepository.GetPersonnageById(id);
            if (existingPersonnage == null)
                return NotFound();

            var updatedPersonnage = await _personnageRepository.UpdatePersonnage(personnage);
            return Ok(updatedPersonnage);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePersonnage(int id)
        {
            var deleted = await _personnageRepository.DeletePersonnage(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}

