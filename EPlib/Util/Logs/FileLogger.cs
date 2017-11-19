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

        /// <summary>
        /// Logs an error
        /// </summary>
        /// <param name="message"></param>
        public void LogError(string message)
        {
            Log(LogType.ERROR, message);
        }

        /// <summary>
        /// Logs info
        /// </summary>
        /// <param name="message"></param>
        public void LogInfo(string message)
        {
            Log(LogType.INFO, message);
        }

        /// <summary>
        /// Logs general information
        /// </summary>
        /// <param name="message"></param>
        public void LogGeneral(string message)
        {
            Log(LogType.GENERAL, message);
        }

        /// <summary>
        /// Writes to the log file
        /// </summary>
        /// <param name="type"></param>
        /// <param name="message"></param>
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
