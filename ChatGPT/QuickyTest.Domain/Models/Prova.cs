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
    public List<Questo> questoes { get; set; }
    public List<Resposta> respostas { get; set; }
    public string UUID_usuario { get; set; }
    public string UUID_prova => Guid.NewGuid().ToString();

    public static implicit operator Prova? (string json) => JsonSerializer.Deserialize<Prova>(json);
}
