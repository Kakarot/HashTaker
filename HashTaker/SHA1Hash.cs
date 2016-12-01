using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HashTaker
{
    class SHA1Hash : IHash
    {
        string IHash.Hash(string fullFilePath)
        {
            string strResult = "";
            string strHashData = "";


            byte[] arrbytHashValue;
            System.IO.FileStream oFileStream = null;

            var oSHA1Hasher = System.Security.Cryptography.SHA1.Create();

            try
            {
                if (File.Exists(fullFilePath))
                {
                    oFileStream = GetFileStream(fullFilePath);
                    arrbytHashValue = oSHA1Hasher.ComputeHash(oFileStream);
                    oFileStream.Dispose();

                    strHashData = System.BitConverter.ToString(arrbytHashValue);
                    strHashData = strHashData.Replace("-", "");
                    strResult = strHashData;
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception("Exception calculating SHA1 signature: " + ex.Message);
            }
            return (strResult);
        }


        public System.IO.FileStream GetFileStream(string pathName)
        {
            return (new System.IO.FileStream(pathName, System.IO.FileMode.Open,
                      System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite));
        }
    }
}
