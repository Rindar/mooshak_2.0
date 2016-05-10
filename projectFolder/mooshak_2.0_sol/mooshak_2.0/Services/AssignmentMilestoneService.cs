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

        public List<ProblemViewModel> GetMilestoneInAssignment(int assignmentId)
        {
            var allMilestones = from milestones in _db.milestones
                                where milestones.assignmentID.Equals(assignmentId)
                                select milestones;

            var milestoneViewList = new List<ProblemViewModel>();
            foreach (var milestone in allMilestones)
            {
                var tempViewModel = new ProblemViewModel();
                tempViewModel.Id = milestone.id;
                tempViewModel.Title = milestone.title;
                tempViewModel.Weight = milestone.weight;
                milestoneViewList.Add(tempViewModel);
            }
            return milestoneViewList;
        }
    }
}