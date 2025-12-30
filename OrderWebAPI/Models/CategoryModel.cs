using System.Text.Json.Serialization;

namespace OrderWebAPI.Models;

public class CategoryModel
{

    public int CategoryId { get; set; }
    public string? Service_Type { get; set; }

    [JsonIgnore]
    public ICollection<OrderModel>? OrderModels { get; set; }

    
}


