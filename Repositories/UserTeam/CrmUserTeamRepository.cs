using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using crm_app.Dto.UserTeam;
using crm_app.Dto.Team;
using crm_app.Dto.User;
using crm_app.Models.General;
using AppContext = crm_app.Utils.EntityDbContext;

namespace crm_app.Repositories.UserTeam
{
    public class CrmUserTeamRepository : ICrmUserTeamRepository
    {
        private readonly AppContext _context;

        public CrmUserTeamRepository(AppContext context)
        {
            _context = context;
        }
        
        //----------listar
        public async Task<IEnumerable<UserTeamDto>> GetAll()
        {
            var userTeamsQuery = _context.UserTeam.AsQueryable();

            // Aquí puedes agregar filtros si los necesitas:
            // userTeamsQuery = userTeamsQuery.Where(...);

            var result = MapeoUserTeamDto(userTeamsQuery);

            return await Task.FromResult(result);
        }
       
        
        //----------listar
        private List<UserTeamDto> MapeoUserTeamDto(IQueryable<CrmUserTeam> userTeams)
        {
            return (
                from ut in userTeams
                join u in _context.User on ut.UserId equals u.UserId into userGroup
                from u in userGroup.DefaultIfEmpty()
                join t in _context.Teams on ut.TeamId equals t.TeamId into teamGroup
                from t in teamGroup.DefaultIfEmpty()
                orderby ut.UsersTeamId descending
                select new UserTeamDto
                {
                    UsersTeamId = ut.UsersTeamId,
                    UserId = ut.UserId,
                    TeamId = ut.TeamId,
                    Role = ut.Role,
                    State = ut.State,
                    CreatedBy = ut.CreatedBy,
                    CreationDate = ut.CreationDate,
                    ModifiedBy = ut.ModifiedBy,
                    ModificationDate = ut.ModificationDate,

                    User = u == null ? null : new UserDto
                    {
                        UserId = u.UserId,
                        UserName = u.UserName,
                        UserEmail = u.UserEmail
                        // No asignamos el resto de campos
                    },

                    Team = t == null ? null : new TeamDto
                    {
                        TeamId = t.TeamId,
                        TeamName = t.TeamName,
                        TeamDescription = t.TeamDescription
                        // No asignamos el resto de campos
                    }
                }).ToList();
        }


    }
}
    