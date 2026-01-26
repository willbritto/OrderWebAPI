using OrderWebAPI.Models;

namespace OrderWebAPI.DTOs.EntitieDTOs
{
    public class OrderDTO 
    {
       
        public int NumOrder { get; set; }
        public string NameFull { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public DateTime Date { get; set; }

        public StatusOrderService Status { get; set; }

        public int CategoryId { get; set; }
    }
}
