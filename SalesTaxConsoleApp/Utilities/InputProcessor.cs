using System.Globalization;
using System.Text.RegularExpressions;
using SalesTaxApp.Models;

namespace MyApp.Utilities;

public static class InputProcessor
{
    public static List<Product> ProcessInput(string input)
    {
        List<Product> products = new();
        var inputList = input.Split(",");
        foreach (var items in inputList)
        {
            var match = Regex.Match(items, @"(\d+) (.+) at (\d+\.\d{2})");
            if (!match.Success) throw new ArgumentException("Invalid input format.");

            int quantity = int.Parse(match.Groups[1].Value);
            string name = match.Groups[2].Value.Trim();
            decimal price = decimal.Parse(match.Groups[3].Value, CultureInfo.InvariantCulture);

            var product = new Product { Quantity = quantity, Name = name, Price = price };
            products.Add(product);

            Console.WriteLine($"Product Name:{product.Name}");
        }
        return products;
    }
}