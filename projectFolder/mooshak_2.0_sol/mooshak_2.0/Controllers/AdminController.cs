using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using mooshak_2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mooshak_2._0.Helpers;
using mooshak_2._0.Models.ViewModels;
using mooshak_2._0.Services;
using mooshak_2._0.Models.Entities;

namespace mooshak_2._0.Controllers
{
    public class AdminController : Controller
    {
        Dbcontext db = new Dbcontext();
        CourseService _courseService = new CourseService(new AssignmentsService());
        UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new Dbcontext()));

        // GET: Admin
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            List<CourseViewModel> getAllCourses = _courseService.GetAllCourses();
            return View(getAllCourses);
        }

        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateUser(FormCollection form)
        {
            //Validate 
            //create user
            ApplicationUser newUser = new ApplicationUser() { Email = "email", UserName = "username" };
            var result = UserManager.Create(newUser, "some password");

            return View();
        }
        public ActionResult UserList()
        {
            return View(db.Users.ToList());
        }
        public ActionResult UserCourses()
        {
            var q = (from t in db.courses
                     select new { t.name });

            var courses = new List<Course>();
            foreach (var t in q)
            {
                courses.Add(new Course()
                {
                    name = t.name,
                });
            }
            return View(db.courses);
        }
        
        public ActionResult DeleteUser(string id)
        {
            var userToRemove = db.Users.Find(id);
            if (userToRemove == null)
            {
                return HttpNotFound();
            }
            UserManager.Delete(userToRemove);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}