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
                where milestones.AssignmentId.Equals(assignmentId)
                select milestones;

            var milestoneViewList = new List<MilestoneViewModel>();
            foreach (var milestone in allMilestones)
            {
                var tempViewModel = new MilestoneViewModel();
                tempViewModel.Id = milestone.Id;
                tempViewModel.Title = milestone.Title;
                tempViewModel.Weight = milestone.Weight;
                tempViewModel.Input = milestone.Input;
                tempViewModel.Output = milestone.Output;
                milestoneViewList.Add(tempViewModel);
            }
            return milestoneViewList;
        }

        public MilestoneViewModel GetSingleAssignmentsInCourse(int assignmentId)
        {
            var theMilestone = (from milestone in _db.milestones
                where milestone.AssignmentId.Equals(assignmentId)
                select milestone).SingleOrDefault();

            if (theMilestone == null)
            {
                //TODO: throw an exeption, an error has occured
            }

            var tempViewModel = new MilestoneViewModel();
            tempViewModel.Id = theMilestone.Id;
            tempViewModel.Title = theMilestone.Title;
            tempViewModel.Weight = theMilestone.Weight;
            tempViewModel.AssignmentId = assignmentId;

            return tempViewModel;

        }
    }
}