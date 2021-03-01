using System;
using System.IO;


namespace partycli.Logger
{
    public class FileLogger
    {  
        public static LogResponse Write(string fileName, MsgCategory category, string message)
        {
            // Don't log if no message provided
            if (String.IsNullOrEmpty(message))
            {
                return new LogResponse {
                              Logger="FileLogger",
                              MessageLogged = false,
                              ResponseMessage = "No message provided.",
                };
            }            

            try
            {
                using (StreamWriter tw = new StreamWriter("data/"+fileName, append: true))
                {
                    string line = DateTime.Now.ToString() + " : " + category.ToString() + " : " + message;
                    tw.WriteLine(line);
                    return new LogResponse
                    {
                        Logger = "FileLogger",
                        MessageLogged = true,
                        ResponseData = tw.BaseStream.ToString()
                    };                                        
                }
            }
            catch (Exception ex)
            {
                return new LogResponse
                {
                    Logger = "FileLogger",
                    MessageLogged = false,
                    ResponseMessage = "Could not write message to file.",
                    ResponseData = ex
                };                
            }

        }    
    }
}
