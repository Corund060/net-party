using System.CommandLine;
using partycli.Store.Formats;

namespace partycli.Commands
{
    public enum State
    {
       disabled,
       enabled       
    }

    /// <summary>
    /// Extensions for System.CommandLine Command class used to add Opions.
    /// </summary>
    internal static class CommandExtension
    {
        /// <summary>
        /// Optional --local option for commands
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        internal static Command OptionalLocal(this Command command)
            => command.Local("Get locally stored server list.", isRequired: false);

        private static Command Local(this Command command, string description, bool isRequired)
        {
            command.Add(new Option(new[] { "--local" }, description)
            {
                IsRequired = isRequired
            });
            return command;
        }                

        /// <summary>
        /// Required --user-name option for commands
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        internal static Command RequiredUserName(this Command command)
            => command.UserName("Set user name.", isRequired: true);

        private static Command UserName(this Command command, string description, bool isRequired)
        {
            command.Add(new Option<string>(new[] { "--username" }, description)
            {
                IsRequired = isRequired,                
            });
            return command;
        }               

        /// <summary>
        /// Required --password option for commands
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        internal static Command RequiredPassword(this Command command)
            => command.Password("Set user password.", isRequired: true);

        private static Command Password(this Command command, string description, bool isRequired)
        {
            command.Add(new Option<string>(new[] { "--password" }, description)
            {
                IsRequired = isRequired
            });
            return command;
        }

        /// <summary>
        /// Required --filename option for commands 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        internal static Command RequiredFileName(this Command command)
            => command.FileName("Set file name for exported data.", isRequired: true);       

        private static Command FileName(this Command command, string description, bool isRequired)
        {
            command.Add(new Option<string>(new[] { "--filename" }, description)
            {
                IsRequired = isRequired
            });
            return command;
        }

        /// <summary>
        /// Required --format option fro commands
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        internal static Command RequiredFormat(this Command command)
            => command.Format("Choose data format.", isRequired: true);

        private static Command Format(this Command command, string description, bool isRequired)
        {
            command.Add(new Option<Dataformat>(new[] { "--format" }, description)
            {
                IsRequired = isRequired
            });
            return command;
        }
    }
}
