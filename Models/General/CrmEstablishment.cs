using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using crm_core.Utils;

namespace crm_app.Models.General

{
    [Table("crm_establishment", Schema = "public")]
    public class CrmEstablishment
    {
        [Key]
        [Column("establishment_id")]
        public long EstablishmentId { get; set; }

        [ForeignKey("ParentEstablishment")]
        [Column("parent_establishment_id")] //llave hacia la si misma
        public long? ParentEstablishmentId { get; set; }

        [Required]
        [Column("establishment_key")]
        [MaxLength(255)]
        public string EstablishmentKey { get; set; }

        [Required]
        [Column("establishment_name")]
        [MaxLength(255)]
        public string EstablishmentName { get; set; }

        [Column("establishment_description")]
        public string EstablishmentDescription { get; set; }

        [Required]
        [Column("establishment_address")]
        [MaxLength(500)]
        public string EstablishmentAddress { get; set; }

        [Required]
        [Column("establishment_email")]
        [MaxLength(255)]
        public string EstablishmentEmail { get; set; }

        [Required]
        [Column("establishment_phone")]
        [MaxLength(50)]
        public string EstablishmentPhone { get; set; } 

        [Required]
        [Column("establishment_type")]
        public long EstablishmentType { get; set; }

        [Required]
        [Column("establishment_status")]
        public long EstablishmentStatus { get; set; } = CrmConstants.ESTADO_ACTIVO;  // Default "Activo"

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

        // Relaciones
        public virtual CrmEstablishment ParentEstablishment { get; set; }
    }
    
}