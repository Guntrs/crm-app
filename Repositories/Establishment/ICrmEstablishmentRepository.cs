using System.Collections.Generic;
using System.Threading.Tasks;
using crm_app.Dto.Establishment;
using crm_app.Dto.Team; // usar los dto

namespace crm_app.Repositories.Establishment
{
    public interface ICrmEstablishmentRepository
    {
        // Listar todos los equipos
        public Task<IEnumerable<EstablishmentDto>> GetAll();

        // Buscar un equipo por su ID
        public Task<EstablishmentDto?> GetByIdAsync(long id);
        
        // nuevo
        public Task<EstablishmentDto> CreateAsync(EstablishmentPostDto establishment);
        
        // Actualizar un equipo existente
        public Task<bool> UpdateAsync(long id, EstablishmentPutDto updatedEstablishmentDto);
       
    }
}