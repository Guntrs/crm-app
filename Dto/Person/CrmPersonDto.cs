using System;
using System.ComponentModel.DataAnnotations;
using crm_app.Dto.Typology;

namespace crm_app.Dto.Person
{
    public class PersonDto
    {
        
        public long PersonId { get; set; }
        public string PersonKey { get; set; }
        public string FirstName { get; set; }
        public string? SecondName { get; set; }
        public string FirstSurname { get; set; }
        public string? SecondSurname { get; set; }
        public string? Birthdate { get; set; }
        public string? Profession { get; set; }
        public long? CUI { get; set; }
        public string? NIT { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? SecondaryPhoneNumber { get; set; }
        public string? Address { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModificationDate { get; set; }
        
        
        //datos relacionados
        public TypologyDto? Gender { get; set; }
        public TypologyDto? State { get; set; }
        public TypologyDto? BloodType { get; set; }
    }
}