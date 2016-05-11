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
using Microsoft.AspNet.Identity;

namespace mooshak_2._0.Controllers
{
    [Authorize(Roles = "teacher")]
    public class TeacherController : Controller
    {
        readonly CourseService _courseService = new CourseService(new AssignmentsService());
        readonly AssignmentsService _assignmentService;
        readonly Dbcontext _db = new Dbcontext();

        public TeacherController()
        {
            _assignmentService = new AssignmentsService();
        }

        public ActionResult Index()
        {
            List<CourseViewModel> listOfCourseViewModels = _courseService.GetCoursesByUser(User.Identity.GetUserId());
            return View(listOfCourseViewModels);
        }

        public ActionResult Course(int? id)
        {
            if (!id.HasValue)
            {
                return View("Error");
            }

            var realId = id.Value;
            var model = _courseService.GetCourseByID(realId);
            return View(model);
        }

        public ActionResult Assignment(int id)
        {
           /* if (!id.HasValue)
            {
                return View("Error");
            }
            */
            var realId = id;
            List<AssignmentViewModel> model = _assignmentService.GetAssignmentsInCourse(realId);
            return PartialView(model);
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
            const string connect = @"Data Source=hrnem.ru.is;Initial Catalog=VLN2_2016_H17;User ID=VLN2_2016_H17_usr;Password=tinynight17";

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
                fileContent = (byte[])reader["FileContent"];
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
    }
    
}




