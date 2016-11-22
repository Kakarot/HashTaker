using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HashTaker
{
    /*
     *The purpose of the Scripter class is to create the physical file
     * on containing hash values in CSV, XML, SQL, etc.
     */

    public class Scripter
    {
       

        /// <summary>
        /// Writes a text file (based on fileName extension) to the current directory
        /// </summary>
        public void WriteFile(string fileName, StringBuilder fileContents)
        {
            if (String.IsNullOrEmpty(fileName))
            {
                var errorMessage = "fileName was invalid. Either null or empty.";

                throw new ArgumentException(errorMessage);
            }
            if (fileContents == null)
            {
                var errorMessage = "Stringbuilder fileContents was null.";
                throw new ArgumentNullException(errorMessage);
            }
            if (String.IsNullOrEmpty(fileContents.ToString()))
            {
                var errorMessage = "string inside fileContents was invalid. Either null or empty.";
                throw new ArgumentException(errorMessage);
            }

            File.WriteAllText(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + fileName, fileContents.ToString());
        }

        /// <summary>
        /// Takes in IEnumerable of files, and hashes each file and creates a CSV string.
        /// </summary>
        /// <returns>StringBuilder of CSV</returns>
        public StringBuilder GenerateCSVHash(IEnumerable<FileInfo> files, IHash injectedHashingAlgorithm)
        {
            if (files == null)
            {
                var errorMessage = "Null enumeration of 'files' were passed to be Hash'd. Check XML file.";
                throw new ArgumentNullException(errorMessage);
            }
            if (!files.Any())
            {              
                return new StringBuilder("-- No files to hash passed in to GenerateCSVHash()").AppendLine();
            }

            StringBuilder sb = new StringBuilder("");
            IHash hashAlgorithm = injectedHashingAlgorithm;
            String hashText = String.Empty;
            foreach (var f in files)
            {
                try
                {
                    hashText = hashAlgorithm.Hash(f.FullName);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                sb.AppendLine(f.Name+","+ hashText);
            }
            sb.AppendLine();
            return sb;
        }
        

    }
}
