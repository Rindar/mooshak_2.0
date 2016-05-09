using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mooshak_2._0.Services;
using mooshak_2._0.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using mooshak_2._0.Models.Entities;
using System.Security.Claims;
using mooshak_2._0.Models.ViewModels;
using System.Collections;
using System.Web.Security;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;

namespace mooshak_2._0.Controllers
{
    public class CourseController : Controller
    {
        private CourseService _courseService = new CourseService();
        Dbcontext db = new Dbcontext();

        // GET: Assignments
        public ActionResult Index()
        {
            return View();
        }

        //the method that is called to see details for an assignment
        public ActionResult Details(int id)
        {
            var viewModel = _courseService.GetCourseByID(id); // creates a viewmodel for the assignment
            return View(viewModel);
        }


        public ActionResult CourseIndex()
        {
            return View(db.courses.ToList());
        }

        [HttpPost]
        public ActionResult CourseIndex(Course courses)
        {
        
            if (ModelState.IsValid)
            {
                using (db)
                {
                    string new_item = Request.Form["new_item"];
                    if (new_item.IsNullOrWhiteSpace())
                    {
                        return View(db.courses.ToList());
                    }
                    var dbList = db.courses.Create();
                    dbList.name = new_item;
                    db.courses.Add(dbList);
                    db.SaveChanges();
                }
            }
            else
            {
                ModelState.AddModelError("", "Incorrect format has been placed");

            }
            return RedirectToAction("CourseIndex");
        }

        
        public ActionResult AddToCourse()
        {
            var allUsers = db.Users.ToList();
            return View(allUsers);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = db.courses.Find(id);

            if(model == null)
            {
                return HttpNotFound();
            }
            db.courses.Remove(model);
            db.SaveChanges();
            return RedirectToAction("CourseIndex");
        }
        [HttpGet]
        public ActionResult EditCourse(int id)
        {
            var db = new Dbcontext();
            var model = new Course();
            model = db.courses.Find(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult EditCourse(Course courses)
        {
            var db = new Dbcontext();
            string new_item = Request.Form["new_item"];

            if (ModelState.IsValid)
            {

                courses.name = new_item;

                db.Entry(courses).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("CourseIndex");
            }
            return View(courses);
        }
    }
}