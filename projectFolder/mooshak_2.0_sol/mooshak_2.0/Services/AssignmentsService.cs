using mooshak_2._0.Models;
using mooshak_2._0.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mooshak_2._0.Services
{
    public class AssignmentsService
    {
        private ApplicationDbContext _db;

        public AssignmentsService()
        {
            _db = new ApplicationDbContext();
        }
       public List<AssignmentViewModel> GetAssignmentsInCourse(int courseID)
        {
            // TODO: 
            return null;
        }
        public AssignmentViewModel GetAssignmentByID(int assignmentID)
        {
            //gets an assignment link by the assignmentID to the database ( a single assignment will be recived "single or default")
            var assignment = _db.Assignments.SingleOrDefault(x => x.id == assignmentID);
            if(assignment == null)
            {
                //TODO: throw an exeption, an error has occured
            }
            //Does the assignment contain any milestones ?(can return many milestones if the assignment contains multiple parts)
            //Here we recive only the milestone for the assignment with the correct AssignmentID
            var milestones = _db.Milestones.Where(x => x.assignmentID == assignmentID)
                .Select(x => new AssignmentMilestoneViewModel //creates an object that contains the milestone that we return 
                {
                    Title = x.title    

                })
                .ToList();//will return an empy list if the assignment contains no milestones

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