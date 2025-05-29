using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using crm_app.Dto.Person;
using crm_app.Dto.Team;
using crm_app.Repositories.Person;
using Microsoft.AspNetCore.Authorization;

namespace crm_app.Controllers
{
    [Authorize]
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
        
        
        //----------------------------Listar Id--------------------------
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(long id)
        {
            var persons = await _personRepository.GetByIdAsync(id);
            if (persons == null)
            {
                return NotFound(new 
                { 
                    Message = $"No se encontró Persona con ID {id}." 
                });
            }

            return Ok(new 
            { 
                Message = "Persona Encontrada.", 
                Data = persons 
            });
        }
        
        //-------------nuevo---------------------
        
        // POST: api/team
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] PersonPostDto personPostDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var createdPerson = await _personRepository.CreateAsync(personPostDto);
            
            // Se retorna un código 201 con la ruta para obtener el registro y un mensaje
            return CreatedAtAction(nameof(GetById), new { id = createdPerson.PersonId }, new 
            { 
                Message = "La persona se creó correctamente.", 
                Data = createdPerson
            });
        }
        
        //----------------------------Actualizar--------------------------
        // PUT: api/team/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(long id, [FromBody] PersonPutDto updatedPersonDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            // Verificar que el ID de la URL coincida con el ID del DTO
            if (id != updatedPersonDto.PersonId)
            {
                return BadRequest(new { Message = "El ID de la URL no coincide con el ID de la Persona." });
            }
            
            var updateResult = await _personRepository.UpdateAsync(id, updatedPersonDto);
            if (!updateResult)
            {
                return NotFound(new { Message = $"No se Encontro La Persona con ID {id}." });
            }
            return Ok(new { Message = "La Persona se actualizó correctamente." });
        }
        
        
    }
    
}

