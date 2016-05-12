using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace mooshak_2._0.ErrorHandler
{
    public class Logger
    {
        private static Logger theInstance = null;

        public static Logger Instance
        {
            get
            {
                if (theInstance == null) {
                    theInstance = new Logger();
                }
                return theInstance;
            }
        }

        public void Log(Exception e)
        {
            string logMessage = string.Format("Time: {0} Message: {1} Source: {2} Trace:{3}{4}{3}",
                DateTime.Now, e.Message, e.Source, Environment.NewLine, e.StackTrace);

            string LogPath = ConfigurationManager.AppSettings["LogPath"];
            
            StreamWriter writer = new StreamWriter(LogPath, true, Encoding.Default);
            writer.WriteLine(logMessage);
            writer.Close();
        }
    }
}