using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Data {

    public List<string> ReadNames(string filePath)
    {
        //String filePath = System.IO.Path.GetFullPath(Directory.GetCurrentDirectory() + @"\" + fileName);

        // Print the filepath
        Console.WriteLine("File Path is: " + filePath);

        return System.IO.File.ReadAllLines(filePath).ToList();
    }

}