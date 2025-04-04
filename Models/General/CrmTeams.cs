using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace crm_app.Models.General

{
    [Table("crm_teams", Schema = "public")]
    public class CrmTeam
    {
        [Key]
        [Column("team_id")]
        public long TeamId { get; set; }

        [Required]
        [Column("team_name")]
        [MaxLength(255)]
        public string TeamName { get; set; }

        [Column("team_description")]
        public string TeamDescription { get; set; } = "S/D";

        [Required]
        [Column("state")]
        public long State { get; set; }

        [Required]
        [Column("created_by")]
        public long CreatedBy { get; set; }

        [Required]
        [Column("creation_date")]
        public DateTime CreationDate { get; set; }

        [Required]
        [Column("modified_by")]
        public long ModifiedBy { get; set; }

        [Required]
        [Column("modification_date")]
        public DateTime ModificationDate { get; set; }
    }
    
}

