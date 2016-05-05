using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            
            List<CourseViewModel> courseViewModel = _courseService.GetAllCourses();
            //var model = courseViewModel;
            //var viewModel = ViewBag.courseViewModel;
            ViewBag.courseViewModel = courseViewModel;
            return View(ViewBag.courseViewModel);
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