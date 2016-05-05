using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mooshak_2._0.Models.Entities
{
<<<<<<< HEAD:projectFolder/mooshak_2.0_sol/mooshak_2.0/Models/Entities/Courses.cs
    public class Courses
=======
    public class Course : IEnumerable
>>>>>>> 0769bd14c21d1eb308146378f302064faefcdf93:projectFolder/mooshak_2.0_sol/mooshak_2.0/Models/Entities/Course.cs
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