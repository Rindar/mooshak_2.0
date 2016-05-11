using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mooshak_2._0.Models.Entities;

namespace mooshak_2._0.Models.ViewModels
{
    public class MilestoneViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Weight { get; set; }
        public int AssignmentId { get; set; }
    }
}