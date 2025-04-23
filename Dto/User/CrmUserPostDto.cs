using System;
using System.ComponentModel.DataAnnotations;

namespace crm_app.Dto.User
{
    public class UserPostDto
    {
        [Required(ErrorMessage = "La clave del usuario es obligatoria")]
        [MaxLength(255, ErrorMessage = "Máximo 255 caracteres")]
        public string UserKey { get; set; }

        public long? ParentUserId { get; set; }

        [Required(ErrorMessage = "El ID de la persona es obligatorio")]
        public long PersonId { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        [MaxLength(255, ErrorMessage = "Máximo 255 caracteres")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [MaxLength(255, ErrorMessage = "Máximo 255 caracteres")]
        public string Password { get; set; }

        [Required(ErrorMessage = "El nombre completo es obligatorio")]
        [MaxLength(500, ErrorMessage = "Máximo 500 caracteres")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [MaxLength(255, ErrorMessage = "Máximo 255 caracteres")]
        [EmailAddress(ErrorMessage = "Debe ser un correo válido")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "El número de teléfono es obligatorio")]
        [MaxLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string UserPhone { get; set; }

        [MaxLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string? ProfessionalNumber { get; set; }

        public string? Signature { get; set; }

        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "El estado de contacto es obligatorio")]
        public long ContactStatus { get; set; }

        public long? State { get; set; }
        
    }
}