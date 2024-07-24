using System;
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
        // GET: Fabrics
        public async Task<IActionResult> Index(string[] qualities, string[] qualityClasses, int pageNumber = 1, int pageSize = 20, string sortOrder = "asc")
        {
            // Temel sorgu
            var fabrics = _context.Fabrics.AsQueryable();

            // Filtreleme
            if (qualities != null && qualities.Length > 0)
            {
                fabrics = fabrics.Where(f => qualities.Contains(f.Qualities));
            }

            if (qualityClasses != null && qualityClasses.Length > 0)
            {
                fabrics = fabrics.Where(f => qualityClasses.Contains(f.QualityClass));
            }

            // Sıralama
            fabrics = sortOrder == "desc" ? fabrics.OrderByDescending(f => f.QualityName) : fabrics.OrderBy(f => f.QualityName);

            // Sayfalama
            var pagedFabrics = await fabrics
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return View(pagedFabrics);
        }
        public async Task<IActionResult> ExportToExcel()
        {
            var fabrics = await _context.Fabrics.ToListAsync();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Fabrics");

                // Başlıkları yazma
                worksheet.Cells[1, 1].Value = "Quality Class";
                worksheet.Cells[1, 2].Value = "Fabric Code";
                worksheet.Cells[1, 3].Value = "Qualities";
                worksheet.Cells[1, 4].Value = "Quality Name";
                worksheet.Cells[1, 5].Value = "Quality Group";
                worksheet.Cells[1, 6].Value = "Quality Composition";
                worksheet.Cells[1, 7].Value = "Pattern Type";
                worksheet.Cells[1, 8].Value = "Width";
                worksheet.Cells[1, 9].Value = "Weight";
                worksheet.Cells[1, 10].Value = "Raw Fabric Price";
                worksheet.Cells[1, 11].Value = "Domestic Price";
                worksheet.Cells[1, 12].Value = "Export Price";

                // İçeriği yazma
                for (int i = 0; i < fabrics.Count; i++)
                {
                    var fabric = fabrics[i];
                    worksheet.Cells[i + 2, 1].Value = fabric.QualityClass;
                    worksheet.Cells[i + 2, 2].Value = fabric.FabricCode;
                    worksheet.Cells[i + 2, 3].Value = fabric.Qualities;
                    worksheet.Cells[i + 2, 4].Value = fabric.QualityName;
                    worksheet.Cells[i + 2, 5].Value = fabric.QualityGroup;
                    worksheet.Cells[i + 2, 6].Value = fabric.QualityComposition;
                    worksheet.Cells[i + 2, 7].Value = fabric.PatternType;
                    worksheet.Cells[i + 2, 8].Value = fabric.Width;
                    worksheet.Cells[i + 2, 9].Value = fabric.Weight;
                    worksheet.Cells[i + 2, 10].Value = fabric.RawFabricPrice;
                    worksheet.Cells[i + 2, 11].Value = fabric.DomesticPrice;
                    worksheet.Cells[i + 2, 12].Value = fabric.ExportPrice;
                }

                // Stil ayarları
                worksheet.Cells[1, 1, 1, 12].Style.Font.Bold = true;
                worksheet.Cells.AutoFitColumns(0); // Sütun genişliklerini otomatik ayarlar

                var stream = new MemoryStream();
                package.SaveAs(stream);
                var content = stream.ToArray();

                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Fabrics.xlsx");
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