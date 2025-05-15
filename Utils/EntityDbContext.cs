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
        
        //Teams
        public DbSet<CrmTeam> Teams { get; set; }
        
        //Establishment
        public DbSet<CrmEstablishment> Establishment { get; set; }
        
        //Person
        public DbSet<CrmPerson> Person { get; set; }
        
        //User
        public DbSet<CrmUser> User { get; set; }
        
        //UserTeam
        public DbSet<CrmUserTeam> UserTeam { get; set; }
    }
}