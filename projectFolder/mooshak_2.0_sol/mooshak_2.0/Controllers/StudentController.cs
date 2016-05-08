using Microsoft.AspNet.Identity;
using mooshak_2._0.Models.ViewModels;
using mooshak_2._0.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace mooshak_2._0.Controllers
{

    [Authorize(Roles = "student")]
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            CourseService _courseService = new CourseService(new AssignmentsService());
            List<CourseViewModel> StudentCourses = _courseService.GetCoursesByStudent(User.Identity.GetUserId());
            return View(StudentCourses);
        }

        public ActionResult Course()
        {
            return View();
        }

        public ActionResult Assignment()
        {
            return View();
        }

        public ActionResult Submission()
        {
            return View();
        }
    }
}