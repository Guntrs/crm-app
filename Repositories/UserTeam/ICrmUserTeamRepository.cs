    using System.Collections.Generic;
    using System.Threading.Tasks;
    using crm_app.Dto.Establishment;
    using crm_app.Dto.UserTeam; // usar los dto

    namespace crm_app.Repositories.UserTeam
    {
        public interface ICrmUserTeamRepository
        {
            //Listar
            public Task<IEnumerable<UserTeamDto>> GetAll();
            
            // Nuevo: listado sin async, personalizado
           // List<UserTeamDto> ListadoUserTeam();
            
            
        
        //buscar por id
     //   public Task<UserDto?> GetByIdAsync(long id);
     
        // Nuevo
       // public Task<UserDto> CreateAsync(UserPostDto user);
        
        //actualizar
      //  public Task<bool> UpdateAsync(long id, UserPutDto updateuserDto);
        
        
    }
}