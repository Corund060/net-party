using Microsoft.VisualStudio.TestTools.UnitTesting;
using partycli.Logger;
using System.Linq;

namespace partycli_UnitTest.Logger
{
    [TestClass]
    public class AppLogger_UnitTest
    {
        private AppLogger _logger=new AppLogger("log.txt");

        [TestMethod]
        public void Write_Provided_Category_And_Message_ConsoleFalse_FileTrue_Expected_ResponseMessageLogged_True()
        {
            _logger.Responses.Clear();
            MsgCategory category = MsgCategory.Info;            
            string message = "test message";
            bool console = false;
            bool file = true;

            _logger.Write(category, message, console, file);

            Assert.IsTrue(_logger.Responses.FirstOrDefault().MessageLogged);
            Assert.IsTrue(_logger.Responses.FirstOrDefault().Logger.Contains("FileLogger"));
        }

        [TestMethod]
        public void Write_Provided_Category_And_Message_ConsoleTrue_FileFalse_Expected_ResponseMessageLogged_True()
        {
            _logger.Responses.Clear();
            MsgCategory category = MsgCategory.Info;            
            string message = "test message";
            bool console = true;
            bool file = false;

            _logger.Write(category, message, console, file);

            Assert.IsTrue(_logger.Responses.FirstOrDefault().MessageLogged);
            Assert.IsTrue(_logger.Responses.FirstOrDefault().Logger.Contains("ConsoleLogger"));
        }        

        [TestMethod]
        public void Write_Provided_Category_And_Message_NULL_Expected_False()
        {
            _logger.Responses.Clear();
            MsgCategory category = MsgCategory.Info;            
            string message = null;
            bool console = false;
            bool file = true;

            _logger.Write(category, message, console, file);

            Assert.IsFalse(_logger.Responses.FirstOrDefault().MessageLogged);
            Assert.IsTrue(_logger.Responses.FirstOrDefault().ResponseMessage=="No message provided.");
        }

        [TestMethod]
        public void Write_Provided_Category_And_Message_NULL_And_Log_Expected_ResponseMessageLogged_False()
        {
            _logger.Responses.Clear();
            MsgCategory category = MsgCategory.Info;            
            string message = null;           
            bool console = true;
            bool file = true;

            _logger.Write(category, message, console, file);

            Assert.IsFalse(_logger.Responses.FirstOrDefault().MessageLogged);
        }

        [TestMethod]
        public void Write_Provided_Category_And_Message_ConsoleAndFileFalse_Expected_NoResponseMessages()
        {
            _logger.Responses.Clear();
            MsgCategory category = MsgCategory.Info;            
            string message = "test message";
            bool console = false;
            bool file = false;

            _logger.Write(category, message, console, file);

            Assert.IsTrue(_logger.Responses.Count==0);
        }

        [TestMethod]
        public void Write_Provided_Category_And_Message_File_True_LoggerFileName_INVALID_Expected_ErrorResponseMessage()
        {
            _logger = new AppLogger("G://@");
            MsgCategory category = MsgCategory.Info;
            string message = "test message";
            bool console = false;
            bool file = true;

            _logger.Write(category, message, console, file);

            Assert.IsTrue(_logger.Responses.FirstOrDefault().ResponseMessage.Contains("Could not write message to file."));
        }

    }
}
