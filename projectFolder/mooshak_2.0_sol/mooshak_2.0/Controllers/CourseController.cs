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
    [Authorize(Roles = "admin")]
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

        public ActionResult AddNewCourse()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNewCourse(CourseViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (_db)
                {
                    Course newCourse = new Course()
                    {
                        name = model.Title,
                        description =  model.Description,
                        id = model.Id,
                        teacher = model.Teacher
                    };
                    //string newItem = Request.Form["newItem"];
                    if (newCourse.name.IsNullOrWhiteSpace())
                    {
                        return RedirectToAction("AddNewCourse");
                    }
                    var dbList = _db.courses.Create();
                    dbList.name = newCourse.name;
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
            var userCourseModels = _courseService.GetUsersInSomeCourse(id);
            if(model == null)
            {
                return HttpNotFound();
            }
            foreach (var item in userCourseModels)
            {
                //Because of reasons unknown to mankind
                _db.userCourse.Attach(item);
                _db.userCourse.Remove(item);
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
            Course course = _db.courses.Find(id);
            //CourseViewModel courseViewmodel = _courseService.GetCourseByID(id);
            return View(course);
        }

        [HttpPost]
        public ActionResult EditCourse(Course model)
        {
            if (ModelState.IsValid)
            {
                using (_db)
                {
                    var courseToChange = _db.courses.Find(model.id);

                    if (!model.name.IsNullOrWhiteSpace())
                    {
                        courseToChange.name = model.name;
                    }
                    if (!model.description.IsNullOrWhiteSpace())
                    {
                        courseToChange.description = model.description;
                    }
                    if (!model.teacher.IsNullOrWhiteSpace())
                    {
                        courseToChange.teacher = model.teacher;
                    }
                    _db.Entry(courseToChange).State = System.Data.Entity.EntityState.Modified;
                    _db.SaveChanges();
                }
                return RedirectToAction("CourseIndex");
            }
            else
            {
                ModelState.AddModelError("", "Incorrect format has been placed");
            }
            return RedirectToAction("CourseIndex");
        }
    }
}