namespace QuickyTest.Domain.Models;

public class Alternativa
{
    public string alternativa { get; set; }
    public string enunciado { get; set; }

    public string ToRequest() => alternativa is null ? String.Empty : $"{alternativa}{enunciado}";
    public string ToRequestInCompleteModel() => alternativa is null ? String.Empty : $"{alternativa} {enunciado}";
}