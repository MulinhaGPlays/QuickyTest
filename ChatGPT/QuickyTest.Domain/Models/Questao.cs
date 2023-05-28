namespace QuickyTest.Domain.Models;

public class Questao
{
    public string numero_questao { get; set; }
    public object? imagem { get; set; }
    public string? contexto { get; set; }
    public string pergunta { get; set; }
    public int qtdalternativas { get => alternativas.Count; }
    public List<Alternativa> alternativas { get; set; } = new();

    public string ToRequest() => $"{numero_questao}{contexto}{pergunta}{String.Join(String.Empty, alternativas.Select(x => x.ToRequest()).Where(x => !String.IsNullOrWhiteSpace(x)))}";
    public string ToRequestInCompleteModel() => $"{numero_questao} {contexto} {pergunta}{String.Join(String.Empty, alternativas.Select(x => $"\n{x.ToRequestInCompleteModel()}").Where(x => !String.IsNullOrWhiteSpace(x)))}";
}
