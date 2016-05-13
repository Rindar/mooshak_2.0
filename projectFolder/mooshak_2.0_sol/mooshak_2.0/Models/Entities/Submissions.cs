using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mooshak_2._0.Models.Entities
{
    public class Submissions
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public string Status { get; set; }
        public int MileStoneId { get; set; }
        public string UserName { get; set; }
        public string FileName { get; set; }

    }
}