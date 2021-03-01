using System;
using System.Collections.Generic;

namespace partycli.Store.Formats
{

    /// <summary>
    /// Available data formats for exporting data
    /// </summary>
    public enum Dataformat
    {
        Xml,
        Json
    }

    /// <summary>
    /// Dependency injection class for data formats.
    /// </summary>
    public class Format
    {
        /// <summary>
        /// Dictionairy of formats and classes responsible for their implementation
        /// </summary>
        public static Dictionary<Dataformat, Type> FormatTypeByName = new Dictionary<Dataformat, Type>
        {
            { Dataformat.Xml,    typeof(Xmlformat)},
            { Dataformat.Json, typeof(Jsonformat)}

        };

        public IFormat _format;

        public Format(IFormat format)
        {
            _format = format;
        }
        public dynamic Read(string DataFileName, Type dataType)
        {
            return _format.Read(DataFileName, dataType);
        }

        public string Save(string DataFileName, dynamic data)
        {
            return _format.Save(DataFileName, data);
        }
               
    }
}
