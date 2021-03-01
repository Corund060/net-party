using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using partycli.Store.Data;

namespace partycli.API
{
    public class Api
    {
        /// <summary>
        /// Request apiTokenUrl with provided credentials for authorization token to data server.
        /// </summary>
        /// <param name="authServer">Authorisation server URL</param>
        /// <param name="username">Username for authorisation server</param>
        /// <param name="password">Password for authorisation server</param>
        /// <returns></returns>
        public static async Task<string> GetTokenAsync(string authServer, string username, string password)
        {
            var httpRequest = WebRequest.Create(authServer);
            httpRequest.Method = "POST";
            httpRequest.ContentType = "application/json";
            var post_data = "{\"username\":\"" + username + "\",\"password\":" + "\"" + password + "\"}";

            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(post_data);
            }
            var httpResponse = await httpRequest.GetResponseAsync();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return result.Substring(10, result.Length - 12);
            }             
        }

        
        /// <summary>
        /// Get list of servers from data server.
        /// </summary>
        /// <param name="server">Data server URL.</param>
        /// <param name="token">Token for accessing data server.</param>
        /// <returns></returns>
        public static async Task<List<Server>> GetServersAsync(string server, string token)
        {
            var httpRequest = WebRequest.Create(server);
            httpRequest.Method = "GET";
            httpRequest.ContentType = "application/json";
            httpRequest.Headers.Add("Authorization: Bearer " + token);

            var httpResponse =await httpRequest.GetResponseAsync();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Server>>(result);
            }            
        }
    }
}
