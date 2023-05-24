using QuickyTest.Domain.Models;
using System.Text.Json;
using System.Text;
using OpenAI;
using OpenAI.Chat;
using OpenAI.Models;

namespace QuickyTest.Infra.Services;

public class ProveGenerator
{
    private OpenAIClient _client;
    private string _apiKey;
    private Prompt _prompt;
    private Prova? _prove;

    public void Build()
    {
        _client = new OpenAIClient(_apiKey);
    }

    public void SetApiKey(string key) => _apiKey = key;
    public void SetPrompt(Prompt prompt) => _prompt = prompt;
    public Prova? GetProve() => _prove;

    public async IAsyncEnumerable<string> GenerateProve()
    {
        var chatPrompts = new List<Message>
        {
            new Message(Role.System, "Você é um gerador de provas no formato json que organiza-as da seguinte forma: {\"assunto\":\"\",\"materia\":\"\",\"serie\":\"\",\"nivel\":\"\",\"qtdquestoes\":10,\"possuicontexto\":false,\"questoes\":[{\"numero_questao\":\"1.\",\"contexto\":\"\",\"pergunta\":\"\",\"alternativas\":[{\"alternativa\":\"a)\",\"enunciado\":\"\"},{\"alternativa\":\"b)\",\"enunciado\":\"\"},{\"alternativa\":\"c)\",\"enunciado\":\"\"},{\"alternativa\":\"d)\",\"enunciado\":\"\"},{\"alternativa\":\"e)\",\"enunciado\":\"\"}]}],\"respostas\":[{\"numero_questao\":\"1.\",\"alternativa\":\"a)\",\"explicacao\":\"\"}]} é importante priorizar o fato que o json não deve de forma alguma ter espaços ou quebras de linha, exceto dentro das aspas. Além disso você vai se basear nos campos \"assunto\", \"materia\", \"serie\", \"nivel\" e \"qtdquestoes\" que irá receber tambem no formato json, para implementar os campos: \"questoes\" e \"respostas\", Caso o campo \"possuicontexto\" for true, as questões devem possuir uma contextualização para a pergunta, uma historia ou um fato por exemplo, dentro do campo \"contexto\", se não o campo continuará vazio e terá apenas uma pergunta no campo \"enunciado\". Além disso você receberá um json com as informações iniciais para a implementação."),
            new Message(Role.User, _prompt.Build()),
        };

        var chatRequest = new ChatRequest(chatPrompts, Model.GPT3_5_Turbo);

        StringBuilder json = new();

        string text = String.Empty;
        await foreach (var result in _client.ChatEndpoint.StreamCompletionEnumerableAsync(chatRequest))
        {
            Prova? asyncProva = null;
            json.Append(result.FirstChoice.ToString());
            if (!String.IsNullOrWhiteSpace(json.ToString().LastOrDefault().ToString()))
            {
                string validJson = JsonValidator.CloseJson(json.ToString());
                try
                {
                    asyncProva = (Prova?)((Prova?)validJson)?.ToString();
                }
                catch { }
            }
            if (asyncProva is not null)
            {
                string chunck = asyncProva.Build()[text.Length..];
                yield return chunck;
                text += chunck;
            }
        }
        string json2 = json.ToString();
        _prove = (Prova?) json.ToString();
    }
}
