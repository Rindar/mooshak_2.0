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
using Microsoft.AspNet.Identity;

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
            ListOfCourseViewModels = _courseService.GetCoursesByUser(User.Identity.GetUserId());
            return View(ListOfCourseViewModels);
        }

        public ActionResult Course(int? id)
        {
            if (id.HasValue)
            {
                int realID = id.Value;
                CourseViewModel model = _courseService.GetCourseByID(realID);
                return View(model);
            }
            return View("Error");
        }

        public ActionResult Assignment()
        {
            return View();
        }

        public ActionResult Sidebar()
        {
            var model = _courseService.GetCoursesByUser(User.Identity.GetUserId());
            return PartialView("~/Views/Shared/_SideBarTeacherStudent.cshtml", model);
        }
    }
}