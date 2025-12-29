namespace OrderWebAPI.Models;

public class OrderModel
{
    public int OrderId { get; set; }
    public int NumOrder { get; set; }

    public string? Descricao { get; set; }

    public decimal Preco { get; set; }
    public StatusOrderService Status { get; set; }

    
    public ICollection<CategoryModel> CategoryModel { get; set; }


}


public enum StatusOrderService
{
    Ausente,
    Em_Andamento,
    Concluido
}