using SalesTaxApp.Models;
using SalesTaxCalculator;

namespace SalesTaxApp.Services.Interfaces;

public class BasicSalesTaxRule : ITaxRuleService
{
    private readonly List<string> _exemptKeywords = new() { InputConstants.BOOK, InputConstants.CHOCOLATE, InputConstants.PILL };

    public decimal CalculateTax(Product product)
    {
        if (_exemptKeywords.Any(k => product.Name!.Contains(k, StringComparison.OrdinalIgnoreCase)))
            return 0m;
        return ITaxRuleService.RoundUpToNearestPoint05(product.Price * 0.10m);
    }
}