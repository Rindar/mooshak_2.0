using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using mooshak_2._0.Models.Entities;

namespace mooshak_2._0.Models.ViewModels
{
    public class AssignmentViewModel :IEnumerable
    {
        public int id { get; set; }
        public string title { get; set; }
        public List<MilestoneViewModel> milestones { get; set; }
        public int courseId { get; set; }
        public Course partOfCourse { get; set; }
        public  string description { get; set; }


        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}