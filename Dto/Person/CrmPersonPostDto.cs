using System;
using System.ComponentModel.DataAnnotations;

namespace crm_app.Dto.Person
{
    public class PersonPostDto
    {
        
        public string PersonKey { get; set; }
        
        public string FirstName { get; set; }
        
        public string? SecondName { get; set; }
        
        public string FirstSurname { get; set; }
        
        public string? SecondSurname { get; set; }

        public string? Birthdate { get; set; }
        
        public long Gender { get; set; }
        
        public long BloodType { get; set; }

        public string? Profession { get; set; }

        public long? CUI { get; set; }
        
        public string? NIT { get; set; }
        
        public string? Email { get; set; }
        
        public string? PhoneNumber { get; set; }
        
        public string? SecondaryPhoneNumber { get; set; }
        
        public string? Address { get; set; }

        // Se puede establecer el estado si se desea, o dejar que el modelo lo asigne por defecto.
        public long? State { get; set; }
        
        
    }
}