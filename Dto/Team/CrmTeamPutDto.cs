using System.ComponentModel.DataAnnotations;

namespace crm_app.Dto.Team
{
    public class CrmTeamPutDto
    {
        // Generalmente, el ID se utiliza para saber qué registro se va a actualizar.
        // Se asume que lo recibes en el cuerpo o en la ruta (dependiendo de tu API).
        [Required(ErrorMessage = "El ID del equipo es obligatorio")]
        public long TeamId { get; set; }

        [Required(ErrorMessage = "El nombre del equipo es obligatorio")]
        [MaxLength(255, ErrorMessage = "Máximo 255 caracteres")]
        public string TeamName { get; set; }

        [MaxLength(255, ErrorMessage = "Máximo 255 caracteres")]
        public string? TeamDescription { get; set; }

        // Si tu lógica permite cambiar el estado (por ejemplo, de Activo a Inactivo),
        // inclúyelo aquí. De lo contrario, podrías omitirlo.
        public long? State { get; set; }

        // Si deseas registrar quién está haciendo la modificación,
        // puedes incluirlo en el DTO. Si lo manejas automáticamente
        // en tu capa de servicio o en la BD, podrías omitirlo.
        public long? ModifiedBy { get; set; }
    }
}