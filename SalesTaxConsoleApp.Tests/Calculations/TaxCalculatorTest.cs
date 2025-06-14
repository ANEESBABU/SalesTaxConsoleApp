using Moq; 
using SalesTaxApp.Models;
using SalesTaxApp.Services.Interfaces;
using SalesTaxApp.Calculations;

namespace SalesTaxConsoleApp.Tests.Calculations;

public class TaxCalculatorTests
{
    private const string INPUT_INVALID_FORMAT_MESSAGE = "Invalid input format.";
    // Test case 1: Valid input string, single tax rule, tax applied
    [Fact]
    public void ParseAndCalculate_ValidInput_AppliesSingleTaxRuleCorrectly()
    {
        // Arrange
        var mockTaxRule = new Mock<ITaxRuleService>();
        // Configure the mock to return a specific tax amount (e.g., 10% of price)
        mockTaxRule.Setup(r => r.CalculateTax(It.IsAny<Product>()))
                    .Returns((Product p) => p.Price * 0.10M); // Returns 10% tax

        var taxCalculator = new TaxCalculator(new List<ITaxRuleService> { mockTaxRule.Object });
        string input = "1 book at 10.00";

        // Act
        Product result = taxCalculator.ParseAndCalculate(input);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Quantity);
        Assert.Equal("book", result.Name);
        Assert.Equal(10.00M, result.Price);
        Assert.Equal(1.00M, result.Tax); // 10.00 * 0.10
    }

    // Test case 2: Valid input string, multiple tax rules, taxes sum up
    [Fact]
    public void ParseAndCalculate_ValidInput_AppliesMultipleTaxRulesCorrectly()
    {
        // Arrange
        var mockBasicTaxRule = new Mock<ITaxRuleService>();
        mockBasicTaxRule.Setup(r => r.CalculateTax(It.IsAny<Product>()))
                        .Returns((Product p) => p.Price * 0.10M); // 10% basic tax

        var mockImportTaxRule = new Mock<ITaxRuleService>();
        mockImportTaxRule.Setup(r => r.CalculateTax(It.IsAny<Product>()))
                        .Returns((Product p) => p.Price * 0.05M); // 5% import tax

        var taxCalculator = new TaxCalculator(new List<ITaxRuleService> { mockBasicTaxRule.Object, mockImportTaxRule.Object });
        string input = "1 imported bottle of perfume at 10.00";

        // Act
        Product result = taxCalculator.ParseAndCalculate(input);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Quantity);
        Assert.Equal("imported bottle of perfume", result.Name);
        Assert.Equal(10.00M, result.Price);
        Assert.Equal(1.50M, result.Tax); // (10.00 * 0.10) + (10.00 * 0.05) = 1.00 + 0.50
    }

    // Test case 3: Valid input, no tax rules, tax should be zero
    [Fact]
    public void ParseAndCalculate_NoTaxRules_TaxIsZero()
    {
        // Arrange
        var taxCalculator = new TaxCalculator(new List<ITaxRuleService>()); // Empty list of rules
        string input = "1 book at 12.49";

        // Act
        Product result = taxCalculator.ParseAndCalculate(input);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(0.00M, result.Tax);
    }

    // Test case 4: Invalid input format - missing quantity
    [Fact]
    public void ParseAndCalculate_InvalidInput_ThrowsArgumentException_MissingQuantity()
    {
        // Arrange
        var taxCalculator = new TaxCalculator(new List<ITaxRuleService>());
        string input = "book at 10.00";

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => taxCalculator.ParseAndCalculate(input));
        Assert.Equal(INPUT_INVALID_FORMAT_MESSAGE, exception.Message);
    }

    // Test case 5: Invalid input format - missing price
    [Fact]
    public void ParseAndCalculate_InvalidInput_ThrowsArgumentException_MissingPrice()
    {
        // Arrange
        var taxCalculator = new TaxCalculator(new List<ITaxRuleService>());
        string input = "1 book at ";

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => taxCalculator.ParseAndCalculate(input));
        Assert.Equal(INPUT_INVALID_FORMAT_MESSAGE, exception.Message);
    }

    // Test case 6: Product name contains "at"
    [Fact]
    public void ParseAndCalculate_ProductNameContainsAt_ParsesCorrectly()
    {
        // Arrange
        var mockTaxRule = new Mock<ITaxRuleService>();
        mockTaxRule.Setup(r => r.CalculateTax(It.IsAny<Product>()))
                    .Returns(0M); // No tax for simplicity in this test

        var taxCalculator = new TaxCalculator(new List<ITaxRuleService> { mockTaxRule.Object });
        string input = "1 box of chocolates at home at 15.00"; // Product name has "at"

        // Act
        Product result = taxCalculator.ParseAndCalculate(input);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Quantity);
        Assert.Equal("box of chocolates at home", result.Name); // Ensure full name is captured
        Assert.Equal(15.00M, result.Price);
        Assert.Equal(0.00M, result.Tax);
    }

    // Test case 8: Zero quantity
    [Fact]
    public void ParseAndCalculate_ZeroQuantity_ParsesCorrectly()
    {
        // Arrange
        var taxCalculator = new TaxCalculator(new List<ITaxRuleService>());
        string input = "0 items at 10.00";

        // Act
        Product result = taxCalculator.ParseAndCalculate(input);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(0, result.Quantity);
    }

    // Test case 9: Price with leading zeros
    [Fact]
    public void ParseAndCalculate_PriceWithLeadingZeros_ParsesCorrectly()
    {
        // Arrange
        var taxCalculator = new TaxCalculator(new List<ITaxRuleService>());
        string input = "1 item at 005.75";

        // Act
        Product result = taxCalculator.ParseAndCalculate(input);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(5.75M, result.Price);
    }
}