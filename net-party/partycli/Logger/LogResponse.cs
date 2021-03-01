
namespace partycli.Logger
{
    /// <summary>
    /// Logger Write response object.
    /// </summary>
    public class LogResponse
    {
        /// <summary>
        /// Who is sending response ?
        /// </summary>
        public string Logger { get; set; }

        /// <summary>
        /// Was message logged succsessfully ?
        /// </summary>
        public bool MessageLogged { get; set; }

        /// <summary>
        /// Response additional data (file name, error message etc.)
        /// </summary>
        public object ResponseData { get; set; }

        /// <summary>
        /// Response message.
        /// </summary>
        public string ResponseMessage { get; set; }
    }
}
