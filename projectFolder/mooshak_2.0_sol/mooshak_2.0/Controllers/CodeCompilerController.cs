using mooshak_2._0.Models.ViewModels;
using mooshak_2._0.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace mooshak_2._0.Controllers
{
    public class CodeCompilerController : Controller
    {
        // GET: CodeCompiler
        readonly CourseService _courseService = new CourseService(new AssignmentsService());
        readonly AssignmentsService _assignmentService = new AssignmentsService();
        readonly AssignmentMilestoneService _assignmentMilestoneService = new AssignmentMilestoneService();
        public ActionResult CompilerIndex(int ? id)
        {
            if (!id.HasValue)
            {
                throw new EntryPointNotFoundException();
            }
            int realID = id.Value;
            MilestoneViewModel model = _assignmentMilestoneService.GetSingleMilestoneInAssignment(realID);
            return View(model);
        }
        [HttpPost]
        public ActionResult CompilerIndex(FormCollection data)
        { 
            var dasBoot = Server.MapPath("~/App_Data/TestCode/MyCode.txt");
            string code = System.IO.File.ReadAllText(dasBoot);
            
            var workingFolder = Server.MapPath("~/App_Data/Solution_Uploads/");
            var cppFileName = "Hello.cpp";
            //directory.createdirectory (so we can create a folder for each user)
            var exeFilePath = workingFolder + "Hello.exe";

            System.IO.File.WriteAllText(workingFolder + cppFileName, code);
            var compilerFolder = "C:\\Program Files (x86)\\Microsoft Visual Studio 14.0\\VC\\bin\\";
            string stringToParse = Request.Form["milestoneId"];
            int milestoneId = int.Parse(stringToParse);
          
            Process compiler = new Process();
            compiler.StartInfo.FileName                 = "cmd.exe";
            compiler.StartInfo.WorkingDirectory         = workingFolder;
            compiler.StartInfo.RedirectStandardInput    = true;
            compiler.StartInfo.RedirectStandardOutput   = true;
            compiler.StartInfo.UseShellExecute          = false;

            compiler.Start();
            compiler.StandardInput.WriteLine("\"" + compilerFolder + "vcvars32.bat" + "\"");
            compiler.StandardInput.WriteLine("cl.exe /nologo /EHsc " + cppFileName);
            compiler.StandardInput.WriteLine("exit");
            string output = compiler.StandardOutput.ReadToEnd();
            compiler.WaitForExit();
            compiler.Close();


            if(System.IO.File.Exists(exeFilePath))
            {

                var processInfoExe = new ProcessStartInfo(exeFilePath, "");
                processInfoExe.UseShellExecute          = false;
                processInfoExe.RedirectStandardOutput   = true;
                processInfoExe.RedirectStandardError    = true;
                processInfoExe.CreateNoWindow           = true;
                using (var processExe = new Process())
                {
                    processExe.StartInfo = processInfoExe;
                    processExe.Start();

                    var lines = new List<string>();
                    while(!processExe.StandardOutput.EndOfStream)
                    {
                        lines.Add(processExe.StandardOutput.ReadLine());
                    }
                    ViewBag.Output = lines;
                }
            }
            
            MilestoneViewModel model = _assignmentMilestoneService.GetSingleMilestoneInAssignment(milestoneId);
            return View(model);
        }
    }
}