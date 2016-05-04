using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mooshak_2._0.Controllers
{
    public class CourseController : Controller
    {
        // GET: Course
        public ActionResult CourseIndex()
        {
            return View();
        }
    }
}