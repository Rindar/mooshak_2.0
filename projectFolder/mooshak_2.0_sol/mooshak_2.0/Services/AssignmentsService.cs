using mooshak_2._0.Models;
using mooshak_2._0.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Web;
using mooshak_2._0.Models.Entities;

namespace mooshak_2._0.Services
{
    public class AssignmentsService
    {
        private readonly Dbcontext _db;
        private readonly AssignmentMilestoneService _assignmentMilestoneService;
        private readonly CourseService _courseService;

        public AssignmentsService()
        {
            _db = new Dbcontext();
            _assignmentMilestoneService = new AssignmentMilestoneService();
        }

        public List<AssignmentViewModel> GetAssignmentsInCourse(int courseID)
        {

            // TODO: 
            //Gets an assignment link by the assignmentID to the database ( a single assignment will be recived "single or default")
            var allAssignments = from assignments in _db.assignments
                                 where assignments.courseId.Equals(courseID)
                                 select assignments;

            if (allAssignments == null)
            {
                //TODO: throw an exeption, an error has occured
            }
            
            var assignmentViewModelList = new List<AssignmentViewModel>();
            foreach (var assignment in allAssignments)
            {
                var newViewModel = new AssignmentViewModel
                {
                    id = assignment.id,
                    title = assignment.title
                };
                var milestoneViewList = _assignmentMilestoneService.GetMilestoneInAssignment(assignment.id);
                newViewModel.milestones = milestoneViewList;
                assignmentViewModelList.Add(newViewModel);

            }
            return assignmentViewModelList;
           
        }
        public AssignmentViewModel GetAssignmentById(int assignmentId)
        {
            //Gets an assignment link by the assignmentID to the database ( a single assignment will be recived "single or default")
            var assignment = (from assignments in _db.assignments
                              where assignments.id.Equals(assignmentId)
                              select assignments).SingleOrDefault();

            if (assignment == null)
            {
                throw new ArgumentNullException();
            }
            var allMilestones = from milestones in _db.milestones
                                where milestones.assignmentID.Equals(assignmentId)
                                select milestones;

            var milestoneViewList = new List<ProblemViewModel>();
            foreach (var milestone in allMilestones)
            {
                var tempViewModel = new ProblemViewModel
                {
                    Id = milestone.id,
                    Title = milestone.title,
                    Weight = milestone.weight
                };
                milestoneViewList.Add(tempViewModel);
            }

            //create a viewmodel fot the assignment that has a milestone
            var viewModel = new AssignmentViewModel
            {
                id = assignment.id,
                title = assignment.title,
                milestones = milestoneViewList
            };

            return viewModel;
        }

        public AssignmentViewModel createAssignment(int courseId)
        {
            Course currentCourse = (from courses in _db.courses
                                 where courses.id == courseId
                                 select courses).SingleOrDefault();
            
            Assignment newAssignment = new Assignment()
            {
                courseId = currentCourse.id,
                timeStarts = DateTime.Now,
                timeEnds = DateTime.MaxValue
            };


            AssignmentViewModel viewModeltoReturn = new AssignmentViewModel()
            {
                courseId = newAssignment.courseId,
                partOfCourse = currentCourse,
                title = newAssignment.title,
                milestones = _assignmentMilestoneService.GetMilestoneInAssignment(newAssignment.id)

            };

            var dbList = _db.assignments.Create();
            dbList.title = newAssignment.title;
            dbList.courseId = newAssignment.courseId;
            _db.assignments.Add(dbList);
            _db.SaveChanges();

            return viewModeltoReturn;
        }
    }
}