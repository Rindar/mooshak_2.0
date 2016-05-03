using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using mooshak_2._0.Models;
using mooshak_2._0.Models.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace mooshak_2._0.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.IsInRole("student"))
            {
                return RedirectToAction("Index", "Student");
            }
            else if (User.IsInRole("teacher"))
            {
                return RedirectToAction("Index", "Teacher");
            }
            else if (User.IsInRole("admin"))
            {
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return View();
            }
            
        }
        
        public ActionResult About()
        {
            ViewBag.Message = "Your application admin description page.";

            return View();
        }

        public ActionResult Help()
        {
            ViewBag.Message = "Help page";

            return View();
        }
    }
}