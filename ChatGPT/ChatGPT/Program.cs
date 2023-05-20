using OpenAI;
using OpenAI.Chat;
using OpenAI.Models;
using QuickyTest.Domain.Models;
using System.Text;
using System.Text.Json;

File.CreateText(@$"C:\Users\MulinhaGPlays\Documents\GitHub\QuickyTest\result.txt").Close();
var api = new OpenAIClient("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");

var prompt = new Prompt
{
    assunto = "Física moderna",
    materia = "Física",
    serie = "5",
    nivel = "Faculdade",
    qtdquestoes = 2,
    possuicontexto = true,
};

var chatPrompts = new List<Message>
{
    new Message(Role.System, "Você é um gerador de provas no formato json que organiza-as da seguinte forma: { \"assunto\": \"\", \"materia\": \"\", \"serie\": \"\", \"nivel\": \"\", \"qtdquestoes\": 10, \"possuicontexto\": false, \"questoes\": [ { \"numero_questao\": 1, \"contexto\": \"\", \"pergunta\": \"\", \"qtdalternativas\": 5, \"alternativas\": [ { \"alternativa\": \"A\", \"enunciado\": \"\" }, { \"alternativa\": \"B\", \"enunciado\": \"\" }, { \"alternativa\": \"C\", \"enunciado\": \"\" }, { \"alternativa\": \"D\", \"enunciado\": \"\" }, { \"alternativa\": \"E\", \"enunciado\": \"\" } ] } ], \"respostas\": [ { \"numero_questao\": 1, \"alternativa\": \"A\", \"explicacao\": \"\" } ] } além disso você vai se basear nos campos \"assunto\", \"materia\", \"serie\", \"nivel\" e \"qtdquestoes\" que irá receber tambem no formato json, para implementar os campos: \"questoes\" e \"respostas\", Caso o campo \"possuicontexto\" for true, as questões devem possuir uma contextualização para a pergunta, uma historia ou um fato por exemplo, dentro do campo \"contexto\", se não o campo continuará vazio e terá apenas uma pergunta no campo \"enunciado\". Além disso você receberá um json com as informações iniciais para a implementação."),
    new Message(Role.User, prompt.Build()),
};

var chatRequest = new ChatRequest(chatPrompts, Model.GPT3_5_Turbo);

StringBuilder json = new();
Console.ForegroundColor = ConsoleColor.Red;

await foreach (var result in api.ChatEndpoint.StreamCompletionEnumerableAsync(chatRequest))
{
    string value = result.FirstChoice;
    json.Append(value);
    Console.Write(value);
    await File.AppendAllTextAsync(@"C:\Users\MulinhaGPlays\Documents\GitHub\QuickyTest\result.txt", value, Encoding.UTF8);
    try {
        JsonSerializer.Deserialize<Prova>(json.ToString()); break;
    }
    catch { continue; }
}

var prova = (Prova?) json.ToString();

Console.Clear();
Console.WriteLine(prova?.ToString() ?? "Erro do sistema");