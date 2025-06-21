using System.Globalization;
using System.Text.RegularExpressions;
using MyApp.Utilities;
using SalesTaxApp.Models;
using SalesTaxApp.Services.Interfaces;
using SalesTaxCalculator;

ITaxRuleService taxRule = new TaxRuleService();
var receipt = new Receipt();

System.Console.WriteLine("Enter the products data in comma seperated line:");
string? inputData = Console.ReadLine();

while (inputData!.ToLower() != "done")
{
    System.Console.Write("Enter the Country Code(IND, UAE, US):");
    var CountryCode = Console.ReadLine();


    var productsList = InputProcessor.ProcessInput(inputData!);
    productsList = taxRule.CalculateTax(productsList, CountryCode!);
    receipt.Products = productsList;
    receipt.Print();
    
    System.Console.WriteLine("Enter the products data in comma seperated line:");
    inputData = Console.ReadLine();
}