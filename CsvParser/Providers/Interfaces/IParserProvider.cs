using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CsvParser.Providers.Interfaces
{
    public interface IParserProvider
    {
        string ParseCsv(string data);
    }
}
