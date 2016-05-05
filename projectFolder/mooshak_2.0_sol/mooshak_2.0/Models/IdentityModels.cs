using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using mooshak_2._0.Models.Entities;

namespace mooshak_2._0.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string Address { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {

            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            return userIdentity;
        }
    }

    public class Dbcontext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Assignments>          assignments { get; set; } // NOTE!:we have to do this for all the entity classes
        public DbSet<AssignmentMilestone> milestones  { get; set; } // like this
        public DbSet<Courses>              courses     { get; set; }
      
        public Dbcontext() : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static Dbcontext Create()
        {

            return new Dbcontext();
        }

        //public System.Data.Entity.DbSet<mooshak_2._0.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}