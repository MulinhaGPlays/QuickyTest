namespace QuickyTest.API.Models
{
    public class ResponseProofViewModel
    {
        public string? pdfURL { get; set; }
        public string? wordURL { get; set; }
        public string? content { get; set; }
        public string status { get; set; } = "LOADING";
        public string? uuid_usuario { get; set; } = null!;
        public string? uuid_prova { get; set; } = null!;
    }
}
