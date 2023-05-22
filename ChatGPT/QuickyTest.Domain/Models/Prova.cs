using QuickyTest.Domain.Models.Base;
using System.Text.Json;

namespace QuickyTest.Domain.Models;

public class Prova : Json<Prova>
{
    public Prova() => _model = this;

    public int ano => DateTime.Now.Year;
    public string assunto { get; set; }
    public string materia { get; set; }
    public string serie { get; set; }
    public string nivel { get; set; }
    public int qtdquestoes { get; set; }
    public bool possuicontexto { get; set; }
    public List<Questao> questoes { get; set; } = new();
    public List<Resposta> respostas { get; set; } = new();
    public string UUID_usuario { get; set; }
    public string UUID_prova { get; private set; }

    public string Build() 
        => $"{assunto}{materia}{serie}{nivel}" +
        $"{String.Join(String.Empty, questoes.Select(x => x.ToRequest()).Where(x => !String.IsNullOrWhiteSpace(x)))}" +
        $"{String.Join(String.Empty, respostas.Select(x => x.ToRequest()).Where(x => !String.IsNullOrWhiteSpace(x)))}";

    public void SetProvaUUID() => UUID_prova = Guid.NewGuid().ToString();

    public static implicit operator Prova? (string json) => JsonSerializer.Deserialize<Prova>(json);
}
