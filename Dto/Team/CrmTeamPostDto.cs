using System;
using System.ComponentModel.DataAnnotations;

namespace crm_app.Dto.Team
{
    public class TeamPostDto
    {
        [Required(ErrorMessage = "El nombre del equipo es obligatorio")]
        [MaxLength(255, ErrorMessage = "Máximo 255 caracteres")]
        public string TeamName { get; set; }

        [MaxLength(255, ErrorMessage = "Máximo 255 caracteres")]
        public string? TeamDescription { get; set; }
    }
}