using System;
using System.Threading.Tasks;

namespace partycli.Store.Formats
{
    public interface IFormat
    {
        string Save(string DataFileName, dynamic data);

        dynamic Read(string DataFileName, Type dataType);
    }
}
