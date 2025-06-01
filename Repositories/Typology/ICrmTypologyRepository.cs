using System.Collections.Generic;  // IEnumerable
using System.Threading.Tasks; //metodos asincronos
using crm_app.Models;  //para acceder al modelo
using crm_app.Dto.Typology;

namespace crm_app.Repositories.Typologies
{
    public interface ICrmTypologyRepository
    {
        //LISTAR 
        //Linq
        //list-ienumerable
        public  Task <IEnumerable<TypologyDto>> GetAll();
        
        //Listar ID
        public Task<TypologyDto?> GetByIdAsync(int id);
        
        // Listar por tipo de tipología 
        Task<IEnumerable<TypologyDto>> GetByParentIdAsync(long parentId);
        
    }
}