using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace partycli.Store.Formats
{
    public class Xmlformat : IFormat
    {
        /// <summary>
        /// Read Aplication data from file
        /// </summary>
        /// <param name="DataFileName"></param>
        /// <returns></returns>
        public dynamic Read(string DataFileName, Type dataType)
        {
            XmlSerializer xs = new XmlSerializer(dataType);
            if (File.Exists("data/" + DataFileName + ".xml"))
            {
                using (Stream stream = new FileStream("data/" + DataFileName + ".xml", FileMode.Open, FileAccess.Read))
                {
                    return xs.Deserialize(stream);
                }
            }
            return null;
        }

        /// <summary>
        /// Save aplication data to file
        /// </summary>
        /// <param name="DataFileName"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string Save(string DataFileName, dynamic data)
        {
            if (string.IsNullOrEmpty(DataFileName) || data == null )
            {
                return null;
            }

            XmlSerializer xs = new XmlSerializer(data.GetType());
            using (StreamWriter sw = new StreamWriter("data/" + DataFileName + ".xml"))
            {
                xs.Serialize(sw, data);
                return ((FileStream)sw.BaseStream).Name;
            }
        }
    }
}
