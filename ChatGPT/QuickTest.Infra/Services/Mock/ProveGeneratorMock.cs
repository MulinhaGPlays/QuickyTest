using QuickyTest.Domain.Models;
using QuickyTest.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickyTest.Infra.Services.Mock
{
    public class ProveGeneratorMock : IProveGenerator
    {
        private StringBuilder _json = null!;

        public ProveGeneratorMock()
        {
            _json = new StringBuilder();
        }

        public void Build() { }

        public async Task<Prova?> GenerateProveAsync()
        {
            Prova? prova = ((Prova)await File.ReadAllTextAsync(@$"C:\Users\MulinhaGPlays\Documents\GitHub\QuickyTest\result.txt"))!;
            prova?.SetProvaUUID();
            return prova;
        }

        public async IAsyncEnumerable<ChunkModel> GenerateProveEnumerableAsync()
        {
            string text = String.Empty;
            foreach (var result in await File.ReadAllTextAsync(@$"C:\Users\MulinhaGPlays\Documents\GitHub\QuickyTest\RevolucaoFrancesaComContexto.txt"))
            {
                _json.Append(result);
                Prova? asyncProva = null;
                if (!String.IsNullOrWhiteSpace(_json.ToString().Last().ToString()))
                {
                    string validJson = JsonValidator.CloseJson(_json.ToString());
                    try
                    {
                        asyncProva = (Prova?)((Prova?)validJson)?.ToString();
                    }
                    catch { }
                }
                if (asyncProva is not null) 
                {
                    string chunck = asyncProva.Build()[text.Length..];
                    yield return new ChunkModel
                    {
                        Chunk = chunck,
                        Prove = asyncProva,
                    };
                    text += chunck;
                }
            }
        }

        public Prova? GetProve()
        {
            throw new NotImplementedException();
        }

        public void SetApiKey(string key) { }

        public void SetPrompt(Prompt prompt) { }
    }
}
