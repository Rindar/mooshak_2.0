using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.SqlServer.Server;

namespace mooshak_2._0.Error_Handling
{
    public class Logger
    {
        private static Logger _theInstance = null;
        public static Logger Instance => _theInstance ?? (_theInstance = new Logger());
        public void LogException(Exception ex)
        {
            string logMessage = string.Format("Time: {0} Message: {1} Source: {2}{3}",
                DateTime.Now,
                ex.Message,
                ex.Source,
                System.Environment.NewLine);

                string LogPath = ConfigurationManager.AppSettings["LogFile"];

                StreamWriter writer = new StreamWriter(LogPath, true, Encoding.Default);
                writer.WriteLine(logMessage);
            writer.Close();
        }
    }
}