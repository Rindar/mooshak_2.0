using mooshak_2._0.Models;
using mooshak_2._0.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Web;

namespace mooshak_2._0.Services
{
    public class AssignmentsService
    {
        public Dbcontext _db;

        public AssignmentsService()
        {
            _db = new Dbcontext();
        }
       public List<AssignmentViewModel> GetAssignmentsInCourse(int courseID)
       {
            // TODO: 
            //Gets an assignment link by the assignmentID to the database ( a single assignment will be recived "single or default")
            var assignments = _db.assignments.Where(x => x.courseId == courseID);
            if (assignments == null)
            {

                //TODO: throw an exeption, an error has occured
            }
            
            //create a viewmodel fot the assignment that has a milestone
            List<AssignmentViewModel> result = new List<AssignmentViewModel>();
            foreach (var assignment in assignments)
            {
                var newViewModel = new AssignmentViewModel();
                newViewModel.Title = assignment.title;
                result.Add(newViewModel);
            }
            return result;
           
        }
        public AssignmentViewModel GetAssignmentByID(int assignmentID)
        {
            //Gets an assignment link by the assignmentID to the database ( a single assignment will be recived "single or default")
            var assignment = _db.assignments.SingleOrDefault(x => x.id == assignmentID);
            if(assignment == null)
            {
                //TODO: throw an exeption, an error has occured
            }
            //Does the assignment contain any milestones ?(can return many milestones if the assignment contains multiple parts)
            //Here we recive only the milestone for the assignment with the correct AssignmentID
            var milestones = _db.milestones.Where(x => x.assignmentID == assignmentID)
                .Select(x => new ProblemViewModel //Creates an object that contains the milestone that we return 
                {
                    Title = x.title    
                })
                //will return an empy list if the assignment contains no milestones.
                .ToList();

            //create a viewmodel fot the assignment that has a milestone
            var viewModel = new AssignmentViewModel 
            {
                Title = assignment.title,
                Milestones = milestones
            };
            return viewModel;
        }
    }
}