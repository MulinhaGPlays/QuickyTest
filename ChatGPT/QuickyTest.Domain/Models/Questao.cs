namespace QuickyTest.Domain.Models;

public class Questo
{
    public int numero_questao { get; set; }
    public object? imagem { get; set; }
    public string? contexto { get; set; }
    public string pergunta { get; set; }
    public int qtdalternativas { get; set; }
    public List<Alternativa> alternativas { get; set; }
}
