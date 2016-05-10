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
        CourseService _courseService = new CourseService(new AssignmentsService());
        AssignmentsService _assignmentService = new AssignmentsService();
        // GET: Student
        public ActionResult Index()
        {
            List<CourseViewModel> UserCourse = _courseService.GetCoursesByUser(User.Identity.GetUserId());
            if(UserCourse.Any())
            {
                return View(UserCourse);
            }
            return View("Error");
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

        public ActionResult Assignment(int? id)
        {
            if (id.HasValue)
            {
                int realID = id.Value;
                AssignmentViewModel model = _assignmentService.GetAssignmentByID(realID);
                return View(model);
            }
            return View("Error");
        }

        public ActionResult Submission()
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