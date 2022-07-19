using System;

// encodeList
using System.Collections.Generic;
using System.IO;
using System.Linq;

//https://dotnetfiddle.net/
//https://graphicore.github.io/librebarcode/documentation/code128.html
//https://fonts.google.com/specimen/Libre+Barcode+128?preview.text=%C3%8CPKT%C3%87%C3%874%C3%82%C3%82s@%C3%880X%C3%8E&preview.text_type=custom
//https://en.m.wikipedia.org/wiki/Code_128

namespace barcode_scanner
{
    public class BarcodeValue
    {
        public static int inputLength;         // If the characters from checksumIndex are numeric, then indexChar = -1
        public static int indexChar;           // Defines the value used as a representation of the inputValue in the checksumValue for each Index
        public static int replacedValue;       // Defines the position of the values being replaced by the checksum
                                               // Every value for subset B, every second value for subset C
        public static int checksumIndex;       // Stores the checksum value
        public static int checksumValue;       // Stores the full encoded string
        public static bool isSubsetB;          // Defines if using the barcode 128 subset B
        public static String encodedValue;
        public static String inputValue;
        public static String outputCode;
        public static string Main(String inputCode)
        {
        inputLength = 0;
        indexChar = 0;
        replacedValue = 0;
        checksumIndex = 0;
        checksumValue = 0;
        isSubsetB = true;
        encodedValue = "";

        inputValue = inputCode;
            //inputValue = "PKT20000083320";
            outputCode = BarcodeValue.Encode();
            //Console.WriteLine(outputCode);
            return outputCode;
        }
        public static String Encode()
        {
            inputLength = inputValue.Length;

            while (checksumIndex < inputLength)
            {
                // Execute if subset B encoding one character at a time
                if (isSubsetB == true)
                {
                    BarcodeValue.encodeSubsetB();
                }
                // Execute if subset C encoding two characters at a time
                if (isSubsetB == false)
                {
                    BarcodeValue.encodeSubsetC();
                }

                if (isSubsetB == true)
                {
                    encodedValue = encodedValue + inputValue[checksumIndex];
                    checksumIndex = checksumIndex + 1;
                }
            }

            outputCode = BarcodeValue.encodeFinalValue();
            return outputCode;
        }
        public static void encodeSubsetB()
        {
            if ((checksumIndex == 0) || (checksumIndex + 3 == inputLength - 1))
            {
                indexChar = 4;
            }
            else
            {
                indexChar = 6;
            }
            indexChar = indexChar - 1;

            // 
            if ((checksumIndex + indexChar) <= inputLength - 1)
            {
                while (indexChar >= 0)
                {
                    if ((inputValue[checksumIndex + indexChar] < 48) || (inputValue[checksumIndex + indexChar] > 57))
                    {

                        break;
                    }
                    indexChar = indexChar - 1;
                }
            }

            if (indexChar < 0)
            {
                //Check if we are at the first character position
                if (checksumIndex == 0)
                {
                    // Writes encoded Start character
                    encodedValue = Char.ToString((char)205);
                }
                else
                {
                    // Writes encoded Start character for Subset C if its not the beginning of the code
                    encodedValue = encodedValue + Char.ToString((char)199);
                }
                isSubsetB = false;
            }
            // Writes encoded Start character
            else
            if (checksumIndex == 0)
            {
                encodedValue = Char.ToString((char)204);
            }
        }
        public static void encodeSubsetC()
        {
            indexChar = 1;
            if (checksumIndex + indexChar < inputLength)
            {
                while (indexChar >= 0)
                {
                    if (((inputValue[checksumIndex + indexChar]) < 48) || ((inputValue[checksumIndex]) > 57))
                    {
                        break;
                    }
                    indexChar = indexChar - 1;
                }
            }

            // Check if we have 2 values use subset C
            if (indexChar < 0)
            {

                replacedValue = Int32.Parse(inputValue.Substring(checksumIndex, 2));

                if (replacedValue < 95)
                {
                    replacedValue = replacedValue + 32;
                }
                else
                {
                    replacedValue = replacedValue + 100;
                }
                // Change to character 194 if using 00
                if (inputValue.Substring(checksumIndex, 2) == "00")
                {
                    encodedValue = encodedValue + (char)194;
                }
                else
                {
                    encodedValue = encodedValue + (char)(replacedValue);
                }

                checksumIndex = checksumIndex + 2;
            }
            // We only have 1 value, change back to subset B
            else
            {
                encodedValue = encodedValue + Char.ToString((char)200);
                isSubsetB = true;
            }
        }
        public static String encodeFinalValue()
        {
            // Writes last encoded value and stop character  
            for (checksumIndex = 0; checksumIndex <= encodedValue.Length - 1; checksumIndex++)
            {
                replacedValue = encodedValue[checksumIndex];

                if (replacedValue == 194)
                {
                    replacedValue = 32;
                }
                if (replacedValue < 127)
                {
                    replacedValue = replacedValue - 32;
                }
                else
                {
                    replacedValue = replacedValue - 100;
                }
                if (checksumIndex == 0)
                {
                    checksumValue = replacedValue;
                }

                checksumValue = (checksumValue + (checksumIndex) * replacedValue) % 103;
            }
            if (checksumValue < 95)
            {
                checksumValue = checksumValue + 32;
            }
            else
            {
                checksumValue = checksumValue + 100;
            }
            encodedValue = encodedValue + Char.ToString((char)checksumValue)
                    + Char.ToString((char)206);

            return encodedValue;
        }
    }
}
