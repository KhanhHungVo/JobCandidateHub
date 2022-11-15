using CsvHelper;
using JobCandidateHub.Core.Application.Interfaces;
using System.Globalization;

namespace JobCandidateHub.Infrastructure
{
    public class CsvStorageService : ICsvStorageService
    {
        public async Task AddRecord<T>(T data)
        {
            using var writer = new StreamWriter("candidate.csv");
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecord(data);
            await Task.Run(() => csv.WriteRecord(data));
        }
        public async Task AddRecords<T>(IEnumerable<T> data)
        {
            using var writer = new StreamWriter("candidate.csv");
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            await csv.WriteRecordsAsync(data);
        }

        public async Task<IEnumerable<T>> GetAllRecords<T>()
        {
            using var streamReader = File.OpenText("candidate.csv");
            using var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
            IEnumerable<T> records = await Task.Run(() => csvReader.GetRecords<T>());
            return records;
        }
    }
}
