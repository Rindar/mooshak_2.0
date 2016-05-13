using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using mooshak_2._0.Models;
using mooshak_2._0.Models.Entities;
using Microsoft.AspNet.Identity;
using mooshak_2._0.Models.ViewModels;
using System.Security.Principal;

namespace mooshak_2._0.Services
{
    public class CourseService
    {
        private readonly Dbcontext _db = new Dbcontext();
        private readonly AssignmentsService _assignmentsService;

        public CourseService(AssignmentsService assignmentsService)
        {
            _assignmentsService = assignmentsService;
        }

        public CourseService()
        {
            _assignmentsService = new AssignmentsService();
        }
        
        public List<CourseViewModel> GetAllCourses()
        {
            IEnumerable<Course> allCourses = from courses in _db.courses
                                             select courses;

            List<CourseViewModel> courseViewModelList = new List<CourseViewModel>();

            foreach (var course in allCourses)
            {
                CourseViewModel courseViewModel = new CourseViewModel();
                courseViewModel.Title = course.Title;
                courseViewModel.Assignments = _assignmentsService.GetAssignmentsInCourse(course.Id);
                courseViewModel.Id = course.Id;
                courseViewModel.Description = course.Description;
                
                courseViewModelList.Add(courseViewModel);
            }
            return courseViewModelList;
            
        }

        public CourseViewModel GetCourseById(int courseId)
        {

            var course = (from courses in _db.courses
                          where courses.Id.Equals(courseId)
                          select courses).SingleOrDefault();

            if (course.Id != courseId)
            {
                throw new ArgumentNullException();
            }

            List<AssignmentViewModel> assignments = _assignmentsService.GetAssignmentsInCourse(courseId);

            //Create a viewmodel fot the assignment that has a milestone
            var viewModel = new CourseViewModel();
            viewModel.Id = course.Id;
            viewModel.Title = course.Title;
            viewModel.Assignments = assignments;
            viewModel.Description = course.Description;
            return viewModel;

        }
        
        public List<CourseViewModel> GetCoursesByUser(string id)
        {
            IEnumerable<UserCourse> allUserCourses = from courses in _db.userCourse
                                                     where courses.userId.Equals(id)
                                                     select courses;
            
            List<CourseViewModel> userCourses = new List<CourseViewModel>();
            foreach(var item in allUserCourses)
            {
                CourseViewModel tmpModel = GetCourseById(item.courseId);
                userCourses.Add(tmpModel);
            }
            return userCourses;
        }

        public List<UserCourse> GetUsersInSomeCourse(int courseId)
        {
            List<UserCourse> allUsersinCourse = (from usersAndCourses in _db.userCourse
                                                 where usersAndCourses.courseId == courseId
                                                 select usersAndCourses).ToList();


            return allUsersinCourse;
        }
    }
}