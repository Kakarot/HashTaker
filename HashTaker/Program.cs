using HashTaker;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HashTaker
{
    class Program
    {
        static int Main(string[] args)
		{
            try
            {
                HashTakerConfiguration htc = new HashTakerConfiguration();
                Scripter scripter = new Scripter();

                //Get list of locations to perform Hash
                //TODO allow replacement of hashing algorithm

                IList<string> locations = htc.GetValuesFromXML("location");
                StringBuilder sb = new StringBuilder();

				IHash algorithm = htc.DetermineAlgorithmToUse();
				if (algorithm == null)
				{
					throw new NullReferenceException("hash algorithm was null after reading XML configuration."+
					                                 "Check HashTaker.xml.");
				}

                //If HashTaker.xml is set to hash the current directory, perform this logic 
                //This assembly will not take the hash of itself when using the current directory.
                if (htc.UseCurrentDirectory())
                {
                    sb.Append(scripter.GenerateCSVHash(htc.GetListOfBuiltFiles(Directory.GetCurrentDirectory()), algorithm));
#if DEBUG
                    ConsoleGreen(@"--Files in Current Directory--");
                    foreach (var f in htc.GetListOfBuiltFiles(Directory.GetCurrentDirectory()))
                    {
                        ConsoleWhite(f.Name);
                    }
                    Console.WriteLine();
#endif
                }


                //Flush CSV string to disk as a '.csv' file.
                scripter.WriteFile("Hash.csv", sb); 
            }
            catch (Exception ex)
            {
                //When an exception occurs, the hash file is not generated. 

                //We write a local text file containg the error
                var errorMessage = "Error, text file not generated.\n" +
                " An exception was triggered with the message: " + ex.Message;
                File.WriteAllText(Directory.GetCurrentDirectory() + @"\" + "ErrorHashTakerReport.txt", errorMessage);
                 return -1;
            }
            return 0;
           
        }

        //Static helper method for helpful Console color coded debugging in debug builds
        private static void ConsoleGreen(string outputToConsole)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(outputToConsole);
            Console.ResetColor();
        }

        //Static helper method for helpful Console color coded debugging in debug builds
        private static void ConsoleWhite(string outputToConsole)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(outputToConsole);
            Console.ResetColor();
        }

    }

}

