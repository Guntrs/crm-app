using System;
using System.ComponentModel.DataAnnotations;

namespace crm_app.Dto.Typology
{
    public class TypologyDto
    {
        [Required]
        public long TypologyId { get; set; }

        public long? ParentTypologyId { get; set; }

        [Required]
        [MaxLength(255, ErrorMessage = "Máximo 255 caracteres")]
        public string Description { get; set; }

        [MaxLength(255, ErrorMessage = "Máximo 255 caracteres")]
        public string? Value1 { get; set; }

        [MaxLength(255, ErrorMessage = "Máximo 255 caracteres")]
        public string? Value2 { get; set; }

        [MaxLength(255, ErrorMessage = "Máximo 255 caracteres")]
        public string? Value3 { get; set; }

        public long State { get; set; }

        public long CreatedBy { get; set; }

        public DateTime CreationDate { get; set; }

        public long ModifiedBy { get; set; }

        public DateTime ModificationDate { get; set; }
    }
}