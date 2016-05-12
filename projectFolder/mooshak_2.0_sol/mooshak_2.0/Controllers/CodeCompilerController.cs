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
            /*var code = "#include <iostream>\n" +
                        "using namespace std;\n" +
                        "int main()\n" +
                        "{\n" +
<<<<<<< HEAD
                        "cout << \"Hello World\" << endl;\n" +
                        "cout << \"The Output should contain two lines\" << endl;\n" +
                        "bool x = true;\n"+
                        "if(x == true)\n"+
                        "{\n"+
                        "cout << \"WHATAP\"<<endl;\n"+
                        "}\n"+
                        "else\n"+
                        "cout << \"fucabits\"<<endl;\n"+
                        "return 0;\n" +
=======
                        "\tcout << \"Hello World\" << endl;\n" +
                        "\tcout << \"The Output should contain two lines\" << endl;\n" +
                        "\tint a = 5;\n"+
                        "\tcout << a << endl;\n"+
                        "\treturn 0;\n" +
>>>>>>> cc9a3653f8b047671a2a460512bbca5a73185ead
                        "}";
                        */

            var dasBoot = Server.MapPath("~/App_Data/TestCode/MyCode.txt");
            string code = System.IO.File.ReadAllText(dasBoot);
           
            var workingFolder = Server.MapPath("~/App_Data/Solution_Uploads/");
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