# SalesTaxConsoleApp

This is a console application designed to perform sales tax calculations for a predefined set of products. It demonstrates core principles of calculating basic sales tax and import duty based on specific rules.

## How to Run

To run the application and see the tax calculations for the pre-configured products:

1.  **Navigate to the project directory:**
    Open your terminal or command prompt and change to the `SalesTaxConsoleApp` directory (which is inside your `SalesTax` parent folder).
    ```bash
    cd SalesTaxConsoleApp
    ```

2.  **Run the application:**
    ```bash
    dotnet run
    ```
    The application will execute, process the product inputs, and display the calculated taxes and total amounts on the console.

## Customizing Product Data

If you wish to change the products for which tax is calculated, or modify their details:

1.  **Locate the input file:**
    Navigate to the `Constants` folder within your main application project:
    `SalesTaxConsoleApp/Constants/InputConstants.cs`

2.  **Modify the `InputConstants.cs` file:**
    Open this file in your preferred code editor. You can directly edit the `const string` variables or `static` fields that define the product input data.

3.  **Save and re-run:**
    After making your changes, save the file and run the application again using `dotnet run` from the `SalesTaxConsoleApp` directory to see the new calculations.

## Project Structure

This solution is organized into two main projects:

* **`SalesTaxConsoleApp`**: Contains the core logic for parsing input, applying tax rules, and calculating totals.
* **`SalesTaxConsoleApp.Tests`**: Houses the unit tests for the `SalesTaxConsoleApp` to ensure the correctness of tax calculations and parsing logic.
