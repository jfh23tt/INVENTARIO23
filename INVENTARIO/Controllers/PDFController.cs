using INVENTARIO.Models;
using INVENTARIO.Servicios;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace INVENTARIO.Controllers
{
    public class PDFController : Controller
    {
        private readonly Irepositoriopdf repositoriopdf;
        public PDFController(Irepositoriopdf repositoriopdf)
        {
            this.repositoriopdf = repositoriopdf;
        }
        // GET: PDFController
        public IActionResult PDF()
        {
            return View();
        }

        public IActionResult pdfInventario()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                PdfWriter writer = new PdfWriter(stream, new WriterProperties().SetCompressionLevel(9));
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);

                // Fuente en negrita
                var boldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

                // Título del documento
                document.Add(new Paragraph("Listado de Inventario")
                    .SetFontSize(18)
                    .SetFont(boldFont)
                    .SetTextAlignment(TextAlignment.CENTER));

                // Tabla con 6 columnas
                Table table = new Table(6);

                // Encabezados en negrita
                table.AddHeaderCell(new Cell().Add(new Paragraph("Nombre").SetFont(boldFont)));
                table.AddHeaderCell(new Cell().Add(new Paragraph("Categoría").SetFont(boldFont)));
                table.AddHeaderCell(new Cell().Add(new Paragraph("Cantidad").SetFont(boldFont)));
                table.AddHeaderCell(new Cell().Add(new Paragraph("Precio").SetFont(boldFont)));
                table.AddHeaderCell(new Cell().Add(new Paragraph("Proveedor").SetFont(boldFont)));
                table.AddHeaderCell(new Cell().Add(new Paragraph("Fecha Ingreso").SetFont(boldFont)));

                // Llenar la tabla con datos
                InventarioModel pdfinventario = new InventarioModel();
                var inventario = repositoriopdf.Invetariopdf(pdfinventario);

                foreach (var item in inventario)
                {
                    table.AddCell(item.Nombre ?? "");
                    table.AddCell(item.Categoria ?? "");
                    table.AddCell(item.Cantidad.ToString());
                    table.AddCell(item.Precio.ToString("C"));
                    table.AddCell(item.Proveedor ?? "");
                    table.AddCell(item.FechaIngreso.ToString("dd/MM/yyyy"));
                }

                // Agregar tabla al documento
                document.Add(table);

                document.Close();

                byte[] pdfBytes = stream.ToArray();
                return File(pdfBytes, "application/pdf", "ListadoInventario.pdf");
            }
        }
        // GET: PDFController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PDFController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PDFController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PDFController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PDFController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PDFController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PDFController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
