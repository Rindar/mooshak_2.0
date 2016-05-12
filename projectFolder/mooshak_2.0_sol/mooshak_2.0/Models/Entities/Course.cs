using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mooshak_2._0.Models.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Teacher { get; set; }
    }
}