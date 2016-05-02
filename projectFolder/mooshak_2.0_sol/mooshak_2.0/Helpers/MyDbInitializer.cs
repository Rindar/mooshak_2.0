using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using mooshak_2._0.Models;
using mooshak_2._0.Models.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace mooshak_2._0.Helpers
{
    public class MyDbInitializer : DropCreateDatabaseAlways<context>
    {
        protected override void Seed(context context)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            string userName = "admin@1.com";
            string password = "admin1";

            //Create Role Admin if it does not exist
            if (!RoleManager.RoleExists(UserRoles.ADMINROLE))
            {
                    var roleresult = RoleManager.Create(new IdentityRole(UserRoles.ADMINROLE));
            }
            if (!RoleManager.RoleExists(UserRoles.TEACHERROLE))
            {
                var roleresult = RoleManager.Create(new IdentityRole(UserRoles.TEACHERROLE));
            }
            if (!RoleManager.RoleExists(UserRoles.STUDENTROLE))
            {
                var roleresult = RoleManager.Create(new IdentityRole(UserRoles.STUDENTROLE));
            }
            

        //Create User=admin@1.com with password=admin1
            var user = new ApplicationUser();
            user.UserName = userName;
            var adminResult = UserManager.Create(user, password);

            //Add User Admin to Role Admin
            if (adminResult.Succeeded)
            {
                var result = UserManager.AddToRole(user.Id, UserRoles.ADMINROLE);
            }
            base.Seed(context);
        }
    }
}