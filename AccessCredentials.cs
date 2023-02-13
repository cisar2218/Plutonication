using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Security.Cryptography;

namespace Plutonication
{
    public class AccessCredentials
    {
        public string Address;
        public string Key;
        public string Name;
        public string Icon;

        public static string GenerateKey(int keyLen = 30)
        {
            // Create a string of characters, numbers, and special characters that are allowed in the password
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();

            // Select one random character at a time from the string
            // and create an array of chars
            char[] chars = new char[keyLen];
            for (int i = 0; i < keyLen; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }
        public Uri ToUri()
        {
            // plutonication:?url=192.168.0.1:8000&key=password123&name=Galaxy logic game&icon=http://rostislavlitovkin.pythonanywhere.com/logo
            var queryParams = HttpUtility.ParseQueryString(string.Empty);
            queryParams["url"] = Address;
            queryParams["key"] = Key;
            if (Name != null)
            {
                queryParams["name"] = Name;
            }
            if (Icon != null)
            {
                queryParams["icon"] = Icon;
            }
            UriBuilder builder = new UriBuilder();
            builder.Scheme = "plutonication";
            builder.Host = String.Empty;
            builder.Query = queryParams.ToString();
            Uri uri = builder.Uri;

            return uri;
        }
    }
}