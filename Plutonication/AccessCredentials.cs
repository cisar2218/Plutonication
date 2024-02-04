using System.Web;
using System.Collections.Specialized;

namespace Plutonication
{
    public sealed class AccessCredentials
    {
        public string? Url { get; set; }
        public string Key { get; set; } = GenerateKey();
        public string? Name { get; set; } // optional
        public string? Icon { get; set; } // optional

        private const string QUERY_PARAM_URL = "url";
        private const string QUERY_PARAM_KEY = "key";
        private const string QUERY_PARAM_NAME = "name";
        private const string QUERY_PARAM_ICON = "icon";

        public AccessCredentials(Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException();
            }

            NameValueCollection queryParams = HttpUtility.ParseQueryString(uri.Query);

            Url = queryParams[QUERY_PARAM_URL] ?? throw InvalidUrlParam(QUERY_PARAM_URL);

            Key = queryParams[QUERY_PARAM_KEY] ?? throw InvalidUrlParam(QUERY_PARAM_KEY);

            Name ??= queryParams[QUERY_PARAM_NAME];

            Icon ??= queryParams[QUERY_PARAM_ICON];

            Exception InvalidUrlParam(string nameOfParam)
            {
                return new Exception($"{nameOfParam} url param is missing value.");
            }
        }

        public AccessCredentials() { }

        private static string GenerateKey()
        {
            return DateTime.UtcNow.Ticks.ToString();
        }

        public Uri ToUri()
        {
            if (Url is null)
            {
                throw new AccessCredentialsBadFormatException("Url property must not be null");
            }

            string link = "plutonication:?";
            link += "url=" + Uri.EscapeDataString(Url);
            link += "&key=" + Uri.EscapeDataString(Key);
            if (Name != null)
            {
                link += "&name=" + Uri.EscapeDataString(Name);
            }
            if (Icon != null)
            {
                link += "&icon=" + Uri.EscapeDataString(Icon);
            }

            return new Uri(link);
        }
    }
}