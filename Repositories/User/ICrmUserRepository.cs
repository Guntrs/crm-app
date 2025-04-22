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
        
        //buscar por id
     //   public Task<PersonDto?> GetByIdAsync(long id);
     
        // Nuevo
      //  public Task<PersonDto> CreateAsync(PersonPostDto person);
        
        //actualizar
     //   public Task<bool> UpdateAsync(long id, PersonPutDto updatepersonDto);
        
        
    }
}