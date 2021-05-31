using CsvHelper;
using CSVManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Windows.Storage;

namespace CSVManager
{
    public class DataWriter : IDataWriter
    {
        public DataWriter()
        {

        }
        public async void WriteRecordsToCSV(IEnumerable<IEnumerable<string>> records, string filename)
        {
            StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(filename, CreationCollisionOption.FailIfExists);
            var lines = records.Select(x => string.Join(',', x));

            await FileIO.AppendLinesAsync(file, lines);
            Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.Add(file);
        }

        public void WriteFieldsToCSV(IEnumerable<string> fields, string filename)
        {
            using (var writer = new StreamWriter("D:\\diplomaFiles\\1.csv"))
            using (var csvWriter = new CsvWriter(writer, CultureInfo.CurrentCulture))
            {
                foreach (var field in fields)
                {
                    csvWriter.WriteField(field);
                }
                csvWriter.NextRecord();

                writer.Flush();
            }
        }

        public async void LaunchCSVFile(string filename)
        {
            StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync(filename);

            if (file != null)
            {
                // Launch the retrieved file
                var success = await Windows.System.Launcher.LaunchFileAsync(file);

                if (success)
                {
                    // File launched
                }
                else
                {
                    // File launch failed
                }
            }
            else
            {
                // Could not find file
            }
        }
    }
}
