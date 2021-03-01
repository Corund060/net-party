using Microsoft.VisualStudio.TestTools.UnitTesting;
using partycli.API;
using System.Threading.Tasks;

namespace partycli_UnitTest.API
{
    [TestClass]
    public class Api_UnitTest
    {
        [TestMethod]
        public async Task GetToken_ProvidedTesonetData_ExpectedResultToken()
        {
            string server = "https://playground.tesonet.lt/v1/tokens";
            string username = "tesonet";
            string password = "partyanimal";
            var result =await GetTokenAsync(server, username, password);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetToken_Provided_Wrong_Server_ExpectedException()
        {
            string server = "https://wrong.url.com";
            string username = "tesonet";
            string password = "partyanimal";
            Assert.ThrowsExceptionAsync<System.Net.WebException>(() => GetTokenAsync(server, username, password));
        }

        [TestMethod]
        public void GetToken_Provided_Server_Wrong_Username_And_Password_ExpectedException()
        {
            string server = "https://playground.tesonet.lt/v1/tokens";
            string username = "wrong username";
            string password = "wrong password";
            Assert.ThrowsExceptionAsync<System.Net.WebException>(() => GetTokenAsync(server, username, password));
        }

        [TestMethod]
        public void GetToken_ProvidedWrongData_ExpectedException()
        {
            string server = "https://wrong.url.com";
            string username = "wrongusername";
            string password = "wrongpassword";            
            Assert.ThrowsExceptionAsync<System.Net.WebException>(() => GetTokenAsync(server, username, password));          
        }


        [TestMethod]
        public void GetServers_ProvidedTesonetToken_ExpectedResultServerList()
        {
            var token = GetTokenAsync("https://playground.tesonet.lt/v1/tokens", "tesonet", "partyanimal").Result;
            var server = "https://playground.tesonet.lt/v1/servers";
            var result = Task.Run(async () =>
            {
                return await Api.GetServersAsync(server, token);
            }).GetAwaiter().GetResult();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetServers_ProvidedWrongToken_ExpectedException()
        {
            var token = "wrong token";
            var server = "https://playground.tesonet.lt/v1/tokens";         
            Assert.ThrowsExceptionAsync<System.Net.WebException>(()=>Api.GetServersAsync(server, token));
        }

        private async static Task<string> GetTokenAsync(string serverAuth, string username, string password)
        {
            var token = await Api.GetTokenAsync(serverAuth, username, password);
            return token;
        }

    }
}
