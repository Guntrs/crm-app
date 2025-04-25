using System;
using System.ComponentModel.DataAnnotations;

namespace crm_app.Dto.UserTeam
{
    public class UserTeamDto
    {
        [Required]
        public long UsersTeamId { get; set; }

        [Required]
        public long UserId { get; set; }

        [Required]
        public long TeamId { get; set; }

        [Required]
        public long Role { get; set; } = 310; // Por defecto

        [Required]
        public long State { get; set; } = 501; // Por defecto

        public long CreatedBy { get; set; }

        public DateTime CreationDate { get; set; }

        public long ModifiedBy { get; set; }

        public DateTime ModificationDate { get; set; }
    }
}