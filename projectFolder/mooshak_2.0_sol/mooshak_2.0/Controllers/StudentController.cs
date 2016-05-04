using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mooshak_2._0.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        [Authorize(Roles = "student")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "student")]
        public ActionResult Course()
        {
            return View();
        }

        [Authorize(Roles = "student")]
        public ActionResult Assignment()
        {
            return View();
        }
        [Authorize(Roles = "student")]
        public ActionResult Submission()
        {
            return View();
        }
    }
}