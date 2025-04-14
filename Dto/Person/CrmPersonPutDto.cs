using System;
using System.ComponentModel.DataAnnotations;

namespace crm_app.Dto.Person
{
    public class PersonPutDto
    {
        [Required(ErrorMessage = "El ID de la persona es obligatorio")]
        public long PersonId { get; set; }

        [Required(ErrorMessage = "La clave de la persona es obligatoria")]
        [MaxLength(255, ErrorMessage = "Máximo 255 caracteres")]
        public string PersonKey { get; set; }

        [Required(ErrorMessage = "El primer nombre es obligatorio")]
        [MaxLength(255, ErrorMessage = "Máximo 255 caracteres")]
        public string FirstName { get; set; }

        [MaxLength(255, ErrorMessage = "Máximo 255 caracteres")]
        public string? SecondName { get; set; }

        [Required(ErrorMessage = "El primer apellido es obligatorio")]
        [MaxLength(255, ErrorMessage = "Máximo 255 caracteres")]
        public string FirstSurname { get; set; }

        [MaxLength(255, ErrorMessage = "Máximo 255 caracteres")]
        public string? SecondSurname { get; set; }

        public DateTime? Birthdate { get; set; }

        [Required(ErrorMessage = "El género es obligatorio")]
        public long Gender { get; set; }

        [Required(ErrorMessage = "El tipo de sangre es obligatorio")]
        public long BloodType { get; set; }

        public string? Profession { get; set; }

        public long? CUI { get; set; }

        [MaxLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string? NIT { get; set; }

        [MaxLength(255, ErrorMessage = "Máximo 255 caracteres")]
        [EmailAddress(ErrorMessage = "Debe ser un correo válido")]
        public string? Email { get; set; }

        [MaxLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string? PhoneNumber { get; set; }

        [MaxLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string? SecondaryPhoneNumber { get; set; }

        [MaxLength(500, ErrorMessage = "Máximo 500 caracteres")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "El estado de la persona es obligatorio")]
        public long State { get; set; }
    }
}