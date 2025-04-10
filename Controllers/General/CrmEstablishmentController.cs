using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using crm_app.Dto.Establishment;
using crm_app.Dto.Team;
using crm_app.Repositories.Establishment;

namespace crm_app.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstablishmentController : ControllerBase
    {
        private readonly ICrmEstablishmentRepository _repository;

        public EstablishmentController(ICrmEstablishmentRepository repository)
        {
            _repository = repository;
        }
        
        //----------------------------Listar--------------------------
        // GET: api/team
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var establishments = await _repository.GetAll();
            if (establishments == null || !establishments.Any())
            {
                return Ok(new 
                { 
                    Message = "No se encontraron Sucursales.", 
                    Data = establishments 
                });
            }

            return Ok(new 
            { 
                Message = "Sucursales encontradas.", 
                Data = establishments 
            });
        }

        //----------------------------Listar Id--------------------------
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(long id)
        {
            var establishment = await _repository.GetByIdAsync(id);
            if (establishment == null)
            {
                return NotFound(new 
                { 
                    Message = $"No se encontró el Sucursal con ID {id}." 
                });
            }

            return Ok(new 
            { 
                Message = "Sucursal Encontrada.", 
                Data = establishment 
            });
        }
        
        
        //-------------nuevo---------------------
        
        // POST: api/team
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] EstablishmentPostDto establishmentPostDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var createdEstablishment = await _repository.CreateAsync(establishmentPostDto);
            
            // Se retorna un código 201 con la ruta para obtener el registro y un mensaje
            return CreatedAtAction(nameof(GetById), new { id = createdEstablishment.EstablishmentId }, new 
            { 
                Message = "La Sucursal se creó correctamente.", 
                Data = createdEstablishment
            });
        }
        
        
        //----------------------------Actualizar--------------------------
        // PUT: api/team/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(long id, [FromBody] EstablishmentPutDto updatedEstablishmentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            // Verificar que el ID de la URL coincida con el ID del DTO
            if (id != updatedEstablishmentDto.EstablishmentId)
            {
                return BadRequest(new { Message = "El ID de la URL no coincide con el ID del equipo en el cuerpo." });
            }
            
            var updateResult = await _repository.UpdateAsync(id, updatedEstablishmentDto);
            if (!updateResult)
            {
                return NotFound(new { Message = $"No se encontró la Sucursal con ID {id}." });
            }
            return Ok(new { Message = "La Sucursal se actualizó correctamente." });
        }
        
        
    }
}