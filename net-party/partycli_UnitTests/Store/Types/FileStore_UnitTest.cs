using Microsoft.VisualStudio.TestTools.UnitTesting;
using partycli.Store.Data;
using partycli.Store.Formats;
using partycli.Store.StoreType;
using System.IO;

namespace partycli_UnitTest.Store.Types
{

    [TestClass]
    public class FileStore_UnitTest
    {
        [TestMethod]
        public void Read_Provided_FileName_DataType_Expected_Result_ofDataType()
        {
            FileStore _store = new FileStore("test_file", typeof(Xmlformat), typeof(AppData));
            
            AppData result = _store.Read("test_data", typeof(AppData));

            Assert.IsInstanceOfType(result, typeof(AppData));
        }

        [TestMethod]
        public void Write_Provided_FileName_DataType_Data_Expected_File()
        {
            FileStore _store = new FileStore("test_file", typeof(Xmlformat), typeof(AppData));
            string fileName = "write_test";
            AppData data = new AppData();

            string result = _store.Write(fileName, typeof(Jsonformat), data);

            Assert.IsTrue(File.Exists(result));
            File.Delete(result);
        }
    }
}
