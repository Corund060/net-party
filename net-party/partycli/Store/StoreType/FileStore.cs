using System;
using System.Threading.Tasks;
using partycli.Store.Formats;
using Unity;

namespace partycli.Store.StoreType
{
    public class FileStore : IStore
    {
        /// <summary>
        /// Dependency injection container for file format.
        /// </summary>
        private UnityContainer _container;

        private string _dataFileName;

        /// <summary>
        /// Type of data stored in data file.
        /// </summary>
        private Type _dataType;

        /// <summary>
        /// Format of data file.
        /// </summary>
        public Format _dataFormat { get; set; }

        public FileStore(string fileName, Type dataFormat, Type dataType)
        {
            _container = new UnityContainer();
            _dataFileName = fileName;
            _dataType = dataType;

            _container.RegisterType(typeof(IFormat), dataFormat);
            _dataFormat = _container.Resolve<Format>();
        }       

        public dynamic Read(string dataFile, Type customType)
        {
            _dataFileName = dataFile;            
            return _dataFormat.Read(dataFile, customType);
        }

        public dynamic Read()
        {
            return _dataFormat.Read(_dataFileName, _dataType);
        }


        public string Write(string fileName, Type dataFormat, dynamic data )
        {
            _container.RegisterType(typeof(IFormat), dataFormat);
            Format export = _container.Resolve<Format>();
            return export.Save(fileName, data);           
        }

        public async Task<string> WriteAsync(string fileName, Type dataType, dynamic data)
        {
            return await Task<string>.Run(() => {
                    _container.RegisterType(typeof(IFormat), dataType);
                    Format export = _container.Resolve<Format>();
                    return export.Save(fileName, data);
            });
        }

        public async Task<string> WriteAsync(dynamic data)
        {            
            return await Task<string>.Run(() => {                
                return _dataFormat.Save(_dataFileName, data);
            });
        }

        public string Write(dynamic data)
        {
            return _dataFormat.Save(_dataFileName, data);
        }
    }
}
