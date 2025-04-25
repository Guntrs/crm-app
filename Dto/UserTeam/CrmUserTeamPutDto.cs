using System;
using System.ComponentModel.DataAnnotations;

namespace crm_app.Dto.UserTeam
{
    public class UserTeamPutDto
    {
        [Required(ErrorMessage = "El ID de la relación usuario-equipo es obligatorio")]
        public long UsersTeamId { get; set; }

        [Required(ErrorMessage = "El ID del usuario es obligatorio")]
        public long UserId { get; set; }

        [Required(ErrorMessage = "El ID del equipo es obligatorio")]
        public long TeamId { get; set; }

        [Required(ErrorMessage = "El rol es obligatorio")]
        public long Role { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio")]
        public long State { get; set; }
    }
}