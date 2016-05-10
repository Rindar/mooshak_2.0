using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mooshak_2._0.Models.ViewModels
{
    public class CourseViewModel
    {
        public string title { get; set; }
        public List<AssignmentViewModel> assignments { get; set; }
        public string teacher { get; set; }
        public int id { get; set; }
    }
}