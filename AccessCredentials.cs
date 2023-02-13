using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Security.Cryptography;

namespace Plutonication
{
    public sealed class AccessCredentials
    {
        public string Address {get;set;}
        public string Key {get;}

        public string Name {get;set;} // optional
        public string Icon {get;set;} // optional
        public AccessCredentials(string address)
        {
            if (address == null) {
                throw new Exception("Given address is null.");
            }
            Address = address;
            Key = AccessCredentials.GenerateKey();
        }
        public AccessCredentials(string address, string name) : this(address)
        {
            Name = name;
        }
        public AccessCredentials(string address, string name, string icon) : this(address, name)
        {
            Icon = icon;
        }
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