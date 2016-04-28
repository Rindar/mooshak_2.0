using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mooshak_2._0.Models.Entities
{
    public class Assignment
    {
        public int ID { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; }
    }
}