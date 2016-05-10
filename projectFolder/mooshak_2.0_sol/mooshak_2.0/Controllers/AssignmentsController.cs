using mooshak_2._0.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mooshak_2._0.Controllers
{
    public class AssignmentsController : Controller
    {
        private readonly AssignmentsService _service = new AssignmentsService();
        // GET: Assignments
        public ActionResult Index()
        {
            return View();
        }


        //the method that is called to see details for an assignment
        public ActionResult Details (int id) 
        {
            var viewModel = _service.GetAssignmentById(id); // creates a viewmodel for the assignment
            return View(viewModel);
        }
    }
}