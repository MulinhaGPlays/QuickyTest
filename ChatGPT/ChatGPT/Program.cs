//using System.Text.RegularExpressions;

//string texto = "Seu     \n \n\n\n\n texto \"dadada  555edgd \" \"dadadaa\"com espaços e quebras de linha.";
//string resultado = Regex.Replace(texto, @"(\s|\n)(?=(?:[^""]*""[^""]*"")*[^""]*$)", "");
//Console.WriteLine(resultado);

//==================================

//using OpenAI;
//using OpenAI.Chat;
//using OpenAI.Models;
//using QuickyTest.Domain.Models;
//using QuickyTest.Infra.Services;
//using System.Text;
//using System.Text.Json;

//File.CreateText(@$"C:\Users\MulinhaGPlays\Documents\GitHub\QuickyTest\result.txt").Close();
//var api = new OpenAIClient("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");

//var prompt = new Prompt
//{
//    assunto = "A Internacionalização da produção.",
//    materia = "Geografia",
//    serie = "3",
//    nivel = "Ensino médio",
//    qtdquestoes = 1,
//    possuicontexto = true,
//};

//var chatPrompts = new List<Message>
//{
//    new Message(Role.System, "Você é um gerador de provas no formato json que organiza-as da seguinte forma: { \"assunto\": \"\", \"materia\": \"\", \"serie\": \"\", \"nivel\": \"\", \"qtdquestoes\": 10, \"possuicontexto\": false, \"questoes\": [ { \"numero_questao\": 1, \"contexto\": \"\", \"pergunta\": \"\", \"qtdalternativas\": 5, \"alternativas\": [ { \"alternativa\": \"A\", \"enunciado\": \"\" }, { \"alternativa\": \"B\", \"enunciado\": \"\" }, { \"alternativa\": \"C\", \"enunciado\": \"\" }, { \"alternativa\": \"D\", \"enunciado\": \"\" }, { \"alternativa\": \"E\", \"enunciado\": \"\" } ] } ], \"respostas\": [ { \"numero_questao\": 1, \"alternativa\": \"A\", \"explicacao\": \"\" } ] } além disso você vai se basear nos campos \"assunto\", \"materia\", \"serie\", \"nivel\" e \"qtdquestoes\" que irá receber tambem no formato json, para implementar os campos: \"questoes\" e \"respostas\", Caso o campo \"possuicontexto\" for true, as questões devem possuir uma contextualização para a pergunta, uma historia ou um fato por exemplo, dentro do campo \"contexto\", se não o campo continuará vazio e terá apenas uma pergunta no campo \"enunciado\". Além disso você receberá um json com as informações iniciais para a implementação."),
//    new Message(Role.User, prompt.Build()),
//};

//var chatRequest = new ChatRequest(chatPrompts, Model.GPT3_5_Turbo);

//StringBuilder json = new();
//Console.ForegroundColor = ConsoleColor.Red;

//var options = new JsonSerializerOptions
//{
//    PropertyNameCaseInsensitive = true
//};

//string texto = String.Empty;//não vai ter
//await foreach (var result in api.ChatEndpoint.StreamCompletionEnumerableAsync(chatRequest))
//{
//    json.Append(result.FirstChoice);
//    await File.AppendAllTextAsync(@"C:\Users\MulinhaGPlays\Documents\GitHub\QuickyTest\result.txt", result.FirstChoice, Encoding.UTF8);
//    try {
//        string validJson = JsonValidator.CloseJson(json.ToString());
//        await using var stream = new MemoryStream(Encoding.UTF8.GetBytes(validJson));
//        var asyncProva = await JsonSerializer.DeserializeAsync<Prova>(stream, options);
//        string txtnovo = asyncProva?.questoes.FirstOrDefault()?.contexto ?? String.Empty;
//        //TODO: chamar o commando de construir prova e retornar texto no yield return
//        Console.Write(txtnovo[texto.Length..]); //não vai ter
//        texto += txtnovo[texto.Length..]; //não vai ter
//        JsonSerializer.Deserialize<Prova>(json.ToString()); break;
//    }
//    catch { continue; }
//}

//var prova = (Prova?) json.ToString();

//Console.Clear();
//Console.WriteLine(prova?.ToString() ?? "Erro do sistema");

//==================================

using QuickyTest.Domain.Models;
using QuickyTest.Infra.Services;

var gerador = new ProveGenerator();
var prompt = new Prompt
{
    assunto = "Pitágoras",
    materia = "Matemática",
    serie = "1",
    nivel = "Ensino Médio",
    qtdquestoes = 5,
    possuicontexto = true,
};

gerador.Build();
gerador.SetApiKey("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");
gerador.SetPrompt(prompt);

Console.ForegroundColor = ConsoleColor.Green;
await foreach (string chunk in gerador.GenerateProve())
    Console.Write(chunk);

//==================================

//using QuickyTest.Domain.Models;

//Console.Write(((Prova?)File.ReadAllText(@$"C:\Users\MulinhaGPlays\Documents\GitHub\QuickyTest\result.txt"))?.Build() ?? "Não foi possível converter");

//===================================

//using QuickyTest.Domain.Models;
//using QuickyTest.Infra.Services;
//using System.Text;
//using System.Text.Json;

//StringBuilder json = new();

//Console.ForegroundColor = ConsoleColor.Green;
//string text = String.Empty;
//foreach (var result in File.ReadAllText(@$"C:\Users\MulinhaGPlays\Documents\GitHub\QuickyTest\RevolucaoFrancesaComContexto.txt"))
//{
//    json.Append(result);
//    Prova? asyncProva = null;
//    if (!String.IsNullOrWhiteSpace(json.ToString().Last().ToString()))
//    {
//        string validJson = JsonValidator.CloseJson(json.ToString());
//        try
//        {
//            asyncProva = (Prova?)((Prova?)validJson)?.ToString();
//        }
//        catch { }
//    }
//    if (asyncProva is not null)
//    {
//        string chunck = asyncProva.Build()[text.Length..];
//        Console.Write(chunck);
//        text += chunck;
//    }
//}