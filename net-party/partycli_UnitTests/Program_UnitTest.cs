using Microsoft.VisualStudio.TestTools.UnitTesting;
using partycli;
using System.Threading.Tasks;

namespace partycli_UnitTests
{
    [TestClass]
    public class Program_UnitTest
    {
        [TestMethod]
        public async Task Program_Provided_Expected()
        {
            string[] args = new string[] {"server_list"};
            var result = await Program.MainAsync(args);

            Assert.AreEqual(1, result);
        }
    }
}
