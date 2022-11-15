namespace JobCandidateHub.Core.Application.Interfaces
{
    public interface ICsvStorageService
    {
        Task AddRecord<T>(T data);
        Task AddRecords<T>(IEnumerable<T> data);
        Task<IEnumerable<T>> GetAllRecords<T>();
    }
}
