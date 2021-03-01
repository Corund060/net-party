using System.Collections.Generic;

namespace partycli.Logger
{
    /// <summary>
    /// Class responsible for logging implementation.
    /// </summary>
    public class AppLogger : ILogger
    {
        public List<LogResponse> Responses;

        /// <summary>
        /// File name for FileLogger.
        /// </summary>
        private string _fileName;

        public AppLogger(string fileName)
        {
            Responses = new List<LogResponse>();
            _fileName = fileName;
        }
        
        public void Write(MsgCategory category, string message, bool console, bool file)
        {            
            if (console)
            {
                var consoleLogResponse=ConsoleLogger.Write(category, message);
                Responses.Add(consoleLogResponse);
            }
            if (file)
            {
                var fileLogResponse=FileLogger.Write(_fileName, category, message);
                Responses.Add(fileLogResponse);
            }            
        }       
    }
}
