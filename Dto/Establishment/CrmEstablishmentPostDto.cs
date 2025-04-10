using System;
using System.ComponentModel.DataAnnotations;

namespace crm_app.Dto.Establishment
{
    public class EstablishmentPostDto
    {
        public long? ParentEstablishmentId { get; set; }

        [Required(ErrorMessage = "La clave del establecimiento es obligatoria")]
        [MaxLength(255, ErrorMessage = "Máximo 255 caracteres")]
        public string EstablishmentKey { get; set; }

        [Required(ErrorMessage = "El nombre del establecimiento es obligatorio")]
        [MaxLength(255, ErrorMessage = "Máximo 255 caracteres")]
        public string EstablishmentName { get; set; }

        [MaxLength(500, ErrorMessage = "Máximo 500 caracteres")]
        public string? EstablishmentDescription { get; set; }

        [Required(ErrorMessage = "La dirección del establecimiento es obligatoria")]
        [MaxLength(500, ErrorMessage = "Máximo 500 caracteres")]
        public string EstablishmentAddress { get; set; }

        [Required(ErrorMessage = "El email del establecimiento es obligatorio")]
        [MaxLength(255, ErrorMessage = "Máximo 255 caracteres")]
        [EmailAddress(ErrorMessage = "Debe ser un email válido")]
        public string EstablishmentEmail { get; set; }

        [Required(ErrorMessage = "El teléfono del establecimiento es obligatorio")]
        [MaxLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string EstablishmentPhone { get; set; }

        [Required(ErrorMessage = "El tipo de establecimiento es obligatorio")]
        public long EstablishmentType { get; set; }
    }
}