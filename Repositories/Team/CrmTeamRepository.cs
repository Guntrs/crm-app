using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using crm_app.Dto.Team;
using crm_app.Models.General;
using crm_core.Utils;
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

        //---------- Listar todos los equipos------------------
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

        //----------- Buscar un equipo por su ID------------------
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
        
        
        //---------- NUEVO: Crear un nuevo equipo ------------------
        public async Task<TeamDto> CreateAsync(TeamPostDto teamPostDto)
        {
            //Mapear el Dto al Modelo 
            var crmTeam = new CrmTeam
            {
                TeamName = teamPostDto.TeamName,
                TeamDescription = teamPostDto.TeamDescription,
            };
            
            //Agregar el team al contexto
            await _context.AddAsync(crmTeam);
            
            //Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();
            
            // Mapear el modelo creado de vuelta al DTO
            var createTeamDto = new TeamDto
            {
                TeamId = crmTeam.TeamId,
                TeamName = crmTeam.TeamName,
                TeamDescription = crmTeam.TeamDescription,
            };
            
            return createTeamDto;
        }
        
        
        // --------------------actualizar-----------
        public async Task<bool> UpdateAsync(long id, TeamPutDto updatedTeamDto)
        {
            // Buscar el equipo por su ID
            var crmTeam = await _context.Teams.FindAsync(id);
            
            
            if (crmTeam == null)
            {
                return false; // No se encontró el equipo
            }
        
            //Mapear los valores del Dto al Modelo
            crmTeam.TeamName = updatedTeamDto.TeamName;
            crmTeam.TeamDescription = updatedTeamDto.TeamDescription;

            crmTeam.ModificationDate = CrmFunctions.GetDateTime();
            
            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();
            return true;
        }
        
    }
}