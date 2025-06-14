using SalesTaxApp.Calculations;
using SalesTaxApp.Models;
using SalesTaxApp.Services.Interfaces;
using SalesTaxCalculator;

var taxRules = new List<ITaxRuleService> { new BasicSalesTaxRule(), new ImportDutyTaxRule() };
var taxCalculator = new TaxCalculator(taxRules);

var inputBaskets = InputConstants.InputBaskets;

int count = 1;
foreach (var basket in inputBaskets)
{
    Console.WriteLine($"Output {count++}:");
    var receipt = new Receipt();
    foreach (var item in basket)
    {
        var product = taxCalculator.ParseAndCalculate(item);
        receipt.Products.Add(product);
    }
    receipt.Print();
}