using Microsoft.VisualStudio.TestTools.UnitTesting;
using partycli.Logger;
using partycli.Store.Data;
using partycli.Store.Formats;
using partycli.Store.StoreType;
using partycli.Commands;
using System.CommandLine.IO;
using System.Threading.Tasks;

namespace partycli_UnitTest.Commands
{
    [TestClass]
    public class ServerListHandler_UnitTest
    {
        private AppLogger _logger;

        private IStore _store;

        private AppData _data;
        public ServerListHandler_UnitTest()
        {
            _logger = new AppLogger("log.txt");
            _store = new FileStore("test_data", typeof(Xmlformat), typeof(AppData));
            _data = _store.Read();
        }

        [TestMethod]
        public async Task InvAsync_DataNULL_Expected_Result_0()
        {
            _logger.Responses.Clear();
            TestConsole console = new TestConsole();
            bool local = true;
            _data = null;

            ServerListCommand command = new ServerListCommand(local, _store, _logger, _data);

            var result=await command.InvAsync(console);

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public async Task InvAsync_Local_True_Expected_Result_1()
        {
            _logger.Responses.Clear();
            TestConsole console = new TestConsole();
            bool local = true;

            ServerListCommand command = new ServerListCommand(local, _store, _logger, _data);

            var result = await command.InvAsync(console);

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public async Task InvAsync_Local_False_Expected_Result_1_AND_Servers_In_DataStore()
        {
            _logger.Responses.Clear();
            TestConsole console = new TestConsole();
            bool local = false;
            _data.Servers.Clear();
            _store.Write(_data);

            ServerListCommand command = new ServerListCommand(local, _store, _logger, _data);

            var result = await command.InvAsync(console);
            _data = _store.Read();

            Assert.AreEqual(1, result);
            Assert.IsTrue(_data.Servers.Count>0);
        }

        [TestMethod]
        public async Task InvAsync_Local_False_And_AuthServer_Wrong_Expected_Result_0()
        {
            _logger.Responses.Clear();
            TestConsole console = new TestConsole();
            bool local = false;
            _data.AuthServer = "wrongserver";

            ServerListCommand command = new ServerListCommand(local, _store, _logger, _data);

            var result = await command.InvAsync(console);

            Assert.AreEqual(0, result);           
        }

        [TestMethod]
        public async Task InvAsync_Local_False_And_AuthServer_Username_Wrong_Expected_Result_0()
        {
            _logger.Responses.Clear();
            TestConsole console = new TestConsole();
            bool local = false;
            _data.User.UserName = "wrongusername";

            ServerListCommand command = new ServerListCommand(local, _store, _logger, _data);

            var result = await command.InvAsync(console);

            Assert.AreEqual(0, result);
        }
    }
}
