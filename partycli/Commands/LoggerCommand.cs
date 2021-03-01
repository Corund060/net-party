using System.CommandLine;
using System.CommandLine.Invocation;
using System.Threading.Tasks;
using partycli.Logger;
using partycli.Store.Data;
using partycli.Store.StoreType;

namespace partycli.Commands
{
    /// <summary>
    /// Class for implementation of 'log_warnings' command and its handler.
    /// </summary>
    public class LoggerCommand:ICommandHandler
    {
        private IStore _store;
        private ILogger _logger;
        private State _warning;
        private dynamic _data;

        public static Command Create()
        {
            var command= new Command("log_warnings", "Turn logger warnings on/off.")
            {
                new Argument<State>("warning", null)                
            };
            command.Handler = CommandHandler.Create(typeof(LoggerCommand).GetMethod(nameof(ICommandHandler.InvokeAsync)));
            return command;
        }

        public LoggerCommand(State warning, IStore store, ILogger logger, AppData data)
        {
            _store = store;
            _logger = logger;
            _warning = warning;
            _data = data;
        }

        public Task<int> InvokeAsync(InvocationContext context)
            => InvAsync(context.Console);

        /// <summary>
        /// Handler for 'log_warnings' command.
        /// </summary>
        /// <param name="console"></param>
        /// <returns></returns>
        public async Task<int> InvAsync(IConsole console)
        {                      
            if (_data.LoggerWarnings!=_warning)
            {
                _data.LoggerWarnings = _warning;                
                await _store.WriteAsync(_data);                
                _logger.Write(MsgCategory.Info, message: "Local data store was updated.", console: true, file: true);
                return 1;
            }
            _logger.Write(MsgCategory.Info, message: "Warnings are already "+_warning+".", console: true, file: true);
            return 0;
        }
    }
}
