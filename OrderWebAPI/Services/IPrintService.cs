using OrderWebAPI.DTOs.EntitieDTOs;
using OrderWebAPI.Models;

namespace OrderWebAPI.Services
{
    public interface IPrintService
    {
        byte[] GenerateOrderPdf(OrderDTO orderDTO);
    }
}
