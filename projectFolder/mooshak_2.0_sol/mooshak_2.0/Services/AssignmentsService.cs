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

        public AssignmentsService()
        {
            _db = new Dbcontext();
            _assignmentMilestoneService = new AssignmentMilestoneService();
        }

        public List<AssignmentViewModel> GetAssignmentsInCourse(int courseID)
        {
            var allAssignments = from assignments in _db.assignments
                                 where assignments.CourseId.Equals(courseID)
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
                    id = assignment.Id,
                    title = assignment.Title,
                    courseId = assignment.CourseId,
                    description = assignment.Description,
                    milestones = _assignmentMilestoneService.GetMilestoneInAssignment(assignment.Id)
                };
                var milestoneViewList = _assignmentMilestoneService.GetMilestoneInAssignment(assignment.Id);
                newViewModel.milestones = milestoneViewList;
                assignmentViewModelList.Add(newViewModel);

            }
            return assignmentViewModelList;
           
        }
        public AssignmentViewModel GetAssignmentById(int assignmentId)
        {
            //Gets an assignment link by the assignmentID to the database ( a single assignment will be recived "single or default")
            var assignment = (from assignments in _db.assignments
                              where assignments.Id.Equals(assignmentId)
                              select assignments).SingleOrDefault();

            if (assignment == null)
            {
                throw new ArgumentNullException();
            }
            var allMilestones = from milestones in _db.milestones
                                where milestones.assignmentID.Equals(assignmentId)
                                select milestones;

            var milestoneViewList = new List<MilestoneViewModel>();
            foreach (var milestone in allMilestones)
            {
                var tempViewModel = new MilestoneViewModel();
                {
                    tempViewModel.Id = milestone.id;
                    tempViewModel.Title = milestone.title;
                    tempViewModel.Weight = milestone.weight;
                    tempViewModel.AssignmentId = assignmentId;
                   
                };
                milestoneViewList.Add(tempViewModel);
            }

            //create a viewmodel fot the assignment that has a milestone
            var viewModel = new AssignmentViewModel
            {
                id = assignment.Id,
                title = assignment.Title,
                milestones = milestoneViewList
                
            };

            return viewModel;
        }

     /*   public AssignmentViewModel createAssignment(int courseId)
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
        }*/

        public AssignmentViewModel GetSingleAssignmentsInCourse(int courseID, int assignmentId)
        {
            var theAssignment = (from assignments in _db.assignments
                                 where assignments.CourseId.Equals(courseID) && assignments.Id.Equals(assignmentId)
                                 select assignments).SingleOrDefault();

            if (theAssignment == null)
            {
                //TODO: throw an exeption, an error has occured
            }
            
            var newViewModel = new AssignmentViewModel
            {
                id = theAssignment.Id,
                title = theAssignment.Title,
                courseId = theAssignment.CourseId,
                milestones = _assignmentMilestoneService.GetMilestoneInAssignment(theAssignment.Id)
                
            };

            var milestoneViewList = _assignmentMilestoneService.GetMilestoneInAssignment(theAssignment.Id);
            newViewModel.milestones = milestoneViewList;

            return newViewModel;
        }
    }
}