using OrderWebAPI.DTOs.EntitieDTOs;
using OrderWebAPI.Models;
using OrderWebAPI.Services.Interfaces;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;

namespace OrderWebAPI.Services
{
    public class PrintService : IPrintService
    {
        public byte[] GenerateOrderPdf(PrintDTO printDTO)
        {
            using var ms = new MemoryStream();
            var document = new PdfDocument();
            var page = document.AddPage();
            var gfx = XGraphics.FromPdfPage(page);
            var font = new XFont("Verdana", 12);

            gfx.DrawString($"Ordem de serviço #{printDTO.NumOrder}", font, XBrushes.Black, new XPoint(40, 40));
            gfx.DrawString($"Cliente : {printDTO.NameFull}" , font, XBrushes.Black, new XPoint(40, 70));
            gfx.DrawString($"Data : {printDTO.Date}" , font, XBrushes.Black, new XPoint(40, 100));

            int y = 140;

            gfx.DrawString($"Descrição : {printDTO.Description}", font, XBrushes.Black, new XPoint(40, y));
            y += 30;
            gfx.DrawString($"Preço : {printDTO.Price:C}", font, XBrushes.Black, new XPoint(40, y));
            y += 30;           

            document.Save(ms, false);
            return ms.ToArray();
        }
    }
}
