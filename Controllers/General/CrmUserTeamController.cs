using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crm_app.Dto.UserTeam;
using crm_app.Repositories.UserTeam;
using Microsoft.AspNetCore.Authorization;

namespace crm_app.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserTeamController : ControllerBase
    {
        private readonly ICrmUserTeamRepository _repository;

        public UserTeamController(ICrmUserTeamRepository repository)
        {
            _repository = repository;
        }

        //---------------------------- Listar --------------------------
        // GET: api/crmuserteam
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserTeamDto>>> GetAll()
        {
            var userTeams = await _repository.GetAll();
            
            if (userTeams == null || !userTeams.Any())
            {
                return Ok(new
                {
                    Message = "No se encontraron relaciones usuario-equipo.",
                    Data = userTeams
                });
            }

            return Ok(new
            {
                Message = "Relaciones usuario-equipo encontradas.",
                Data = userTeams
            });
        }
    }
}