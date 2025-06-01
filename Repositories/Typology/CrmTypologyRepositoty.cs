using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crm_app.Dto.Typology;
using Microsoft.EntityFrameworkCore;
using crm_app.Repositories.Typologies;
using AppContext = crm_app.Utils.EntityDbContext;

namespace crm_app.Repositories.TypologyRepository
{
    public class CrmTypologyRepository : ICrmTypologyRepository
    {
        private readonly AppContext _context;

        public CrmTypologyRepository(AppContext context)
        {
            _context = context;
        }

        // Listar todos
        public async Task<IEnumerable<TypologyDto>> GetAll()
        {
            return await _context.Typologies
                .Select(t => new TypologyDto
                {
                    TypologyId = t.TypologyId,
                    ParentTypologyId = t.ParentTypologyId,
                    Description = t.Description,
                    Value1 = t.Value1,
                    Value2 = t.Value2,
                    Value3 = t.Value3
                })
                .ToListAsync();
        }

        // Buscar por ID
        public async Task<TypologyDto?> GetByIdAsync(int id)
        {
            return await _context.Typologies
                .Where(t => t.TypologyId == id)
                .Select(t => new TypologyDto
                {
                    TypologyId = t.TypologyId,
                    ParentTypologyId = t.ParentTypologyId,
                    Description = t.Description,
                    Value1 = t.Value1,
                    Value2 = t.Value2,
                    Value3 = t.Value3
                })
                .FirstOrDefaultAsync();
        }
        
        //-----------
        public async Task<IEnumerable<TypologyDto>> GetByParentIdAsync(long parentId)
        {
            return await _context.Typologies
                .Where(t => t.ParentTypologyId == parentId)
                .Select(t => new TypologyDto
                {
                    TypologyId = t.TypologyId,
                    Description = t.Description
                    // Solo pon aquí los campos que NECESITAS para el combo
                })
                .ToListAsync();
        }
    }
}