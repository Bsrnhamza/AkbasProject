using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using TestingAkbas.Data;
using TestingAkbas.Models;

namespace TestingAkbas.Controllers
{
    public class FabricsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FabricsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string[] qualities, string[] qualityClasses)
        {
            var fabrics = _context.Fabrics.AsQueryable();

            if (qualities != null && qualities.Length > 0)
            {
                fabrics = fabrics.Where(f => qualities.Contains(f.Qualities));
            }

            if (qualityClasses != null && qualityClasses.Length > 0)
            {
                fabrics = fabrics.Where(f => qualityClasses.Contains(f.QualityClass));
            }

            // Özel sıralama listesi
            var customOrder = new List<string>
    {
        "Viscose",
        "Rayon",
        "RynSignart",
        "Cotton",
        "Nylon",
        "Polyester",
        "PesDouble",
        "Tencel",
        "Modal",
        "Linen",
        "Jacquard",
        "Mix",
        "Yarndyed"
    };

            // Verileri özel sıraya göre sıralama
            var orderedFabrics = await fabrics.ToListAsync();
            orderedFabrics = orderedFabrics.OrderBy(f => customOrder.IndexOf(f.QualityClass)).ToList();

            return View(orderedFabrics);
        }


        // POST: Fabrics/ExportVisibleToExcel
        [HttpPost]
        public async Task<IActionResult> ExportVisibleToExcel([FromBody] ExportRequest request)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Visible Fabrics");

                // Başlıkları yazma
                for (int i = 0; i < request.Headers.Count; i++)
                {
                    worksheet.Cells[1, i + 1].Value = request.Headers[i];
                }

                // İçeriği yazma
                for (int i = 0; i < request.Data.Count; i++)
                {
                    var rowData = request.Data[i];
                    for (int j = 0; j < rowData.Count; j++)
                    {
                        worksheet.Cells[i + 2, j + 1].Value = rowData[j];
                    }
                }

                // Stil ayarları
                worksheet.Cells[1, 1, 1, request.Headers.Count].Style.Font.Bold = true;
                worksheet.Cells.AutoFitColumns();

                var stream = new MemoryStream();
                package.SaveAs(stream);
                var content = stream.ToArray();

                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "VisibleFabrics.xlsx");
            }
        }


        // GET: Fabrics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fabric = await _context.Fabrics
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fabric == null)
            {
                return NotFound();
            }

            return View(fabric);
        }

        // GET: Fabrics/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fabrics/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,QualityClass,FabricCode,QualityName,QualityGroup,QualityComposition,PatternType,Width,Weight,RawFabricPrice,DomesticPrice,ExportPrice")] Fabric fabric)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fabric);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fabric);
        }

        // GET: Fabrics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fabric = await _context.Fabrics.FindAsync(id);
            if (fabric == null)
            {
                return NotFound();
            }
            return View(fabric);
        }

        // POST: Fabrics/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,QualityClass,FabricCode,QualityName,QualityGroup,QualityComposition,PatternType,Width,Weight,RawFabricPrice,DomesticPrice,ExportPrice")] Fabric fabric)
        {
            if (id != fabric.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fabric);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FabricExists(fabric.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(fabric);
        }

        // GET: Fabrics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fabric = await _context.Fabrics
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fabric == null)
            {
                return NotFound();
            }

            return View(fabric);
        }

        // POST: Fabrics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fabric = await _context.Fabrics.FindAsync(id);
            if (fabric == null)
            {
                return NotFound();
            }

            _context.Fabrics.Remove(fabric);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FabricExists(int id)
        {
            return _context.Fabrics.Any(e => e.Id == id);
        }
    }
}
