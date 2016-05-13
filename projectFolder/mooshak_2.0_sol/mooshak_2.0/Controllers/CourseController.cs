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
using System.Web.UI.WebControls;
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
            var viewModel = _courseService.GetCourseById(id); // creates a viewmodel for the assignment
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
            if (!ModelState.IsValid)
            {
                throw new Exception();
            }
            using (_db)
            {
                Course newCourse = new Course()
                {
                    Title = model.Title,
                    Description =  model.Description,
                    Id = model.Id,
                    Teacher = model.Teacher
                };
                //string newItem = Request.Form["newItem"];
                if (newCourse.Title.IsNullOrWhiteSpace())
                {
                    return RedirectToAction("AddNewCourse");
                }
                var dbList = _db.courses.Create();
                dbList.Title = newCourse.Title;
                _db.courses.Add(dbList);
                _db.SaveChanges();
            }
            return RedirectToAction("CourseIndex");
        }

        [HttpGet]
        public ActionResult AddToCourse(int? id)
        {
            int realId;
            if (id != null)
            {
                realId = (int) id;
            }
            else
            {
                throw new ArgumentNullException();
            }
            AllUsersAndSomeCourseViewModel allUsersAndSomeCourseViewModel = new AllUsersAndSomeCourseViewModel()
            {
                TheCourse = _courseService.GetCourseById(realId),
                ListOfUsers = _db.Users.ToList(),
                AllUsersInCourse = _courseService.GetUsersInSomeCourse(realId)
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
                    string theUserId = model.SelectedUserId;

                    newConnection.userId = theUserId;
                    if (theUserId.IsNullOrWhiteSpace())
                    {
                        return Redirect("/Course/AddToCourse/" + model.TheCourse.Id);
                    }
                    
                    var allUsersInCourse = _courseService.GetUsersInSomeCourse(model.TheCourse.Id);
                    foreach (var user in allUsersInCourse)
                    {
                        //Check if user is already in the course
                        if (theUserId == user.userId)
                        {
                            return Redirect("/Course/AddToCourse/" + model.TheCourse.Id);
                        }
                    }
                    newConnection.userId = theUserId;
                    newConnection.courseId = model.TheCourse.Id;
                    _db.userCourse.Add(newConnection);
                    _db.SaveChanges();
                }
            }
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
                //Because of reasons unknown
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
            if (_db.courses != null)
            {
                Course course = _db.courses.Find(id);
                //CourseViewModel courseViewmodel = _courseService.GetCourseByID(id);
                return View(course);
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        [HttpPost]
        public ActionResult EditCourse(Course model)
        {
            if (ModelState.IsValid)
            {
                using (_db)
                {
                    var courseToChange = _db.courses.Find(model.Id);

                    if (!model.Title.IsNullOrWhiteSpace())
                    {
                        courseToChange.Title = model.Title;
                    }
                    if (!model.Description.IsNullOrWhiteSpace())
                    {
                        courseToChange.Description = model.Description;
                    }
                    if (!model.Teacher.IsNullOrWhiteSpace())
                    {
                        courseToChange.Teacher = model.Teacher;
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