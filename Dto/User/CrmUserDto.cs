using System;
using System.ComponentModel.DataAnnotations;

namespace crm_app.Dto.User
{
    public class UserDto
    {
        [Required]
        public long UserId { get; set; }

        [Required]
        [MaxLength(255, ErrorMessage = "Máximo 255 caracteres")]
        public string UserKey { get; set; }

        public long? ParentUserId { get; set; }

        [Required]
        public long PersonId { get; set; }

        [Required]
        [MaxLength(255, ErrorMessage = "Máximo 255 caracteres")]
        public string UserName { get; set; }

        [Required]
        [MaxLength(255, ErrorMessage = "Máximo 255 caracteres")]
        public string Password { get; set; }

        public DateTime PasswordChangeDate { get; set; }

        [Required]
        public int AccessAttempt { get; set; }

        [Required]
        [MaxLength(500, ErrorMessage = "Máximo 500 caracteres")]
        public string FullName { get; set; }

        [Required]
        [MaxLength(255, ErrorMessage = "Máximo 255 caracteres")]
        public string UserEmail { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string UserPhone { get; set; }

        [MaxLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string? ProfessionalNumber { get; set; }

        public string? Signature { get; set; }

        public string? ImageUrl { get; set; }

        [Required]
        public long ContactStatus { get; set; }

        [Required]
        public long State { get; set; }

        public long CreatedBy { get; set; }

        public DateTime CreationDate { get; set; }

        public long ModifiedBy { get; set; }

        public DateTime ModificationDate { get; set; }
    }
}