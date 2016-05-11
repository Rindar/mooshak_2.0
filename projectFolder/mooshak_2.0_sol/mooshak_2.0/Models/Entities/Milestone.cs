using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mooshak_2._0.Models.Entities
{
    /// <summary>
    /// An Assignment Milsetone represents  a part of  an assignment .
    /// each assignment  may contain  multiple milestones, where each milestone
    /// weigh certain percentage  of the final grade of the assignment
    /// </summary>
    public class Milestone
    {
        /// <summary>
        /// The database generated unique ID for the milestone
        /// </summary>
        public int id { get; set; } // the id of the milestone
        public int assignmentID { get; set; } //the id of the assignment of the milestone
        public string title { get; set; } //title of the milestone
        /// <summary>
        /// determines how mutch each milestone weighs in the assignment 
        /// EXAMPLE:  if the if the milestone is 15% of the grade of the assignment 
        /// then its poperty contains 15
        /// </summary>
        public int weight { get; set; }
    }
}