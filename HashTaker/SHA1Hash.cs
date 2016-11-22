using System;
using System.Collections.Generic;
using System.Text;

namespace HashTaker
{
    class SHA1Hash : IHash
    {
        string IHash.Hash(string fullFilePath)
        {
            return "0C2E99D0949684278C30B9369B82638E1CEAD415";
        }

    }
}
