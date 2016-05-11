using mooshak_2._0.Models;
using mooshak_2._0.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mooshak_2._0.Services
{
    public class AssignmentMilestoneService
    {
        private Dbcontext _db;

        public AssignmentMilestoneService()
        {
            _db = new Dbcontext();
        }

        public List<MilestoneViewModel> GetMilestoneInAssignment(int assignmentId)
        {
            var allMilestones = from milestones in _db.milestones
                where milestones.assignmentID.Equals(assignmentId)
                select milestones;

            var milestoneViewList = new List<MilestoneViewModel>();
            foreach (var milestone in allMilestones)
            {
                var tempViewModel = new MilestoneViewModel();
                tempViewModel.Id = milestone.id;
                tempViewModel.Title = milestone.title;
                tempViewModel.Weight = milestone.weight;
                milestoneViewList.Add(tempViewModel);
            }
            return milestoneViewList;
        }

        public MilestoneViewModel GetSingleAssignmentsInCourse(int assignmentId)
        {
            var theMilestone = (from milestone in _db.milestones
                where milestone.assignmentID.Equals(assignmentId)
                select milestone).SingleOrDefault();

            if (theMilestone == null)
            {
                //TODO: throw an exeption, an error has occured
            }

            var tempViewModel = new MilestoneViewModel();
            tempViewModel.Id = theMilestone.id;
            tempViewModel.Title = theMilestone.title;
            tempViewModel.Weight = theMilestone.weight;
            tempViewModel.AssignmentId = assignmentId;

            return tempViewModel;

        }
    }
}