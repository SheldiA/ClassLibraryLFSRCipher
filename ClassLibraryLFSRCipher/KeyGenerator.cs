using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryLFSRCipher
{
    public class KeyGenerator : MarshalByRefObject
    {
        protected byte GetNextByte(char[] changed_password, byte[] polinomial)
        {
            string currentKey = "";
            currentKey += changed_password[0];
            char first_symbol;
            while (currentKey.Length < 8)
            {
                first_symbol = Xor(changed_password, polinomial);
                Shl(changed_password);
                changed_password[changed_password.Length - 1] = first_symbol;
                currentKey += changed_password[0];
            }
            return Convert.ToByte(currentKey, 2);
        }

        protected void Shl(char[] changed_password)
        {
            for (int i = 0; i < changed_password.Length - 1; ++i)
                changed_password[i] = changed_password[i + 1];

        }

        protected char Xor(char[] changed_password, byte[] polinomial)
        {
            char xor_result;
            xor_result = changed_password[changed_password.Length - polinomial[0]];
            for (int i = 1; i < polinomial.Length; ++i)
            {
                if (xor_result == changed_password[changed_password.Length - polinomial[i]])
                    xor_result = '0';
                else
                    xor_result = '1';
            }
            return xor_result;
        }
    }
}
