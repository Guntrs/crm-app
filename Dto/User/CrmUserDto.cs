using System;
using System.ComponentModel.DataAnnotations;
using crm_app.Dto.Typology;

namespace crm_app.Dto.User
{
    public class UserDto
    {
       
        public long UserId { get; set; }

       
        
        public string UserKey { get; set; }

        public long? ParentUserId { get; set; }

      
        public long PersonId { get; set; }

       
        public string UserName { get; set; }

        
        public string Password { get; set; }

        public DateTime PasswordChangeDate { get; set; }

        
        public int AccessAttempt { get; set; }

        
        public string FullName { get; set; }

        
        public string UserEmail { get; set; }

       
        public string UserPhone { get; set; }

        
        public string? ProfessionalNumber { get; set; }

        public string? Signature { get; set; }

        public string? ImageUrl { get; set; }
        
        public long CreatedBy { get; set; }

        public DateTime CreationDate { get; set; }

        public long ModifiedBy { get; set; }

        public DateTime ModificationDate { get; set; }
        
        //datos relacionados
        public TypologyDto? State { get; set; }
        
        public TypologyDto? ContactStatus { get; set; }
        
    }
}