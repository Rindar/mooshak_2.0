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
                throw new ArgumentNullException();
            }
            
            var assignmentViewModelList = new List<AssignmentViewModel>();
            foreach (var assignment in allAssignments)
            {

                var newViewModel = new AssignmentViewModel
                    {
                        Id = assignment.Id,
                        Title = assignment.Title,
                        CourseId = assignment.CourseId,
                        Description = assignment.Description,
                        Milestones = _assignmentMilestoneService.GetMilestoneInAssignment(assignment.Id),
                        TimeEnds = (DateTime) assignment.TimeEnds
                  
                    };
                    var milestoneViewList = _assignmentMilestoneService.GetMilestoneInAssignment(assignment.Id);
                    newViewModel.Milestones = milestoneViewList;
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
                                where milestones.AssignmentId.Equals(assignmentId)
                                select milestones;

            var milestoneViewList = new List<MilestoneViewModel>();
            foreach (var milestone in allMilestones)
            {
                var tempViewModel = new MilestoneViewModel();
                {
                    tempViewModel.Id = milestone.Id;
                    tempViewModel.Title = milestone.Title;
                    tempViewModel.Weight = milestone.Weight;
                    tempViewModel.AssignmentId = assignmentId;
                   
                };
                milestoneViewList.Add(tempViewModel);
            }

            //create a viewmodel fot the assignment that has a milestone
            var viewModel = new AssignmentViewModel
            {
                Id = assignment.Id,
                Title = assignment.Title,
                CourseId = assignment.CourseId,
                Milestones = milestoneViewList
            };

            return viewModel;
        }

        public AssignmentViewModel GetSingleAssignmentsInCourse(int courseID, int assignmentId)
        {
            var theAssignment = (from assignments in _db.assignments
                                 where assignments.CourseId.Equals(courseID) && assignments.Id.Equals(assignmentId)
                                 select assignments).SingleOrDefault();

            if (theAssignment == null)
            {
                throw new ArgumentNullException();
            }
            
            var newViewModel = new AssignmentViewModel
            {
                Id = theAssignment.Id,
                Title = theAssignment.Title,
                CourseId = theAssignment.CourseId,
                Milestones = _assignmentMilestoneService.GetMilestoneInAssignment(theAssignment.Id)
                
            };

            var milestoneViewList = _assignmentMilestoneService.GetMilestoneInAssignment(theAssignment.Id);
            newViewModel.Milestones = milestoneViewList;

            return newViewModel;
        }
    }
}