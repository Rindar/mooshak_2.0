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
        public CourseViewModel TheCourse { get; set; }
        public List<ApplicationUser> ListOfUsers { get; set; }
        public string SelectedUserId { get; set; }
        public List<UserCourse> AllUsersInCourse { get; set; }
    }
}