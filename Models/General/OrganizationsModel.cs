using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace crm_app.Models
{
    [Table("crm_organizations")]
    public class OrganizationModel
    {
        [Key]
        [Column("organization_id")]
        public long OrganizationId { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("organization_name")]
        public string OrganizationName { get; set; }

        [MaxLength(15)]
        [Column("organization_phone")]
        public string? OrganizationPhone { get; set; }

        [MaxLength(2083)]
        [Column("logo_url")]
        public string? LogoUrl { get; set; }

        [EmailAddress]
        [MaxLength(255)]
        [Column("primary_contact_email")]
        public string? PrimaryContactEmail { get; set; }

        [MaxLength(255)]
        [Column("sector_type")]
        public string? SectorType { get; set; }

        [Column("created_by")]
        public long CreatedBy { get; set; }

        [Column("creation_date")]
        public DateTime CreationDate { get; set; }

        [Column("modified_by")]
        public long ModifiedBy { get; set; }

        [Column("modification_date")]
        public DateTime ModificationDate { get; set; }
    }
}