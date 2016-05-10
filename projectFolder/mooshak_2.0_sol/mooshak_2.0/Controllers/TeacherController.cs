<<<<<<< HEAD
﻿using System;
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

namespace mooshak_2._0.Controllers
{
    [Authorize(Roles = "teacher")]
    public class TeacherController : Controller
    {
        CourseService _courseService = new CourseService(new AssignmentsService());
        // GET: Teacher
        Dbcontext db = new Dbcontext();
        public ActionResult Index()
        {
            List<CourseViewModel> ListOfCourseViewModels = new ListStack<CourseViewModel>();
            ListOfCourseViewModels = _courseService.GetAllCourses();
            return View(ListOfCourseViewModels);
        }

        public ActionResult Course()
        {
            return View();
        }

        public ActionResult Assignment()
        {
            return View();
        }
        public ActionResult Submissions()
        {
            return View(db.submissions.ToList());
        }
        public FileContentResult GetFile(int id)
        {
            SqlDataReader reader; byte[] fileContent = null;
            string mimeType = ""; string fileName = "";
            const string connect = @"Data Source=hrnem.ru.is;Initial Catalog=VLN2_2016_H17;User ID=VLN2_2016_H17_usr;Password=tinynight17";

            using (var conn = new SqlConnection(connect))
            {
                var qry = "SELECT FileContent, MimeType, FileName FROM Submissions WHERE ID = @ID";
                var cmd = new SqlCommand(qry, conn);
                cmd.Parameters.AddWithValue("@ID", id);
                conn.Open();
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    fileContent = (byte[])reader["FileContent"];
                    mimeType = reader["MimeType"].ToString();
                    fileName = reader["FileName"].ToString();
                }
            }
            return File(fileContent, mimeType, fileName);
        }
    }
}
=======
﻿using System;
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
using Microsoft.AspNet.Identity;
using System.Data.SqlClient;

namespace mooshak_2._0.Controllers
{
    [Authorize(Roles = "teacher")]
    public class TeacherController : Controller
    {
        Dbcontext db = new Dbcontext();
        CourseService _courseService = new CourseService(new AssignmentsService());
        // GET: Teacher
        public ActionResult Index()
        {
            List<CourseViewModel> ListOfCourseViewModels = new ListStack<CourseViewModel>();
            ListOfCourseViewModels = _courseService.GetCoursesByUser(User.Identity.GetUserId());
            return View(ListOfCourseViewModels);
        }

        public ActionResult Course(int? id)
        {
            if (id.HasValue)
            {
                int realID = id.Value;
                CourseViewModel model = _courseService.GetCourseByID(realID);
                return View(model);
            }
            return View("Error");
        }

        public ActionResult Assignment()
        {
            return View();
        }

        public ActionResult Sidebar()
        {
            var model = _courseService.GetCoursesByUser(User.Identity.GetUserId());
            return PartialView("~/Views/Shared/_SideBarTeacherStudent.cshtml", model);
        }
        public ActionResult Submissions()
        {
            return View(db.submissions.ToList());
        }

        public FileContentResult GetFile(int id)
        {
            SqlDataReader rdr; byte[] fileContent = null;
            string mimeType = ""; string fileName = "";
            const string connect = @"Data Source=hrnem.ru.is;Initial Catalog=VLN2_2016_H17;User ID=VLN2_2016_H17_usr;Password=tinynight17";

            using (var conn = new SqlConnection(connect))
            {
                var qry = "SELECT FileContent, MimeType, FileName FROM Submissions WHERE ID = @ID";
                var cmd = new SqlCommand(qry, conn);
                cmd.Parameters.AddWithValue("@ID", id);
                conn.Open();
                rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    rdr.Read();
                    fileContent = (byte[])rdr["FileContent"];
                    mimeType = rdr["MimeType"].ToString();
                    fileName = rdr["FileName"].ToString();
                }
            }
            return File(fileContent, mimeType, fileName);
        }
    }
}

>>>>>>> bab5ea0c74fbd043a058d7e7ed960ccfdba8067e
