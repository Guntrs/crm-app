using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using crm_core.Utils;

namespace crm_app.Models.General
{
    [Table("crm_persons", Schema = "public")]
    public class CrmPerson
    {
        [Key]
        [Column("person_id")]
        public long PersonId { get; set; }

        [Required]
        [Column("person_key")]
        [MaxLength(255)]
        public string PersonKey { get; set; }

        [Required]
        [Column("first_name")]
        [MaxLength(255)]
        public string FirstName { get; set; } 

        [Column("second_name")]
        [MaxLength(255)]
        public string SecondName { get; set; }

        [Required]
        [Column("first_surname")]
        [MaxLength(255)]
        public string FirstSurname { get; set; } 

        [Column("second_surname")]
        [MaxLength(255)]
        public string SecondSurname { get; set; }

        [Column("birthdate")]
        public DateTime? Birthdate { get; set; }

        [Required]
        [Column("gender")]
        public long Gender { get; set; } 

        [Required]
        [Column("blood_type")]
        public long BloodType { get; set; } 

        [Column("profession")]
        public string Profession { get; set; }

        [Column("cui")]
        public long? CUI { get; set; }

        [Column("nit")]
        [MaxLength(50)]
        public string NIT { get; set; }

        [Column("email")]
        [MaxLength(255)]
        public string Email { get; set; }

        [Column("phone_number")]
        [MaxLength(50)]
        public string PhoneNumber { get; set; }

        [Column("secondary_phone_number")]
        [MaxLength(50)]
        public string SecondaryPhoneNumber { get; set; }

        [Column("address")]
        [MaxLength(500)]
        public string Address { get; set; }

        [Required]
        [Column("state")]
        public long State { get; set; } = CrmConstants.ESTADO_ACTIVO;  // Default "Activo"

        // Auditoría
        [Required]
        [Column("created_by")]
        public long CreatedBy { get; set; } = 0;

        [Required]
        [Column("creation_date")]
        public DateTime CreationDate { get; set; }= CrmFunctions.GetDateTime();

        [Required]
        [Column("modified_by")]
        public long ModifiedBy { get; set; } = 0;

        [Required]
        [Column("modification_date")]
        public DateTime ModificationDate { get; set; } = CrmFunctions.GetDateTime();
    }
}