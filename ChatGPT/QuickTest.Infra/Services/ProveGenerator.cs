using QuickyTest.Domain.Models;
using System.Text.Json;
using System.Text;
using OpenAI;
using OpenAI.Chat;
using OpenAI.Models;

namespace QuickyTest.Infra.Services;

public class ProveGenerator
{
    private string _apiKey { get; set; }
    private Prova? _prove { get; set; }

    public async IAsyncEnumerable<string> GenerateProve()
    {
        var api = new OpenAIClient("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");

        var prompt = new Prompt
        {
            assunto = "Revolução Industrial",
            materia = "Geografia",
            serie = "2",
            nivel = "Ensino Médio",
            qtdquestoes = 5,
            possuicontexto = true,
        };
        var chatPrompts = new List<Message>
        {
            new Message(Role.System, "Você é um gerador de provas no formato json que organiza-as da seguinte forma: {\"assunto\":\"\",\"materia\":\"\",\"serie\":\"\",\"nivel\":\"\",\"qtdquestoes\":10,\"possuicontexto\":false,\"questoes\":[{\"numero_questao\":\"1.\",\"contexto\":\"\",\"pergunta\":\"\",\"alternativas\":[{\"alternativa\":\"a)\",\"enunciado\":\"\"},{\"alternativa\":\"b)\",\"enunciado\":\"\"},{\"alternativa\":\"c)\",\"enunciado\":\"\"},{\"alternativa\":\"d)\",\"enunciado\":\"\"},{\"alternativa\":\"e)\",\"enunciado\":\"\"}]}],\"respostas\":[{\"numero_questao\":\"1.\",\"alternativa\":\"a)\",\"explicacao\":\"\"}]} é importante priorizar o fato que o json não deve de forma alguma ter espaços ou quebras de linha, exceto dentro das aspas. Além disso você vai se basear nos campos \"assunto\", \"materia\", \"serie\", \"nivel\" e \"qtdquestoes\" que irá receber tambem no formato json, para implementar os campos: \"questoes\" e \"respostas\", Caso o campo \"possuicontexto\" for true, as questões devem possuir uma contextualização para a pergunta, uma historia ou um fato por exemplo, dentro do campo \"contexto\", se não o campo continuará vazio e terá apenas uma pergunta no campo \"enunciado\". Além disso você receberá um json com as informações iniciais para a implementação."),
            new Message(Role.User, prompt.Build()),
        };

        var chatRequest = new ChatRequest(chatPrompts, Model.GPT3_5_Turbo);

        StringBuilder json = new();

        string text = String.Empty;
        await foreach (var result in api.ChatEndpoint.StreamCompletionEnumerableAsync(chatRequest))
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
        _prove = (Prova?) json.ToString();
    }
}
