using System.Collections.Generic;
using System.Threading.Tasks;
using crm_app.Dto.Team;

namespace crm_app.Repositories.Team
{
    public interface ICrmTeamRepository
    {
        // Listar todos los equipos
       public Task<IEnumerable<TeamDto>> GetAll();

        // Buscar un equipo por su ID
        public Task<TeamDto?> GetByIdAsync(long id);
    }
}