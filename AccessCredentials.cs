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
        public string Url { get; set; }
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

            Url = queryParams[QUERY_PARAM_URL] ?? throw InvalidUrlParam(QUERY_PARAM_URL);

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
        public AccessCredentials() {
            
        }
        
        public static string GenerateKey()
        {
            return DateTime.Now.ToString();
        }

        public Uri ToUri()
        {
            string link = "plutonication:?";
            link += "url=" + Url;
            link += "&key=" + Key;
            if (Name != null)
            {
                link += "&name=" + Name;
            }
            if (Icon != null)
            {
                link += "&icon=" + Icon;
            }

            return new Uri(link);
        }
    }
}