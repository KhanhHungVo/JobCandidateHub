using CsvHelper;
using CsvHelper.Configuration;
using JobCandidateHub.Core.Application.Interfaces;
using System.Globalization;

namespace JobCandidateHub.Infrastructure
{
    public class CsvStorageService : ICsvStorageService
    {
        public async Task AddRecords<T>(IEnumerable<T> data)
        {
            await using var writer = new StreamWriter($"{typeof(T).Name}.csv");
            await using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteHeader<T>();
            csv.NextRecord();
            await csv.WriteRecordsAsync(data);
        }

        public async Task<IEnumerable<T>> GetAllRecords<T>()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null
            };
            using var streamReader = File.OpenText($"{typeof(T).Name}.csv");
            using var csvReader = new CsvReader(streamReader, config);
            IEnumerable<T> records = await Task.Run(() => csvReader.GetRecords<T>().ToList());
            return records;
        }
    }
}
