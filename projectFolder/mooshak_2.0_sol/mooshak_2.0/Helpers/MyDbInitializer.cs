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
    //public class MyDbInitializer : DropCreateDatabaseAlways<context>
    public class MyDbInitializer : CreateDatabaseIfNotExists<Dbcontext>
    {
        protected override void Seed(Dbcontext _db)
        {
            //Database.SetInitializer<DbContext>(null);
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_db));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_db));
            string userName = "admin@1.com";
            string password = "admin1";

            //Create Role Admin if it does not exist

            if (!RoleManager.RoleExists(UserRoles.ADMIN.ToString()))
            {
                var roleresult = RoleManager.Create(new IdentityRole(UserRoles.ADMIN.ToString()));
            }
            if (!RoleManager.RoleExists(UserRoles.TEACHER.ToString()))
            {
                var roleresult = RoleManager.Create(new IdentityRole(UserRoles.TEACHER.ToString()));
            }
            if (!RoleManager.RoleExists(UserRoles.STUDENT.ToString()))
            {
                var roleresult = RoleManager.Create(new IdentityRole(UserRoles.STUDENT.ToString()));
            }
            

        //Create User=admin@1.com with password=admin1
            var user = new ApplicationUser();
            user.UserName = userName;
            var adminResult = UserManager.Create(user, password);

            //Add User Admin to Role Admin
            if (adminResult.Succeeded)
            {
                var result = UserManager.AddToRole(user.Id, UserRoles.ADMIN.ToString());
            }
            base.Seed(_db);
        }
    }
}