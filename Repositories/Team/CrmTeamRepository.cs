using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using crm_app.Dto.Team;
using crm_app.Models.General;
using AppContext = crm_app.Utils.EntityDbContext; // Ajusta el namespace según tu proyecto

namespace crm_app.Repositories.Team
{
    public class CrmTeamRepository : ICrmTeamRepository
    {
        private readonly AppContext _context;

        public CrmTeamRepository(AppContext context)
        {
            _context = context;
        }

        // Listar todos los equipos
        public async Task<IEnumerable<TeamDto>> GetAll()
        {
            return await _context.Teams
                .Select(t => new TeamDto
                {
                    TeamId = t.TeamId,
                    TeamName = t.TeamName,
                    TeamDescription = t.TeamDescription,
                    State = t.State,
                    CreatedBy = t.CreatedBy,
                    CreationDate = t.CreationDate,
                    ModifiedBy = t.ModifiedBy,
                    ModificationDate = t.ModificationDate
                })
                .ToListAsync();
        }

        // Buscar un equipo por su ID
        public async Task<TeamDto?> GetByIdAsync(long id)
        {
            return await _context.Teams
                .Where(t => t.TeamId == id)
                .Select(t => new TeamDto
                {
                    TeamId = t.TeamId,
                    TeamName = t.TeamName,
                    TeamDescription = t.TeamDescription,
                    State = t.State,
                    CreatedBy = t.CreatedBy,
                    CreationDate = t.CreationDate,
                    ModifiedBy = t.ModifiedBy,
                    ModificationDate = t.ModificationDate
                })
                .FirstOrDefaultAsync();
        }
    }
}