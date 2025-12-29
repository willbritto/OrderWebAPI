namespace OrderWebAPI.Models;

public class CategoryModel
{

    public int CategoryId { get; set; }
    public string? Service_Type { get; set; }
    
    public int OrderId { get; set; }

    public OrderModel OrderModel { get; set; }
}


