using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using crm_app.Dto.Establishment;
using crm_app.Dto.User;
using crm_app.Models.General;
using crm_core.Utils;
using crm_app.Dto.Typology;
using AppContext = crm_app.Utils.EntityDbContext;
using Microsoft.AspNetCore.Identity;

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
            var userQuery = _context.User.AsQueryable();

            // Mapear usando el método
            var result = MapeoUserDto(userQuery);

            return await Task.FromResult(result);
        }
        
        //----------- Método privado de mapeo con joins
        private List<UserDto> MapeoUserDto(IQueryable<CrmUser> users)
        {
            return (
                from u in users

                join st in _context.Typologies on u.State equals st.TypologyId into stateGroup
                from state in stateGroup.DefaultIfEmpty()

                join cs in _context.Typologies on u.ContactStatus equals cs.TypologyId into contactStatusGroup
                from contactStatus in contactStatusGroup.DefaultIfEmpty()

                orderby u.UserId descending

                select new UserDto
                {
                    UserId = u.UserId,
                    UserKey = u.UserKey,
                    ParentUserId = u.ParentUserId,
                    PersonId = u.PersonId,

                    UserName = u.UserName,
                    Password = u.Password,
                    PasswordChangeDate = u.PasswordChangeDate,
                    AccessAttempt = u.AccessAttempt,
                    FullName = u.FullName,
                    UserEmail = u.UserEmail,
                    UserPhone = u.UserPhone,
                    ProfessionalNumber = u.ProfessionalNumber,
                    Signature = u.Signature,
                    ImageUrl = u.ImageUrl,

                    CreatedBy = u.CreatedBy,
                    CreationDate = u.CreationDate,
                    ModifiedBy = u.ModifiedBy,
                    ModificationDate = u.ModificationDate,

                    State = state == null ? null : new TypologyDto
                    {
                        TypologyId = state.TypologyId,
                        Description = state.Description
                    },
                    ContactStatus = contactStatus == null ? null : new TypologyDto
                    {
                        TypologyId = contactStatus.TypologyId,
                        Description = contactStatus.Description
                    }
                }
            ).ToList();
        }

