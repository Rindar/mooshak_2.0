using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace mooshak_2._0.Models.Entities
{
    public class User
    {
        public int id { get; set; }
        public string userName { get; set; }
        public string role { get; set; }
    }
}