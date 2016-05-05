using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mooshak_2._0.Models.Entities
{
    public class Courses
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
    }
}