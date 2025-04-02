using Microsoft.EntityFrameworkCore;
using crm_app.Models;
using crm_app.Models.General; 

namespace crm_app.Utils 
{
    public class EntityDbContext : DbContext
    {
        public EntityDbContext(DbContextOptions<EntityDbContext> options) : base(options) { }
        
        // Typologies
        public DbSet<CrmTypology> Typologies { get; set; }
        
    }
}