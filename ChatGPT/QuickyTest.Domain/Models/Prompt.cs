using QuickyTest.Domain.Models.Base;

namespace QuickyTest.Domain.Models;

public class Prompt : Json<Prompt>
{
    public Prompt() => _model = this;

    public string assunto { get; set; }
    public string materia { get; set; }
    public string serie { get; set; }
    public string nivel { get; set; }
    public int qtdquestoes { get; set; }
    public bool possuicontexto { get; set; }

    public string Build() => $"{this} \n Com base nas informações fornecidas, gere um modelo de prova com {qtdquestoes} questões sobre {assunto}:";
}
