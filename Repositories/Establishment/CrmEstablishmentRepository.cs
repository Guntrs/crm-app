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

        //---------- Listar ------------------
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
        
        
        
        //----------- Buscar por su ID------------------
        public async Task<EstablishmentDto?> GetByIdAsync(long id)
        {
            return await _context.Establishment
                .Where(e => e.EstablishmentId == id)
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
                .FirstOrDefaultAsync();
        }
        
        //----------- Nuevo------------------
        public async Task<EstablishmentDto> CreateAsync(EstablishmentPostDto establishmentPostDto)
        {
            // Mapear el Dto al Modelo 
            var establishment = new CrmEstablishment
            {
                ParentEstablishmentId = establishmentPostDto.ParentEstablishmentId,
                EstablishmentKey = establishmentPostDto.EstablishmentKey,
                EstablishmentName = establishmentPostDto.EstablishmentName,
                EstablishmentDescription = establishmentPostDto.EstablishmentDescription ?? "S/D",
                EstablishmentAddress = establishmentPostDto.EstablishmentAddress,
                EstablishmentEmail = establishmentPostDto.EstablishmentEmail,
                EstablishmentPhone = establishmentPostDto.EstablishmentPhone,
                EstablishmentType = establishmentPostDto.EstablishmentType,
                EstablishmentStatus = CrmConstants.ESTADO_ACTIVO,
                CreatedBy = 0,
                CreationDate = CrmFunctions.GetDateTime(),
                ModifiedBy = 0,
                ModificationDate = CrmFunctions.GetDateTime()
            };

            // Agregar el establecimiento al contexto
            await _context.Establishment.AddAsync(establishment);

            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();

            // Mapear el modelo creado de vuelta al DTO
            var createdDto = new EstablishmentDto
            {
                EstablishmentId = establishment.EstablishmentId,
                ParentEstablishmentId = establishment.ParentEstablishmentId,
                EstablishmentKey = establishment.EstablishmentKey,
                EstablishmentName = establishment.EstablishmentName,
                EstablishmentDescription = establishment.EstablishmentDescription,
                EstablishmentAddress = establishment.EstablishmentAddress,
                EstablishmentEmail = establishment.EstablishmentEmail,
                EstablishmentPhone = establishment.EstablishmentPhone,
                EstablishmentType = establishment.EstablishmentType,
                EstablishmentStatus = establishment.EstablishmentStatus,
                CreatedBy = establishment.CreatedBy,
                CreationDate = establishment.CreationDate,
                ModifiedBy = establishment.ModifiedBy,
                ModificationDate = establishment.ModificationDate
            };

            return createdDto;
        }
        
        // --------------------Actualizar un establecimiento------------------
        public async Task<bool> UpdateAsync(long id, EstablishmentPutDto dto)
        {
            // Buscar el establecimiento por su ID
            var establishment = await _context.Establishment.FindAsync(id);

            if (establishment == null)
            {
                return false; // No se encontró el establecimiento
            }

            // Mapear los valores del Dto al Modelo
            establishment.ParentEstablishmentId = dto.ParentEstablishmentId;
            establishment.EstablishmentKey = dto.EstablishmentKey;
            establishment.EstablishmentName = dto.EstablishmentName;
            establishment.EstablishmentDescription = dto.EstablishmentDescription ?? "S/D";
            establishment.EstablishmentAddress = dto.EstablishmentAddress;
            establishment.EstablishmentEmail = dto.EstablishmentEmail;
            establishment.EstablishmentPhone = dto.EstablishmentPhone;
            establishment.EstablishmentType = dto.EstablishmentType;
            establishment.EstablishmentStatus = dto.EstablishmentStatus;

            establishment.ModificationDate = CrmFunctions.GetDateTime();
            establishment.ModifiedBy = 0; // Reemplazar si se maneja el ID de usuario

            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();
            return true;
        }
        
        
    }
}