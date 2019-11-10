using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using wafabank.Models;

[assembly: OwinStartupAttribute(typeof(wafabank.Startup))]
namespace wafabank
{
    public partial class Startup
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateDefaultRolesAndUsers();
            users();
        }
        public void users()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var  user = new ApplicationUser();
            user.UserName = "Admin123";
            user.Email = "Admin123@Amine.com";
            var check = userManager.Create(user, "Amin@@@");

            if (check.Succeeded)
            {
                userManager.AddToRole(user.Id, "Admins");

            }

        }
        public void CreateDefaultRolesAndUsers()
        {
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            IdentityRole Role = new IdentityRole();

            if (!RoleManager.RoleExists("Admins") && !RoleManager.RoleExists("Admins"))
            {
                Role.Name = "Admins";
                RoleManager.Create(Role);

                ApplicationUser user = new ApplicationUser();

                user.UserName = "Mourid";
                user.Email = "Mourid@Amine.com";
               
                var check = userManager.Create(user, "Amin@@@");

                if (check.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admins");

                }

            }
        }
    }
}
