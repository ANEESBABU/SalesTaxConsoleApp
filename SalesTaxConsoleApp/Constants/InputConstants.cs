namespace SalesTaxCalculator;

public static class InputConstants
{
    public const string IMPORTED = "imported";
    public const string BOOK = "book";
    public const string CHOCOLATE = "chocolate";
    public const string PILL = "pill";

    public static List<List<string>> InputBaskets = new List<List<string>>
    {
        new List<string>
        {
            "1 book at 12.49",
            "1 music CD at 14.99",
            "1 chocolate bar at 0.85"
        },
        new List<string>
        {
            "1 imported box of chocolates at 10.00",
            "1 imported bottle of perfume at 47.50"
        },
        new List<string>
        {
            "1 imported bottle of perfume at 27.99",
            "1 bottle of perfume at 18.99",
            "1 packet of headache pills at 9.75",
            "1 box of imported chocolates at 11.25"
        }
    };

}