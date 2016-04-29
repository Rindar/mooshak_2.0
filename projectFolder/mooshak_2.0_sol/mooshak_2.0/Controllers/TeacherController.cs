using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mooshak_2._0.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        [Authorize(Roles = "teacher")]
        public ActionResult Index()
        {
            return View();
        }
    }
}