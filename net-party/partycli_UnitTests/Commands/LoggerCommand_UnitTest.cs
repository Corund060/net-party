using Microsoft.VisualStudio.TestTools.UnitTesting;
using partycli.Commands;
using partycli.Logger;
using partycli.Store.Data;
using partycli.Store.Formats;
using partycli.Store.StoreType;
using System.CommandLine.IO;
using System.Threading.Tasks;

namespace partycli_UnitTests.Commands
{
    [TestClass]
    public class LoggerCommand_UnitTest
    {
        private AppLogger _logger;

        private IStore _store;

        private AppData _data;
        public LoggerCommand_UnitTest()
        {
            _logger = new AppLogger("log.txt");
            _store = new FileStore("test_data", typeof(Xmlformat), typeof(AppData));
        }

        [TestMethod]
        public async Task InvAsync_Warning_Enabled_And_Warnings_in_Data_Disabled_Expected_ChangedData_And_Result_1()
        {
            TestConsole console = new TestConsole();
            State warning = State.enabled;
            _data = _store.Read();
            _data.LoggerWarnings=State.disabled;
            _store.Write(_data);            

            LoggerCommand command = new LoggerCommand(warning, _store, _logger, _data);

            var result = await command.InvAsync(console);
            _data = _store.Read();

            Assert.AreEqual(1, result);
            Assert.IsTrue(_data.LoggerWarnings==State.enabled);

        }

        [TestMethod]
        public async Task InvAsync_Warning_Enabled_And_Warnings_in_Data_Enabled_Expected_Result_0()
        {
            TestConsole console = new TestConsole();
            State warning = State.enabled;
            _data = _store.Read();
            _data.LoggerWarnings = State.enabled;
            _store.Write(_data);            

            LoggerCommand command = new LoggerCommand(warning, _store, _logger, _data);

            var result = await command.InvAsync(console);
            _data = _store.Read();

            Assert.AreEqual(0, result);
        }
    }
}
