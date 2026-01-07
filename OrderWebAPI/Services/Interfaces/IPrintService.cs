using OrderWebAPI.DTOs.EntitieDTOs;
using OrderWebAPI.Models;

namespace OrderWebAPI.Services.Interfaces
{
    public interface IPrintService
    {
        byte[] GenerateOrderPdf(PrintDTO printDTO);
    }
}
