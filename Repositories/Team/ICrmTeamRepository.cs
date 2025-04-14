using System.Collections.Generic;
using System.Threading.Tasks;
using crm_app.Dto.Team; // usar los dto

namespace crm_app.Repositories.Team
{
    public interface ICrmTeamRepository
    {
        // Listar todos los equipos
       public Task<IEnumerable<TeamDto>> GetAll();

        // Buscar un equipo por su ID
        public Task<TeamDto?> GetByIdAsync(long id);
        
        // nuevo
        public Task<TeamDto> CreateAsync(TeamPostDto team);
        
        // Actualizar un equipo existente
        public Task<bool> UpdateAsync(long id, TeamPutDto updatedTeamDto);
       
    }
}