using ControleDeValidade.Context;
using ControleDeValidade.Models;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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

        // IMPORTA EXCEL - versão refatorada e reduzida
        [HttpPost]
        public async Task<IActionResult> ImportarExcel(IFormFile arquivoExcel)
        {
            if (arquivoExcel == null || arquivoExcel.Length == 0)
            {
                TempData["Tipo"] = "error";
                TempData["Msg"] = "Selecione um arquivo Excel válido.";
                return RedirectToAction("Index");
            }

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var erros = new List<string>();
            var dados = new List<Material>();

            using (var stream = new MemoryStream())
            {
                await arquivoExcel.CopyToAsync(stream);
                stream.Position = 0;

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
                        // Considera Nome na coluna 2 e CodigoMaterial na coluna 4
                        var nome = ws.Cells[linha, 2].Text?.Trim();
                        var codigo = ws.Cells[linha, 4].Text?.Trim();

                        if (string.IsNullOrWhiteSpace(nome) && string.IsNullOrWhiteSpace(codigo))
                            continue; // linha em branco

                        if (string.IsNullOrWhiteSpace(nome))
                        {
                              erros.Add($"Linha {linha}: Nome vazio.");
                            continue;
                        }

                        if (string.IsNullOrWhiteSpace(codigo))
                        {
                            erros.Add($"Linha {linha}: Código do material vazio.");
                            continue;
                        }

                        // evita duplicados já presentes no banco
                        var exists = await _context.Materiais
                            .AsNoTracking()
                            .AnyAsync(m => m.Nome == nome && m.CodigoMaterial == codigo);

                        if (exists)
                        {
                            erros.Add($"Linha {linha}: Material '{nome}' com código '{codigo}' já existe.");
                            continue;
                        }

                        dados.Add(new Material
                        {
                            Nome = nome,
                            CodigoMaterial = codigo
                        });
                    }

                    if (dados.Any())
                    {
                        _context.Materiais.AddRange(dados);
                        await _context.SaveChangesAsync();
                    }
                }
            }

            if (erros.Any())
            {
                TempData["Tipo"] = "warning";
                TempData["Msg"] = $"A importação finalizou com {erros.Count} avisos/erros.";
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
