using Newtonsoft.Json;
using System;
using System.IO;

namespace partycli.Store.Formats
{ 
    public class Jsonformat : IFormat
    {
        /// <summary>
        /// Read application data from file.
        /// </summary>
        /// <param name="DataFileName"></param>
        /// <returns></returns>
        public dynamic Read(string DataFileName, Type dataType)
        {
            if (File.Exists("data/" + DataFileName + ".json"))
            {
                using (StreamReader r = new StreamReader("data/" + DataFileName + ".json"))
                {
                    string json = r.ReadToEnd();
                    return JsonConvert.DeserializeObject(json, dataType);
                }
            }
            return null;
        }

        /// <summary>
        /// Save application data to file
        /// </summary>
        /// <param name="DataFileName"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string Save(string DataFileName, dynamic data)
        {
            if (string.IsNullOrEmpty(DataFileName) || data==null)
            {
                return null;
            }
           
            using (StreamWriter file = File.CreateText("data/" + DataFileName + ".json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, data);
                return ((FileStream)file.BaseStream).Name;
            }                                
        }
    }
}
