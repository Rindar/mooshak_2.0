using System;
using System.Collections.Generic;
using System.Linq;
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
            return View();
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