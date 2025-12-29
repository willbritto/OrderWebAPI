namespace OrderWebAPI.Models;

public class OrderModel
{
    public int OrderId { get; set; }
    public int NumOrder { get; set; }

    public string? Descricao { get; set; }

    public decimal Preco { get; set; }


    public CategoryModel CategoryModel { get; set; }


}
