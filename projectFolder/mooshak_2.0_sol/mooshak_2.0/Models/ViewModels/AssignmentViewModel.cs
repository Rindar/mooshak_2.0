using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mooshak_2._0.Models.ViewModels
{
    public class AssignmentViewModel
    {
        public int id { get; set; }
        public string title { get; set; }
        public List<ProblemViewModel> milestones { get; set; }
    }
}