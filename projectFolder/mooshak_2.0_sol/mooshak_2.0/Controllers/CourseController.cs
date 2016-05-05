using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mooshak_2._0.Models;
using System.Security.Claims;
using mooshak_2._0.Models.Entities;

namespace mooshak_2._0.Controllers
{
    public class CourseController : Controller
    {


        public ActionResult CourseIndex()
        {
            var db = new Dbcontext();
            return View(db.courses.ToList());
        }


        [HttpPost]
        public ActionResult CourseIndex(Courses course)
        {


            if (ModelState.IsValid)
            {

                using (var db = new Dbcontext())
                {
                    //Claim sessionEmail = ClaimsPrincipal.Current.FindFirst(ClaimTypes.Email);
                    //string userEmail = sessionEmail.Value;
                    //var userIdQuery = db.Users.Where(u => u.Email == userEmail).Select(u => u.Id);
                   // var userId = userIdQuery.ToList();
                    //string checkPublic = Request.Form["checkPublic"];
                    string new_item = Request.Form["new_item"];

                    var dbList = db.courses.Create();

                    dbList.name = new_item;
                    db.courses.Add(dbList);
                    db.SaveChanges();
                }
            }
            else
            {
                ModelState.AddModelError("", "Incorrect format has been placed");

            }
            var listTable = new Dbcontext();
            return View(listTable.courses.ToList());
        }
    }
}