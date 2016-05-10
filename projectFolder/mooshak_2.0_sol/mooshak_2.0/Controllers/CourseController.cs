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
        private readonly CourseService _courseService = new CourseService();
        readonly Dbcontext _db = new Dbcontext();

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
            return View(_db.courses.ToList());
        }

        [HttpPost]
        public ActionResult CourseIndex(Course courses)
        {
            if (ModelState.IsValid)
            {
                using (_db)
                {
                    string new_item = Request.Form["new_item"];
                    if (new_item.IsNullOrWhiteSpace())
                    {
                        return View(_db.courses.ToList());
                    }
                    var dbList = _db.courses.Create();
                    dbList.name = new_item;
                    _db.courses.Add(dbList);
                    _db.SaveChanges();
                }
            }
            else
            {
                ModelState.AddModelError("", "Incorrect format has been placed");

            }
            return RedirectToAction("CourseIndex");
        }

        [HttpGet]
        public ActionResult AddToCourse(int id)
        {
            AllUsersAndSomeCourseViewModel allUsersAndSomeCourseViewModel = new AllUsersAndSomeCourseViewModel()
            {
                TheCourse = _courseService.GetCourseByID(id),
                ListOfUsers = _db.Users.ToList(),
                AllUsersInCourse = _courseService.GetUsersInSomeCourse(id)
             };
            return View(allUsersAndSomeCourseViewModel);
        }
        
        [HttpPost]
        public ActionResult AddTheUserToTheCourse(AllUsersAndSomeCourseViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (_db)
                {
                    var newConnection = _db.userCourse.Create();
                    //var theUser = db.Users.Find(model.selectedUserId).ToString();
                    string theUserId = model.SelectedUserId;

                    newConnection.userId = theUserId;
                    if (theUserId.IsNullOrWhiteSpace())
                    {
                        return Redirect("/Course/AddToCourse/" + model.TheCourse.Id);
                    }
                    
                    //TODO: If user is already in course, dont add him 
                    /*if (model.theCourse.ID == db.userCourse.Find(theUserId).id)
                    {
                        System.Diagnostics.Debug.WriteLine("works");
                    }*/
                    var allUsersInCourse = _courseService.GetUsersInSomeCourse(model.TheCourse.Id);
                    foreach (var user in allUsersInCourse)
                    {
                        //Check if user is already in the course
                        if (theUserId == user.userId)
                        {
                            //TODO: Alert user is already in course
                            return Redirect("/Course/AddToCourse/" + model.TheCourse.Id);
                        }
                    }
                    newConnection.userId = theUserId;
                    newConnection.courseId = model.TheCourse.Id;
                    _db.userCourse.Add(newConnection);
                    _db.SaveChanges();
                }
            }
            //TODO: Redirect to the same course again
            //Redirect back to the same page
            return Redirect("/Course/AddToCourse/" + model.TheCourse.Id);
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = _db.courses.Find(id);

            if(model == null)
            {
                return HttpNotFound();
            }
            _db.courses.Remove(model);
            _db.SaveChanges();
            return Redirect("/Course/CourseIndex/" + id);
        }

        [HttpGet]
        //Removes the selected user from a specified course
        public ActionResult RemoveUser(int id)
        {
            var model = _db.userCourse.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            _db.userCourse.Remove(model);
            _db.SaveChanges();
            return Redirect("/Course/AddToCourse/" + model.courseId);
        }
        
        [HttpGet]
        public ActionResult EditCourse(int id)
        {
            //Finds course by the id
            Course model = _db.courses.Find(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditCourse(Course courses)
        {
            string newItem = Request.Form["new_item"];

            if (!ModelState.IsValid)
            {
                return View(courses);
            }

            courses.name = newItem;
            _db.Entry(courses).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("CourseIndex");
        }
    }
}