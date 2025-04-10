using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using crm_app.Dto.Establishment;
using crm_app.Models.General;
using crm_core.Utils;
using AppContext = crm_app.Utils.EntityDbContext; // Ajusta el namespace según tu proyecto

namespace crm_app.Repositories.Establishment
{
    public class CrmEstablishmentRepository : ICrmEstablishmentRepository
    {
        private readonly AppContext _context;

        public CrmEstablishmentRepository(AppContext context)
        {
            _context = context;
        }

        //---------- Listar todos los equipos------------------
        public async Task<IEnumerable<EstablishmentDto>> GetAll()
        {
            return await _context.Establishment
                .Select(e => new EstablishmentDto
                {
                    EstablishmentId = e.EstablishmentId,
                    ParentEstablishmentId = e.ParentEstablishmentId,
                    EstablishmentKey = e.EstablishmentKey,
                    EstablishmentName = e.EstablishmentName,
                    EstablishmentDescription = e.EstablishmentDescription,
                    EstablishmentAddress = e.EstablishmentAddress,
                    EstablishmentEmail = e.EstablishmentEmail,
                    EstablishmentPhone = e.EstablishmentPhone,
                    EstablishmentType = e.EstablishmentType,
                    EstablishmentStatus = e.EstablishmentStatus,
                    CreatedBy = e.CreatedBy,
                    CreationDate = e.CreationDate,
                    ModifiedBy = e.ModifiedBy,
                    ModificationDate = e.ModificationDate
                })
                .ToListAsync();
        }

        
        
    }
}