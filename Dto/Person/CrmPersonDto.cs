using System;
using System.ComponentModel.DataAnnotations;

namespace crm_app.Dto.Person
{
    public class PersonDto
    {
        [Required]
        public long PersonId { get; set; }

        [Required]
        [MaxLength(255, ErrorMessage = "Máximo 255 caracteres")]
        public string PersonKey { get; set; }

        [Required]
        [MaxLength(255, ErrorMessage = "Máximo 255 caracteres")]
        public string FirstName { get; set; }

        [MaxLength(255, ErrorMessage = "Máximo 255 caracteres")]
        public string? SecondName { get; set; }

        [Required]
        [MaxLength(255, ErrorMessage = "Máximo 255 caracteres")]
        public string FirstSurname { get; set; }

        [MaxLength(255, ErrorMessage = "Máximo 255 caracteres")]
        public string? SecondSurname { get; set; }

        public DateTime? Birthdate { get; set; }

        [Required]
        public long Gender { get; set; }

        [Required]
        public long BloodType { get; set; }

        public string? Profession { get; set; }

        public long? CUI { get; set; }

        [MaxLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string? NIT { get; set; }

        [MaxLength(255, ErrorMessage = "Máximo 255 caracteres")]
        public string? Email { get; set; }

        [MaxLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string? PhoneNumber { get; set; }

        [MaxLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string? SecondaryPhoneNumber { get; set; }

        [MaxLength(500, ErrorMessage = "Máximo 500 caracteres")]
        public string? Address { get; set; }

        [Required]
        public long State { get; set; }

        public long CreatedBy { get; set; }

        public DateTime CreationDate { get; set; }

        public long ModifiedBy { get; set; }

        public DateTime ModificationDate { get; set; }
    }
}