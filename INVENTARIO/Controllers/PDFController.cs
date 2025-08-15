using INVENTARIO.Models;
using INVENTARIO.Servicios;
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
            // Generar el PDF
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = new PdfWriter(stream);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            // Título del documento
            document.Add(new Paragraph("Listado de Inventario")
                .SetFontSize(18)
                .SetBold()
                .SetTextAlignment(TextAlignment.CENTER));

            // Tabla con encabezados
            Table table = new Table(6, true); // 6 columnas

            table.AddHeaderCell("Nombre");
            table.AddHeaderCell("Categoría");
            table.AddHeaderCell("Cantidad");
            table.AddHeaderCell("Precio");
            table.AddHeaderCell("Proveedor");
            table.AddHeaderCell("Fecha Ingreso");

            // Llenar la tabla con datos
            InventarioModel pdfinventario = new InventarioModel();
            var inventario = repositoriopdf.Invetariopdf(pdfinventario);

            foreach (var item in inventario)
            {
                table.AddCell(item.Nombre);
                table.AddCell(item.Categoria);
                table.AddCell(item.Cantidad.ToString());
                table.AddCell(item.Precio.ToString("C")); // formato moneda
                table.AddCell(item.Proveedor);
                table.AddCell(item.FechaIngreso.ToString("dd/MM/yyyy"));
            }

            // Agregar tabla al documento y cerrar
            document.Add(table);
            document.Close();

            // Devolver el archivo como PDF
            byte[] pdfBytes = stream.ToArray();
            Response.Headers.Add("Content-Disposition", "inline; filename=ListadoInventario.pdf");
            return File(pdfBytes, "application/pdf");
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
