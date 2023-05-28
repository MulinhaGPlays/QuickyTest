using QuickyTest.Domain.Models;

namespace QuickyTest.Domain.Repositories
{
    public interface IProveGeneratorRepository
    {
        Task<Prova?> BuildAProveAsync(string apiKey, Prompt prompt);
        IAsyncEnumerable<ChunkModel> BuildAProveEnumerableAsync(string apiKey, Prompt prompt);
        Task<Prova?[]> BuildAProvesAsync(string apiKey, List<Prompt> prompts);
    }
}
