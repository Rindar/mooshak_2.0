using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime.Misc;
using mooshak_2._0.Models;
using mooshak_2._0.Models.Entities;
using mooshak_2._0.Models.ViewModels;
using mooshak_2._0.Services;

namespace mooshak_2._0.Controllers
{
    [Authorize(Roles = "teacher")]
    public class TeacherController : Controller
    {
        CourseService _courseService = new CourseService(new AssignmentsService());
        // GET: Teacher
        public ActionResult Index()
        {
            List<CourseViewModel> ListOfCourseViewModels = new ListStack<CourseViewModel>();
            ListOfCourseViewModels = _courseService.GetAllCourses();
            return View(ListOfCourseViewModels);
        }

        public ActionResult Course()
        {
            return View();
        }

        public ActionResult Assignment()
        {
            return View();
        }
    }
}