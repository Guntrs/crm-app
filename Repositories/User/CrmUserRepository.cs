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
        
        //----------- Buscar por su ID ------------------
        public async Task<UserDto?> GetByIdAsync(long id)
        {
            return await _context.User
                .Where(p => p.UserId == id)
                .Select(p => new UserDto
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
                .FirstOrDefaultAsync();
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
                Password = userPostDto.Password,
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

            // Agregar al contexto
            await _context.User.AddAsync(user);

            // Guardar cambios
            await _context.SaveChangesAsync();

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
                ContactStatus = user.ContactStatus,
                State = user.State,
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
            user.Password = dto.Password;
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
        
        /// <summary>
        /// Busca un usuario por su nombre de usuario (UserName) en la base de datos.
        /// </summary>
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
                    ContactStatus = u.ContactStatus,
                    State = u.State,
                    CreatedBy = u.CreatedBy,
                    CreationDate = u.CreationDate,
                    ModifiedBy = u.ModifiedBy,
                    ModificationDate = u.ModificationDate
                })
                .FirstOrDefaultAsync();
        }
        
    }
}
