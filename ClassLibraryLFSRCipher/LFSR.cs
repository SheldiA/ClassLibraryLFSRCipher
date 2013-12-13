using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.Serialization;

namespace ClassLibraryLFSRCipher
{
    public class LFSR : KeyGenerator,ICiphers
    {
        private readonly byte[] polinomial = { 1, 3, 4, 24 };
        private readonly byte polinomial_degree = 24;

        private char[] password;
        private char[] changed_password;
        public string AlgorithmName { get; set; }
        private delegate void ProggressBarWork(ProgressBar pb);

        public LFSR(byte[] passwordBytes)
        {
            string strPassword = GetBinaryPassword(passwordBytes);
            this.password = strPassword.ToCharArray(0, polinomial_degree);
            changed_password = strPassword.ToCharArray(0, polinomial_degree);
            AlgorithmName = "LFSR";
        }
        
        public byte[] Encrypt(byte[] incomingMessage,object pb)
        {
            byte[] resultMessage = new byte[incomingMessage.Length];
            byte byteKey;
            ProgressBar pbar = pb as ProgressBar;            
            for (int i = 0; i < incomingMessage.Length; ++i)
            {
                byteKey = GetNextByte(changed_password,polinomial);
                resultMessage[i] = (byte)(incomingMessage[i] ^ byteKey);
                pbar.Invoke(new ProggressBarWork((p) => p.PerformStep()), pbar); 
            }

            return resultMessage;
        }

        public byte[] Decrypt(byte[] incomingBytes, object pb)
        {
            return Encrypt(incomingBytes,pb);
        }

        private string GetBinaryPassword(byte[] data)
        {
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; ++i)
                sBuilder.Append(Convert.ToString(data[i], 2));
            return sBuilder.ToString();
        }
        
    }
}
