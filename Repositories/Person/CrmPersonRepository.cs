using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using crm_app.Dto.Establishment;
using crm_app.Dto.Person;
using crm_app.Models.General;
using crm_core.Utils;
using AppContext = crm_app.Utils.EntityDbContext;

namespace crm_app.Repositories.Person
{
    public class CrmPersonRepository : ICrmPersonRepository
    {
        private readonly AppContext _context;

        public CrmPersonRepository(AppContext context)
        {
            _context = context;
        }
        
        //----------listar
        public async Task<IEnumerable<PersonDto>> GetAll()
        {
            return await _context.Person
                .Select(p=>new PersonDto
                {
                    PersonId = p.PersonId,
                    PersonKey = p.PersonKey,
                    FirstName = p.FirstName,
                    SecondName = p.SecondName,
                    FirstSurname = p.FirstSurname,
                    SecondSurname = p.SecondSurname,
                    Birthdate = p.Birthdate,
                    Gender = p.Gender,
                    BloodType = p.BloodType,
                    Profession = p.Profession,
                    CUI = p.CUI,
                    NIT = p.NIT,
                    Email = p.Email,
                    PhoneNumber = p.PhoneNumber,
                    SecondaryPhoneNumber = p.SecondaryPhoneNumber,
                    Address = p.Address,
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
