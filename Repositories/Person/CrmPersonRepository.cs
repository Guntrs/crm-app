using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using crm_app.Dto.Establishment;
using crm_app.Dto.Person;
using crm_app.Dto.Typology;
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
            var personQuery = _context.Person.AsQueryable();

            // Aquí puedes agregar filtros si los necesitas:
            // userTeamsQuery = userTeamsQuery.Where(...);

            var result = MapeoPersonDto(personQuery);

            return await Task.FromResult(result);
        }
        
        //----------- Mapeo con joins y TypologyDto
        private List<PersonDto> MapeoPersonDto(IQueryable<CrmPerson> persons)
        {
            return (
                from p in persons

                join g in _context.Typologies on p.Gender equals g.TypologyId into genderGroup
                from gender in genderGroup.DefaultIfEmpty()

                join bt in _context.Typologies on p.BloodType equals bt.TypologyId into bloodTypeGroup
                from bloodType in bloodTypeGroup.DefaultIfEmpty()

                join st in _context.Typologies on p.State equals st.TypologyId into stateGroup
                from state in stateGroup.DefaultIfEmpty()

                orderby p.PersonId descending
                select new PersonDto
                {
                    PersonId = p.PersonId,
                    PersonKey = p.PersonKey,
                    FirstName = p.FirstName,
                    SecondName = p.SecondName,
                    FirstSurname = p.FirstSurname,
                    SecondSurname = p.SecondSurname,
                    Birthdate = p.Birthdate,
                    Profession = p.Profession,
                    CUI = p.CUI,
                    NIT = p.NIT,
                    Email = p.Email,
                    PhoneNumber = p.PhoneNumber,
                    SecondaryPhoneNumber = p.SecondaryPhoneNumber,
                    Address = p.Address,
                    CreatedBy = p.CreatedBy,
                    CreationDate = p.CreationDate,
                    ModifiedBy = p.ModifiedBy,
                    ModificationDate = p.ModificationDate,

                    Gender = gender == null ? null : new TypologyDto() {
                        TypologyId = gender.TypologyId,
                        Description = gender.Description,
                        // agrega otros campos si los tienes en TypologyDto
                    },
                    BloodType = bloodType == null ? null : new TypologyDto {
                        TypologyId = bloodType.TypologyId,
                        Description = bloodType.Description
                    },
                    State = state == null ? null : new TypologyDto {
                        TypologyId = state.TypologyId,
                        Description = state.Description
                    }
                }
            ).ToList();
        }
        
        /*
        
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
                
        }*/
        
        /*
        //----------- Buscar por su ID ------------------
        public async Task<PersonDto?> GetByIdAsync(long id)
        {
            return await _context.Person
                .Where(p => p.PersonId == id)
                .Select(p => new PersonDto
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
                .FirstOrDefaultAsync();
        }*/
        
        
        /*
                //----------- Nuevo ------------------
        public async Task<PersonDto> CreateAsync(PersonPostDto personPostDto)
        {
            // Mapear el DTO al modelo CrmPerson
            var person = new CrmPerson
            {
                PersonKey = personPostDto.PersonKey,
                FirstName = personPostDto.FirstName,
                SecondName = personPostDto.SecondName ?? "S/D",
                FirstSurname = personPostDto.FirstSurname,
                SecondSurname = personPostDto.SecondSurname ?? "S/D",
                Birthdate = personPostDto.Birthdate,
                Gender = personPostDto.Gender,
                BloodType = personPostDto.BloodType,
                Profession = personPostDto.Profession,
                CUI = personPostDto.CUI,
                NIT = personPostDto.NIT,
                Email = personPostDto.Email,
                PhoneNumber = personPostDto.PhoneNumber,
                SecondaryPhoneNumber = personPostDto.SecondaryPhoneNumber,
                Address = personPostDto.Address,
                State = CrmConstants.ESTADO_ACTIVO,
                CreatedBy = 0,
                CreationDate = CrmFunctions.GetDateTime(),
                ModifiedBy = 0,
                ModificationDate = CrmFunctions.GetDateTime()
            };

            // Agregar al contexto
            await _context.Person.AddAsync(person);

            // Guardar cambios
            await _context.SaveChangesAsync();

            // Mapear al DTO de salida
            var createdDto = new PersonDto
            {
                PersonId = person.PersonId,
                PersonKey = person.PersonKey,
                FirstName = person.FirstName,
                SecondName = person.SecondName,
                FirstSurname = person.FirstSurname,
                SecondSurname = person.SecondSurname,
                Birthdate = person.Birthdate,
                Gender = person.Gender,
                BloodType = person.BloodType,
                Profession = person.Profession,
                CUI = person.CUI,
                NIT = person.NIT,
                Email = person.Email,
                PhoneNumber = person.PhoneNumber,
                SecondaryPhoneNumber = person.SecondaryPhoneNumber,
                Address = person.Address,
                State = person.State,
                CreatedBy = person.CreatedBy,
                CreationDate = person.CreationDate,
                ModifiedBy = person.ModifiedBy,
                ModificationDate = person.ModificationDate
            };

            return createdDto;
        }*/
        
        // --------------------Actualizar ------------------
        public async Task<bool> UpdateAsync(long id, PersonPutDto dto)
        {
            // Buscar el establecimiento por su ID
            var person = await _context.Person.FindAsync(id);

            if (person == null)
            {
                return false; // No se encontró la Persona
            }

            // Mapear los valores del Dto al Modelo
            person.PersonKey = dto.PersonKey;
            person.FirstName = dto.FirstName;
            person.SecondName = dto.SecondName ?? "S/D";
            person.FirstSurname = dto.FirstSurname;
            person.SecondSurname = dto.SecondSurname ?? "S/D";
            person.Gender = dto.Gender;
            person.BloodType = dto.BloodType;
            person.Profession = dto.Profession;
            person.CUI = dto.CUI;
            person.NIT = dto.NIT;
            person.Email = dto.Email;
            person.PhoneNumber = dto.PhoneNumber;
            person.SecondaryPhoneNumber = dto.SecondaryPhoneNumber;
            person.Address = dto.Address;
            person.State = dto.State;

            person.ModificationDate = CrmFunctions.GetDateTime();
            person.ModifiedBy = 0; // Reemplazar si se maneja el ID de usuario

            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();
            return true;
        }
                
        
        
    }
}
