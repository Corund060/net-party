
using System.Threading.Tasks;

namespace partycli.Logger
{
    /// <summary>
    /// Output (log/console) message category
    /// </summary>
    public enum MsgCategory
    {
        Undefined,
        Info,
        Error,
        Warning
    }

    public interface ILogger
    {
        /// <summary>
        /// Write message of provided category to chosen outputs.
        /// </summary>
        /// <param name="category">Category of message of MsgCategory type.</param>
        /// <param name="message">Message to be logged.</param>
        /// <param name="console">True - if logged to console window.</param>
        /// <param name="file">True - if logged to external text file.</param>
        void Write(MsgCategory category, string message, bool console, bool file);                 
    }
}
