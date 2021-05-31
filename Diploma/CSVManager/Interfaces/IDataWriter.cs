using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVManager.Interfaces
{
    public interface IDataWriter
    {
        void WriteRecordsToCSV(IEnumerable<IEnumerable<string>> records, string filename);
        void WriteFieldsToCSV(IEnumerable<string> fields, string filename);
        void LaunchCSVFile(string filename);
    }
}
