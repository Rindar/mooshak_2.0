using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mooshak_2._0.Models.Entities
{
    public class Course : IEnumerable
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string teacher { get; set; }
        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}