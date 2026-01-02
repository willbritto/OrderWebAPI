using OrderWebAPI.Models;

namespace OrderWebAPI.Services
{
    public interface IPrintService
    {
        byte[] GenerateOrderPdf(OrderModel orderModel);
    }
}
