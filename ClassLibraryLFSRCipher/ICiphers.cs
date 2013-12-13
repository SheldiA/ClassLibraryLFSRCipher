using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClassLibraryLFSRCipher
{
    public interface ICiphers
    {
        string AlgorithmName { get; set; }
        byte[] Encrypt(byte[] incomingBytes, object pb);
        byte[] Decrypt(byte[] incomingBytes, object pb);
    }
}
