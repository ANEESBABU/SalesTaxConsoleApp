
using SalesTaxApp.Models;

namespace SalesTaxApp.Services.Interfaces;

public interface ITaxRuleService
{
    decimal CalculateTax(Product product);

    protected static decimal RoundUpToNearestPoint05(decimal value)
    {
        return Math.Ceiling(value * 20) / 20;
    }
}
