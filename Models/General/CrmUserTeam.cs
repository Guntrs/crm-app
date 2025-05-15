using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using crm_core.Utils;

namespace crm_app.Models.General
{
    [Table("crm_users_teams", Schema = "public")]
    public class CrmUserTeam
    {
        [Key]
        [Column("users_team_id")]
        public long UsersTeamId { get; set; }

        [Required]
        [Column("user_id")]
        public long UserId { get; set; }

        [Required]
        [Column("team_id")]
        public long TeamId { get; set; }

        [Required]
        [Column("role")]
        public long Role { get; set; } = 310;

        [Required]
        [Column("state")]
        public long State { get; set; } = 501;

        // Auditoría
        [Required]
        [Column("created_by")]
        public long CreatedBy { get; set; } = 0;

        [Required]
        [Column("creation_date")]
        public DateTime CreationDate { get; set; } = CrmFunctions.GetDateTime();

        [Required]
        [Column("modified_by")]
        public long ModifiedBy { get; set; } = 0;

        [Required]
        [Column("modification_date")]
        public DateTime ModificationDate { get; set; } = CrmFunctions.GetDateTime();

   
        
    }
}