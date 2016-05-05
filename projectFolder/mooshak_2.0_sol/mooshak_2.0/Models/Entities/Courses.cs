using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mooshak_2._0.Models.Entities
{

    public class Courses

   /* public class Course : IEnumerable*/
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}