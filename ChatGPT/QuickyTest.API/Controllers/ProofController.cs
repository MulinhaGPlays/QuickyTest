using Microsoft.AspNetCore.Mvc;
using QuickyTest.API.Models;
using QuickyTest.Domain.Models;
using QuickyTest.Domain.Repositories;

namespace QuickyTest.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProofController : ControllerBase
    {
        private readonly IProveGeneratorRepository _proveGeneratorRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProofController(IWebHostEnvironment webHostEnvironment, IProveGeneratorRepository proveGeneratorRepository)
        {
            _proveGeneratorRepository = proveGeneratorRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public async IAsyncEnumerable<ResponseProofViewModel> GetGeneratingProof(GetProofViewModel model)
        {
            string uuid = Guid.NewGuid().ToString();
            string? cookie_do_usuario = Request.Cookies["uuid_do_usuario"];
            if (cookie_do_usuario == null) Response.Cookies.Append("uuid_do_usuario", uuid);
            cookie_do_usuario ??= uuid;

            string guid = Guid.NewGuid().ToString();
            string downloadUrl = $"{Request.Scheme}://{Request.Host}/temp/";
            string archivesPath1 = _webHostEnvironment.WebRootPath;
            if (ModelState.IsValid)
            {
                if (model.VisualReturn)
                {
                    await foreach (string response in _proveGeneratorRepository.BuildAProveEnumerableAsync(model.API_KEY, model.Prompts.First()))
                    {
                        yield return new ResponseProofViewModel
                        {
                            content = response,
                            pdfURL = downloadUrl,
                            wordURL = downloadUrl,
                            uuid_prova = guid,
                            uuid_usuario = cookie_do_usuario
                        };
                    }
                }
                else
                {
                    foreach (Prova? response in await _proveGeneratorRepository.BuildAProvesAsync(model.API_KEY, model.Prompts))
                    {
                        response?.SetUsuarioUUID(cookie_do_usuario);
                        yield return new ResponseProofViewModel
                        {
                            status = response is null ? "UNDEFINED" : "COMPLETE",
                            content = response?.BuildInCompleteModel(),
                            pdfURL = $"{downloadUrl}/{response?.UUID_prova ?? "undefined"}.pdf",
                            wordURL = $"{downloadUrl}/{response?.UUID_prova ?? "undefined"}.docx",
                            uuid_prova = response?.UUID_prova,
                            uuid_usuario = response?.UUID_usuario,
                        };
                    }
                }
            }
        }
    }
}
