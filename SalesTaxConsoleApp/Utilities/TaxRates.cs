namespace SalesTaxConsoleApp.Utilities;

public static class TaxRates
{
    public static readonly Dictionary<string, (decimal SalesTax, decimal ImportTax)> CountryTaxRates = new ()
    {
        { "IND", (0.10m, 0.05m) },
        { "UAE",   (0.00m, 0.05m) },
        { "US",    (0.08m, 0.10m) }
    };

    public static (decimal SalesTax, decimal ImportTax) GetRates(string country)
    {
        return CountryTaxRates.ContainsKey(country)
            ? CountryTaxRates[country]
            : (0.0m, 0.0m);
    }
}
