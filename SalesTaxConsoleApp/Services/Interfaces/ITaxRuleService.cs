
using SalesTaxApp.Models;

namespace SalesTaxApp.Services.Interfaces;

public interface ITaxRuleService
{
    List<Product> CalculateTax(List<Product> product, string CountryCode);

    protected static decimal RoundUpToNearestPoint05(decimal value)
    {
        return Math.Ceiling(value * 20) / 20;
    }
}
