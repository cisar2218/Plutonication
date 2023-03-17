using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.Collections.Specialized;

namespace Plutonication
{
    public sealed class AccessCredentials
    {
        public string Address { get; set; }
        public int Port { get; set; }
        public string Key { get; set; }

        public string Name { get; set; } // optional
        public string Icon { get; set; } // optional

        public const string QUERY_PARAM_URL = "url";
        public const string QUERY_PARAM_KEY = "key";
        public const string QUERY_PARAM_NAME = "name";
        public const string QUERY_PARAM_ICON = "icon";
        public AccessCredentials(Uri uri)
        {
            if (uri == null) { throw new ArgumentNullException(); }

            NameValueCollection queryParams = HttpUtility.ParseQueryString(uri.Query);

            string url = queryParams[QUERY_PARAM_URL] ?? throw InvalidUrlParam(QUERY_PARAM_URL);
            string[] ipPort = url.Split(":");
            Address = ipPort[0];
            Port = Int32.Parse(ipPort[1]);

            Key = queryParams[QUERY_PARAM_KEY] ?? throw InvalidUrlParam(QUERY_PARAM_KEY);

            try
            { // optional param
                Name = queryParams[QUERY_PARAM_NAME] ?? throw InvalidUrlParam(QUERY_PARAM_NAME);
            }
            catch { }
            try
            { // optional param
                Icon = queryParams[QUERY_PARAM_ICON] ?? throw InvalidUrlParam(QUERY_PARAM_ICON);
            }
            catch { }

            Exception InvalidUrlParam(string nameOfParam)
            {
                return new Exception($"{nameOfParam} url param is value.");
            }
        }
        public AccessCredentials(IPAddress address, int port)
        {
            if (address == null)
            {
                throw new Exception("Given address is null.");
            }
            Address = address.ToString();
            Port = port;
            Key = AccessCredentials.GenerateKey();
        }
        public AccessCredentials(IPAddress address, int port, string key) : this(address, port)
        {
            Key = key;
        }
        public AccessCredentials(IPAddress address, int port, string name, string icon) : this(address, port, name)
        {
            Name = name;
            Icon = icon;
        }
        public AccessCredentials(IPAddress address, int port, string key, string name, string icon) : this(address, port, name, icon)
        {
            Key = key;
        }
        public static string GenerateKey(int keyLen = 30)
        {
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();

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
            queryParams["url"] = String.Format($"{Address}:{Port}");
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