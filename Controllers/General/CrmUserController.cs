using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using crm_app.Dto.User;

using crm_app.Repositories.User;
using Microsoft.AspNetCore.Authorization;

namespace crm_app.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ICrmUserRepository _repository;

        public UserController(ICrmUserRepository repository)
        {
            _repository = repository;
        }
        
        //----------------------------Listar--------------------------
        // GET: api/team
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
        {
            var users = await _repository.GetAll();
            if (users == null || !users.Any())
            {
                return Ok(new 
                { 
                    Message = "No se encontraron Usuarios.", 
                    Data = users 
                });
            }

            return Ok(new 
            { 
                Message = "Usuarios encontradas.", 
                Data = users 
            });
        }

        
        //----------------------------Listar Id--------------------------
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(long id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound(new 
                { 
                    Message = $"No se encontró el Usuario con ID {id}." 
                });
            }

            return Ok(new 
            { 
                Message = "Usuario Encontrado.", 
                Data = user
            });
        }
        

        //-------------nuevo---------------------
        
        // POST: api/team
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] UserPostDto userPostDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var createdUser = await _repository.CreateAsync( userPostDto);
            
            // Se retorna un código 201 con la ruta para obtener el registro y un mensaje
            return CreatedAtAction(nameof(GetById), new { id = createdUser.UserId }, new 
            { 
                Message = "El usuario se creó correctamente.", 
                Data = createdUser
            });
        }
        
                
        //----------------------------Actualizar--------------------------
        // PUT: api/team/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(long id, [FromBody] UserPutDto updatedUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            // Verificar que el ID de la URL coincida con el ID del DTO
            if (id != updatedUserDto.UserId)
            {
                return BadRequest(new { Message = "El ID de la URL no coincide con el ID del equipo en el cuerpo." });
            }
            
            var updateResult = await _repository.UpdateAsync(id, updatedUserDto);
            if (!updateResult)
            {
                return NotFound(new { Message = $"No se encontró el Usuario con ID {id}." });
            }
            return Ok(new { Message = "El Usuario se actualizó correctamente." });
        }
        
        
    }
}