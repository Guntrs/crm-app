using Microsoft.AspNetCore.Mvc;
using crm_app.Dto.User;
using crm_app.Repositories.User;

namespace crm_app.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        // Inyecta repositorio o servicio si lo necesitas más adelante
        private readonly ICrmUserRepository _userRepository;

        public AuthController(ICrmUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Buscar usuario por nombre de usuario (UserName)
            var user = await _userRepository.GetByUserNameAsync(loginDto.UserName);

            if (user == null)
                return Unauthorized(new { message = "Usuario o contraseña incorrectos." });

            // Validar contraseña (texto plano por ahora)
            if (user.Password != loginDto.Password)
                return Unauthorized(new { message = "Usuario o contraseña incorrectos." });

            // Si todo está bien, responde OK
            return Ok(new { message = "Login exitoso", userId = user.UserId });
        }
    }
}