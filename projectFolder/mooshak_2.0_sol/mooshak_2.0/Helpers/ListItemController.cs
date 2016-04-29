using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mooshak_2._0.Helpers
{
    public class ListItemController : Controller
    {
        public class SelectListItemHelper
        {
            public static IEnumerable<SelectListItem> getRoleListItems()
            {
                List<SelectListItem> User_Roles = new List<SelectListItem>()
                {
                    new SelectListItem() {Text = "Admin", Value = "admin"},
                    new SelectListItem() {Text = "Teacher", Value = "teacher"},
                    new SelectListItem() {Text = "Student", Value = "student"},
                };

                return User_Roles;
            }
        }
    }
}