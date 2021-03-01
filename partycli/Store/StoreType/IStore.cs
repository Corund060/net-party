using System;
using System.Threading.Tasks;

namespace partycli.Store.StoreType
{
    public interface IStore
    {
        /// <summary>
        /// Read method for custom data type.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="dataType"></param>
        /// <returns></returns>
        dynamic Read(string fileName, Type dataType);

        /// <summary>
        /// Read method for default (Xml) data type.
        /// </summary>
        /// <returns></returns>
        dynamic Read();

        /// <summary>
        /// Write method for custom data type.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="dataType"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        string Write(string fileName, Type dataType, dynamic data);

        /// <summary>
        /// Asynchronous write method for custom data type.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="dataType"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<string> WriteAsync(string fileName, Type dataType, dynamic data);

        /// <summary>
        /// Write method for default (Xml) data store.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        string Write(dynamic data);

        /// <summary>
        /// Asynchronous write method for default (Xml) data store.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<string> WriteAsync(dynamic data);
    }
}
