using System;
using System.Text;

namespace Application
{
    public class Testcode
    {
        public static string Decrypt(string koppelingkey)
        {
            //decrypt de koppelingkey van Base64 naar UTF8
            string decodedkey;
            decodedkey = Encoding.UTF8.GetString(Convert.FromBase64String(koppelingkey));
            return decodedkey;
        }
    }
}

