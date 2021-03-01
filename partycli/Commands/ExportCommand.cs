using System.CommandLine;
using System.CommandLine.Invocation;
using System.Threading.Tasks;
using partycli.Store.Formats;
using partycli.Store.StoreType;
using partycli.Logger;
using partycli.Store.Data;

namespace partycli.Commands
{
    /// <summary>
    /// Class for implementation of 'export' command and its handler.
    /// </summary>
    public class ExportCommand : ICommandHandler
    {
        private string _filename;
        private Dataformat _format;
        private IStore _store;
        private ILogger _logger;
        private dynamic _data;

        public ExportCommand(string fileName, Dataformat format, IStore store, ILogger logger, AppData data)
        {
            _filename = fileName;
            _format = format;
            _store = store;
            _logger = logger;
            _data = data;
        }

        public static Command Create()
        {
            return new Command("export", "Export application data to file of chosen data format.")
            {
                Handler = CommandHandler.Create(typeof(ExportCommand).GetMethod(nameof(ICommandHandler.InvokeAsync)))
            }
            .RequiredFileName()
            .RequiredFormat();
        }

        public Task<int> InvokeAsync(InvocationContext context)
            => InvAsync(context.Console);

        /// <summary>
        /// Handler for 'export' command.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<int> InvAsync(IConsole context)
        {                     
            if (_data == null)
            {
                _logger.Write(MsgCategory.Error, message: "Error: No data file found.", console:true, file:true);
                return 0;
            }
            if (_filename == null)
            {
                _logger.Write(MsgCategory.Error, message: "Missing option.", console: true, file: true);
                return 0;
            }
            await _store.WriteAsync(_filename, Format.FormatTypeByName[_format], _data);
            _logger.Write(MsgCategory.Info, message: "'export' command completed successfully.", console: false, file: true);
            return 1;
        }

       
    }
}
