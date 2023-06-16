namespace QuickyTest.Domain.Models;

public class Resposta
{
    public string numero_questao { get; set; }
    public string alternativa { get; set; }
    public string explicacao { get; set; }

    public string ToRequest() => $"{numero_questao}{alternativa}{explicacao}";
    public string ToRequestInCompleteModel() => $"{numero_questao}: {alternativa} \nExplicação: {explicacao}";
}
