using System.CommandLine;
using System.CommandLine.Invocation;
using System.Threading.Tasks;
using partycli.Store.StoreType;
using partycli.Logger;
using partycli.Store.Data;

namespace partycli.Commands
{
    /// <summary>
    /// Class for implementation of 'config' command and its handler.
    /// </summary>
    public class ConfigCommand:ICommandHandler
    {
        private string _username;
        private string _password;
        private IStore _store;
        private ILogger _logger;
        private dynamic _data;
        
        public static Command Create()
        {
            return new Command("config", "Store user credentials in the persistent data store.")
            {
                Handler = CommandHandler.Create(typeof(ConfigCommand).GetMethod(nameof(ICommandHandler.InvokeAsync)))
            }
            .RequiredUserName()
            .RequiredPassword();
        }

        public ConfigCommand(string userName, string password, IStore store, ILogger logger, AppData data)
        {
            _username = userName;
            _password = password;
            _store = store;
            _logger = logger;
            _data = data;
        }

        public Task<int> InvokeAsync(InvocationContext context)
            => InvAsync(context.Console);

        /// <summary>
        /// Handler for 'config' command.
        /// </summary>
        /// <param name="console"></param>
        /// <returns></returns>
        public async Task<int> InvAsync(IConsole console)
        {            
            if (_data == null || _data.User == null)
            {
                _logger.Write(MsgCategory.Error, message: "No data file or wrong data format.", console: true, file: true);
                return 0;
            }
            
            _data.User.UserName = _username;
            _data.User.Password = _password;
            await _store.WriteAsync(_data);
            _logger.Write(MsgCategory.Info, message: "Local data store was updated.", console:true, file: true);
            _logger.Write(MsgCategory.Info, message: "'config' command completed successfully.", console: true, file: false);                                                  
            return 1;
        }
    }
}
