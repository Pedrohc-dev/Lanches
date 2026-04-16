using ControleDeValidade.Context;
using ControleDeValidade.Models;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Globalization;

namespace ControleDeValidade.Controllers
{
    public class MaterialController : Controller
    {
        private readonly AppDbContext _context;


        public MaterialController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var materiais = _context.Materiais.ToList();
            return View(materiais);
        }
         public IActionResult Details(int id)
        {
            var material = _context.Materiais.FirstOrDefault(m => m.MaterialId == id);
            if (material == null)
            {
                return NotFound();
            }
            return View(material);
        }

        //IMPORTA EXCEL
        [HttpPost]
        public async Task<IActionResult> ImportarExcel(IFormFile arquivoExcel)
        {


            if (arquivoExcel == null || arquivoExcel.Length == 0)
            {
                TempData["Tipo"] = "error";
                TempData["Msg"] = "Selecione um arquivo Excel válido.";
                return RedirectToAction("Index");
            }
            ExcelPackage.License.SetNonCommercialOrganization("My Noncommercial organization");
            //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var erros = new List<string>();
            var dados = new List<Material>();

            using (var stream = new MemoryStream())
            {
                await arquivoExcel.CopyToAsync(stream);
                stream.Position = 0; // garante que o stream seja lido do início

                using (var pacote = new ExcelPackage(stream))
                {
                    var ws = pacote.Workbook.Worksheets.FirstOrDefault();
                    if (ws == null)
                    {
                        TempData["Tipo"] = "error";
                        TempData["Msg"] = "O arquivo Excel não possui planilhas.";
                        return RedirectToAction("Index");
                    }

                    int linhas = ws.Dimension?.Rows ?? 0;
                    if (linhas < 2)
                    {
                        TempData["Tipo"] = "error";
                        TempData["Msg"] = "O arquivo Excel não possui dados.";
                        return RedirectToAction("Index");
                    }



                    for (int linha = 2; linha <= linhas; linha++)
                    {
                        bool linhaVazia = ws.Cells[linha, 1, linha, 10].All(c => string.IsNullOrWhiteSpace(c.Text));

                        if (linhaVazia)
                            continue;

                        try
                        {
                            //Data
                            var valorData = ws.Cells[linha, 1].Value;

                            DateTime data;

                            if (valorData is double numeroExcel)
                            {

                                data = DateTime.FromOADate(numeroExcel);
                            }
                            else if (!DateTime.TryParse(valorData?.ToString(), new CultureInfo("pt-BR"), DateTimeStyles.None, out data))
                            {
                                erros.Add($"Linha {linha}: Data inválida (valor lido: '{valorData}')");
                                continue;
                            }

                            //Linha
                            var linhaNome = ws.Cells[linha, 2].Text?.Trim();

                            if (string.IsNullOrWhiteSpace(linhaNome))
                            {
                                erros.Add($"Linha {linha}: Valor da coluna 'Linha' está vazio.");
                                continue;
                            }


                            var local = await _context.Locais.AsNoTracking()
                                .FirstOrDefaultAsync(l => l.Nome == linhaNome);

                            if (local == null)
                            {
                                erros.Add($"Linha {linha}: O valor '{linhaNome}' não existe na tabela Local.");
                                continue;
                            }

                            var localId = local.Nome;

                            //Certificado
                            var certificadoTexto = ws.Cells[linha, 3].Text?.Trim().ToLower();

                            int? numeroCertificado = null;

                            if (int.TryParse(certificadoTexto, out var num))
                            {
                                numeroCertificado = num;
                            }
                            else if (certificadoTexto == "s/certificado" || certificadoTexto == "s/ certificado")
                            {
                                numeroCertificado = null;
                            }
                            else if (string.IsNullOrWhiteSpace(certificadoTexto))
                            {
                                erros.Add($"Linha {linha}: Nº do certificado vazio.");
                                continue;
                            }
                            else
                            {
                                erros.Add($"Linha {linha}: Nº do certificado inválido ({certificadoTexto}).");
                                continue;
                            }

                            //CodigoMaterial
                            var codigoMaterial = ws.Cells[linha, 4].Text;

                            //Quantidade De Pacotes
                            var valorQtde = ws.Cells[linha, 5].Value;
                            double qtdePacotes = 0;

                            if (valorQtde is double d)
                            {
                                qtdePacotes = d;
                            }
                            else if (valorQtde != null &&
                                     double.TryParse(valorQtde.ToString(), NumberStyles.Any, new CultureInfo("pt-BR"), out var q))
                            {
                                qtdePacotes = q;
                            }

                            //Calibre
                            var valorCalibre = ws.Cells[linha, 6].Value;
                            double? calibre = null;

                            if (valorCalibre is double ncalibre)
                            {
                                calibre = ncalibre;
                            }
                            else if (valorCalibre != null &&
                                     double.TryParse(valorCalibre.ToString(), NumberStyles.Any, new CultureInfo("pt-BR"), out var c))
                            {
                                calibre = c;
                            }

                            //Parede
                            var valorParede = ws.Cells[linha, 7].Value;
                            double parede;

                            if (valorParede is double nParede)
                            {
                                parede = nParede;
                            }
                            else if (!double.TryParse(valorParede?.ToString(), NumberStyles.Any, new CultureInfo("pt-BR"), out parede))
                            {
                                erros.Add($"Linha {linha}: Parede inválida (valor lido: '{valorParede}')");
                                continue;
                            }

                            //Comprimento
                            var valorComp = ws.Cells[linha, 8].Value;
                            double comprimento;

                            if (valorComp is double nComp)
                            {
                                comprimento = nComp;
                            }
                            else if (!double.TryParse(valorComp?.ToString(), NumberStyles.Any, new CultureInfo("pt-BR"), out comprimento))
                            {
                                erros.Add($"Linha {linha}: Comprimento inválido (valor lido: '{valorComp}')");
                                continue;
                            }

                            //Acabamento
                            var valorAcab = ws.Cells[linha, 9].Value;
                            double acabamento;

                            if (valorAcab is double nAcab)
                            {
                                acabamento = nAcab;
                            }
                            else if (!double.TryParse(valorAcab?.ToString(), NumberStyles.Any, new CultureInfo("pt-BR"), out acabamento))
                            {
                                erros.Add($"Linha {linha}: Acabamento inválido (valor lido: '{valorAcab}')");
                                continue;
                            }

                            //Observação
                            var observacao = ws.Cells[linha, 10].Text;

                            dados.Add(new  Material
                            {
                                Data = data,
                                MaterialId = local.LocalId,
                                Nome = linhaNome,
                                CodigoMaterial = codigoMaterial,
                              
                            });
                        }
                        catch (Exception ex)
                        {
                            erros.Add($"Linha {linha}: Erro inesperado - {ex.Message}");
                        }
                    }

                    if (dados.Any())
                    {
                        _context.Materiais.AddRange(dados);
                        await _context.SaveChangesAsync();
                    }

                    TempData["Tipo"] = "success";
                    TempData["Msg"] = "Dados importados com sucesso!";
                }
            }

            if (erros.Any())
            {
                TempData["Msg"] = $"A importação gerou {erros.Count} erro. Exiba-os na tela de detalhes.";
                TempData["ErrosImportacao"] = string.Join("||", erros);

            }
            else
            {
                TempData["Tipo"] = "success";
                TempData["Msg"] = "Dados importados com sucesso!";
            }

            return RedirectToAction("Index");
        }

    }
}
