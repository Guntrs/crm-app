using System;
using System.ComponentModel.DataAnnotations;

namespace crm_app.Dto.Team
{
    public class TeamDto
    {
        [Required]
        public long TeamId { get; set; }
        
        [Required]
        [MaxLength(255, ErrorMessage = "Máximo 255 caracteres")]
        public string TeamName { get; set; }
        
        [MaxLength(255, ErrorMessage = "Máximo 255 caracteres")]
        public string? TeamDescription { get; set; }
        
        public long State { get; set; }
        
        public long CreatedBy { get; set; }
        
        public DateTime CreationDate { get; set; }
        
        public long ModifiedBy { get; set; }
        
        public DateTime ModificationDate { get; set; }
    }
}