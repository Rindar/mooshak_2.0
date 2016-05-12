using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mooshak_2._0.Models.ViewModels
{
    public class CourseViewModel
    {
        public string Title { get; set; }
        public List<AssignmentViewModel> Assignments { get; set; }
        public string Teacher { get; set; }
        public int Id { get; set; }
        public string Description { get; set; }
    }
}