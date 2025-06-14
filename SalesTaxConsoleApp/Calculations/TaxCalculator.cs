using System.Globalization;
using System.Text.RegularExpressions;
using SalesTaxApp.Models;
using SalesTaxApp.Services.Interfaces;

namespace SalesTaxApp.Calculations;

public class TaxCalculator
{
    private readonly List<ITaxRuleService> _taxRules;

    public TaxCalculator(List<ITaxRuleService> taxRules)
    {
        _taxRules = taxRules;
    }

    public Product ParseAndCalculate(string input)
    {
        var match = Regex.Match(input, @"(\d+) (.+) at (\d+\.\d{2})");
        if (!match.Success) throw new ArgumentException("Invalid input format.");

        int quantity = int.Parse(match.Groups[1].Value);
        string name = match.Groups[2].Value.Trim();
        decimal price = decimal.Parse(match.Groups[3].Value, CultureInfo.InvariantCulture);

        var product = new Product { Quantity = quantity, Name = name, Price = price };
        product.Tax = _taxRules.Sum(rule => rule.CalculateTax(product));

        return product;
    }
}