using System;
using System.ComponentModel.DataAnnotations;

namespace crm_app.Dto.UserTeam
{
    public class UserTeamPostDto
    {
        [Required(ErrorMessage = "El ID del usuario es obligatorio")]
        public long UserId { get; set; }

        [Required(ErrorMessage = "El ID del equipo es obligatorio")]
        public long TeamId { get; set; }

        [Required(ErrorMessage = "El rol es obligatorio")]
        public long Role { get; set; } 

        public long? State { get; set; } 
    }
}