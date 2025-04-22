using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using crm_app.Dto.Establishment;
using crm_app.Dto.User;
using crm_app.Models.General;
using crm_core.Utils;
using AppContext = crm_app.Utils.EntityDbContext;

namespace crm_app.Repositories.User
{
    public class CrmUserRepository : ICrmUserRepository
    {
        private readonly AppContext _context;

        public CrmUserRepository(AppContext context)
        {
            _context = context;
        }
        
        //----------listar
        public async Task<IEnumerable<UserDto>> GetAll()
        {
            return await _context.User
                .Select(p=>new UserDto
                {
                    UserId = p.UserId,
                    UserKey = p.UserKey,
                    ParentUserId = p.ParentUserId,
                    PersonId = p.PersonId,
                    
                    UserName = p.UserName,
                    Password = p.Password,
                    PasswordChangeDate = p.PasswordChangeDate,
                    AccessAttempt = p.AccessAttempt,
                    FullName = p.FullName,
                    UserEmail = p.UserEmail,
                    UserPhone = p.UserPhone,
                    ProfessionalNumber = p.ProfessionalNumber,
                    Signature = p.Signature,
                    ImageUrl = p.ImageUrl,
                    ContactStatus = p.ContactStatus,
                    
                    State = p.State,
                    CreatedBy = p.CreatedBy,
                    CreationDate = p.CreationDate,
                    ModifiedBy = p.ModifiedBy,
                    ModificationDate = p.ModificationDate
                    
                })
                .ToListAsync();
                
        }
        
        
        
        
    }
}
