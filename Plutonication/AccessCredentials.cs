using System.Web;
using System.Collections.Specialized;
using System.Security.Cryptography;

namespace Plutonication
{
    /// <summary>
    /// Class to use correct acces credential information in the wallet
    /// </summary>
    public sealed class AccessCredentials
    {
        public string? Url { get; set; }
        public string Key { get; set; } = GenerateKey();
        public string? Name { get; set; } // optional
        public string? Icon { get; set; } // optional
        public string? PlutoLayout { get; set; } // optional


        private const string QUERY_PARAM_URL = "url";
        private const string QUERY_PARAM_KEY = "key";
        private const string QUERY_PARAM_NAME = "name";
        private const string QUERY_PARAM_ICON = "icon";
        private const string QUERY_PARAM_PLUTO_LAYOUT = "plutolayout";
        
        public AccessCredentials(Uri uri)
        {
            if (uri is null)
            {
                throw new ArgumentNullException();
            }

            NameValueCollection queryParams = HttpUtility.ParseQueryString(uri.Query);

            Url = queryParams[QUERY_PARAM_URL] ?? throw InvalidUrlParam(QUERY_PARAM_URL);

            Key = queryParams[QUERY_PARAM_KEY] ?? throw InvalidUrlParam(QUERY_PARAM_KEY);

            Name ??= queryParams[QUERY_PARAM_NAME];

            Icon ??= queryParams[QUERY_PARAM_ICON];

            PlutoLayout ??= queryParams[QUERY_PARAM_PLUTO_LAYOUT];

            Exception InvalidUrlParam(string nameOfParam)
            {
                return new Exception($"{nameOfParam} url param is missing value.");
            }
        }

        public AccessCredentials() { }

        /// <summary>
        /// Helper method that generates a random key.
        /// </summary>
        /// <returns></returns>
        private static string GenerateKey()
        {
            // This is a cryptographically secure random number generator, however it does not strictly have to be.
            // https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.randomnumbergenerator?view=net-8.0
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] randomBytes = new byte[8];
                rng.GetBytes(randomBytes);
                return BitConverter.ToUInt64(randomBytes, 0).ToString();
            }
        }

        /// <summary>
        /// Converts the credentials into an Uri that can used in the Plutonication application.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="AccessCredentialsBadFormatException"></exception>
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
            if (PlutoLayout != null)
            {
                link += "&plutolayout=" + Uri.EscapeDataString(PlutoLayout);
            }

            return new Uri(link);
        }

        /// <summary>
        /// Converts the credentials to string representation.
        ///
        /// Useful when adding the text into a QR code link.
        /// It makes sure that the deep link is recognized as link and not as text.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="AccessCredentialsBadFormatException"></exception>
        public string ToEncodedString()
        {
            if (Url is null)
            {
                throw new AccessCredentialsBadFormatException("Url property must not be null");
            }

            string link = "plutonication:?";
            link += "url=" + Url;
            link += "&key=" + Key;
            if (Name != null)
            {
                link += "&name=" + Uri.EscapeDataString(Name);
            }
            if (Icon != null)
            {
                link += "&icon=" + Icon;
            }
            if (PlutoLayout != null)
            {
                link += "&plutolayout=" + PlutoLayout;
            }

            return Uri.EscapeDataString(link);
        }
    }
}
