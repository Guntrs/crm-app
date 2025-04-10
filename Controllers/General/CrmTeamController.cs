using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using crm_app.Dto.Team;
using crm_app.Repositories.Team;

namespace crm_app.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly ICrmTeamRepository _repository;

        public TeamController(ICrmTeamRepository repository)
        {
            _repository = repository;
        }
        
        //----------------------------Listar--------------------------
        // GET: api/team
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var teams = await _repository.GetAll();
            if (teams == null || !teams.Any())
            {
                return Ok(new 
                { 
                    Message = "No se encontraron equipos.", 
                    Data = teams 
                });
            }

            return Ok(new 
            { 
                Message = "Equipos encontrados.", 
                Data = teams 
            });
        }

        //----------------------------Listar Id--------------------------
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(long id)
        {
            var team = await _repository.GetByIdAsync(id);
            if (team == null)
            {
                return NotFound(new 
                { 
                    Message = $"No se encontró el equipo con ID {id}." 
                });
            }

            return Ok(new 
            { 
                Message = "Equipo encontrado.", 
                Data = team 
            });
        }
        
        //-------------nuevo---------------------
        
        // POST: api/team
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] TeamPostDto teamPostDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var createdTeam = await _repository.CreateAsync(teamPostDto);
            
            // Se retorna un código 201 con la ruta para obtener el registro y un mensaje
            return CreatedAtAction(nameof(GetById), new { id = createdTeam.TeamId }, new 
            { 
                Message = "El equipo se creó correctamente.", 
                Data = createdTeam 
            });
        }
        
        //----------------------------Actualizar--------------------------
        // PUT: api/team/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(long id, [FromBody] TeamPutDto updatedTeamDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            // Verificar que el ID de la URL coincida con el ID del DTO
            if (id != updatedTeamDto.TeamId)
            {
                return BadRequest(new { Message = "El ID de la URL no coincide con el ID del equipo en el cuerpo." });
            }
            
            var updateResult = await _repository.UpdateAsync(id, updatedTeamDto);
            if (!updateResult)
            {
                return NotFound(new { Message = $"No se encontró el equipo con ID {id}." });
            }
            return Ok(new { Message = "El equipo se actualizó correctamente." });
        }
        
    }
}