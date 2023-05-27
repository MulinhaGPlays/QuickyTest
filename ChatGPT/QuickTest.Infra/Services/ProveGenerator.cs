using QuickyTest.Domain.Models;
using System.Text;
using OpenAI;
using OpenAI.Chat;
using OpenAI.Models;
using QuickyTest.Domain.Services;
using OpenAI.Completions;

namespace QuickyTest.Infra.Services;

public class ProveGenerator : IProveGenerator
{
    private OpenAIClient? _client;
    private string? _apiKey;
    private Prompt? _prompt;
    private Prova? _prove;
    private StringBuilder _json = null!;
    private List<Message> _chatPrompts = null!;

    private void Validation()
    {
        if (_apiKey is null) throw new Exception("Chave API não setada");
        if (_client is null) throw new Exception("O gerador não foi construido");
        if (_prompt is null) throw new Exception("Prompt não setado");
    }

    public void Build()
    {
        _json = new();
        _client = new OpenAIClient(_apiKey ?? String.Empty);
        _chatPrompts = new List<Message>
        {
            new Message(Role.System, "Você é um gerador de provas no formato json que organiza-as da seguinte forma: {\"assunto\":\"\",\"materia\":\"\",\"serie\":\"\",\"nivel\":\"\",\"qtdquestoes\":10,\"possuicontexto\":false,\"questoes\":[{\"numero_questao\":\"1.\",\"contexto\":\"\",\"pergunta\":\"\",\"alternativas\":[{\"alternativa\":\"a)\",\"enunciado\":\"\"},{\"alternativa\":\"b)\",\"enunciado\":\"\"},{\"alternativa\":\"c)\",\"enunciado\":\"\"},{\"alternativa\":\"d)\",\"enunciado\":\"\"},{\"alternativa\":\"e)\",\"enunciado\":\"\"}]}],\"respostas\":[{\"numero_questao\":\"1.\",\"alternativa\":\"a)\",\"explicacao\":\"\"}]} é importante priorizar o fato que o json não deve de forma alguma ter espaços ou quebras de linha, exceto dentro das aspas. Além disso você vai se basear nos campos \"assunto\", \"materia\", \"serie\", \"nivel\" e \"qtdquestoes\" que irá receber tambem no formato json, para implementar os campos: \"questoes\" e \"respostas\", Caso o campo \"possuicontexto\" for true, as questões devem possuir uma contextualização para a pergunta, uma historia ou um fato por exemplo, dentro do campo \"contexto\", se não o campo continuará vazio e terá apenas uma pergunta no campo \"enunciado\". Além disso você receberá um json com as informações iniciais para a implementação."),
            new Message(Role.User, _prompt?.Build() ?? String.Empty),
        };
        Validation();
    }

    public void SetApiKey(string key) => _apiKey = key;
    public void SetPrompt(Prompt prompt) => _prompt = prompt;
    public Prova? GetProve() => _prove;

    public async IAsyncEnumerable<string> GenerateProveEnumerableAsync()
    {
        var chatRequest = new ChatRequest(_chatPrompts, Model.GPT3_5_Turbo);
        string text = String.Empty;
        await foreach (var result in _client!.ChatEndpoint.StreamCompletionEnumerableAsync(chatRequest))
        {
            var choice = result.FirstChoice;
            if (choice != null && choice.FinishReason == null)
            {
                Prova? asyncProva = null;
                _json.Append(choice.ToString());
                if (!String.IsNullOrWhiteSpace(((string?)choice)?.LastOrDefault().ToString()))
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
                    yield return chunck;
                    text += chunck;
                }
            } else if (choice?.FinishReason == "stop" && choice?.Message != null)
            {
                _json.Clear();
                _json.Append(choice.Message);
            }
        }
        _prove = (Prova?) _json.ToString();
    }

    public async Task<Prova?> GenerateProveAsync()
    {
        var chatRequest = new ChatRequest(_chatPrompts, Model.GPT3_5_Turbo);
        var response = await _client!.ChatEndpoint.StreamCompletionAsync(chatRequest, res => res.ToString());
        _json.Append(response.ToString());
        Prova? prova = (Prova?)response.ToString();
        prova?.SetProvaUUID();
        return prova;
    }
}
