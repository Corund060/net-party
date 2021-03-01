using Microsoft.VisualStudio.TestTools.UnitTesting;
using partycli.Store.Data;
using partycli.Store.Formats;
using partycli.Store.StoreType;
using System;
using System.IO;

namespace partycli_UnitTest.Store.Formats
{
    [TestClass]
    public class Jsonformat_UnitTest
    {
        private string _fileName="test_data";

        private Type _dataFormat = typeof(Jsonformat);

        private Type _dataType = typeof(AppData);

        private IStore _store;

        public Jsonformat_UnitTest()
        {
            _store = new FileStore(_fileName, _dataFormat, _dataType);
        }

       [TestMethod]
        public void Read_Provided_Filename_Expected_Data()
        {
            Jsonformat format = new Jsonformat();
            Type dataType = typeof(AppData);

            var result=format.Read(_fileName, dataType);
            Assert.IsInstanceOfType(result, typeof(AppData));
        }

        [TestMethod]
        public void Read_Provided_Filename_NULL_Expected_NULL()
        {
            string filename = null;
            Jsonformat format = new Jsonformat();
            Type dataType = typeof(AppData);

            var result = format.Read(filename, dataType);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Read_Provided_Filename_Data_Expected_File_Path()
        {
            string filename = "testfile";
            AppData data = _store.Read();
            data.User = new User
            {
                UserName = "test name",
                Password = "test password"
            };
            Jsonformat format = new Jsonformat();

            var result = format.Save(filename,  data);
            var fileExists = File.Exists(result);
            Assert.IsTrue(fileExists);
            File.Delete(result);
        }

        [TestMethod]
        public void Read_Provided_Filename_NULL_Data_Expected_NULL()
        {
            string filename = null;
            FileStore store = new FileStore(_fileName, _dataFormat, _dataType);
            AppData data = store.Read();
            data.User = new User
            {
                UserName = "test name",
                Password = "test password"
            };
            Jsonformat format = new Jsonformat();

            var result = format.Save(filename, data);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Read_Provided_Filename_Data_NULL_Expected_NULL()
        {
            string filename = "testfile";
            AppData data = null;
            Jsonformat format = new Jsonformat();

            var result = format.Save(filename, data);
            Assert.IsNull(result);
        }
      

    }
}
