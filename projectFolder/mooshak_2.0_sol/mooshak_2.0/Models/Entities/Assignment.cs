using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mooshak_2._0.Models.Entities
{
    public class Assignment
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; }
        public DateTime? TimeStarts { get; set; }
        public DateTime? TimeEnds { get; set; }
        public string Description { get; set; }

    }
}