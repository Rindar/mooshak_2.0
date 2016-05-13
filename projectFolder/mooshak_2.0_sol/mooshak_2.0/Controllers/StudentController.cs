using Microsoft.AspNet.Identity;
using mooshak_2._0.Models.ViewModels;
using mooshak_2._0.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using mooshak_2._0.ErrorHandler;

namespace mooshak_2._0.Controllers
{
    [Authorize(Roles = "student")]
    [ErrorAttributeHandler]
    public class StudentController : Controller
    {
        readonly CourseService _courseService = new CourseService(new AssignmentsService());
        readonly AssignmentsService _assignmentService = new AssignmentsService();
        readonly AssignmentMilestoneService _assignmentMilestoneService = new AssignmentMilestoneService();
        // GET: Student
        public ActionResult Index()
        {
            List<CourseViewModel> userCourse = _courseService.GetCoursesByUser(User.Identity.GetUserId());
            return View(userCourse);
        }

        public ActionResult Course(int? id)
        {
            if (!id.HasValue)
            {
                throw new EntryPointNotFoundException();
            }
            int realID = id.Value;
            CourseViewModel model = _courseService.GetCourseById(realID);
            return View(model);
        }

        public ActionResult Assignment(int? id)
        {
            if (!id.HasValue)
            {
                throw new EntryPointNotFoundException();
            }
            int realID = id.Value;
            AssignmentViewModel model = _assignmentService.GetAssignmentById(realID);
            return View(model);
        }
        /*
        public ActionResult Submission()
        {
            if(Request.Files == null) { throw new Exception(); }
            foreach (string upload in Request.Files)
            {
                //if (!Request.Files[upload].HasFile()) continue;

                string mimeType = Request.Files[upload].ContentType;
                Stream fileStream = Request.Files[upload].InputStream;
                string fileName = Path.GetFileName(Request.Files[upload].FileName);
                string userName = Request.Form["user_name"];
                int fileLength = Request.Files[upload].ContentLength;
                byte[] fileData = new byte[fileLength];
                fileStream.Read(fileData, 0, fileLength);

                const string connect = @"Data Source=hrnem.ru.is;Initial Catalog=VLN2_2016_H17;User ID=VLN2_2016_H17_usr;Password=tinynight17";
                using (var conn = new SqlConnection(connect))
                {
                    var qry = "INSERT INTO Submissions (FileContent, MimeType, FileName, UserName) VALUES (@FileContent, @MimeType, @FileName,@UserName)";
                    var cmd = new SqlCommand(qry, conn);
                    cmd.Parameters.AddWithValue("@FileContent", fileData);
                    cmd.Parameters.AddWithValue("@MimeType", mimeType);
                    cmd.Parameters.AddWithValue("@FileName", fileName);
                    cmd.Parameters.AddWithValue("@UserName", userName);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            return View();
        }
        */
        public ActionResult Submission(int ? id)
        {
            int realID = id.Value;
            //AssignmentViewModel model = _assignmentService.GetAssignmentById(realID);
            MilestoneViewModel model = _assignmentMilestoneService.GetSingleMilestoneInAssignment(realID);
            return View(model);
        }
        [HttpPost]
        public ActionResult Submission(HttpPostedFileBase file)
        {
            var theFileName = Path.GetFileName(file.FileName);
            var path = Path.Combine(Server.MapPath("~/App_Data/TestCode"), theFileName);
            if (file.ContentLength > 0)
            {
                file.SaveAs(path);
            }
            string fileName = file.FileName;
            string filePath = path;
            string theUserName = User.Identity.Name;
            string stringToParse = Request.Form["MileStoneId"];
            int mileStoneId = int.Parse(stringToParse);
          
            const string connect = @"Data Source=hrnem.ru.is;Initial Catalog=VLN2_2016_H17;User ID=VLN2_2016_H17_usr;Password=tinynight17";
            using (var conn = new SqlConnection(connect))
            {
                var qry = "INSERT INTO Submissions (FilePath,UserName,MileStoneId,FileName) VALUES (@FilePath,@UserName,@MileStoneId,@FileName)";
                var cmd = new SqlCommand(qry, conn);
                
                cmd.Parameters.AddWithValue("@FilePath", filePath);
                cmd.Parameters.AddWithValue("@UserName", theUserName);
                cmd.Parameters.AddWithValue("@MileStoneId", mileStoneId);
                cmd.Parameters.AddWithValue("@FileName", fileName);

                conn.Open();
                cmd.ExecuteNonQuery();

                return RedirectToAction("CompilerIndex", "CodeCompiler", new { id = mileStoneId });
            }
        }
        public ActionResult Sidebar()
        {
            var model = _courseService.GetCoursesByUser(User.Identity.GetUserId());
            return PartialView("~/Views/Shared/_SideBarTeacherStudent.cshtml", model);
        }
    }
}