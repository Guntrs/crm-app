using Microsoft.AspNetCore.Mvc;
using crm_app.Dto.User;
using crm_app.Repositories.User;
using Microsoft.AspNetCore.Identity;

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

            // 1) Buscar usuario por UserName
            var user = await _userRepository.GetByUserNameAsync(loginDto.UserName);
            if (user == null)
                return Unauthorized(new { message = "Usuario o contraseña incorrectos." });

            // 2) Verificar hash en lugar de comparar texto
            var hasher = new PasswordHasher<UserDto>();
            var result = hasher.VerifyHashedPassword(user, user.Password, loginDto.Password);
            if (result == PasswordVerificationResult.Failed)
                return Unauthorized(new { message = "Usuario o contraseña incorrectos." });

            // 3) Si todo OK
            return Ok(new { message = "Login exitoso", userId = user.UserId });
        }
    }
}