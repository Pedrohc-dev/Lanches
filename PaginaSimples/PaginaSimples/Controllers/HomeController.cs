using Microsoft.AspNetCore.Mvc;
using PaginaSimples.Models;
using System.Diagnostics;

namespace PaginaSimples.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
        public ActionResult Instalador()
        {
            string caminhoArquivo = @"\\sbrri1s0008\Instalar_Impressoras\Instalar_Impressoras.bat";
            string nomeDownload = "Instalar Impressoras.bat";

            if (!System.IO.File.Exists(caminhoArquivo))
            {
                return NotFound("Arquivo não encontrado.");
            }

            byte[] fileBytes = System.IO.File.ReadAllBytes(caminhoArquivo);

            return File(fileBytes,
                        "application/octet-stream",
                        nomeDownload);
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
