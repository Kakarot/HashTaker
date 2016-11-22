using System;
using System.Collections.Generic;
using System.Text;

namespace HashTaker
{
    /*
     * Make every algorithm class implement this interface so that we can swap out hashing algorithms
     */
    public interface IHash
    {
        string Hash(string fullFilePath);
    }
}
