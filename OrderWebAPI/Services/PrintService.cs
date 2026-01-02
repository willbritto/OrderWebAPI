using OrderWebAPI.Models;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;

namespace OrderWebAPI.Services
{
    public class PrintService : IPrintService
    {
        public byte[] GenerateOrderPdf(OrderModel orderModel)
        {
            using var ms = new MemoryStream();
            var document = new PdfDocument();
            var page = document.AddPage();
            var gfx = XGraphics.FromPdfPage(page);
            var font = new XFont("Verdana", 12);

            gfx.DrawString($"Ordem de serviço #{orderModel.NumOrder}", font, XBrushes.Black, new XPoint(40, 40));
            gfx.DrawString($"Cliente : {orderModel.NameFull}" , font, XBrushes.Black, new XPoint(40, 70));
            gfx.DrawString($"Data : {orderModel.Date}" , font, XBrushes.Black, new XPoint(40, 100));

            int y = 140;

            gfx.DrawString($"Descrição : {orderModel.Description}", font, XBrushes.Black, new XPoint(40, y));
            y += 30;
            gfx.DrawString($"Preço : {orderModel.Price:C}", font, XBrushes.Black, new XPoint(40, y));
            y += 30;
            gfx.DrawString($"Status : {orderModel.Status}", font, XBrushes.Black, new XPoint(40, y));
            y += 30;
            gfx.DrawString($"Categoria : {orderModel.CategoryId}", font, XBrushes.Black, new XPoint(40, y));

            document.Save(ms, false);
            return ms.ToArray();
        }
    }
}
