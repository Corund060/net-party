using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace partycli.Logger
{
    public class ConsoleLogger
    {
        /// <summary>
        /// Color of text in console window depending on message category
        /// </summary>
        private static readonly Dictionary<MsgCategory, ConsoleColor> MsgColorByCategory = new Dictionary<MsgCategory, ConsoleColor>
        {
            { MsgCategory.Undefined,    ConsoleColor.White},
            { MsgCategory.Error,          ConsoleColor.Red},
            { MsgCategory.Info,         ConsoleColor.Green},
            { MsgCategory.Warning, ConsoleColor.DarkYellow}
        };

        /// <summary>
        /// Write message to console 
        /// </summary>
        /// <param name="category"></param>
        /// <param name="message"></param>
        public static LogResponse Write(MsgCategory category, string message)
        { 
            if (!String.IsNullOrEmpty(message))
            {
                Console.ForegroundColor = MsgColorByCategory[category];
                Console.WriteLine(message);
                Console.ResetColor();
                return new LogResponse
                {
                    Logger = "ConsoleLogger",
                    MessageLogged = true
                };               
            }
            return new LogResponse
            {
                Logger = "ConsoleLogger",
                MessageLogged = false,
                ResponseMessage = "No message provided.",
            };
        }
    }
}
