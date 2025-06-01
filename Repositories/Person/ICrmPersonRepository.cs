using System.Collections.Generic;
using System.Threading.Tasks;
using crm_app.Dto.Establishment;
using crm_app.Dto.Person; // usar los dto

namespace crm_app.Repositories.Person
{
    public interface ICrmPersonRepository
    {
        //ListarPersonas
        public Task<IEnumerable<PersonDto>> GetAll();
        
        //buscar por id
       public Task<PersonDto?> GetByIdAsync(long id);
     
        
        
        // Nuevo
        public Task<PersonDto> CreateAsync(PersonPostDto person);
        
        //actualizar
        public Task<bool> UpdateAsync(long id, PersonPutDto updatepersonDto);
        
        
    }
}

