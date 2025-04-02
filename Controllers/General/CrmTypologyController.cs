using Microsoft.AspNetCore.Mvc; 
using System.Collections.Generic; 
using System.Threading.Tasks; 
using crm_app.Dto.Typology;
using crm_app.Repositories.Typologies; 

namespace crm_app.Controllers
{
    [ApiController] 
    [Route("api/[controller]")] 
    public class TypologyController : ControllerBase
    {
        private readonly ICrmTypologyRepository _repository;

        public TypologyController(ICrmTypologyRepository repository)
        {
            _repository = repository;
        }

        // GET: api/typology
        [HttpGet] 
        public async Task<ActionResult<IEnumerable<TypologyDto>>> GetAll()
        {
            var typologies = await _repository.GetAll(); 
            return Ok(typologies); 
        }

        // GET: api/typology/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TypologyDto>> GetById(int id)
        {
            var typology = await _repository.GetByIdAsync(id);
            if (typology == null)
                return NotFound();

            return Ok(typology);
        }
    }
}