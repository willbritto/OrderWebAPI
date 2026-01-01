using System.Text.Json.Serialization;

namespace OrderWebAPI.Models;

public class OrderModel
{
    public int OrderId { get; set; }
    public int NumOrder { get; set; }

    public string NameFull { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }
    public StatusOrderService Status { get; set; }

    public int CategoryId { get; set; }

    [JsonIgnore]
    public CategoryModel? CategoryModel { get; set; }

}

public enum StatusOrderService
{
    Ausente,
    Em_Andamento,
    Concluido
}