using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using crm_core.Utils;

namespace crm_app.Models.General
{
    [Table("crm_users", Schema = "public")]
    public class CrmUser
    {
        [Key]
        [Column("user_id")]
        public long UserId { get; set; }

        [Required]
        [Column("user_key")]
        [MaxLength(255)]
        public string UserKey { get; set; }

        [Column("parent_user_id")]
        public long? ParentUserId { get; set; }

        [Required]
        [Column("person_id")]
        public long PersonId { get; set; }

        [Required]
        [Column("user_name")]
        [MaxLength(255)]
        public string UserName { get; set; }

        [Required]
        [Column("password")]
        [MaxLength(255)]
        public string Password { get; set; }

        [Column("password_change_date")]
        public DateTime PasswordChangeDate { get; set; } = CrmFunctions.GetDateTime();

        [Required]
        [Column("access_attempt")]
        public int AccessAttempt { get; set; } = 0;

        [Required]
        [Column("full_name")]
        [MaxLength(500)]
        public string FullName { get; set; }

        [Required]
        [Column("user_email")]
        [MaxLength(255)]
        public string UserEmail { get; set; }

        [Required]
        [Column("user_phone")]
        [MaxLength(50)]
        public string UserPhone { get; set; }

        [Column("professional_number")]
        [MaxLength(100)]
        public string ProfessionalNumber { get; set; }

        [Column("signature")]
        public string Signature { get; set; }

        [Column("image_url")]
        public string ImageUrl { get; set; }

        [Required]
        [Column("contact_status")]
        public long ContactStatus { get; set; }  // 211

        [Required]
        [Column("state")]
        public long State { get; set; } = CrmConstants.ESTADO_ACTIVO; // 501

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

        // Relaciones (Opcionales si usarás Fluent API para navegación)
        // public CrmPerson Person { get; set; }
        // public CrmUser ParentUser { get; set; }
    }
}