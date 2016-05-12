using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using mooshak_2._0.Models.Entities;

namespace mooshak_2._0.Models.ViewModels
{
    public class AssignmentViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<MilestoneViewModel> Milestones { get; set; }
        public int CourseId { get; set; }
        public Course PartOfCourse { get; set; }
        public  string Description { get; set; }
    }
}