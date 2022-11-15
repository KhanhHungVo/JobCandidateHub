using CsvHelper;
using CsvHelper.Configuration;
using JobCandidateHub.Core.Application.Interfaces;
using System.Globalization;

namespace JobCandidateHub.Infrastructure
{
    public class CsvStorageService : ICsvStorageService
    {
        public async Task AddRecord<T>(T data)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                NewLine = Environment.NewLine,
            };
            using var writer = new StreamWriter("candidate.csv");
            using var csv = new CsvWriter(writer, config);
            await Task.Run(() => csv.WriteRecord(data));
        }
        public async Task AddRecords<T>(IEnumerable<T> data)
        {
            using var writer = new StreamWriter("candidate.csv");
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteHeader<T>();
            csv.NextRecord();
            await csv.WriteRecordsAsync(data);
        }

        public async Task<IEnumerable<T>> GetAllRecords<T>()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                NewLine = Environment.NewLine,
                Delimiter = "\t",
                HeaderValidated = null
            };

            using var streamReader = File.OpenText("candidate.csv");
            using var csvReader = new CsvReader(streamReader, config);
            IEnumerable<T> records = await Task.Run(() => csvReader.GetRecords<T>().ToList());
            return records;
        }
    }
}
