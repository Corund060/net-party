using System.CommandLine;
using System.CommandLine.Invocation;
using System.Threading.Tasks;
using partycli.Store.StoreType;
using partycli.Logger;
using System;
using partycli.Store.Data;
using partycli.API;

namespace partycli.Commands
{
    /// <summary>
    /// Class for implementation of 'server_list' command and its handler.
    /// </summary>
    public class ServerListCommand:ICommandHandler
    {
        private bool _local;

        private IStore _store;

        private ILogger _logger;

        private dynamic _data;

        public ServerListCommand(bool local, IStore store, ILogger logger, AppData data)
        {
            _local = local;
            _store = store;
            _logger = logger;
            _data = data;
        }

        public static Command Create()
        {
            return new Command("server_list", "Fetch servers from API, store them in persistent data store and display server names and total number of servers in the console.")
            {
                Handler = CommandHandler.Create(typeof(ServerListCommand).GetMethod(nameof(ICommandHandler.InvokeAsync)))
            }.OptionalLocal();
        }

        public Task<int> InvokeAsync(InvocationContext context)
            => InvAsync(context.Console);

        /// <summary>
        /// Handler for 'server_list' command.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<int> InvAsync(IConsole context)
        {          
            if (_data == null)
            {
                _logger.Write(MsgCategory.Error, message: "Error: No data file found.", console:true, file: true);
                return 0;
            }
            if (!_local)
            {
                try
                {
                    _logger.Write(MsgCategory.Info, message: "Fetching data from remote server.", console: true, file: true);
                    string token = await Api.GetTokenAsync(_data.AuthServer, _data.User.UserName, _data.User.Password);                    
                    var servers = await Api.GetServersAsync(_data.DataServer, token);
                    _data.Servers = servers;
                    _store.Write(_data);
                    _logger.Write(MsgCategory.Info, message: "Updated servers in local data store.", console: true, file: true);
                }
                catch (Exception ex)
                {
                    _logger.Write(MsgCategory.Error, message: "Connection error: "+ex.Message, console: true, file: true);
                    return 0;
                }               
            }

            // Outputing servers from local data store to console window

            _logger.Write(MsgCategory.Info, message: "Listing servers from local data store:", console:true, file:false);
            foreach (var server in _data.Servers)
            {
                _logger.Write(MsgCategory.Undefined, server.Name, console:true, file:false);
            }
            _logger.Write(MsgCategory.Info, message: "There are a total of " + _data.Servers.Count + " servers in the list.", console:true, file:false);
            _logger.Write(MsgCategory.Info, message: "'server_list' command completed successfully.", console:true, file: false);
            return 1;
        }       
        
    }
}
