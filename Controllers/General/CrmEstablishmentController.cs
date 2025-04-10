using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        
        
        
        //----------------------------Actualizar--------------------------
        
        
    }
}