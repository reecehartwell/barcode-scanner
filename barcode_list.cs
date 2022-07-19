using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace barcode_scanner
{
    internal class barcode_list
    {
        //static void Main(string[] args)
        static void Main()
        {

            // Set file path using value entered into the command line
            //String filePath = System.IO.Path.GetFullPath(Directory.GetCurrentDirectory() + @"\" + args[0]);

            //WORKING:
            //String filePath = System.IO.Path.GetFullPath(Directory.GetCurrentDirectory() + @"\list_barcodes.txt");
            String filePath = "\\Development\\C#\\barcode-scanner\\barcode-scanner\\list_barcodes.txt";

            // Print the filepath
            Console.WriteLine("File Path is: " + filePath);

            Data dataObj = new Data();
            // Save each line from the text file into an array using the filepath
            //List<string> dataList = dataObj.ReadNames(args[0]);
            List<string> dataList = dataObj.ReadNames(filePath);

            List<string> encodedList = new List<string>();

            // Loop through the list writing each entry to the console
            foreach (var line in dataList)
            {
                Console.WriteLine(line.ToString());
                encodedList.Add(barcode_scanner.BarcodeValue.Main(line.ToString()));
                Console.WriteLine(barcode_scanner.BarcodeValue.Main(line.ToString()));
            }

            // Create the output file from the list of sorted names
            File.WriteAllLines("encoded_barcodes.txt", encodedList);
        }
    }
}