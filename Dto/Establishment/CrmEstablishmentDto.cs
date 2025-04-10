using System;
using System.ComponentModel.DataAnnotations;

namespace crm_app.Dto.Establishment
{
    public class EstablishmentDto
    {
        [Required]
        public long EstablishmentId { get; set; }

        public long? ParentEstablishmentId { get; set; }

        [Required]
        [MaxLength(255, ErrorMessage = "Máximo 255 caracteres")]
        public string EstablishmentKey { get; set; }

        [Required]
        [MaxLength(255, ErrorMessage = "Máximo 255 caracteres")]
        public string EstablishmentName { get; set; }

        public string? EstablishmentDescription { get; set; }

        [Required]
        [MaxLength(500, ErrorMessage = "Máximo 500 caracteres")]
        public string EstablishmentAddress { get; set; }

        [Required]
        [MaxLength(255, ErrorMessage = "Máximo 255 caracteres")]
        public string EstablishmentEmail { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string EstablishmentPhone { get; set; }

        [Required]
        public long EstablishmentType { get; set; }

        [Required]
        public long EstablishmentStatus { get; set; }

        public long CreatedBy { get; set; }

        public DateTime CreationDate { get; set; }

        public long ModifiedBy { get; set; }

        public DateTime ModificationDate { get; set; }
    }
}