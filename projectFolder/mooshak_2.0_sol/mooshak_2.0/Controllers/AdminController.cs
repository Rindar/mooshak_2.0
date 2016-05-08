using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using mooshak_2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mooshak_2._0.Models.ViewModels;
using mooshak_2._0.Services;
using mooshak_2._0.Models.Entities;

namespace mooshak_2._0.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            CourseService _courseService = new CourseService(new AssignmentsService());
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
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new Dbcontext()));
            var result = UserManager.Create(newUser, "some password");

            return View();
        }
        public ActionResult UserList()
        {
            var db = new Dbcontext();
            return View(db.Users.ToList());
        }
        public ActionResult UserCourses()
        {
            var db = new Dbcontext();
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
            var db = new Dbcontext();
            var userToRemove = db.Users.Find(id);
            if (userToRemove == null)
            {
                return HttpNotFound();
            }
            db.Users.Remove(userToRemove);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}