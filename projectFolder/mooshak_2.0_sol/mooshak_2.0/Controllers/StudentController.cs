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
            //List<CourseViewModel> UserCourses = _courseService.GetCoursesByUser(User.Identity.GetUserId());
            //return View(UserCourses);
            return View();
        }

        public ActionResult Course(int? id)
        {
            if (id.HasValue)
            {
                int realID = id.Value;
                CourseService _courseService = new CourseService(new AssignmentsService());
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
                AssignmentsService _assignmentService = new AssignmentsService();
                AssignmentViewModel model = _assignmentService.GetAssignmentByID(realID);
                return View(model);
            }
            return View("Error");
        }

        public ActionResult Submission()
        {
            return View();
        }
    }
}