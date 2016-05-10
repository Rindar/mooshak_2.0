﻿using System;
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

        [HttpGet]
        public ActionResult AddToCourse(int id)
        {
            AllUsersAndSomeCourseViewModel allUsersAndSomeCourseViewModel = new AllUsersAndSomeCourseViewModel()
            {
                theCourse = _courseService.GetCourseByID(id),
                listOfUsers = db.Users.ToList(),
                allUsersInCourse = _courseService.GetUsersInSomeCourse(id)
             };
            return View(allUsersAndSomeCourseViewModel);
        }
        
        [HttpPost]
        public ActionResult AddTheUserToTheCourse(AllUsersAndSomeCourseViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (db)
                {
                    var newConnection = db.userCourse.Create();
                    //var theUser = db.Users.Find(model.selectedUserId).ToString();
                    string theUserId = model.selectedUserId;

                    newConnection.userId = theUserId;
                    if (theUserId.IsNullOrWhiteSpace())
                    {
                        return RedirectToAction("CourseIndex");
                    }
                    
                    //TODO: If user is already in course, dont add him 
                    /*if (model.theCourse.ID == db.userCourse.Find(theUserId).id)
                    {
                        System.Diagnostics.Debug.WriteLine("works");
                    }*/

                    newConnection.userId = theUserId;
                    newConnection.courseId = model.theCourse.id;
                    db.userCourse.Add(newConnection);
                    db.SaveChanges();
                }
            }
            //TODO: Redirect to the same course again
            return RedirectToAction("CourseIndex");
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
        public ActionResult RemoveUser(int id)
        {
            var model = db.userCourse.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            db.userCourse.Remove(model);
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