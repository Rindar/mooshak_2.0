using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using mooshak_2._0.Models;
using mooshak_2._0.Models.Entities;

namespace mooshak_2._0.Helpers
{
    /// <summary>
    /// Creates an istance of every role if none exists and the first admin account. This is called on APP start
    /// Credits go to http://jorgeramon.me/2015/how-to-seed-users-and-roles-in-an-asp-net-mvc-application/
    /// </summary>
    public class IdentityHelper
    {
        internal static void SeedIdentities(DbContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!roleManager.RoleExists(UserRoles.ADMINROLE))
            {
                var roleresult = roleManager.Create(new IdentityRole(UserRoles.ADMINROLE));
            }
            if (!roleManager.RoleExists(UserRoles.TEACHERROLE))
            {
                var roleresult = roleManager.Create(new IdentityRole(UserRoles.TEACHERROLE));
            }
            if (!roleManager.RoleExists(UserRoles.STUDENTROLE))
            {
                var roleresult = roleManager.Create(new IdentityRole(UserRoles.STUDENTROLE));
            }

            string userName = "admin@admin.com";
            string password = "adminadmin";

            ApplicationUser user = userManager.FindByName(userName);
            if (user == null)
            {
                user = new ApplicationUser()
                {
                    UserName = userName,
                    Email = userName,
                    EmailConfirmed = true
                };
                IdentityResult userResult = userManager.Create(user, password);
                if (userResult.Succeeded)
                {
                    var result = userManager.AddToRole(user.Id, UserRoles.ADMINROLE);
                }
            }
        }
    }
}