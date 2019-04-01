using System;
using System.Collections.Generic;
// Create an alias to the Excel object model.
using Excel = Microsoft.Office.Interop.Excel;

namespace intro
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Car> carsInStock = new List<Car>
            {
                new Car {Color = "Green", Make = "VW", PetName = "Mary"},
                new Car {Color = "Red", Make = "Saab", PetName = "Mel"},
                new Car {Color = "Black", Make = "Ford", PetName = "Hank"},
                new Car {Color = "Yellow", Make = "BMW", PetName = "Davie"}
            };

            ExportToExcel(carsInStock);

            Console.WriteLine("Hello World!");
            Console.ReadKey();


        }

        static void ExportToExcel(List<Car> carsInStock)
        {
            // Load up Excel, then make a new empty workbook.
            Excel.Application excelApp = new Excel.Application();
            // Go ahead and make Excel visible on the computer.
            excelApp.Visible = true;
            excelApp.Workbooks.Add();
            // This example uses a single workSheet.
            Excel._Worksheet workSheet = excelApp.ActiveSheet;
            // Establish column headings in cells.
            workSheet.Cells[1, "A"] = "Make";
            workSheet.Cells[1, "B"] = "Color";
            workSheet.Cells[1, "C"] = "Pet Name";
            // Now, map all data in List<Car> to the cells of the spreadsheet.
            int row = 1;
            foreach (Car c in carsInStock)
            {
                row++;
                workSheet.Cells[row, "A"] = c.Make;
                workSheet.Cells[row, "B"] = c.Color;
                workSheet.Cells[row, "C"] = c.PetName;
            }

            // Give our table data a nice look and feel.
            workSheet.Range["A1"].AutoFormat(Excel.XlRangeAutoFormat.xlRangeAutoFormatClassic2);
            // Save the file, quit Excel, and display message to user.
            workSheet.SaveAs($@"{Environment.CurrentDirectory}\Inventory.xlsx");
            excelApp.Quit();
            Console.WriteLine("The Inventory.xslx file has been saved to your app folder");

        }
    }
}
