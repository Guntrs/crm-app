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
    }
}