using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mooshak_2._0.Models.Entities
{
    public class Assignment :IEnumerable
    {
        public int id { get; set; }
        public int courseId { get; set; }
        public string title { get; set; }
        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}