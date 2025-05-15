using System;
using System.ComponentModel.DataAnnotations;
using crm_app.Dto.Team;
using crm_app.Dto.User;

namespace crm_app.Dto.UserTeam
{
    public class UserTeamDto
    {
       
        public long UsersTeamId { get; set; }

        
        public long UserId { get; set; }

        
        public long TeamId { get; set; }

        
        public long Role { get; set; } 

       
        public long State { get; set; } 

        public long CreatedBy { get; set; }

        public DateTime CreationDate { get; set; }

        public long ModifiedBy { get; set; }

        public DateTime ModificationDate { get; set; }
        
        //datos relacionados
        
        public TeamDto? Team { get; set; }
        
        public UserDto? User { get; set; }
    }
}