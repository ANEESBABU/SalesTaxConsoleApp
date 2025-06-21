namespace SalesTaxApp.Models;

public class Receipt
{
    public List<Product> Products { get; set; } = new();

    public decimal Total => Products.Sum(p => p.PriceWithTax * p.Quantity);
    public decimal TotalTaxes => Products.Sum(p => p.Tax * p.Quantity);

    public void Print()
    {
        System.Console.WriteLine("\n\n---------------------Reciept---------------------\n");
        foreach (var product in Products)
        {
            Console.WriteLine($"{product.Quantity} {product.Name}: {(product.PriceWithTax * product.Quantity):0.00}");
        }
        Console.WriteLine($"Sales Taxes: {TotalTaxes:0.00}");
        Console.WriteLine($"Total: {Total:0.00}\n");
    }
}