//----------- Buscar por su ID ------------------
        public async Task<UserDto?> GetByIdAsync(long id)
        {
            var query = (
                from u in _context.User
                where u.UserId == id

                join st in _context.Typologies on u.State equals st.TypologyId into stateGroup
                from state in stateGroup.DefaultIfEmpty()

                join cs in _context.Typologies on u.ContactStatus equals cs.TypologyId into contactStatusGroup
                from contactStatus in contactStatusGroup.DefaultIfEmpty()

                select new UserDto
                {
                    UserId = u.UserId,
                    UserKey = u.UserKey,
                    ParentUserId = u.ParentUserId,
                    PersonId = u.PersonId,

                    UserName = u.UserName,
                    Password = u.Password,
                    PasswordChangeDate = u.PasswordChangeDate,
                    AccessAttempt = u.AccessAttempt,
                    FullName = u.FullName,
                    UserEmail = u.UserEmail,
                    UserPhone = u.UserPhone,
                    ProfessionalNumber = u.ProfessionalNumber,
                    Signature = u.Signature,
                    ImageUrl = u.ImageUrl,

                    CreatedBy = u.CreatedBy,
                    CreationDate = u.CreationDate,
                    ModifiedBy = u.ModifiedBy,
                    ModificationDate = u.ModificationDate,

                    State = state == null ? null : new TypologyDto
                    {
                        TypologyId = state.TypologyId,
                        Description = state.Description
                    },
                    ContactStatus = contactStatus == null ? null : new TypologyDto
                    {
                        TypologyId = contactStatus.TypologyId,
                        Description = contactStatus.Description
                    }
                }
            );

            return await query.FirstOrDefaultAsync();
        }
        
        
    
        
        
         //----------- Nuevo------------------
        public async Task<UserDto> CreateAsync(UserPostDto userPostDto)
        {
            // Mapear el DTO al modelo CrmUser
            var user = new CrmUser
            {
                UserKey = userPostDto.UserKey,
                ParentUserId = userPostDto.ParentUserId,
                PersonId = userPostDto.PersonId,
                UserName = userPostDto.UserName,
                // Password la asignas después del hashing,
                PasswordChangeDate = CrmFunctions.GetDateTime(),
                AccessAttempt = 0,
                FullName = userPostDto.FullName,
                UserEmail = userPostDto.UserEmail,
                UserPhone = userPostDto.UserPhone,
                ProfessionalNumber = userPostDto.ProfessionalNumber,
                Signature = userPostDto.Signature,
                ImageUrl = userPostDto.ImageUrl,
                ContactStatus = userPostDto.ContactStatus,
                State = CrmConstants.ESTADO_ACTIVO,
                CreatedBy = 0,
                CreationDate = CrmFunctions.GetDateTime(),
                ModifiedBy = 0,
                ModificationDate = CrmFunctions.GetDateTime()
            };
            
            // -------------- hash ---------------
            var hasher = new PasswordHasher<CrmUser>();
            user.Password = hasher.HashPassword(user, userPostDto.Password);
            // ---------------------------------------------------

            // Agregar al contexto
            await _context.User.AddAsync(user);

            // Guardar cambios
            await _context.SaveChangesAsync();
            
            // Obtén las tipologías relacionadas (puedes hacer un solo query si quieres performance, aquí lo hago simple)
            

            var stateTypology = await _context.Typologies
                .FirstOrDefaultAsync(t => t.TypologyId == user.State);
            
            var contactTypeTypology = await _context.Typologies
                .FirstOrDefaultAsync(t => t.TypologyId == user.ContactStatus);
            

            // Mapear al DTO de salida
            var createdDto = new UserDto
            {
                UserId = user.UserId,
                UserKey = user.UserKey,
                ParentUserId = user.ParentUserId,
                PersonId = user.PersonId,
                UserName = user.UserName,
                Password = user.Password,
                PasswordChangeDate = user.PasswordChangeDate,
                AccessAttempt = user.AccessAttempt,
                FullName = user.FullName,
                UserEmail = user.UserEmail,
                UserPhone = user.UserPhone,
                ProfessionalNumber = user.ProfessionalNumber,
                Signature = user.Signature,
                ImageUrl = user.ImageUrl,
               // ContactStatus = user.ContactStatus,
                ContactStatus  = contactTypeTypology == null ? null : new TypologyDto
                {
                    TypologyId = contactTypeTypology.TypologyId,
                    Description = contactTypeTypology.Description
                },
                State = stateTypology == null ? null : new TypologyDto
                {
                    TypologyId = stateTypology.TypologyId,
                    Description = stateTypology.Description
                },
                CreatedBy = user.CreatedBy,
                CreationDate = user.CreationDate,
                ModifiedBy = user.ModifiedBy,
                ModificationDate = user.ModificationDate
            };


            return createdDto;
        }
        
        
        
        
        // --------------------Actualizar ------------------
        public async Task<bool> UpdateAsync(long id, UserPutDto dto)
        {
            // Buscar el usuario por su ID
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return false; // No se encontró el usuario
            }

            // Mapear los valores del DTO al modelo
            user.UserKey = dto.UserKey;
            user.ParentUserId = dto.ParentUserId;
            user.PersonId = dto.PersonId;
            user.UserName = dto.UserName;
            
            
            if (!string.IsNullOrEmpty(dto.Password))
            {
                var hasher = new PasswordHasher<CrmUser>();
                user.Password = hasher.HashPassword(user, dto.Password);
            }

            
            user.FullName = dto.FullName;
            user.UserEmail = dto.UserEmail;
            user.UserPhone = dto.UserPhone;
            user.ProfessionalNumber = dto.ProfessionalNumber;
            user.Signature = dto.Signature;
            user.ImageUrl = dto.ImageUrl;
            user.ContactStatus = dto.ContactStatus;
            user.State = dto.State;

            // Actualizar los campos de auditoría
            user.ModificationDate = CrmFunctions.GetDateTime();
            user.ModifiedBy = 0; // Reemplazar si se maneja ID de usuario en sesión

            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();
            return true;
        }
        
        
        
        
      
        /// Busca un usuario por su nombre de usuario (UserName) en la base de datos
        /// <param name="userName">Nombre de usuario a buscar</param>
        /// <returns>UserDto o null si no existe</returns>
        public async Task<UserDto?> GetByUserNameAsync(string userName)
        {
            return await _context.User
                .Where(u => u.UserName == userName)
                .Select(u => new UserDto
                {
                    // Mapea todos los campos del usuario
                    UserId = u.UserId,
                    UserKey = u.UserKey,
                    ParentUserId = u.ParentUserId,
                    PersonId = u.PersonId,
                    UserName = u.UserName,
                    Password = u.Password,
                    PasswordChangeDate = u.PasswordChangeDate,
                    AccessAttempt = u.AccessAttempt,
                    FullName = u.FullName,
                    UserEmail = u.UserEmail,
                    UserPhone = u.UserPhone,
                    ProfessionalNumber = u.ProfessionalNumber,
                    Signature = u.Signature,
                    ImageUrl = u.ImageUrl,
                    //ContactStatus = u.ContactStatus,
                    //State = u.State,
                    CreatedBy = u.CreatedBy,
                    CreationDate = u.CreationDate,
                    ModifiedBy = u.ModifiedBy,
                    ModificationDate = u.ModificationDate
                })
                .FirstOrDefaultAsync();
        }
        
        
        
    }
}
