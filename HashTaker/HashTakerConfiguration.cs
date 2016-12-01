using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace HashTaker
{
    class HashTakerConfiguration
    {
        /// <summary>
        /// This method grabs recently built files within the passed in directory
        /// relative to the current directory. This logic prevents taking the
        /// hash of itself.
        /// </summary>
        /// <returns>IEnumerable FileInfo</returns>
        public IEnumerable<FileInfo> GetListOfBuiltFiles(string path)
        {
            var extensions = this.GetValuesFromXML("extension");
            var dir = new DirectoryInfo(path);
            IEnumerable<FileInfo> currentFiles = dir.EnumerateFiles();
            return currentFiles.Where(i => extensions.Contains(i.Extension)
                 && i.Name != System.Reflection.Assembly.GetEntryAssembly().GetName().Name + ".exe"
                 && i.Name != System.Reflection.Assembly.GetEntryAssembly().GetName().Name + ".dll");
        }

        /// <summary>
        /// Grabs a list of values from the XML element passed in.
        /// </summary>
        /// <returns>IList listOfExtensions</returns>
        public IList<string> GetValuesFromXML(string element)
        {
            IList<string> listOfValues = new List<string>();
            string path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            XDocument xmlHashTakerDocument = XDocument.Load(path + Path.DirectorySeparatorChar+@"HashTaker.xml");
            var xElementValue = xmlHashTakerDocument.Descendants(element);
            if (xElementValue != null && xElementValue.Any())
            {
                foreach (var e in xElementValue)
                {
                    listOfValues.Add(e.Value);
                }
            }
            return listOfValues;
        }


        /// <summary>
        /// Determines if flag is set to hash the current directory
        /// </summary>
        /// <returns>IList locations</returns>
        public Boolean UseCurrentDirectory()
        {
            IList<string> listOfLocations = new List<string>();
            string path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            XDocument xmlHashTakerDocument = XDocument.Load(path + Path.DirectorySeparatorChar+@"HashTaker.xml");
            var xElementLocation = xmlHashTakerDocument.Descendants("Locations");
            string useLocation = String.Empty;
            if (xElementLocation != null && xElementLocation.Any())
            {

                foreach (var e in xElementLocation)
                {
                    useLocation = e.FirstAttribute.Value;
                }
            }
            return string.Equals(useLocation, "yes", StringComparison.OrdinalIgnoreCase);
        }

		public IHash DetermineAlgorithmToUse()
		{
			IList<string> algorithms = this.GetValuesFromXML("Algorithm");
			string algorithm = algorithms[0];
			IHash algorithmObject = null;
			if(algorithm.Equals("sha1",StringComparison.CurrentCultureIgnoreCase))
			{
				algorithmObject = new SHA1Hash();
		    }
			return algorithmObject;
		}
    }
}
