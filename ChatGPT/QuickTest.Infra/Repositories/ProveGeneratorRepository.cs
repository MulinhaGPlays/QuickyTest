using QuickyTest.Domain.Models;
using QuickyTest.Domain.Repositories;
using QuickyTest.Domain.Services;

namespace QuickyTest.Infra.Repositories
{
    public class ProveGeneratorRepository : IProveGeneratorRepository
    {
        private const string _root = "wwwroot/provas";
        private readonly IProveGenerator _context;

        public ProveGeneratorRepository(IProveGenerator context)
        {
            _context = context;

            if (!Directory.Exists(_root)) Directory.CreateDirectory(_root);
        }

        private void Build(string apiKey, Prompt prompt)
        {
            _context.SetApiKey(apiKey);
            _context.SetPrompt(prompt);
            _context.Build();
        }

        public async Task<Prova?> BuildAProveAsync(string apiKey, Prompt prompt)
        {
            this.Build(apiKey, prompt);
            Prova? prova = await _context.GenerateProveAsync();
            using var file = File.CreateText(Path.Combine(_root, $"{prova?.UUID_prova ?? "UNDEFINED"}.json"));
            await file.WriteAsync(prova?.ToString());
            file.Close();
            return prova;
        }

        public async IAsyncEnumerable<string> BuildAProveEnumerableAsync(string apiKey, Prompt prompt)
        {
            this.Build(apiKey, prompt);
            await foreach (string chunk in _context.GenerateProveEnumerableAsync())
                yield return chunk;
        }

        public async Task<Prova?[]> BuildAProvesAsync(string apiKey, List<Prompt> prompts)
        {
            var list = new List<Task<Prova?>>();
            list.AddRange(prompts.Select(x => this.BuildAProveAsync(apiKey, x)));
            return await Task.WhenAll(list);
        }
    }
}
