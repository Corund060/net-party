using System;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;
using System.Configuration;
using partycli.Logger;
using partycli.Commands;
using partycli.Store.Formats;
using partycli.Store.StoreType;
using partycli.Store.Data;
using System.Linq;
using System.Threading.Tasks;
using Nito.AsyncEx;

/*
 * Aplication uses System.CommandLine librarie to implement command line commands and options.
 * Commands are defined in separate classes together with implementations of their handlers.
 * Options are defined in separate extension class. 
 * Options can be used for multiple commands by calling their methods in Create method of command class (exmpl: .RequiredUsername()).
 * Data management is realised throught IStore interface.
 * Dependency injection is used for reading and writing data of different formats (Xml, Json).
 * Aplication logging is realised throught ILogger interface.         
 */
namespace partycli
{    
    public static class Program
    {
        static void Main(string[] args)
        {
            // Using async comptible context from Nito.AsyncEx package
            // Not blocking on async tasks
            AsyncContext.Run(() => MainAsync(args));
        }

        public static async Task<int> MainAsync(string[] args)
        {             
            //Default data source.
            var dataFileName = ConfigurationManager.AppSettings["dataFilename"];

            // Default format of data storage (Xml, Json atm.)
            Type dataStoreFormat = typeof(Xmlformat);

            // Default data type
            Type dataType = typeof(AppData);

            // Initiate data manager class
            var store = new FileStore(dataFileName, dataStoreFormat, dataType);
            
            // Initiate Application logger
            var logger = new AppLogger("log.txt");

            // Reading data of default data type from default source.
            // If commands needs to operate with different data type and/or location,
            // store.Read() should be called at the begining of Command handler InvokeAsync method.
            var data = store.Read();
            if (data == null)
            {
                // Log warning and go on with code
                logger.Responses.Add(new LogResponse { 
                    Logger="Main",
                    MessageLogged=false,
                    ResponseMessage="Can't load/missing application data needed for at least some commands."
                });
            }

            logger.Write(MsgCategory.Info, "Application started.", console:false, file:true);

            // Building command line commands and parsing provided arguments
            try
            {
                RootCommand command = new RootCommand
                {
                    ServerListCommand.Create(),
                    ConfigCommand.Create(),
                    ExportCommand.Create(),
                    LoggerCommand.Create()
                };
                Parser parser = BuildParser(command, logger, store, data);
                await parser.InvokeAsync(args);                
            }
            catch (Exception ex)
            {
                logger.Write(MsgCategory.Error, "Application terminated: " + ex.Message, console: true, file:true);
                return 0;
            }

            // If app settings for warnings are enabled
            // Informing user about warnings before exit
            var appWarn = logger.Responses.Where(rs => rs.MessageLogged == false).Distinct().ToList();
            if (appWarn.Count > 0)
            {
                logger.Write(MsgCategory.Info, "Application completed with warnings:", console: true, file: false);
                foreach (var response in appWarn)
                {
                    logger.Write(MsgCategory.Warning, message: response.Logger+": "+response.ResponseMessage, console: true, file: true);
                }
                return 1;
            }
            
            logger.Write(MsgCategory.Info, "Application completed.", console: false, file: true);
            return 1;
        }

        /// <summary>
        /// Method for passing logger and storage management instances to command handlers
        /// </summary>
        /// <param name="command"></param>
        /// <param name="logger"></param>
        /// <param name="store"></param>
        /// <returns></returns>
        private static Parser BuildParser(RootCommand command, ILogger logger, IStore store, AppData data)
        {
            var commandLineBuilder = new CommandLineBuilder(command);
                       
            commandLineBuilder.UseMiddleware(ctx =>
            {
                ctx.BindingContext.AddService(p => store);
                ctx.BindingContext.AddService(p => logger );
                ctx.BindingContext.AddService(p=> data);
            });
            return commandLineBuilder.UseDefaults().Build();
        }
    }
}
