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
            // Validación del modelo recibido
            if (!ModelState.IsValid)
            {
                //  Si hay errores de validación en el modelo, se retorna 400 con detalles
                return BadRequest(ModelState);
            }

            try
            {
                //  Intentamos crear la persona desde el repositorio
                var createdPerson = await _personRepository.CreateAsync(personPostDto);

                //  Retornamos código 201 (Created) con el ID del recurso creado y un mensaje personalizado
                return CreatedAtAction(nameof(GetById), new { id = createdPerson.PersonId }, new
                {
                    Message = "La persona se creó correctamente.",
                    Data = createdPerson
                });
            }
            catch (DbUpdateException ex)
            {
                //  Manejamos errores específicos de la base de datos reutilizando lógica centralizada
                return HandleDbUpdateException(ex);
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
            //  Validación del modelo recibido
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //  Validación de coincidencia entre URL y DTO
            if (id != updatedPersonDto.PersonId)
            {
                return BadRequest(new { Message = "El ID de la URL no coincide con el ID de la Persona." });
            }

            try
            {
                //  Intentar actualizar en el repositorio
                var updateResult = await _personRepository.UpdateAsync(id, updatedPersonDto);

                //  Si no se encontró la persona para actualizar
                if (!updateResult)
                {
                    return NotFound(new { Message = $"No se encontró la persona con ID {id}." });
                }

                //  Éxito
                return Ok(new { Message = "La persona se actualizó correctamente." });
            }
            catch (DbUpdateException ex)
            {
                //  Reutilizamos la misma lógica de manejo de errores usada en Create
                return HandleDbUpdateException(ex);
            }
            catch (Exception ex)
            {
                //  Error no controlado
                return StatusCode(500, new { Message = "Error interno del servidor.", Detail = ex.Message });
            }
        }



        /// <summary>
        ///  Método privado centralizado para manejar errores de base de datos (único, FK, otros)
        /// Esto evita repetir el mismo switch/código en cada acción (POST, PUT, etc.)
        /// </summary>
        private ActionResult HandleDbUpdateException(DbUpdateException ex)
        {
            if (ex.InnerException is PostgresException pgEx)
            {
                switch (pgEx.SqlState)
                {
                    case "23505": // 🔒 Violación de constraint UNIQUE
                        var constraint = pgEx.ConstraintName?.ToLower();

                        if (!string.IsNullOrEmpty(constraint))
                        {
                            if (constraint.Contains("person_key"))
                                return BadRequest(new { Message = "Ya existe una persona con ese código (Person Key)." });

                            if (constraint.Contains("email"))
                                return BadRequest(new { Message = "El correo electrónico ya está registrado." });

                            if (constraint.Contains("cui"))
                                return BadRequest(new { Message = "El número de DPI ya existe en el sistema." });

                            if (constraint.Contains("nit"))
                                return BadRequest(new { Message = "El número de NIT ya está en uso." });
                        }

                        // Si no se pudo identificar el campo, devolver mensaje genérico
                        return BadRequest(new { Message = "Ya existe un dato único duplicado (clave, correo, etc.)." });

                    case "23503": //  Violación de clave foránea
                        return BadRequest(new { Message = "Valor inválido para género, tipo de sangre u otra relación de tipología." });

                    default:
                        // Otros errores específicos de PostgreSQL
                        return BadRequest(new { Message = $"Error de base de datos: {pgEx.MessageText}" });
                }
            }

            //  Si no es una excepción de Postgres controlada, responder genéricamente
            return StatusCode(500, new { Message = "Error inesperado en la base de datos." });
        }
              
                
        
        
    }
    
}

