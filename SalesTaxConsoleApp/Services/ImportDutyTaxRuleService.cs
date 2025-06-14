using SalesTaxApp.Models;
using SalesTaxApp.Services.Interfaces;
using SalesTaxCalculator;

namespace SalesTaxApp.Services.Interfaces;

public class ImportDutyTaxRule : ITaxRuleService
{
    public decimal CalculateTax(Product product)
    {
        if (product.Name!.Contains(InputConstants.IMPORTED, StringComparison.OrdinalIgnoreCase))
            return ITaxRuleService.RoundUpToNearestPoint05(product.Price * 0.05m);
        return 0m;
    }
}
