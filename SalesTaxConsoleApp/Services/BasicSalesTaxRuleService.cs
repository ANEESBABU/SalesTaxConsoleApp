using System.Security.Cryptography.X509Certificates;
using SalesTaxApp.Models;
using SalesTaxCalculator;
using SalesTaxConsoleApp.Utilities;

namespace SalesTaxApp.Services.Interfaces;

public class TaxRuleService : ITaxRuleService
{
    private readonly List<string> _exemptKeywords = new() { InputConstants.BOOK, InputConstants.CHOCOLATE, InputConstants.PILL };

    public List<Product> CalculateTax(List<Product> products, string CountryCode)
    {

        var (SalesTax, ImportTax) = TaxRates.GetRates(CountryCode);

        foreach (var product in products)
        {
            decimal totalTaxRate = 0m;
            if (product.Name!.Contains(InputConstants.IMPORTED, StringComparison.OrdinalIgnoreCase))
                totalTaxRate += ImportTax;
            if (!_exemptKeywords.Any(k => product.Name!.Contains(k, StringComparison.OrdinalIgnoreCase)))
                totalTaxRate += SalesTax;
            product.Tax = ITaxRuleService.RoundUpToNearestPoint05(product.Price * totalTaxRate);
        }

        return products;
    }
}