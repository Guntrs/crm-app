using Microsoft.AspNetCore.Mvc;
using crm_app.Dto.User;
using crm_app.Repositories.User;
using Microsoft.AspNetCore.Identity;
using crm_app.Security;

namespace crm_app.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        // Inyecta repositorio o servicio si lo necesitas más adelante
        private readonly ICrmUserRepository _userRepository;
        private readonly CrmJwtService _jwtService;

        public AuthController(ICrmUserRepository userRepository, CrmJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
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

            // 3) Si todo OK, genera el token
            var token = _jwtService.GenerateToken(user.UserId, user.UserName);

            // Devuelve el token al frontend
            return Ok(new { message = "Login exitoso", token = token });
        }
    }
}