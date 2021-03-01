using System.Collections.Generic;
using partycli.Commands;

namespace partycli.Store.Data
{
    /// <summary>
    /// Serializable aplication data.
    /// </summary>   
    public class AppData
    {
        /// <summary>
        /// User credentials for authorisation server.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Authorisation server URL.
        /// </summary>
        public string AuthServer { get; set; }

        /// <summary>
        /// Data server URL.
        /// </summary>
        public string DataServer { get; set; }

        /// <summary>
        /// Enabled/Disabled warnings in console window.
        /// </summary>
        public State LoggerWarnings { get; set; }

        /// <summary>
        /// List of servers downloaded from data server.
        /// </summary>
        public List<Server> Servers { get; set; }    
    }
}
