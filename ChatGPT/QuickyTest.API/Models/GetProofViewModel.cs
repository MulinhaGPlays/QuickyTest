using QuickyTest.API.Utils.CustomAnnotations;
using QuickyTest.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace QuickyTest.API.Models
{
    public class GetProofViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Chave API: https://platform.openai.com/account/api-keys")]
        public string API_KEY { get; set; }

        [Display(Name = "Habilitar retorno visual - limita a geração a uma prova, mas vc verá ela sendo criada.")]
        public bool VisualReturn { get; set; } = true;

        [Required]
        [Display(Name = "Dados das provas")]
        [LimitOfVisualReturn(nameof(VisualReturn))]
        public List<Prompt> Prompts { get; set; }
    }
}
