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

        public ActionResult CompilerIndex()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CompilerIndex(FormCollection data)
        {
            var code = "#include <iostream>\n" +
                        "using namespace std;\n" +
                        "int main()\n" +
                        "{\n" +
                        "cout << \"Hello World\" << endl;\n" +
                        "cout << \"The Output should contain two lines\" << endl;\n" +
                        "int a = 5;"+
                        "cout << a << endl;\n"+
                        "return 0;\n" +
                        "}";
            var workingFolder = "C:\\Users\\Andri\\Documents\\mooshak_2.0\\projectFolder\\mooshak_2.0_sol\\mooshak_2.0\\App_Data\\Solution_Uploads\\";
            var cppFileName = "Hello.cpp";
            //directory.createdirectory (so we can create a folder for each user)
            var exeFilePath = workingFolder + "Hello.exe";

            System.IO.File.WriteAllText(workingFolder + cppFileName, code);
            var compilerFolder = "C:\\Program Files (x86)\\Microsoft Visual Studio 14.0\\VC\\bin\\";


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
                processInfoExe.UseShellExecute = false;
                processInfoExe.RedirectStandardOutput = true;
                processInfoExe.RedirectStandardError = true;
                processInfoExe.CreateNoWindow = true;
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

            return View();
        }
    }
}