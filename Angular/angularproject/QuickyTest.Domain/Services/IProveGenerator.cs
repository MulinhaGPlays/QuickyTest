using QuickyTest.Domain.Models;

namespace QuickyTest.Domain.Services
{
    public interface IProveGenerator
    {
        void Build();
        Prova? GetProve();
        void SetApiKey(string key);
        void SetPrompt(Prompt prompt);
        IAsyncEnumerable<ChunkModel> GenerateProveEnumerableAsync();
        Task<Prova?> GenerateProveAsync();
    }
}
