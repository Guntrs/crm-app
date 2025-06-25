using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using crm_app.Dto.Person;
using crm_app.Dto.Team;
using crm_app.Repositories.Person;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Npgsql;

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

        // POST: api/person
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] PersonPostDto personPostDto)
        {
            //  Validación del modelo recibido
            if (!ModelState.IsValid)
            {
                //  Si hay errores de validación en el modelo, se retorna 400 con detalles
                return BadRequest(ModelState);
            }

            try
            {
                //  Intentamos crear la persona desde el repositorio
                var createdPerson = await _personRepository.CreateAsync(personPostDto);

                // Retornamos código 201 (Created) con el ID del recurso creado y un mensaje personalizado
                return CreatedAtAction(nameof(GetById), new { id = createdPerson.PersonId }, new
                {
                    Message = "La persona se creó correctamente.",
                    Data = createdPerson
                });
            }
            catch (DbUpdateException ex)
            {
                //  Manejamos errores relacionados con la base de datos
                if (ex.InnerException is PostgresException pgEx)
                {
                    switch (pgEx.SqlState)
                    {
                        case "23505": // Duplicado (UNIQUE constraint violation)
                            return BadRequest(new { Message = "Ya existe una persona con esa clave, correo u otro dato único." });

                        case "23503": // Foreign key violation
                            return BadRequest(new { Message = "Valor inválido para género, tipo de sangre u otra relación de tipología." });

                        default:
                            //  Otros errores específicos de PostgreSQL
                            return BadRequest(new { Message = "Error de base de datos: " + pgEx.MessageText });
                    }
                }

                //  Si no se pudo identificar la causa exacta
                return StatusCode(500, new { Message = "Ocurrió un error inesperado al guardar la persona." });
            }
            catch (Exception ex)
            {
                //  Fallback para cualquier otro error no controlado
                return StatusCode(500, new { Message = "Error interno del servidor.", Detail = ex.Message });
            }
        }
        
        
        
        //----------------------------Actualizar--------------------------
        // PUT: api/person/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(long id, [FromBody] PersonPutDto updatedPersonDto)
        {
            // Validación del modelo recibido
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validación de coincidencia entre URL y DTO
            if (id != updatedPersonDto.PersonId)
            {
                return BadRequest(new { Message = "El ID de la URL no coincide con el ID de la Persona." });
            }

            try
            {
                // Intentar actualizar en el repositorio
                var updateResult = await _personRepository.UpdateAsync(id, updatedPersonDto);

                if (!updateResult)
                {
                    return NotFound(new { Message = $"No se encontró la persona con ID {id}." });
                }

                // Éxito
                return Ok(new { Message = "La persona se actualizó correctamente." });
            }
            catch (DbUpdateException ex)
            {
                //  Manejo de errores de base de datos específicos
                if (ex.InnerException is PostgresException pgEx)
                {
                    switch (pgEx.SqlState)
                    {
                        case "23505":
                            return BadRequest(new { Message = "Ya existe otra persona con esa clave, correo u otro dato único." });

                        case "23503":
                            return BadRequest(new { Message = "Valor inválido en tipología u otra relación referencial." });
                    }
                }

                return StatusCode(500, new { Message = "Ocurrió un error inesperado al actualizar la persona." });
            }
            catch (Exception ex)
            {
                // Error no controlado
                return StatusCode(500, new { Message = "Error interno del servidor.", Detail = ex.Message });
            }
        }
        
    }
    
}

