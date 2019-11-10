using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace wafabank.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<offre> jobs { get; set; }

        public string type { get; set; }
        public string photo { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<wafabank.Models.Demande> Applies { get; set; }

        public System.Data.Entity.DbSet<wafabank.Models.offre> jobs { get; set; }
        public System.Data.Entity.DbSet<wafabank.Models.PhotoProfil> Profils { get; set; }



        public System.Data.Entity.DbSet<wafabank.Models.Secteur> Secteurs { get; set; }
        public System.Data.Entity.DbSet<wafabank.Models.poste> postes { get; set; }
        public System.Data.Entity.DbSet<wafabank.Models.Niveau> Niveaus { get; set; }
        public System.Data.Entity.DbSet<wafabank.Models.ville> villes { get; set; }




    }
}