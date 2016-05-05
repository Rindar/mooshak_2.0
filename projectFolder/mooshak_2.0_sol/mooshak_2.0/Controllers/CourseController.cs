using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mooshak_2._0.Services;
using mooshak_2._0.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace mooshak_2._0.Controllers
{
    public class CourseController : Controller
    {
        private CourseService _service = new CourseService();
        // GET: Assignments
        public ActionResult Index()
        {
            return View();
        }

        //the method that is called to see details for an assignment
        public ActionResult Details(int id)
        {
            var viewModel = _service.GetCourseByID(id); // creates a viewmodel for the assignment
            return View(viewModel);
        }
    }
}