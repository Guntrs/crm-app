using System;
using System.ComponentModel.DataAnnotations;

namespace crm_app.Dto.User
{
    public class LoginDto
    {
        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        [MaxLength(255, ErrorMessage = "Máximo 255 caracteres")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [MaxLength(255, ErrorMessage = "Máximo 255 caracteres")]
        public string Password { get; set; }
    }
}