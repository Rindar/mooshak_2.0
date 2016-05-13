<<<<<<< HEAD
﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mooshak_2._0.Models.Entities
{
    public class Submissions : IEnumerable
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public  int AssignmentId { get; set; }
        public string UserName { get; set; }
        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
=======
﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mooshak_2._0.Models.Entities
{
    public class Submissions : IEnumerable
    {
        public int Id                { get; set; }
        public string FileName       { get; set; }
        public string Status         { get; set; }
        public int MileStoneId       { get; set; }
        public string UserName       { get; set; }
        //public int AssignmentId { get; set; }
        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
>>>>>>> 98ac9151b43d9e6c271a7379314e10c5fccac5f7
}