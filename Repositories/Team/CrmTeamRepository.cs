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
            // Mapear el DTO de creación a la entidad CrmTeam,
            // asignando explícitamente State = 501 para evitar que EF envíe 0.
            var newTeam = new CrmTeam
            {
                TeamName = teamPostDto.TeamName,
                TeamDescription = string.IsNullOrEmpty(teamPostDto.TeamDescription) ? null : teamPostDto.TeamDescription,
                State = 501 // Se asigna 501 para cumplir con la FK definida en la BD
            };

            // Agregar la entidad al contexto y guardar los cambios
            await _context.Teams.AddAsync(newTeam);
            await _context.SaveChangesAsync();

            // Recargar la entidad para obtener los valores asignados por la BD (por ejemplo, auditoría y el ID)
            await _context.Entry(newTeam).ReloadAsync();

            // Mapear la entidad a TeamDto y retornarlo
            return new TeamDto
            {
                TeamId = newTeam.TeamId,
                TeamName = newTeam.TeamName,
                TeamDescription = newTeam.TeamDescription,
                State = newTeam.State,
                CreatedBy = newTeam.CreatedBy,
                CreationDate = newTeam.CreationDate,
                ModifiedBy = newTeam.ModifiedBy,
                ModificationDate = newTeam.ModificationDate
            };
        }
        
        // --------------------actulizar-----------
        public async Task<bool> UpdateAsync(long id, CrmTeamPutDto updatedTeamDto)
        {
            // Buscar el equipo por su ID
            var existingTeam = await _context.Teams.FindAsync(id);
            if (existingTeam == null)
            {
                return false; // No se encontró el equipo
            }

            // Actualizar los campos del equipo
            existingTeam.TeamName = updatedTeamDto.TeamName;
            existingTeam.TeamDescription = updatedTeamDto.TeamDescription;
            
            // Si se envía un nuevo estado y es válido, se actualiza.
            if (updatedTeamDto.State.HasValue)
            {
                existingTeam.State = updatedTeamDto.State.Value;
            }

            // Actualizar quién modifica y la fecha de modificación
            if (updatedTeamDto.ModifiedBy.HasValue)
            {
                existingTeam.ModifiedBy = updatedTeamDto.ModifiedBy.Value;
            }
            existingTeam.ModificationDate = DateTime.UtcNow;

            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();
            return true;
        }
        
    }
}