using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Plutonication
{
    public class AccessCredentials
    {
        public string Address;
        public string Key;
        public string Name;
        public string Icon;

        public string GenerateKey()
        {
            return "SampleKey";
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