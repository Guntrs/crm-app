using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using crm_app.Dto.Person;
using crm_app.Dto.Team;
using crm_app.Repositories.Person;

namespace crm_app.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ICrmPersonRepository _personRepository;

        public PersonController(ICrmPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        
        //--------listar
        // GET: api/team
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDto>>> GetAll()
        {
            var persons = await _personRepository.GetAll();
            if (persons == null || !persons.Any())
            {
                return Ok(new 
                { 
                    Message = "No se encontraron Personas.", 
                    Data = persons 
                });
            }

            return Ok(new 
            { 
                Message = "Personas encontradas.", 
                Data = persons 
            });
        }
        
    }
    
}

