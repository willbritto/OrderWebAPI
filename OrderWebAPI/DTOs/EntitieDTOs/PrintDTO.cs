namespace OrderWebAPI.DTOs.EntitieDTOs
{
    public class PrintDTO
    {

        public int NumOrder { get; set; }

        public string NameFull { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public DateTime Date { get; set; }
    }
}
