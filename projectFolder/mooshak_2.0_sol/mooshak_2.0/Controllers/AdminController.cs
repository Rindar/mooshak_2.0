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
using System.Data.Entity;
using mooshak_2._0.ErrorHandler;

namespace mooshak_2._0.Controllers
{
    [ErrorAttributeHandler]
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        readonly Dbcontext db = new Dbcontext();
        readonly CourseService _courseService = new CourseService(new AssignmentsService());
        readonly UserManager<ApplicationUser> _userserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new Dbcontext()));

        // GET: Admin
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            List<CourseViewModel> getAllCourses = _courseService.GetAllCourses();
            return View(getAllCourses);
        }

        public ActionResult UserList()
        {
            return View(db.Users.ToList());
        }

        public ActionResult AddUser(string id)
        {
            var userToRemove = db.Users.Find(id);
            if (userToRemove == null)
            {
                return HttpNotFound();
            }
            _userserManager.Delete(userToRemove);
            return RedirectToAction("Index");
        }

        
        public ActionResult DeleteUser(string id)
        {
            var userToRemove = db.Users.Find(id);
            if (userToRemove == null)
            {
                return HttpNotFound();
            }
            db.Users.Remove(userToRemove);
            db.SaveChanges();
            return RedirectToAction("UserList");
        }
    }
}