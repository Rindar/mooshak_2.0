using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime.Misc;
using mooshak_2._0.Models;
using mooshak_2._0.Models.Entities;
using mooshak_2._0.Models.ViewModels;
using mooshak_2._0.Services;
using System.Data.SqlClient;
using Microsoft.AspNet.Identity;

namespace mooshak_2._0.Controllers
{
    [Authorize(Roles = "teacher")]
    public class TeacherController : Controller
    {
        readonly CourseService _courseService = new CourseService(new AssignmentsService());
        readonly AssignmentsService _assignmentService = new AssignmentsService();
        readonly Dbcontext _db = new Dbcontext();
        
        public ActionResult Index()
        {
            List<CourseViewModel> listOfCourseViewModels = _courseService.GetCoursesByUser(User.Identity.GetUserId());
            return View(listOfCourseViewModels);
        }

        public ActionResult Course(int? id)
        {
            if (id.HasValue)
            {
                var realId = id.Value;
                var model = _courseService.GetCourseByID(realId);        
                return View(model);
            }
            return RedirectToAction("Error", "Home");
        }

        public ActionResult Assignment(int? id)
        {
            if (id.HasValue)
            {
                int realId = id.Value;
                List<AssignmentViewModel> model = _assignmentService.GetAssignmentsInCourse(realId);
                return PartialView(model);
            }
            return RedirectToAction("Error", "Home");
        }

        public ActionResult Submissions()
        {
            return View(_db.submissions.ToList());
        }

        public FileContentResult GetFile(int id)
        {
            byte[] fileContent = null;
            var mimeType = "";
            var fileName = "";
            const string connect =
                @"Data Source=hrnem.ru.is;Initial Catalog=VLN2_2016_H17;User ID=VLN2_2016_H17_usr;Password=tinynight17";

            using (var conn = new SqlConnection(connect))
            {
                var qry = "SELECT FileContent, MimeType, FileName FROM Submissions WHERE ID = @ID";
                var cmd = new SqlCommand(qry, conn);
                cmd.Parameters.AddWithValue("@ID", id);
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    return File(fileContent, mimeType, fileName);
                }
                reader.Read();
                fileContent = (byte[]) reader["FileContent"];
                mimeType = reader["MimeType"].ToString();
                fileName = reader["FileName"].ToString();
            }
            return File(fileContent, mimeType, fileName);
        }

        public ActionResult Sidebar()
        {
            var model = _courseService.GetCoursesByUser(User.Identity.GetUserId());
            return PartialView("~/Views/Shared/_SideBarTeacherStudent.cshtml", model);
        }

        public ActionResult CreateAssignment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAssignment(FormCollection model)
        {
            string getTitle = Request.Form["courseTitle"];
            DateTime getEndDate = DateTime.Parse(Request.Form["endDate"]);
            string getDescription = Request.Form["description"];
            string getInput = Request.Form["input"];
            String getOutput = Request.Form["output"];

            var dbList = _db.assignments.Create();
            dbList.title = getTitle;
            dbList.description = getDescription;
            dbList.timeStarts = DateTime.Now;
            dbList.timeEnds = getEndDate;
            dbList.input = getInput;
            dbList.output = getOutput;
            dbList.courseId = 1; //FIX

            _db.assignments.Add(dbList);
            _db.SaveChanges();

            return View();
        }
    }
}




