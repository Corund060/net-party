using Microsoft.VisualStudio.TestTools.UnitTesting;
using partycli.Commands;
using partycli.Logger;
using partycli.Store.Data;
using partycli.Store.Formats;
using partycli.Store.StoreType;
using System.CommandLine.IO;
using System.IO;
using System.Threading.Tasks;

namespace partycli_UnitTest.Commands
{
    [TestClass]
    public class ExportHandler_UnitTest
    {
        private AppLogger _logger;

        private IStore _store;

        private AppData _data;
        public ExportHandler_UnitTest()
        {
            _logger = new AppLogger("log.txt");
            _store = new FileStore("test_data", typeof(Xmlformat), typeof(AppData));
            _data = _store.Read();
        }

        [TestMethod]
        public async Task InvokeAsync_Data_NULL_Expected_Result_0()
        {
            TestConsole console = new TestConsole();
            string fileName = "test_export";
            Dataformat format = Dataformat.Json;
            _data = null;

            ExportCommand command = new ExportCommand(fileName, format, _store, _logger, _data);

            var result=await command.InvAsync(console);
            Assert.AreEqual(0, result);         
        }

        [TestMethod]
        public async Task InvokeAsync_fileName_NULL_Expected_Result_0()
        {
            TestConsole console = new TestConsole();
            Dataformat format = Dataformat.Json;
            string fileName = null;

            ExportCommand command = new ExportCommand(fileName, format, _store, _logger, _data);

            var result = await command.InvAsync(console);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public async Task InvokeAsync_fileName_format_Expected_Result_1_and_File()
        {
            TestConsole console = new TestConsole();
            Dataformat format = Dataformat.Json;
            string fileName = "test_export";

            ExportCommand command = new ExportCommand(fileName, format, _store, _logger, _data);

            var result = await command.InvAsync(console);
            Assert.AreEqual(1, result);
            Assert.IsTrue(File.Exists("data/"+fileName+".json"));
            File.Delete("data/" + fileName + ".json");
        }
    }
}
