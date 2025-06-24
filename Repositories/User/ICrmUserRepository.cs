using System.Collections.Generic;
using System.Threading.Tasks;
using crm_app.Dto.Establishment;
using crm_app.Dto.User; // usar los dto

namespace crm_app.Repositories.User
{
    public interface ICrmUserRepository
    {
        //ListarPersonas
        public Task<IEnumerable<UserDto>> GetAll();
        
        /*
        //buscar por id
        public Task<UserDto?> GetByIdAsync(long id);
     
        // Nuevo
        public Task<UserDto> CreateAsync(UserPostDto user);
        
        //actualizar
        public Task<bool> UpdateAsync(long id, UserPutDto updateuserDto);
        */
        
        /// <summary>
        /// Busca un usuario por su nombre de usuario (UserName).
        /// </summary>
        /// <param name="userName">Nombre de usuario a buscar</param>
        /// <returns>UserDto o null si no existe</returns>
        public Task<UserDto?> GetByUserNameAsync(string userName);
        
        
    }
}