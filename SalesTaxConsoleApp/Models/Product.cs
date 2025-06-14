namespace SalesTaxApp.Models;

public class Product
{
    public int Quantity { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public decimal Tax { get; set; }
    public decimal PriceWithTax => Price + Tax;
}