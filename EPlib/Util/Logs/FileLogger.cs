using EPlib.Util.Interfaces;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPlib.Util.Logs
{
    public class FileLogger : ILogger
    {
        private readonly string _writePath;

        public enum LogType
        {
            ERROR,
            INFO,
            GENERAL
        }

        public FileLogger(string path)
        {
            _writePath = path;
        }

        public void LogError(string message)
        {
            Log(LogType.ERROR, message);
        }

        public void LogInfo(string message)
        {
            Log(LogType.INFO, message);
        }

        public void LogGeneral(string message)
        {
            Log(LogType.GENERAL, message);
        }

        private void Log(LogType type, string message)
        {
            using (StreamWriter sw = new StreamWriter(_writePath, true))
            {
                StringBuilder writeMessage = new StringBuilder();

                writeMessage.Append(DateTime.Now);

                if(type != LogType.GENERAL)
                {
                    writeMessage.Append(" [" + type.ToString() + "]" + " : " + message);
                }
                else
                {
                    writeMessage.Append(message);
                }

                sw.WriteLine(writeMessage.ToString());

            }
        }
    }
}
