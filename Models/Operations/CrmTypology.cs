using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace crm_app.Models.General
{
    [Table("crm_typologies", Schema = "public")]
    public class CrmTypology
    {
        [Key]
        [Column("typology_id")]
        public long TypologyId { get; set; }

        [Column("parent_typology_id")]
        public long? ParentTypologyId { get; set; }

        [Required]
        [Column("description")]
        public string Description { get; set; }

        [Required]
        [Column("value1")]
        public string Value1 { get; set; }

        [Required]
        [Column("value2")]
        public string Value2 { get; set; }

        [Required]
        [Column("value3")]
        public string Value3 { get; set; }

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
        
        // Navegación recursiva (opcional)
        [ForeignKey("ParentTypologyId")]
        public CrmTypology? Parent { get; set; }
    }
}
