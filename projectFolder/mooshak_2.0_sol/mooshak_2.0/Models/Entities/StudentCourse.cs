using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mooshak_2._0.Models.Entities
{
    public class StudentCourse
    {
        public int id { get; set; }
        public string studentId { get; set; }
        public int courseId { get; set; }
    }
}
