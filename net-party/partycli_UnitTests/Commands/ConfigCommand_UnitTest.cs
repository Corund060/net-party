using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using partycli.Logger;
using partycli.Commands;
using partycli.Store.StoreType;
using partycli.Store.Data;
using partycli.Store.Formats;
using System.Threading.Tasks;
using System.CommandLine.IO;

namespace partycli_UnitTest.Commands
{
    [TestClass]
    public class ConfigHandler_UnitTest
    {
        private AppLogger _logger;

        private IStore _store;

        private AppData _data;
        public ConfigHandler_UnitTest()
        {
            _logger = new AppLogger("log.txt");
            _store = new FileStore("test_data", typeof(Xmlformat), typeof(AppData));
            _data = _store.Read();
        }

        [TestMethod]
        public async Task InvokeAsync_Provided_Username_Password_Expected_Updated_DataFile()
        {
            _logger.Responses.Clear();
            TestConsole console = new TestConsole();

            const string str = "randomstringrandomstring";
            Random r = new Random();
            string username = new string(str.ToCharArray().OrderBy(s => (r.Next(2) % 2) == 0).ToArray());                       
            string password = new string(str.ToCharArray().OrderBy(s => (r.Next(3) % 2) == 0).ToArray());
            ConfigCommand command = new ConfigCommand(username, password, _store, _logger, _data);
            
            await command.InvAsync(console);

            AppData resultData = _store.Read();
            Assert.IsTrue(resultData.User.UserName==username && resultData.User.Password==password);
            _data.User.UserName = "tesonet";
            _data.User.Password = "partyanimal";
            _store.Write(_data);
        }

        [TestMethod]
        public async Task InvokeAsync_Provided_Username_Password_Data_NULL_Expected_Result_0()
        {
            _logger.Responses.Clear();
            TestConsole console = new TestConsole();

            const string str = "randomstringrandomstring";
            Random r = new Random();
            string username = null;
            string password = new string(str.ToCharArray().OrderBy(s => (r.Next(3) % 2) == 0).ToArray());
            _data = null;
            ConfigCommand command = new ConfigCommand(username, password, _store, _logger, _data);

            int result=await command.InvAsync(console);
            
            Assert.IsTrue(result==0);
        }

        [TestMethod]
        public async Task InvokeAsync_Provided_Username_Password_Data_User_NULL_Expected_Result_0()
        {
            _logger.Responses.Clear();
            TestConsole console = new TestConsole();

            const string str = "randomstringrandomstring";
            Random r = new Random();
            string username = null;
            string password = new string(str.ToCharArray().OrderBy(s => (r.Next(3) % 2) == 0).ToArray());
            _data.User = null;
            ConfigCommand command = new ConfigCommand(username, password, _store, _logger, _data);

            int result = await command.InvAsync(console);

            Assert.IsTrue(result == 0);
        }
    }
}
