using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mooshak_2._0.Models.Entities;

namespace mooshak_2._0.Models.ViewModels
{
    // GET: AllUsersAndCourses
    public class AllUsersAndSomeCourseViewModel
    {
        public CourseViewModel theCourse { get; set; }
        public List<ApplicationUser> listOfUsers { get; set; }
        public string selectedUserId { get; set; }
        public List<UserCourse> allUsersInCourse { get; set; }
    }
}