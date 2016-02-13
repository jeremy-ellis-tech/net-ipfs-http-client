using System;
using System.Collections.Generic;
using System.Linq;

namespace Ipfs.Utilities
{
    public static class UriHelper
    {
        public static Uri AppendPath(Uri baseUri, string path)
        {
            if(String.IsNullOrEmpty(path))
            {
                return baseUri;
            }

            var uriBuilder = new UriBuilder(baseUri);

            string pathToAppend = path.TrimStart('/').TrimEnd('/');
            string oldPath = uriBuilder.Path.TrimEnd('/');

            uriBuilder.Path = oldPath + "/" + pathToAppend;

            return uriBuilder.Uri;
        }

        public static Uri AppendQuery(Uri baseUri, IDictionary<string,string> args)
        {
            if(args == null || args.Count <= 0)
            {
                return baseUri;
            }

            var uriBuilder = new UriBuilder(baseUri);

            string query = uriBuilder.Query.TrimStart('?');

            if(!String.IsNullOrEmpty(query))
            {
                query += "&";
            }

            query += String.Join("&", args.Select(x => String.Format("{0}={1}", x.Key, x.Value)));

            uriBuilder.Query = query;

            return uriBuilder.Uri;
        }
    }
}
