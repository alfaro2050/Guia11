using Guia11.Data;
using iText.Kernel.Pdf;
using Guia11.Models;
using iText.Kernel.Pdf;
using iText.Layout.Borders;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.AspNetCore.Mvc;

using System.IO;
using System.Linq;

namespace Guia11.Controllers
{
    public class ReportController : Controller
    {
         
        private readonly Data.ApplicationDbContext _context;

        public ReportController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult GeneratePdf()
        {
            var employees = _context.Employees.ToList();
            byte[] pdf = GeneratePdfReport(employees);

            return File(pdf, "application/pdf", "EmployeesReport.pdf");
        }

        private byte[] GeneratePdfReport(List<Employees> employees)
        {
            using (var stream = new MemoryStream())
            {
                var writer = new PdfWriter(stream);
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf);

                document.Add(new Paragraph("Listado de empleados"));
                var table = new Table(4, true);
                //  table.SetBorder(Border.NO_BORDER);
                table.SetBorder(new SolidBorder(1f));
                table.AddCell("ID");
                table.AddCell("Nombres");
                table.AddCell("Apellidos");
                table.AddCell("Direccion");

                foreach (var employee in employees)
                {
                    table.AddCell(employee.EmployeeID.ToString());
                    table.AddCell(employee.FirstName);
                    table.AddCell(employee.LastName);
                    table.AddCell(employee.Address);
                }

                document.Add(table);
                document.Close();

                return stream.ToArray();
            }
        }
    }
}
