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
            return View();
        }

    }
}